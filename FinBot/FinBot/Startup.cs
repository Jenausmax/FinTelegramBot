using System;
using FinBot.App.Services;
using FinBot.DB;
using FinBot.DB.Repositories;
using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;
using FinBot.Domain.Models.Entities;
using FinBot.Maintenance.Abstractions;
using FinBot.Maintenance.Jobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FinBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataDb>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            //services.AddScoped<IEntity, Entity>();
            services.AddScoped<IUserControl, UserControlService>();
            services.AddScoped<IUpdateService, UpdateService>();
            services.AddSingleton<IBotService, BotService>();
            //services.AddScoped<RepositoryDb<Entity>>();
            //services.AddScoped(typeof(RepositoryDb<>));
            //services.AddScoped<IBaseRepositoryDb<Entity>>(s => s.GetRequiredService<RepositoryDb<Entity>>());
            //services.AddScoped<IRepositoryReader<Entity>>(s => s.GetRequiredService<RepositoryDb<Entity>>());

            services.AddScoped(typeof(IBaseRepositoryDb<>),(typeof(RepositoryDb<>)));
            services.AddScoped(typeof(IRepositoryReader<>),(typeof(RepositoryDb<>)));
            services.AddScoped<IKeyboardBotCreate, KeyboardService>();
            services.AddScoped<ICommandBot, CommandService>();
            services.Configure<BotConfiguration>(Configuration.GetSection("BotConfiguration"));

            

            services
                .AddControllers()
                .AddNewtonsoftJson();

            services.AddHostedService<UserTaskReminderJobs>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
