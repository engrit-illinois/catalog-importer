using Application;
using Application.Common.Constants;
using Application.Common.Utilities;
using Application.Parsers;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

//var scraper = new Scraper();
//await Scraper.Scrape();

var civilEngineeringDegree = Degrees.CivilAndEnvironmentalEngineering.DegreeEntries.First(p => p.MajorCode == "0106");

//var civilEngineeringImporter = ImporterHelper.CreateImporter(civilEngineeringDegree.DegreeImporterType);

//await civilEngineeringImporter.ImportDegreeRequirements(civilEngineeringDegree);


// Setup DI container
var serviceProvider = new ServiceCollection()
    .AddScoped<PageScraper>()
    .AddScoped<SectionParserFactory>()
    .AddScoped<ISectionParser, DefaultSectionParser>()
    .AddScoped<RequirementProcessor>()
    .BuildServiceProvider();

// Resolve the RequirementProcessor and execute it
var processor = serviceProvider.GetRequiredService<RequirementProcessor>();
var catalogUrl = CatalogHelper.GetCurrentUgradUrl(civilEngineeringDegree.CatalogUrl);

await processor.Process(catalogUrl); // Use your URL here
