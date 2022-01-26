using APIDemo;
using APIDemo.IContracts;
using APIDemo.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiDemoContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSingleton<IAccountHolderService,CustomerService>();
builder.Services.AddSingleton<IBankStaffService,StaffService>();
builder.Services.AddSingleton<IUIService,UIService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIDemo v1")
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
