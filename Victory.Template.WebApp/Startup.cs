using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Victory.Core.Helpers;
using log4net;
using System.IO;
using log4net.Config;
using Victory.Template.WebApi.Attribute;
using Victory.Template.Entity;

namespace Victory.Template.WebApi
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
            #region 用Cookie做登录验证
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
            {
                //登录路径：这是当用户试图访问资源但未经过身份验证时，程序将会将请求重定向到这个相对路径
                o.LoginPath = new PathString("/Login/Index");

                //禁止访问路径：当用户试图访问资源时，但未通过该资源的任何授权策略，请求将被重定向到这个相对路径。
                o.AccessDeniedPath = new PathString("/UserRight/NoPermission");

            });
            #endregion

            //二次读流
            services.Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);


            //配置，不用每次修改html 都重新run
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();


            //添加log4net服务
            Log4netHelper.Repository = LogManager.CreateRepository("NETCoreRepository");
            XmlConfigurator.Configure(Log4netHelper.Repository, new FileInfo(Environment.CurrentDirectory + "/log4net.config"));


            //全局捕捉异常，并写log日志
            services.AddControllers(option => {
                if (AppConfig.IsExceptionFilter)
                {
                    option.Filters.Add<ExceptionFilter>();
                }

            })
              .AddNewtonsoftJson(options =>
              {
                  options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                  options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
              });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
