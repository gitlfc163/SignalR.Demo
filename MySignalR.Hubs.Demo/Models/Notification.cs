namespace MySignalR.Hubs.Demo.Models;

/// <summary>
/// 简单的通知中心
/// </summary>
/// <param name="Text"></param>
/// <param name="Date"></param>
public record Notification(string Text, DateTime Date);
