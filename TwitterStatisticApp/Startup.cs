using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using TwitterStatisticApp.Application.Twitter;
using TwitterStatisticApp.Application.Twitter.Interface;
using TwitterStatisticApp.Domain.Interfaces;
using TwitterStatisticApp.Infra.CrossCutting.Identity;
using TwitterStatisticApp.Infra.CrossCutting.Identity.Interface;
using TwitterStatisticApp.Infra.Data.Repository;

namespace TwitterStatisticApp
{
    public class Startup
    {
        private string CaminhoXmlComments { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            CaminhoXmlComments = string.Format("{0}{1}.xml", AppDomain.CurrentDomain.BaseDirectory, env.ApplicationName);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Cors Configuration
            services.AddCors(options =>
                {
                    options.AddPolicy("AllowAll",
                        builder =>
                        {
                            builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                        });
                });
            #endregion

            #region Authentication Service
            services.AddAuthentication("Bearer")
                        .AddIdentityServerAuthentication(options =>
                        {
                            options.Authority = string.Format("{0}{1}", Configuration["IdentidadeServerPrefix"], Configuration["IdentidadeServer"]);
                            options.RequireHttpsMetadata = false;
                            options.ApiName = Configuration["IdentityServer:ScopeApi"];
                        });
            #endregion

            #region MVC Configuration
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2); 
            #endregion

            #region Dependency Injection
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<ITwitterApp, TwitterApp>();
            services.AddSingleton<ILanguageRepository, LanguageRepository>();
            services.AddSingleton<ITweetRepository, TweetRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<ITweetsByHourRepository, TweetsByHourRepository>();
            services.AddSingleton<ITweetsByTagRepository, TweetsByTagRepository>();
            services.AddSingleton<IIdentityUtils, IdentityUtils>();
            #endregion

            #region Spa Angular Configuration
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            }); 
            #endregion

            #region AutoMapper
            services.AddAutoMapper(); 
            #endregion

            #region Swagger Configuration
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1.000",
                        new Info
                        {
                            Title = "API",
                            Version = "v1.000",
                            Description = "Serviços do Sistema",
                            Contact = new Contact
                            {
                                Name = "Teste"
                            }
                        });

                    c.CustomSchemaIds(x => x.FullName);

                    c.IncludeXmlComments(CaminhoXmlComments);
                }); 
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            #region Authentication Activation
            app.UseAuthentication();
            #endregion

            #region Cors
            app.UseCors("AllowAll");
            #endregion

            #region Swagger
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.000/swagger.json",
                    "Serviços do Sistema");
                c.RoutePrefix = "swagger";
            });
            #endregion

            #region MVC Routes
            app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller}/{action=Index}/{id?}");
                });
            #endregion

            #region Spa Angular
            app.UseSpa(spa =>
                {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseAngularCliServer(npmScript: "start");
                    }
                });
            #endregion
        }
    }
}
