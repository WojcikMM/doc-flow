using DocFlow.Core.Domain;
using DocFlow.Infrastructure.EntityFramework;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DocFlow.Core.Repositories;
using DocFlow.Infrastructure.EntityFramework.Repositories;
using DocFlow.Application.Applications;
using DocFlow.Application.Transactions;
using DocFlow.Infrastructure.Services;
using DocFlow.Application.Statuses;
using DocFlow.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using DocFlow.Web.Shared.Common;
using System.Reflection;
using DocFlow.Web.Server.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

namespace DocFlow.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DocFlowDbContext>(options => options.UseInMemoryDatabase("Database"));
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))

            services.AddDatabaseDeveloperPageExceptionFilter();


            services.AddDefaultIdentity<UserEntity>(options =>{
                //options.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<DocFlowDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<UserEntity, DocFlowDbContext>()
                .AddProfileService<CustomProfileService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddControllersWithViews();
            services.AddRazorPages();

     //       services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

            services.AddAutoMapper(Assembly.Load("DocFlow.Web.Server"), Assembly.Load("DocFlow.Infrastructure"));

            services.AddTransient<IApplicationRepository, ApplicationRepository>();
            services.AddTransient<ITransactionRepository, TransactionRepository>();
            services.AddTransient<IStatusesRepository, StatusesRepository>();

            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<ITransactionService, TrasactionService>();
            services.AddTransient<IStatusesService, StatusesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<UserEntity> userManager)
        {

            if (env.IsDevelopment())
            {
                // app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                //   app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseExceptionHandler(x =>
    {
        x.Run(async context =>
        {
            if (context.Request.Path.HasValue && context.Request.Path.Value.StartsWith(@"/api/"))
            {
                var errorHandler = context.Features.Get<IExceptionHandlerPathFeature>();

                context.Response.ContentType = "application/json";

                switch (errorHandler.Error)
                {
                    case AggregateValidationException _:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case AggregateNotFoundException _:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case AggregateIllegalLogicException _:
                        break;
                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                await context.Response.WriteAsync(JsonConvert.SerializeObject(new ExceptionDto()
                {
                    Message = errorHandler.Error.Message
                }));
            }
            else if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                x.UseExceptionHandler("/Error");
            }
        });
    });

            // app.UseSwagger();
            // app.UseSwaggerUI(cfg =>
            //{
            //    cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkflowManager.Monolith");
            //    cfg.RoutePrefix = "swagger"; // TODO: Use "swagger" when go to prod
            //});

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

            var user = new UserEntity(Guid.NewGuid().ToString(), "sample@email.com")
            {
                Email = "sample@email.com"
            };

            userManager.CreateAsync(user, "1234.Abcd");
        }
    }
}
