using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinBot.App.Services;
using FinBot.DB;
using FinBot.DB.Repositories;
using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;
using FinBot.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinBot
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
            services.AddDbContext<DataDb>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddScoped<IEntity, Entity>();
            services.AddScoped<IUpdateService, UpdateService>();
            services.AddSingleton<IBotService, BotService>();
            services.AddScoped<RepositoryDb<Entity>>();
            services.AddScoped<IBaseRepositoryDb<Entity>>(s => s.GetRequiredService<RepositoryDb<Entity>>());
            services.AddScoped<IRepositoryReader<Entity>>(s => s.GetRequiredService<RepositoryDb<Entity>>());
            services.AddScoped<IKeyboardBotCreate, KeyboardService>();
            services.AddScoped<ICommandBot, CommandService>();
            services.Configure<BotConfiguration>(Configuration.GetSection("BotConfiguration"));


            services
                .AddControllers()
                .AddNewtonsoftJson();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
