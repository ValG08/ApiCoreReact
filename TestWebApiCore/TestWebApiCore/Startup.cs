using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TestWebApiCore.Common;
using TestWebApiCore.DAL;
using TestWebApiCore.Helpers;

namespace TestWebApiCore
{
    public class Startup
    {
        private ApplicationSettings _appSettings;
        readonly string _specificOrigins = "specificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            var appSettingsSection = Configuration.GetSection("ApplicationSettings");
            services.Configure<ApplicationSettings>(appSettingsSection);

            _appSettings = appSettingsSection.Get<ApplicationSettings>();

            try
            {
                string path = Directory.GetCurrentDirectory();

                CreateDb createDb = new CreateDb();
                createDb.CreateDataBase
                    (path + _appSettings.PathScriptDataBase, _appSettings.ConnectMaster, _appSettings.ConnectDB);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDependencyInjection();

            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy(name: _specificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000")
                                                            .AllowAnyHeader()
                                                            .AllowAnyMethod();
                                  });
               
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/WebApiLog-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors(_specificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
