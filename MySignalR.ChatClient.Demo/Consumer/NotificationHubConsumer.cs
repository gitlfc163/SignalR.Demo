
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using MySignalR.ChatClient.Demo.Models;
using static System.Net.Mime.MediaTypeNames;

namespace MySignalR.ChatClient.Demo.Consumer;

/// <summary>
/// NotificationHub的客户端消费者
/// 使用生成器模式和相应的 HubConnectionBuilder 类型创建 HubConnection
/// </summary>
public sealed class NotificationHubConsumer : INotificationHubConsumer
{
    private HubConnection _hubConnection;
    private readonly IOptions<AppSetting> _appSetting;

    public NotificationHubConsumer()
    {
        //_appSetting = appSetting;
        string hubHost = "http://localhost:5207";//_appSetting.Value.HubHost
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(new Uri($"{hubHost}/Hubs/NotificationHub"))
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.StartAsync();

        //订阅NotificationHub的NotificationReceived事件
        _hubConnection.On<Notification>("NotificationReceived", OnNotificationReceivedAsync);

        #region snippet_ClosedRestart
        _hubConnection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await _hubConnection.StartAsync();
        };
        #endregion
    }
    /// <summary>
    /// NotificationReceived事件注册
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    private async Task OnNotificationReceivedAsync(Notification notification)
    {
        Console.WriteLine($"OnNotificationReceivedAsync:{notification.Date.ToString()},{notification.Text}");
    }
    /// <summary>
    /// StartAsync
    /// </summary>
    /// <returns></returns>
    public Task StartAsync()
    {
        return _hubConnection.StartAsync();
    }
    /// <summary>
    /// DisposeAsync
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
    /// 调用 NotificationHub的NotifyAll方法
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public Task SendNotificationAsync(string text)
    {
        Notification notification = new Notification(text, DateTime.UtcNow);
        Console.WriteLine($"SendNotificationAsync:{notification.Date.ToString()},{notification.Text}");
        return _hubConnection.InvokeAsync("NotifyAll", notification);
    }

}