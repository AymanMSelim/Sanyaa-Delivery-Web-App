using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SanyaaDelivery.Infra.Data.Context;
using SanyaaDelivery.Infra.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SanyaaDelivery.API.ActionsFilter;
using Microsoft.AspNetCore.Diagnostics;
using SanyaaDelivery.Application.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using App.Global.DTOs;

namespace SanyaaDelivery.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["TokenKey"])),
                       ValidateAudience = false,
                       ValidateIssuer = false
                   };
               });
                services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
                services.AddDbContext<SanyaaDatabaseContext>(options =>
                    options.UseMySql(Configuration.GetConnectionString("sanyaaDatabaseContext")));


               // services.AddDbContext<SanyaaDatabaseContext>(options =>
               //options.UseMySql(Configuration.GetConnectionString("sanyaaDatabaseContext")).EnableSensitiveDataLogging(true).UseLoggerFactory(new LoggerFactory().AddConsole()));

                DependencyContainer.RegisterServices(services);
                services.AddHttpContextAccessor();
                services.AddScoped<CommonService>();
                services.AddCors();
                //services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                //{
                //    Version = "v1",
                //    Title = "Sanyaa API",
                //    Description = "List of Api's.",
                //    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                //    {
                //        Name = "Test site",
                //        Email = string.Empty
                //    },
                //}
                //));
                App.Global.SMS.SMSMisrService.SetParameters(
                    Configuration.GetValue<string>("SMSMisrUsername"),
                    Configuration.GetValue<string>("SMSMisrPassword"),
                    Configuration.GetValue<string>("SMSMisrSender"),
                    Configuration.GetValue<string>("SMSMisrEnvironment")
                    );
            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
            }
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Application.IGeneralSetting generalSetting, ITranslationService translationService)
        {
            try
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseHsts();
                }
                app.UseMiddleware<RequestTimerMiddleware>();
                app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
                app.UseHttpsRedirection();
                app.UseAuthentication();
                app.UseStaticFiles(new StaticFileOptions()
                {
                    OnPrepareResponse = (context) =>
                    {
                        if (!context.File.PhysicalPath.ToLower().Contains("public") && !context.Context.User.Identity.IsAuthenticated)
                        {
                            context.Context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            //throw new Exception("Not authenticated");
                            var response = ResultFactory<object>.CreateErrorResponseMessageFD("NotAuthenticated", App.Global.Enums.ResultStatusCode.NotAuthenticated);
                            context.Context.Response.WriteAsync(App.Global.Serialization.Json.Serialize(response));
                        }
                    }
                });
                app.UseMiddleware<UnauthorizedResponseMiddleware>();
                //app.UseExceptionHandler(c => c.Run(async context =>
                //{
                //    var exception = context.Features
                //        .Get<IExceptionHandlerPathFeature>()
                //        .Error;
                //    var response = new { error = exception.Message };
                //    await context.Response.(response);
                //}));
                app.UseMvc();
                //app.UseSwagger();
                //app.UseSwaggerUI(c =>
                //{
                //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sanyaa API v1");
                //});
                App.Global.Firebase.FirebaseMessaging.Initalize(env.WebRootPath + "/firebase.json", env.WebRootPath + "/empFirebase.json");

            }
            catch (Exception ex)
            {
                App.Global.Logging.LogHandler.PublishException(ex);
            }
        }
    }
}
