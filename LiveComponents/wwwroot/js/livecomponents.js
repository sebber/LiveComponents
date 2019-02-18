const liveComponents = () =>
  Array.from(document.querySelectorAll("[live-component]"));

const clickableParts = component =>
  Array.from(component.querySelectorAll("[live-component-click]"));

const modelParts = component =>
  Array.from(component.querySelectorAll("[live-component-model]"));

const getValue = element => {
  const value = element && element.value ? element.value : undefined;

  if (!value) {
    return undefined;
  }

  if (Number.isInteger(value)) {
    return Number.parseInt(value);
  }

  return value;
};

const action = (name, params) => ({ name, params });

// Add
// Add()
// Todo(title)
// Divide(a, b)
const parseAction = value => {
  let [name, params] = value.split("(");

  if (!params) {
    return action(name);
  }

  if (params.includes(")")) {
    params = params.substring(0, params.length - 1);
  }

  return action(
    name,
    Object.assign(
      {},
      ...params.split(",").map(param => ({ [param.trim()]: null }))
    )
  );
};

window.onload = () => {
  const connection = new signalR.HubConnectionBuilder()
    .withUrl("/componentHub")
    .build();

  connection.on("RenderComponent", html => {
    const doc = document.querySelector("html");
    morphdom(doc, html);
  });

  connection
    .start()
    .then(() => console.log("Connection started"))
    .catch(err => console.error(err));

  const clickers = document.querySelectorAll("[live-component-click]");
  console.log("clickers", clickers);

  liveComponents().map(component => {
    const componentName = component.getAttribute("live-component");

    clickableParts(component).map(part => {
      part.onclick = () => {
        const action = parseAction(part.getAttribute("live-component-click"));

        const modelKeys = Object.getOwnPropertyNames(action.params);

        /*
         * This will be the cool named propertys way of doing things
        const values = modelKeys
          .map(key =>
            component.querySelector("[live-component-model='" + key + "']")
          )
          .map(input => ({
            [input.getAttribute("live-component-model")]: getValue(input)
          }));

        action.params = values;
        */

        /*
       * This will be the simpler array way of doing things
       * */
        const values = modelKeys
          .map(key =>
            component.querySelector("[live-component-model='" + key + "']")
          )
          .map(input => getValue(input));

        action.params = values;

        connection.invoke("CallAction", componentName, {
          Name: action.name,
          Parameters: action.params
        });

        /*
        const data = modelParts(component).map(el => ({
          name: el.getAttribute("live-component-model"),
          value: getValue(el)
        }));
        */

        /*
        const data = modelParts(component).map(el => getValue(el));
        console.log("parameters", data);

        connection.invoke("CallAction", componentName, {
          Name: action,
          Parameters: data.length > 0 ? data : undefined
        });
        */

        /*
        fetch(window.location.href, {
          headers: {
            "LIVE-COMPONENT-ACTION": action
          }
        })
          .then(response => response.text())
          .then(html => {
            const doc = document.querySelector("html");
            morphdom(doc, html);
          });
          */
      };
    });
  });
};
