using Microsoft.EntityFrameworkCore;
using WordQuestAPI.Models;
using Microsoft.OpenApi.Models;
//using Microsoft.Extensions.Configuration;
//using Pomelo.EntityFrameworkCore.MySql.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Connection to the D
var connector = new MySQLConnector(); 


builder.Services.AddDbContext<WordQuestContext>(options =>
    options.UseMySql(connector.GetConnectionString(), ServerVersion.AutoDetect(connector.GetConnectionString())));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseDefaultFiles();
app.UseStaticFiles();


// Connection with swagger
/*
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/



//builder.Services.AddDbContext<WordQuestContext>(opt =>
    //opt.UseInMemoryDatabase("WordQuest"));
//builder.Services.AddEndpointsApiExplorer();




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

/*
public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<WordQuest.Startup>();
            });*/
