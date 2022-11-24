namespace MySignalR.ChatClient.Demo.Consumer;

public interface INotificationHubConsumer : IBaseHubConsumer
{
    /// <summary>
    /// 调用 NotificationHub的NotifyAll方法
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    Task SendNotificationAsync(string text);
}
