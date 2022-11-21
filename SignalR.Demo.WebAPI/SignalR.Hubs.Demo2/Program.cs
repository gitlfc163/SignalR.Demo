using SignalR.Hubs.Demo2.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR(); //ע������SignalR�ķ���

#region   ���ÿ���
//����ͻ��˿������,URL����/����Ҫ;, "http://localhost:5173"
string[] urls = new[] { "http://172.17.70.46:5173","http://localhost:5173" };
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
#endregion

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
app.MapHub<ChatRoomHub>("/Hubs/ChatRoomHub"); //����SignalR�м��
app.MapControllers();
app.Run();
