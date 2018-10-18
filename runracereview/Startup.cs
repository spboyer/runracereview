using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.InteropServices;

namespace runracereview
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services, IHostingEnvironment env)
    {

      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.None;
      });

      // If it is decided to use EF and/or Identity Auth
      //services.AddDbContext<ApplicationDbContext>(options =>
      //{
      //    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
      //    {
      //        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
      //    }
      //    else
      //    {
      //        options.UseSqlite("Data Source=runracereview.db");
      //    }
      //});

      //services.AddDefaultIdentity<IdentityUser>()
      //    .AddEntityFrameworkStores<ApplicationDbContext>();

      //services.AddAuthentication()
      //    .AddFacebook(fb =>
      //    {
      //        fb.AppId = Configuration["Authentication:Facebook:AppId"];
      //        fb.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
      //    })
      //    .AddGoogle(g =>
      //    {
      //        g.ClientId = Configuration["Authentication:Google:ClientId"];
      //        g.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
      //    });

      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

      services.Configure<Settings>(
      options =>
      {
        options.ConnectionString = Configuration.GetSection("MongoDb:ConnectionString").Value;
        options.Database = Configuration.GetSection("MongoDb:Database").Value;
        options.Container = Configuration.GetSection("MongoDb:Container").Value;
        options.IsContained = Configuration["DOTNET_RUNNING_IN_CONTAINER"] != null;
        options.Development = env.IsDevelopment();
      });

      services.AddTransient<Model.IApplicationDbContext, Model.ApplicationDbContext>();
      services.AddTransient<Model.IRaceRepository, Model.RaceRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();

      app.UseMvc();

      // Check and Seed the database
      CheckandSeed(app.ApplicationServices);

    }

    private void CheckandSeed(IServiceProvider services)
    {
      using (var scope = services.CreateScope())
      {
        var db = scope.ServiceProvider.GetService<Model.IRaceRepository>();

        if (db.GetAllRaces().Result.Count() == 0)
        {
          var loader = new Model.RaceLoader(db);
          loader.LoadData();
        }
      }
    }
  }
}
