using Application;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services
            .AddScoped<PageScraper>()
            .AddScoped<SectionParserFactory>()
            .AddScoped<ISectionParser, DefaultSectionParser>()
            .AddScoped<TableRowParser>()
            .AddScoped<RequirementProcessor>();

        return services;
    }
}
