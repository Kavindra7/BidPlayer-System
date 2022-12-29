using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using ASPRad.Models;

namespace ASPRad
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
         
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
			// Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
				mc.ForAllMaps((obj, cnfg) => cnfg.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)));
                mc.ForAllMaps((obj, cnfg) => cnfg.ForAllOtherMembers(opts => opts.UseDestinationValue()));
                mc.AddProfile(new AutoMapping());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
			services.AddTransient<EmailHelper>();
            services.AddTransient<Rbac>();

            
			
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)    
			.AddJwtBearer(options =>    
			{    
				options.TokenValidationParameters = new TokenValidationParameters    
				{    
					ValidateIssuer = true,    
					ValidateAudience = true,    
					ValidateLifetime = true,    
					ValidateIssuerSigningKey = true,    
					ValidIssuer = Configuration["Jwt:Issuer"],    
					ValidAudience = Configuration["Jwt:Issuer"],    
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))    
				};    
			});
			
            
			string ConStr = Configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<AppDBContext>(options =>
                options.UseMySql(ConStr)
            );

			services.AddHttpContextAccessor();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
			app.UseStaticFiles();
            app.UseRouting();
			app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
			
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}");
            });

			app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "listfilter",
                    pattern: "api/{controller=Home}/{action=Index}/{fieldname}/{fieldvalue}");
            });
        }
    }
}
