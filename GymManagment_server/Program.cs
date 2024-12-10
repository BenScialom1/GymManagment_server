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

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddSwaggerGen();

            builder.Services.AddEndpointsApiExplorer();
            var app = builder.Build();

           
            
                   

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseSession();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
