namespace MySignalR.Hubs.Demo.Hubs;

public interface IChatClient
{
    /// <summary>
    /// 向客户端发送消息
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task ReceiveMessage(string user, string message);

    /// <summary>
    /// 从客户端请求结果
    /// </summary>
    /// <returns></returns>
    Task<string> GetMessage();
}
