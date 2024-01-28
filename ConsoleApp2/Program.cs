using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using ConsoleApp2.Services;
using ClassLibrary1.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            // 1. 建立依賴注入的容器
            var serviceCollection = new ServiceCollection();
            /*
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();
            */
            //var builder = WebApplication.CreateBuilder(args);

            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            serviceCollection.AddDbContext<BTContext>(options => options.UseSqlServer(environment));
            // 2. 註冊服務
            serviceCollection.AddTransient<App>();
            serviceCollection.AddTransient<ISerices, EngService>();

            serviceCollection.AddTransient<App>();
            serviceCollection.AddTransient<IEncryptHelper2, EncryptHelper2>();

            // 建立依賴服務提供者
            var serviceProvider = serviceCollection.BuildServiceProvider();
            // app程序运行入口
            //serviceProvider.GetRequiredService<App>().Run2();
            
            serviceProvider.GetRequiredService<App>().RunEncrypt();
            Console.ReadLine(); //緊急修改bug
        }
    }
}
