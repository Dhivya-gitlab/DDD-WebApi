
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentEducationBoardService.Data;
using StudentEducationBoardService.Domain;
using StudentEducationBoardService.Domain.Services;
using StudentEducationBoardService.Services.Services;

namespace StudentEducationBoardService.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment evn, IConfiguration configuration)
        {
            HostEnvironment = evn;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (HostEnvironment.EnvironmentName == "Development")
            {
                services.AddDbContextPool<StudentEducationBoardDbContext>(options =>
                {
                    options.UseSqlServer(Configuration["Data:ConectionStrings:localDbConnection"]);
                });
            }
            else
            {
                services.AddDbContextPool<StudentEducationBoardDbContext>(options =>
                {
                    options.UseSqlServer(Configuration["Data:ConectionStrings:azureDbConnection"]);
                });
            }

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration["Data:ConectionStrings:AzureRedisCache"];
            });

            //services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddControllers();
            services.AddMvc();
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "School Education Board", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StudentEducationBoardDbContext educationContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Enable middleware to serve generated swagger as a json endpoint
            app.UseSwagger();

            //Enable middleware to server swagger-ui
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Education Board"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            educationContext.Database.EnsureCreated();
            //educationContext.Database.Migrate();
        }
    }
}
