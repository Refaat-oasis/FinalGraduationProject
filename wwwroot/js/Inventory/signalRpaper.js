// signalRPaper.js

// Create the connection to the SignalR hub
var hub = new signalR.HubConnectionBuilder()
    .withUrl("/InventoryReorderPoint")
    .build();

hub.start()
    .then(function() {
    console.log("SignalR connection established.");

    // Invoke the server-side method to check paper reorder points.
    hub.invoke("CheckPaperReorderPoint")
        .catch(function(err) {
        console.error("Error invoking CheckPaperReorderPoint: ", err.toString());
    });
})
    .catch(function(err) {
    console.error("SignalR connection error: ", err.toString());
});

// Listen for reorder messages from the hub.
hub.on("ReceiveReorderMessagePaper", function(message) {
    console.log("Received reorder message: ", message);
    var messageDiv = document.getElementById("reorderMessages");
    // Make sure the message div is visible
    messageDiv.style.display = "block";
    // Append the new message rather than replacing previous ones
    messageDiv.innerHTML += "<div>" + message + "</div>";
});