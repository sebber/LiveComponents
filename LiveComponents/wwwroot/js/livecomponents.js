const liveComponents = () =>
  Array.from(document.querySelectorAll("[live-component]"));

const clickableParts = component =>
  Array.from(component.querySelectorAll("[live-component-click]"));

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
        const action = part.getAttribute("live-component-click");

        connection.invoke("CallAction", componentName, action);

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
