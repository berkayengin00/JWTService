using JWTService.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JWTService.Application;
using Serilog;
using JWTService.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Service Registration
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices(); 
#endregion

builder.Services.AddSwaggerGen();

#region JWT Configuration
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
ValidIssuer = builder.Configuration["Token:Issuer"],
ValidAudience = builder.Configuration["Token:Audience"],
IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
	ClockSkew = TimeSpan.Zero

};
});
#endregion

Log.Logger = new LoggerConfiguration()
	.WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
	.CreateLogger();

builder.Services.AddSingleton(Log.Logger);

var app = builder.Build();

app.ConfigureExceptionHandler();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
ConfigurationHelper.Initialize(app.Configuration);

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
