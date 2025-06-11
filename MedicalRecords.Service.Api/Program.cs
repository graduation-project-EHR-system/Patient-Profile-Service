using MedicalRecords.Service.Api.Helper;
using MedicalRecords.Service.Api.Middlewares;
using MedicalRecords.Service.Core.DbContexts;
using MedicalRecords.Service.Core.Helper;
using MedicalRecords.Service.Core.ServicesContract;
using MedicalRecords.Service.Services;
using MedicalRecords.Service.Services.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;
using Talabat.APIS.Middleware;

namespace MedicalRecords.Service.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(o =>
               {
                   o.RequireHttpsMetadata = false;
                   o.SaveToken = false;
                   o.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateAudience = true,
                       ValidateIssuer = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                       ValidAudience = builder.Configuration["JWT:ValidAudience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecurityKey"])),
                       ClockSkew = TimeSpan.Zero
                   };
               });


            builder.Services.AddControllers().AddJsonOptions(options =>

            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())

            ); ;
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MedicalRecordsDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            
            builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.Configure<ApiKeySettings>(builder.Configuration.GetSection("ApiKeySettings"));

            builder.Services.Configure<KafkaConfig>(builder.Configuration.GetSection("Kafka"));
            builder.Services.AddSingleton(resolver =>
                resolver.GetRequiredService<IOptions<KafkaConfig>>().Value
            );


            builder.Services.AddHostedService<KafkaUserConsumerService>();

            var app = builder.Build();

            using var scope = app.Services.CreateScope(); /// instead of using try finally to dispose the scope
            var services = scope.ServiceProvider;
            var _dbcontext = services.GetRequiredService<MedicalRecordsDbContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbcontext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<ILoggerFactory>();
                logger.LogError(ex, "There is Error in Migration");
            }

            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<TokenOrApiKeyAuthMiddleware>();

            app.UseHttpsRedirection();

            app.UseCors("AllowAllOrigins");

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
