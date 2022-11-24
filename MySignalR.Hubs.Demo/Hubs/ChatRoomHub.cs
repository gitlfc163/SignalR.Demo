using Microsoft.AspNetCore.SignalR;

namespace MySignalR.Hubs.Demo.Hubs;

/// <summary>
/// 不是强类型的Hub中心
/// </summary>
public class ChatRoomHub : Hub
{
    #region 向客户端发送消息
    /// <summary>
    /// 将消息发送到所有连接的客户端
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
    /// <summary>
    /// 使用 Clients.Caller 将消息发送回调用方
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessageToCaller(string user, string message)
    {
        await Clients.Caller.SendAsync("ReceiveMessage", user, message);
    }
    /// <summary>
    /// 将消息发送给 SignalR Users 组中的所有客户端
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessageToGroup(string user, string message)
    {
        await Clients.Group("SignalR Users").SendAsync("ReceiveMessage", user, message);
    }
    /// <summary>
    /// 将消息发送到所有连接的客户端
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task SendPublicMessage(string message)
    {
        string connId = Context.ConnectionId;
        string msg = $"{connId}{DateTime.Now}:{message}";
        Console.WriteLine(msg);
        return Clients.All.SendAsync("ReceivePublicMessage", msg);
    }
    #endregion

    #region 从客户端请求结果
    ///// <summary>
    ///// .net6不支持
    ///// </summary>
    ///// <param name="connectionId"></param>
    ///// <returns></returns>
    //public async Task<string> WaitForMessage(string connectionId)
    //{
    //    var message = await Clients.Client(connectionId).InvokeAsync<string>(
    //        "GetMessage");
    //    return message;
    //}
    ///// <summary>
    ///// .net6不支持
    ///// </summary>
    ///// <param name="context"></param>
    ///// <param name="connectionId"></param>
    ///// <returns></returns>
    //public async Task SomeMethod(IHubContext<ChatRoomHub> context, string connectionId)
    //{
    //    string result = await context.Clients.Client(connectionId).InvokeAsync<string>(
    //        "GetMessage");
    //}
    #endregion
}
