using Microsoft.AspNetCore.SignalR;
using MySignalR.Hubs.Demo.Hubs;
using MySignalR.Hubs.Demo.Models;

namespace MySignalR.Hubs.Demo.Services;

public class NotificationService
{
    //访问客户端的上下文列表，公开了广播通知的功能,
    //_hubContext 用于激发 "NotificationReceived" 事件，但它并不用于调用中心的 NotifyAll 方法。
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public Task SendNotificationAsync(Notification notification)
    {
        return notification is not null
             ? _hubContext.Clients.All.SendAsync("NotificationReceived", notification)
             : Task.CompletedTask;
    }
}