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
                options.UseMySql(Configuration.GetConnectionString("sanyaaDatabaseContext")).EnableSensitiveDataLogging(true).UseLoggerFactory(new LoggerFactory().AddConsole()));
            DependencyContainer.RegisterServices(services);
            services.AddHttpContextAccessor();
            services.AddScoped<CommonService>();
            services.AddCors();
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
            {
                Version = "v1",
                Title = "Sanyaa API",
                Description = "List of Api's.",
                Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                {
                    Name = "Test site",
                    Email = string.Empty
                },
            }
            ));
            //App.Global.SMS.SMSMisrService.SetParameters(
            //    Configuration.GetConnectionString("SMSMisrUsername"),
            //    Configuration.GetConnectionString("SMSMisrPassword"),
            //    Configuration.GetConnectionString("SMSMisrSender")
            //    );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Application.IGeneralSetting generalSetting, ITranslationService translationService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = (context) =>
                {
                    if (!context.File.PhysicalPath.ToLower().Contains("public") && !context.Context.User.Identity.IsAuthenticated)
                    {
                        throw new Exception("Not authenticated");
                    }
                }
            });
            //app.UseExceptionHandler(c => c.Run(async context =>
            //{
            //    var exception = context.Features
            //        .Get<IExceptionHandlerPathFeature>()
            //        .Error;
            //    var response = new { error = exception.Message };
            //    await context.Response.(response);
            //}));
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sanyaa API v1");
            });
            if(App.Global.Translation.Translator.TranslationList == null)
            {
                App.Global.Translation.Translator.TranslationList = translationService.GetList().Select(d => new App.Global.Translation.Translation
                {
                    Key = d.Key,
                    LangId = d.LangId,
                    Value = d.Value
                }).ToList();
            }
            //App.Global.Firebase.FirebaseMessaging.Initalize(env.WebRootPath + "/firebase.json");
        }
    }
}
