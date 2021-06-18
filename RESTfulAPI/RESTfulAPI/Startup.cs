using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RESTfulAPI.DataContext;
using RESTfulAPI.Helpers;
using RESTfulAPI.JWTHelper;
using RESTfulAPI.Repositories;
using RESTfulAPI.Services;
using System;
using System.Linq;
using System.Text;

namespace RESTfulAPI
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
            //config reader 
            var configReader = new ConfigReader(Configuration);
            services.AddHttpContextAccessor();

            //Database connection
            services.AddDbContext<RESTfulDataContext>(options =>
                options.UseSqlServer(configReader.GetConnectionStringByEnvironment()));

            var tokenValidationParameters = new JWTHelper.JWTHelper(Configuration).GetTokenValidationParameters();

            //setting jwt authentication scheme
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = false;
                options.TokenValidationParameters = tokenValidationParameters;
            });
            services.AddControllers();

            //Swagger implimentation, to test the API
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Anti forgery token
            services.AddAntiforgery(o => { o.HeaderName = "XSRF-TOKEN"; });

            //Respository and service injection 
            services.AddScoped<IJWTHelper, JWTHelper.JWTHelper>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "RESTful Web API");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
