using Application.Common.Persistence;
using Infrastructure;
using Infrastructure.Mapping;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repository;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IUnitOfWork, UnitofWork>();
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Dependency Injection
            builder.Services.AddPersistence(builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}