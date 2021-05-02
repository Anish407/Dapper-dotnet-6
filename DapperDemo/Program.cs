using Dapper.DAL.Core;
using Dapper.DAL.Extensions;
using Dapper.DAL.Infra;
using Dapper.DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;


IConfiguration configuration = SetupConfiguration();
IServiceProvider serviceProvider = InitializeDI(configuration);

//IProductRepository productRepository = GetImplementation<IProductRepository>(serviceProvider);
//var response = await productRepository.GetAllProductsNames();


IStudentRepository studentRepository = GetImplementation<IStudentRepository>(serviceProvider);

var response = await studentRepository.InsertStudent(new Student { Id = Guid.NewGuid().ToString(), Address = "MVLK", DOB = new DateTime(1988, 12, 22), Name = "ANish", Phone = "23432432" });
Console.WriteLine(response);
//Console.WriteLine(response.ConvertToJsonString());

Console.WriteLine("Complete");
Console.ReadKey();


TService GetImplementation<TService>(IServiceProvider serviceProvider) where TService : class
{
    try
    {
        return serviceProvider.GetRequiredService<TService>();
    }
    catch (Exception ex)
    {
        throw;
    }
}

IConfiguration SetupConfiguration()
    => new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppContext.BaseDirectory, string.Format("..{0}..{0}..{0}", Path.DirectorySeparatorChar),
            "Settings.json"),
            optional: true)
            .Build();

IServiceProvider InitializeDI(IConfiguration configuration)
{
    var services = new ServiceCollection();

    string name = configuration["name"];
    //configuration
    services.Configure<DbConfiguration>(op => op.Connection = configuration["DbConfiguration:Connection"]);

    //repositories
    services.AddScoped<IDapperExecutor, DapperExecutor>();
    services.AddScoped<IProductRepository, ProductRepository>();
    services.AddScoped<IStudentRepository, StudentRepository>();

    return services.BuildServiceProvider();

}