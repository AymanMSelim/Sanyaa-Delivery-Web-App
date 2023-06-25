using App.Global.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SanyaaDelivery.API;
using SanyaaDelivery.Domain.Models;
using SanyaaDelivery.Infra.Data.Context;
using SanyaaDelivery.Infra.IoC;
using System.Configuration;
using System.Text;
using SanyaaDelivery.API.ActionsFilter;
using Hangfire;
using SanyaaDelivery.Application.Interfaces;
using SanyaaDelivery.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
                  options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
          options.TokenValidationParameters = new TokenValidationParameters
          {
              ValidateIssuerSigningKey = true,
              IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("TokenKey"))),
              ValidateAudience = false,
              ValidateIssuer = false
          };
      });
builder.Services.AddDbContext<SanyaaDatabaseContext>(options =>
    options.UseMySQL(builder.Configuration.GetSection("ConnectionStrings")["sanyaaDatabaseContext"]));

DependencyContainer.RegisterServices(builder.Services);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CommonService>();
builder.Services.AddCors();
var service = builder.Services.BuildServiceProvider().GetService<IGeneralSetting>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetSection("ConnectionStrings")["SupportDB"]));
builder.Services.AddHangfireServer();
App.Global.SMS.SMSMisrService.SetParameters(
        builder.Configuration.GetValue<string>("SMSMisrUsername"),
        builder.Configuration.GetValue<string>("SMSMisrPassword"),
        builder.Configuration.GetValue<string>("SMSMisrSender"),
        builder.Configuration.GetValue<string>("SMSMisrEnvironment")
        );
var app = builder.Build();
app.UseRouting();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestTimerMiddleware>();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions()
{
    OnPrepareResponse = (context) =>
    {
        if (!context.File.PhysicalPath.ToLower().Contains("public") && !context.Context.User.Identity.IsAuthenticated)
        {
            context.Context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            var response = ResultFactory<object>.CreateErrorResponseMessageFD("NotAuthenticated", App.Global.Enums.ResultStatusCode.NotAuthenticated);
            context.Context.Response.WriteAsync(App.Global.Serialization.Json.Serialize(response));
        }
    }
});
app.UseMiddleware<UnauthorizedResponseMiddleware>();
app.UseHangfireDashboard();
app.MapControllers();
//app.UseSwagger();
App.Global.Firebase.FirebaseMessaging.Initalize("firebase.json", "empFirebase.json");
try
{
    var operationService = builder.Services.BuildServiceProvider().GetService<IOperationService>();
    Hangfire.RecurringJob.AddOrUpdate(() => operationService.BroadcastTask(), Cron.MinuteInterval(10));
}
catch (Exception ex)
{
    App.Global.Logging.LogHandler.PublishException(ex);
}
app.Run();
