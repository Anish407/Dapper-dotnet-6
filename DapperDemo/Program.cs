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

IProductRepository productRepository = GetImplementation<IProductRepository>(serviceProvider);
IStudentRepository studentRepository = GetImplementation<IStudentRepository>(serviceProvider);

//Get All Products
//var response = await productRepository.GetAllProductsNames();

//Execute multiple queries
//var response = await studentRepository.GetStudentWithAccounts(new Guid("82F667D8-42CC-4763-BA24-85D7862E3667"));

//Insert 
//var response = await studentRepository.InsertStudent(new Student { Id = Guid.NewGuid().ToString(), Address = "MVLK", DOB = new DateTime(1988, 12, 22), Name = "ANish", Phone = "23432432" });

//IN OperatorSQL
var response = await studentRepository.GetStudentWithIDs("82F667D8-42CC-4763-BA24-85D7862E3667", "82F667D8-42CC-4763-BA24-85D7862E3667");

//Console.WriteLine(response);
Console.WriteLine(response.ConvertToJsonString());

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

//Setup IConfiguration
IConfiguration SetupConfiguration()
    => new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(AppContext.BaseDirectory, string.Format("..{0}..{0}..{0}", Path.DirectorySeparatorChar),
            "Settings.json"),
            optional: true)
            .Build();

//Setup DI and register dependencies
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