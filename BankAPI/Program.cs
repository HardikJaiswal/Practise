using BankAPI.IContracts;
using BankAPI.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ServiceContext>(options => options.UseSqlServer(connection));
builder.Services.AddTransient<IAccountHolderService, AccountHolderService>();
builder.Services.AddTransient<IBankStaffService, BankStaffService>();
builder.Services.AddTransient<BankService, BankService>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
