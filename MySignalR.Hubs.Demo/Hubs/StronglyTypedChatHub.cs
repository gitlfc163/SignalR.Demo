using Microsoft.AspNetCore.SignalR;

namespace MySignalR.Hubs.Demo.Hubs;

/// <summary>
/// 强类型中心
/// </summary>
public class StronglyTypedChatHub : Hub<IChatClient>
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
        await Clients.All.ReceiveMessage(user, message);
    }
    /// <summary>
    /// 使用 Clients.Caller 将消息发送回调用方
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessageToCaller(string user, string message)
    {
        await Clients.Caller.ReceiveMessage(user, message);
    }
    /// <summary>
    /// 将消息发送给 SignalR Users 组中的所有客户端
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task SendMessageToGroup(string user, string message)
    {
        await Clients.Group("SignalR Users").ReceiveMessage(user, message);
    }
    #endregion

    #region 从客户端请求结果
    /// <summary>
    /// 从客户端请求结果
    /// </summary>
    /// <param name="connectionId"></param>
    /// <returns></returns>
    public async Task<string> WaitForMessage(string connectionId)
    {
        string message = await Clients.Client(connectionId).GetMessage();
        Console.WriteLine(message);
        return message;
    }
    #endregion

}
