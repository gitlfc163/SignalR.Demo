
using MySignalR.ChatClient.Demo.Consumer;
using MySignalR.ChatClient.Demo.Models;

var builder = WebApplication.CreateBuilder(args);

//采用下面的形式把配置类型 实体注入到容器,以支持IOptions/IOptionsSnapshot/IOptionsMonitor
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
