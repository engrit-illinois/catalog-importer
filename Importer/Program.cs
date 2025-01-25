using System.Text.Json;
using Application;
using Application.Common.Constants;
using Microsoft.Extensions.DependencyInjection;

// Setup DI container
var serviceProvider = new ServiceCollection()
    .AddApplicationServices()
    .BuildServiceProvider();

Console.WriteLine("Let's Parse!");

//var civilEngineeringDegree = Degrees.CivilAndEnvironmentalEngineering.DegreeEntries.First(p => p.MajorCode == "0106");
var aerospaceEngineeringDegree = Degrees.AerospaceEngineering.DegreeEntries.First(d => d.CatalogYear == 2024 && d.MajorCode == "1647");
var processor = serviceProvider.GetRequiredService<RequirementProcessor>();

var data = await processor.Process(aerospaceEngineeringDegree);

Console.WriteLine(JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true }));
