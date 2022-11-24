using Microsoft.AspNetCore.SignalR.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region HubConnection
HubConnection hubConnection = new HubConnectionBuilder()
.WithUrl(new Uri("http://localhost:5207/Hubs/ChatRoomHub"))
.WithAutomaticReconnect(new[] { TimeSpan.Zero, TimeSpan.Zero, TimeSpan.FromSeconds(10) })
.Build();

//�ͻ������������ĵ�GetMessage���ؽ��
hubConnection.On("GetMessage", async () =>
{
    Console.WriteLine("Enter message:");
    var message = "test";//await Console.In.ReadLineAsync()||
    return message;
});

hubConnection.On<string, string>("ReceivePublicMessage", (user, message) =>
{
    Console.WriteLine(message);
}); 
#endregion


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
