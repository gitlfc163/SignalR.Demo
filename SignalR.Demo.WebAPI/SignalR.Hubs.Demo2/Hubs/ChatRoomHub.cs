using Microsoft.AspNetCore.SignalR;

namespace SignalR.Hubs.Demo2.Hubs;

public class ChatRoomHub : Hub
{
    public Task SendPublicMessage(string message)
    {
        string connId = Context.ConnectionId;
        string msg = $"{connId}{DateTime.Now}:{message}";
        Console.WriteLine(msg);
        return Clients.All.SendAsync("ReceivePublicMessage", msg);
    }
}
