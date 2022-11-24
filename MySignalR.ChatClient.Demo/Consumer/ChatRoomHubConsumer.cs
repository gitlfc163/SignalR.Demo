
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using MySignalR.ChatClient.Demo.Models;

namespace MySignalR.ChatClient.Demo.Consumer;

/// <summary>
/// ChatRoomHub的客户端消费者
/// 使用生成器模式和相应的 HubConnectionBuilder 类型创建 HubConnection
/// </summary>
public sealed class ChatRoomHubConsumer : IChatRoomHubConsumer
{
    private HubConnection _hubConnection;
    private readonly IOptions<AppSetting> _appSetting;

    public ChatRoomHubConsumer()
    {
        //_appSetting = appSetting;
        string hubHost = "http://localhost:5207";//_appSetting.Value.HubHost
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(new Uri($"{hubHost}/Hubs/ChatRoomHub"))
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.StartAsync();

        #region snippet_ClosedRestart
        _hubConnection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await _hubConnection.StartAsync();
        };
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Task StartAsync()
    {
        return _hubConnection.StartAsync();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
            _hubConnection = null;
        }
    }

    /// <summary>
    /// 调用 ChatRoomHub的ReceiveMessage方法
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public Task SendMessageAsync(string user, string message)
    {
        Console.WriteLine($"ChatRoomHubConsumer-SendMessage:{user},{message}");
        return _hubConnection.InvokeAsync("SendMessage", user, message);
    }
}