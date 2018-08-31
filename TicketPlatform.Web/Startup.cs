using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NLog;
using TicketPlatform.Web.Helper;
using TicketPlatform.Web.Middleware;
using TicketPlatform.Web.Repository;

namespace TicketPlatform.Web
{
    public class Startup
    {
        private readonly string ConnectionString = JsonConfigurationHelper.GetAppSettings<ConfigDTO>("ConnectionString").value;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
#if DEBUG
            //本地调试不使用连接池
            services.AddDbContext<TpContext>(options => options.UseSqlServer(ConnectionString));
#else
            services.AddDbContextPool<TpContext>(options => options.UseSqlServer(ConnectionString));
#endif
            services.AddSingleton(typeof(ILogger), LogManager.GetLogger("FileLogger"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();//使用静态文件

            app.Use(next => new LogRequestMiddleware(next).Invoke);//记录原始请求,回复

            app.UseMvc();
            
            #region NLog配置
            LogManager.LoadConfiguration($"{Directory.GetCurrentDirectory()}\\Config\\Nlog.config");
            #endregion
        }
    }
}
