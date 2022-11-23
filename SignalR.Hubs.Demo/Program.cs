using SignalR.Hubs.Demo.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR(); //注册所有SignalR的服务
//允许客户端跨域访问
string[] urls = new[] { "http://localhost:5173" };
builder.Services.AddCors(options =>
{
    options.AddPolicy
        (name: "CorsPolicy",
            builde =>
            {
                var hasOrigins = urls.Length > 0;
                if (hasOrigins)
                {
                    builde.WithOrigins(urls);
                }
                else
                {
                    builde.AllowAnyOrigin();
                }

                builde.AllowAnyHeader()
                .AllowAnyMethod().AllowCredentials();
            }
        );
});


var app = builder.Build();
app.UseCors("CorsPolicy");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ChatRoomHub>("/Hubs/ChatRoomHub"); //启用SignalR中间件
app.MapControllers();
app.Run();
