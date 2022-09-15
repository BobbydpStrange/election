using election.Data;
using Microsoft.EntityFrameworkCore;
/*using election.Controllers;

using iro.web.Data;
using Microsoft.EntityFrameworkCore;*/

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<InstantRunoffContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("iro")));
//var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<InstantRunoffContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("iro")));

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

app.MapGet("/test", async (InstantRunoffContext context) => await context.Cities.ToListAsync());
app.Run();
