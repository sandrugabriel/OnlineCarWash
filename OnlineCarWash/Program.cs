using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using OnlineCarWash.Customers.Repository;
using OnlineCarWash.Customers.Repository.interfaces;
using OnlineCarWash.Customers.Services;
using OnlineCarWash.Customers.Services.interfaces;
using OnlineCarWash.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepositoryCustomer, RepositoryCustomer>();
builder.Services.AddScoped<ICommandServiceCustomer, CommandServiceCustomer>();
builder.Services.AddScoped<IQueryServiceCustomer, QueryServiceCustomer>();


builder.Services.AddDbContext<AppDbContext>(op => op.UseMySql(builder.Configuration.GetConnectionString("Default")!,
    new MySqlServerVersion(new Version(8, 0, 21))), ServiceLifetime.Scoped);

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb.AddMySql5().WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
    .ScanIn(typeof(Program).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.Run();
