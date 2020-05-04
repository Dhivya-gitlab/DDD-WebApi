
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StudentEducationBoardService.Data;
using StudentEducationBoardService.Data.Repositories;
using StudentEducationBoardService.Services.Services;
using StudentEducationBoardService.Services.ServicesInterface;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace StudentEducationBoardService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //Configuration = new ConfigurationBinder().AddJsonFile("appsettings.json").Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContextPool<StudentEducationBoardDbContext>(options =>
            //{
            //options.UseSqlServer(@"Data Source = DNETAZ15; Initial Catalog = EducationBoard; Integrated Security = true");
            //});
            services.AddDbContextPool<StudentEducationBoardDbContext>(options =>
            {
                options.UseSqlServer(Configuration["Data:ConectionStrings:azureDbConnection"]);
            });

            //Automatically perform databse migration
            //services.BuildServiceProvider().GetService<StudentEducationBoardDbContext>().Database.Migrate();

            services.AddScoped<ISchoolRepository, SchoolRepository>();
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
        }
    }
}
