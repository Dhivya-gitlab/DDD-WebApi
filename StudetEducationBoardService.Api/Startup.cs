
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

namespace StudentEducationBoardService.Api
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
            services.AddControllers();
            services.AddTransient<ISchoolRepository, SchoolRepository>();
            services.AddTransient<ISchoolService, SchoolService>();
            services.AddDbContext<StudentEducationBoardDbContext>(option => option.UseSqlServer(@"Data Source = DNETAZ15; Initial Catalog = EducationBoard; Integrated Security = true"));
            

            //services.AddMvc().AddControllersAsServices();
            //services.AddControllers();
            //services.AddDbContext<StudentEducationBoardDbContext>(option => option.UseSqlServer(@"Data Source = DNETAZ15; Initial Catalog = EducationBoard; Integrated Security = true"));
            //services.AddSingleton<ISchoolService, SchoolService>();
            //services.AddScoped<ISchoolRepository, SchoolRepository>();
            //services.AddSingleton<IUnitOfWork, UnitOfWork>();
            //services.AddSingleton<SchoolService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, StudentEducationBoardDbContext educationContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
