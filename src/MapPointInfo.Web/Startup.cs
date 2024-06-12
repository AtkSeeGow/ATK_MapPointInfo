using MapPointInfo.Domain.Options;
using MapPointInfo.Repository;
using MapPointInfo.Service;
using MapPointInfo.Web.Handler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json.Serialization;

namespace MapPointInfo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(typeof(DateTime), new DateTimeSerializer(DateTimeKind.Local, BsonType.DateTime));

            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSession();

            var authorizationOption = Configuration.GetSection("AuthorizationOption").Get<AuthorizationOption>()!;
            var tokenOption = Configuration.GetSection("TokenOption").Get<TokenOption>()!;
            var mongoOption = Configuration.GetSection("MongoOption").Get<MongoOption>()!;

            services.AddSingleton(provider => authorizationOption);
            services.AddSingleton(provider => tokenOption);
            services.AddSingleton(provider => mongoOption);

            services.AddScoped<MarkerInfoRepository>();
            services.AddScoped<MarkerRepository>();

            services.AddScoped<AuthorizationService>();

            services.AddControllersWithViews();

            services
                .AddMvc()
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNameCaseInsensitive = true)
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddAuthorization(options =>
            {
                options.AddPolicy("permissionRequirement", policy => policy.Requirements.Add(new PermissionRequirement()));
            }).AddAuthorizationPolicyEvaluator();
            services.AddScoped<IAuthorizationHandler, PermissionRequirementHandler>();

            services.AddAuthentication(options => options.AddScheme("PermissionHandler", o => o.HandlerType = typeof(PermissionHandler)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.Use(async (context, next) =>
            {
                await next.Invoke();

                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = new PathString("/");
                    await next.Invoke();
                }
            });

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
