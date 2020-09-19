using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper;
using Hangfire;
using LogApp.DAL;
using LogApp.DAL.Repositories;
using LogApp.Services;
using LogApp.Core;
using LogApp.Core.Models;
using LogApp.Core.DTO;
using LogApp.Core.Abstractions.Services;
using LogApp.Core.Abstractions.Repositories;
using LogApp.Services.MessageHub;

namespace LogApp
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
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:4200");
            }));

            services.AddHangfire(configuration =>
            {
                configuration.UseSqlServerStorage("Server=(LocalDB)\\MSSQLLocalDB;Integrated Security=true;");
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddControllers();

            services.AddSignalR();

            services.AddDbContext<LogAppContext>(context => context.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection")));

            services.AddScoped<IRecordRepository, RecordRepository>();

            services.AddScoped<IRecordService, RecordService>();
            services.AddScoped<IRecordPagingService<RecordOverallDTO, Record>, RecordPagingService>();
            services.AddScoped<IRecordDetailsService<RecordDetailsDTO>, RecordDetailsService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<NotifyHub>("/notifyNewRecords");
            });

            app.UseHangfireServer();
            RecurringJob.AddOrUpdate<RecordFiller>(x => x.RandomFill(), Cron.MinuteInterval(5));
            app.UseHangfireDashboard();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
