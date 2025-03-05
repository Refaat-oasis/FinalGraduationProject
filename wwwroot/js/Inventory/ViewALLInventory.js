  //<script src="https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/8.0.7/signalr.min.js" integrity="sha512-7SRCYIJtR6F8ocwW7UxW6wGKqbSyqREDbfCORCbGLatU0iugBLwyOXpzhkPyHIFdBO0K2VCu57fvP2Twgx1o2A==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
  //  <script>
  //      var hub = new signalR.HubConnectionBuilder().withUrl("/productHub").build();

  //      hub.start().then(function () {
  //          console.log("Connection success");
  //      }).catch(function (err) {
  //          console.error("Connection error: ", err.toString());
  //      });

  //      // Listen for reorder messages
  //      hub.on("ReceiveReorderMessage", function (message) {
  //          var messageDiv = document.getElementById("reorderMessages");
  //          messageDiv.style.display = "block";
  //          messageDiv.innerText = message;
  //      });

  //      // Listen for inventory updates (if needed)
  //      hub.on("UpdateInventory", function (paper) {
  //          console.log("Received update for paper: ", paper);
  //          // Add logic to update the table dynamically
//      });

// wwwroot/js/signalr-inventory.js

// Establish SignalR connection
var hub = new signalR.HubConnectionBuilder()
    .withUrl("/InventoryReorderPoint")
    .build();

// Start the connection
hub.start()
    .then(function () {
        console.log("SignalR connection established.");
    })
    .catch(function (err) {
        console.error("SignalR connection error: ", err.toString());
    });

// Listen for reorder messages
hub.on("ReceiveReorderMessage", function (message) {
    console.log("Received reorder message: ", message);
    var messageDiv = document.getElementById("reorderMessages");
    if (messageDiv) {
        messageDiv.style.display = "block";
        messageDiv.innerText = message;
    }
});