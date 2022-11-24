using Microsoft.AspNetCore.SignalR;
using MySignalR.Hubs.Demo.Models;

namespace MySignalR.Hubs.Demo.Hubs;

/// <summary>
/// NotificationHub 必须是 Hub 的子类
/// </summary>
public class NotificationHub : Hub
{
    /// <summary>
    /// 依赖于 notification 实例,将激发 NotificationReceived 事件
    /// </summary>
    /// <param name="notification"></param>
    /// <returns></returns>
    public Task NotifyAll(Notification notification)
    {
        //Clients.All 表示所有连接的客户端,NotificationReceived是事件 
        Console.WriteLine($"NotifyAll:{notification.Date.ToString()},{notification.Text}");
        return Clients.All.SendAsync("NotificationReceived", notification);
    }
}
