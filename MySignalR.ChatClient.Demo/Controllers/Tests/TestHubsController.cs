using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using MySignalR.ChatClient.Demo.Consumer;
using MySignalR.ChatClient.Demo.Controllers.Configs;
using MySignalR.ChatClient.Demo.Models;

namespace MySignalR.ChatClient.Demo.Controllers.Tests
{
    public class TestHubsController : AreaController
    {
        private readonly ILogger<TestHubsController> _logger;
        private readonly INotificationHubConsumer notificationHubConsumer;
        private readonly IChatRoomHubConsumer chatRoomHubConsumer;
        private readonly IOptions<AppSetting> _appSetting;

        public TestHubsController(IOptions<AppSetting> appSetting, 
            INotificationHubConsumer _notificationHubConsumer,
            IChatRoomHubConsumer _chatRoomHubConsumer,
            ILogger<TestHubsController> logger)
        {
            this.notificationHubConsumer = _notificationHubConsumer;
            this.chatRoomHubConsumer = _chatRoomHubConsumer;
            _appSetting = appSetting;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> TestSendNotificationAsync(string message)
        {
            await notificationHubConsumer.SendNotificationAsync(message);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> TestSendMessageAsync(string userName, string message)
        {
            await chatRoomHubConsumer.SendMessageAsync(userName,message);
            return Ok();
        }

    }
}