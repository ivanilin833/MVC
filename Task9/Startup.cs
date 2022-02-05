using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Task9.Application;
using Task9.Infrastucture;
using Task9.SqlRepository;


namespace Task9
{
    public class Startup
    {
        private readonly IConfigurationRoot _confgstring;

        public Startup(IHostEnvironment hostEnv)
        {
            _confgstring = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("appsettings.json").Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICourse, CourseRepository>();
            services.AddScoped<IGroup, GroupRepository>();
            services.AddScoped<IStudent, StudentRepository>();
            services.AddScoped<ICourseService,CourseService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddDbContext<AppDBContext>(options => options.UseSqlServer(_confgstring.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Task9")));
            services.AddMvc();
            services.AddAutoMapper(
                    typeof(Startup).Assembly,
                    typeof(CourseService).Assembly,
                    typeof(GroupService).Assembly,
                    typeof(StudentService).Assembly,
                    typeof(AppDBContext).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler("/Error/Error");
            app.UseHsts();
            app.UseStatusCodePagesWithRedirects("/Error/Error");
            app.UseRouting();
            app.UseCors();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Courses}/{action=University}");
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContext content = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                await DbOdjects.Initial(content);
            }
        }
    }
}
