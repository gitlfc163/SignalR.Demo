using SignalR.Hubs.Demo.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR(); //注册所有SignalR的服务
//允许客户端跨域访问
string[] urls = new[] { "http://localhost:5173", "http://localhost:5266" };
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder =>
        builder.WithOrigins(urls).AllowAnyMethod()
            .AllowAnyHeader().AllowCredentials())
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ChatRoomHub>("/Hubs/ChatRoomHub"); //启用SignalR中间件
app.MapControllers();
app.Run();
