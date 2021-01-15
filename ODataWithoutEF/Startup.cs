using System;
using Domain.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;
using Newtonsoft.Json;

namespace ODataWithoutEF
{
    public sealed record Startup(IConfiguration Configuration)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
               .AddMvc(o => o.EnableEndpointRouting = false)
               .AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.Formatting = Formatting.Indented;
                })
               .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.WriteIndented = true;
                });

            //services.For;

            services.AddOData();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();

            app.UseEndpoints(builder =>
            {
                builder.EnableDependencyInjection();
                builder.Expand().Select().OrderBy().Count().Filter().MaxTop(null);
                builder.MapODataRoute("odata", "odata", BuildModel(app.ApplicationServices));

                static IEdmModel BuildModel(IServiceProvider services)
                {
                    var model_builder = new ODataConventionModelBuilder(services);
                    model_builder.EntitySet<Student>("Students");
                    return model_builder.GetEdmModel();
                }
            });

            //app.UseMvc(builder =>
            //{
            //    builder.EnableDependencyInjection();
            //    builder.Expand().Select().OrderBy().Count().Filter().MaxTop(null);
            //    //builder.MapO
            //    builder.MapODataServiceRoute("odata", "odata", BuildModel(app.ApplicationServices));

            //    static IEdmModel BuildModel(IServiceProvider services)
            //    {
            //        var model_builder = new ODataConventionModelBuilder(services);
            //        model_builder.EntitySet<Student>("Students");
            //        return model_builder.GetEdmModel();
            //    }
            //});
        }
    }
}
