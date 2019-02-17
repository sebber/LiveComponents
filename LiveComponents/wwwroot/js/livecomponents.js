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

  Array.from(clickers).map(element => {
    element.onclick = () => {
      const action = element.getAttribute("live-component-click");

      connection.invoke("CallAction", window.location.pathname.substring(1), action);

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
};

