namespace MySignalR.ChatClient.Demo.Consumer;

public interface IBaseHubConsumer: IAsyncDisposable
{
    Task StartAsync();
}
