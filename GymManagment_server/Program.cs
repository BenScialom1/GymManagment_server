using GymManagment_server.Models;
using Microsoft.EntityFrameworkCore;
namespace GymManagment_server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
/*
            #region Add Database context to Dependency Injection
            //Read connection string from app settings.json
            string connectionString = builder.Configuration
                .GetSection("ConnectionStrings")
                .GetSection("GymManagment_server").Value;
*/
            //Add Database to dependency injection
            builder.Services.AddDbContext<BenDBContext>(
                   options => options.UseSqlServer("Server = (localdb)\\MSSQLLocalDB; Initial Catalog = GymManagment_server; User ID = TaskAdminLogin; Password = Petel123; Trusted_Connection = True; MultipleActiveResultSets = true"));


            var app = builder.Build();

            #region for debugginh UI
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #endregion



            #region for debugginh UI
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            #endregion
            #region Add Session
            app.UseSession(); //In order to enable session management
            #endregion 
            // Add services to the container.

            
                   

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
