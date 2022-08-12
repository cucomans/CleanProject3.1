using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Miniblog.Core.Services;
//using Miniblog.Core.Services.BackgroundTasks;
using System.Threading.Tasks;
using WebEssentials.AspNetCore.OutputCaching;
using WebMarkupMin.AspNetCore2;
using WebMarkupMin.Core;
//using Hangfire;
using IWmmLogger = WebMarkupMin.Core.Loggers.ILogger;
using WmmNullLogger = WebMarkupMin.Core.Loggers.NullLogger;
using System.Data;
using System;
using NLog;
using Hangfire.LiteDB;
using NLog.Web;
using Postal.AspNetCore;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Routing;
//using Web.Models;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
//using Web.Services;
using AspNetCore.Identity.LiteDB.Data;
using AspNetCore.Identity.LiteDB.Models;
using AspNetCore.Identity.LiteDB;
using Miniblog.Core.Hubs;


namespace Miniblog.Core
{
    public class LanguageRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {

            if (!values.ContainsKey("culture"))
                return false;

            var culture = values["culture"].ToString();
            return culture == "en" || culture == "tr" || culture == "es" || culture == "zh" || culture == "pt";
        }
    }
    public class RouteDataRequestCultureProvider : RequestCultureProvider
    {
        public int IndexOfCulture;
        public int IndexofUICulture;

        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            string culture = null;
            string uiCulture = null;

            var twoLetterCultureName = httpContext.Request.Path.Value.Split('/')[IndexOfCulture]?.ToString();
            var twoLetterUICultureName = httpContext.Request.Path.Value.Split('/')[IndexofUICulture]?.ToString();

            if (twoLetterCultureName == "tr")
                culture = "tr-TR";
            if (twoLetterCultureName == "es")
                culture = "es-AR";
            if (twoLetterCultureName == "pt")
                culture = "pt-BR";
            else if (twoLetterCultureName == "en")
                culture = uiCulture = "en-US";

            if (twoLetterUICultureName == "tr")
                culture = "tr-TR";
            else if (twoLetterUICultureName == "en")
                culture = uiCulture = "en-US";

            if (culture == null && uiCulture == null)
                return NullProviderCultureResult;

            if (culture != null && uiCulture == null)
                uiCulture = culture;

            if (culture == null && uiCulture != null)
                culture = uiCulture;

            var providerResultCulture = new ProviderCultureResult(culture, uiCulture);

            return Task.FromResult(providerResultCulture);
        }
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                //NLog: catch setup errors
                logger.Error(exception, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    {
                        webBuilder
                        .UseKestrel()
                        .UseStartup<Startup>()
                        .ConfigureKestrel(options => options.AddServerHeader = false)
                        .UseUrls("http://localhost:5000/");
                    }
                    else
                    {
                        webBuilder
                             .UseStartup<Startup>()
                             .ConfigureKestrel(options => options.AddServerHeader = false);
                    }

                }).ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Error);
                })
                .UseNLog();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {

            }
            services.AddSignalR();
            services.AddControllersWithViews();
            IMvcBuilder builder = services.AddRazorPages().AddNewtonsoftJson()
                .AddRazorRuntimeCompilation();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
               .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
               .AddDataAnnotationsLocalization();
            services.AddSingleton<ILiteDbContext, LiteDbContext>();
           

            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                     new CultureInfo("en"),
                    new CultureInfo("de"),
                    new CultureInfo("fr"),
                    new CultureInfo("es"),
                    new CultureInfo("ru"),
                    new CultureInfo("ja"),
                    new CultureInfo("pt"),
                    new CultureInfo("zh"),
                    new CultureInfo("en-GB"),
                    new CultureInfo("es"),
                    new CultureInfo("es-AR")
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "es-AR", uiCulture: "es-AR");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new[]{ new RouteDataRequestCultureProvider{
                    IndexOfCulture=1,
                    IndexofUICulture=1
                }};
            });

            services.Configure<RouteOptions>(options =>
            {
                options.ConstraintMap.Add("culture", typeof(LanguageRouteConstraint));
            });

            services.AddSingleton<ILiteDbContext, LiteDbContext>();

            services.AddIdentity<ApplicationUser, AspNetCore.Identity.LiteDB.IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
               //.AddEntityFrameworkStores<ApplicationDbContext>()
               .AddUserStore<LiteDbUserStore<ApplicationUser>>()
               .AddRoleStore<LiteDbRoleStore<AspNetCore.Identity.LiteDB.IdentityRole>>()
               .AddDefaultTokenProviders();

            // Add application services.
           // services.AddTransient<IEmailSenderService, EmailSender>();
            //var asd = Configuration["ConnectionStrings:Hangfire"];

            //services.AddHangfire(configuration => configuration
            //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //    .UseSimpleAssemblyNameTypeSerializer()
            //    .UseRecommendedSerializerSettings()
            //    //.UseLiteDbStorage("Filename=database.db;", new LiteDbStorageOptions
            //    .UseLiteDbStorage(Configuration["ConnectionStrings:Hangfire"]));
            //// Add the processing server as IHostedService
            //services.AddHangfireServer(opt =>
            //{
            //    opt.WorkerCount = 1;
            //    //opt.Queues = new[] { "critical", "default" };
            //});

            services.Configure<BlogSettings>(Configuration.GetSection("blog"));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // services.Configure<EmailSenderOptions>(Configuration.GetSection("EmailSender"));
            // services.AddPostal();
            // services.AddTransient<IEmailSenderService, EmailSender>();

            //// Progressive Web Apps https://github.com/madskristensen/WebEssentials.AspNetCore.ServiceWorker
            services.AddProgressiveWebApp(new WebEssentials.AspNetCore.Pwa.PwaOptions
            {
                OfflineRoute = "/shared/offline/"
            });

            // Output caching (https://github.com/madskristensen/WebEssentials.AspNetCore.OutputCaching)
            services.AddOutputCaching(options =>
            {
                options.Profiles["default"] = new OutputCacheProfile
                {
                    Duration = 3600
                };
            });


            // HTML minification (https://github.com/Taritsyn/WebMarkupMin)
            services
                .AddWebMarkupMin(options =>
                {
                    options.AllowMinificationInDevelopmentEnvironment = true;
                    options.DisablePoweredByHttpHeaders = true;
                })
                .AddHtmlMinification(options =>
                {
                    options.MinificationSettings.RemoveOptionalEndTags = false;
                    options.MinificationSettings.WhitespaceMinificationMode = WhitespaceMinificationMode.Safe;
                });
            services.AddSingleton<IWmmLogger, WmmNullLogger>(); // Used by HTML minifier

            // Bundling, minification and Sass transpilation (https://github.com/ligershark/WebOptimizer)
            services.AddWebOptimizer(pipeline =>
            {
                pipeline.MinifyJsFiles();
                pipeline.CompileScssFiles()
                        .InlineImages(1);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
                app.UseHsts();
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                app.UseHttpsRedirection();
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                });
            }
            app.Use((context, next) =>
            {
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                return next();
            });
            //backgroundJobs.Enqueue(() => InitHangfire.InitializeMyFriend());
            app.UseStatusCodePagesWithReExecute("/Shared/Error");
            app.UseWebOptimizer();
            app.UseStaticFilesWithCache();
            if (Configuration.GetValue<bool>("forcessl"))
            {
                app.UseHttpsRedirection();
            }
            app.UseAuthentication();
            app.UseOutputCaching();
            app.UseWebMarkupMin();
            app.UseRouting();
            var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
            app.UseRequestLocalization(localizationOptions);
            app.UseAuthorization();
            //app.UseHangfireDashboard("/hangfire", new DashboardOptions
            //{
            //    Authorization = new[] { new MyAuthorizationFilter() }
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
               // endpoints.MapHub<OnlineCountHub>("/onlinecount");
                endpoints.MapControllerRoute(
                    name: "LocalizedDefault",
                    pattern: "{culture:culture}/{controller=Landing}/{action=Index}/{id?}",
                    defaults: new { controller = "Landing", action = "Index", culture = "es" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{*catchall}",
                    defaults: new { controller = "Landing", action = "RedirectToDefaultLanguage", culture = "es" });
            });

        }
    }
}
