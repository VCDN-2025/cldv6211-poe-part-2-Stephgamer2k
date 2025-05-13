using Azure.Storage.Blobs;
using EventEaseDB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventEaseDB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<EventEaseDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("EventEaseDBContext") ?? throw new InvalidOperationException("Connection string 'EventEaseDBContext' not found.")));

            // Add DbContext
            builder.Services.AddDbContext<EventEaseDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //register blob service client by reading StorageAccount connection string from appsettings.json
            builder.Services.AddSingleton(x => new BlobServiceClient(builder.Configuration.GetConnectionString("StorageAccount")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

/*****************************************************
******************************************************
*Title: Integrating Azure Blob Storage with .NET
*
*Author:
*
*Date: 4 May 2025
*
*Availability: https://medium.com/c-sharp-programming/integrating-azure-blob-storage-with-net-b4fc16dfde73
*
******************************************************
******************************************************/


/*****************************************************
******************************************************
*Title: Model Validation in ASP.NET Core MVC
*
*Author: Microsoft Documentation Team
*
*Date: 11 May 2025
*
*Availability: https://learn.microsoft.com/en-us/aspnet/core/mvc/models/validation
*
******************************************************
******************************************************/


/*****************************************************
******************************************************
*Title: Handle errors in ASP.NET Core
*
*Author: Microsoft Documentation Team
*
*Date: 11 May 2025
*
*Availability:https://learn.microsoft.com/en-us/aspnet/core/fundamentals/error-handling
*
******************************************************
******************************************************/



/*****************************************************
******************************************************
*Title: Partial views, View Components, and ViewModels in ASP.NET Core
*
*Author:Microsoft Documentation Team
*
*Date: 11 May 2025
*
*Availability: https://learn.microsoft.com/en-us/aspnet/core/mvc/views/overview
*
*
******************************************************
******************************************************/



/*****************************************************
******************************************************
*Title: Search Functionality in ASP.NET Core MVC
*
*Author: TutorialsTeacher Team
*
*Date: 11 May 2025
*
*Availability: https://www.tutorialsteacher.com/mvc/search-in-aspnet-mvc
*
******************************************************
******************************************************/
