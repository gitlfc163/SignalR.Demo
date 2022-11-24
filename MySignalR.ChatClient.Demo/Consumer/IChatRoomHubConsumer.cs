namespace MySignalR.ChatClient.Demo.Consumer;

public interface IChatRoomHubConsumer : IBaseHubConsumer
{
    /// <summary>
    /// 调用 ChatRoomHub的ReceiveMessage方法
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    Task SendMessageAsync(string user, string message);
}
