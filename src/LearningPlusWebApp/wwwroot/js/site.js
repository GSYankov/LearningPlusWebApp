// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var connection =
    new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .build();

connection.on("NewMessage",
    function (message) {
        var chatInfo = `<div>${message.user}: ${message.text}</div>`;
        $("#messagesList").append(chatInfo);
    });

$("#sendButton").click(function () {
    var message = $("#messageInput").val();
    connection.invoke("SendAsync", message);
    $("#messageInput").val("");
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});