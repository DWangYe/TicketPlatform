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
using Swashbuckle.AspNetCore.Swagger;
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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {
                    Version = "v1",
                    Title = "年卡服务API",
                    Description = "暂无",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = "https://twitter.com/spboyer"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();//使用静态文件

            #region swagger 配置
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            #endregion

            app.Use(next => new LogRequestMiddleware(next).Invoke);//记录原始请求,回复

            app.UseMvc();
            
            #region NLog配置
            LogManager.LoadConfiguration($"{Directory.GetCurrentDirectory()}\\Config\\Nlog.config");
            #endregion
        }
    }
}
