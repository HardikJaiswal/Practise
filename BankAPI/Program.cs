using BankAPI.IContracts;
using BankAPI.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddDbContext<ServiceContext>(options => options.UseSqlServer(connection));
builder.Services.AddSingleton<IAccountHolderService, AccountHolderService>();
builder.Services.AddSingleton<IBankStaffService, BankStaffService>();
builder.Services.AddSingleton<BankService, BankService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
