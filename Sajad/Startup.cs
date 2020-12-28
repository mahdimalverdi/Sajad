using System.IO;
using Abstraction.Managers;
using Abstraction.Repositories;
using Business.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Storage;
using Storage.Repositories;

namespace Sajad
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
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });

            services.AddDbContext<SajadDbContext>(o => o.UseSqlite(Configuration["ConnectionString"]));

            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IParaghraphRepository, ParaghraphRepository>();

            services.AddScoped<IContentManager, ContentManager>();
            services.AddScoped<IParaghraphManager, ParaghraphManager>();
            services.AddScoped<IQuestionManager, QuestionManager>();
            services.AddScoped<IDocumentManager, DocumentManager>();

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
            });

            services.Configure<KestrelServerOptions>(options =>
            {
                options.Limits.MaxRequestBodySize = int.MaxValue; 
            });

            var provider = services.BuildServiceProvider();
            using var dbContext = provider.GetService<SajadDbContext>();
            dbContext.Database.Migrate();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDefaultFiles();

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404
                && !Path.HasExtension(context.Request.Path.Value)
                && !context.Request.Path.Value.ToLower().StartsWith("/api"))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseStaticFiles();
        }
    }
}
