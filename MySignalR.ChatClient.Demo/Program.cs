
using MySignalR.ChatClient.Demo.Consumer;
using MySignalR.ChatClient.Demo.Models;

var builder = WebApplication.CreateBuilder(args);

//�����������ʽ���������� ʵ��ע�뵽����,��֧��IOptions/IOptionsSnapshot/IOptionsMonitor
builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));

#region HubConsumer
builder.Services.AddSingleton<INotificationHubConsumer, NotificationHubConsumer>();
builder.Services.AddSingleton<IChatRoomHubConsumer, ChatRoomHubConsumer>();
#endregion


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
