using FluentMigrator.Runner;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OnlineCarWash.Appointments.Repository;
using OnlineCarWash.Appointments.Repository.interfaces;
using OnlineCarWash.Appointments.Services;
using OnlineCarWash.Appointments.Services.interfaces;
using OnlineCarWash.Customers.Models;
using OnlineCarWash.Customers.Repository;
using OnlineCarWash.Customers.Repository.interfaces;
using OnlineCarWash.Customers.Services;
using OnlineCarWash.Customers.Services.interfaces;
using OnlineCarWash.Data;
using OnlineCarWash.Options.Repository;
using OnlineCarWash.Options.Repository.interfaces;
using OnlineCarWash.Options.Services;
using OnlineCarWash.Options.Services.interfaces;
using OnlineCarWash.Services.Repository;
using OnlineCarWash.Services.Repository.interfaces;
using OnlineCarWash.Services.ServiceCommandQuery;
using OnlineCarWash.Services.ServiceCommandQuery.interfaces;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IRepositoryCustomer, RepositoryCustomer>();
        builder.Services.AddScoped<ICommandServiceCustomer, CommandServiceCustomer>();
        builder.Services.AddScoped<IQueryServiceCustomer, QueryServiceCustomer>();

        builder.Services.AddScoped<IRepositoryOption, RepositoryOption>();
        builder.Services.AddScoped<IOptionCommandService, OptionCommandService>();
        builder.Services.AddScoped<IOptionQueryService, OptionQueryService>();

        builder.Services.AddScoped<IRepositoryService, RepositoryService>();
        builder.Services.AddScoped<IServiceQueryService, ServiceQueryService>();
        builder.Services.AddScoped<IServiceCommandService, ServiceCommandService>();

        builder.Services.AddScoped<IRepositoryAppointment, RepositoryAppointment>();
        builder.Services.AddScoped<IAppointmentQueryService, AppointmentQueryService>();


        builder.Services.AddDbContext<AppDbContext>(op => op.UseMySql(builder.Configuration.GetConnectionString("Default")!,
            new MySqlServerVersion(new Version(8, 0, 21))), ServiceLifetime.Scoped);

        builder.Services.AddIdentity<Customer, IdentityRole<int>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb.AddMySql5().WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
            .ScanIn(typeof(Program).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
        });
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Api", Version = "v1" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference{
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                        }
                    },
                new string[] { }
                }
            });
        
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }

        app.Run();
    }
}