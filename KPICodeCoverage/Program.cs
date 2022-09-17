using GitLabApiClient;
using KPICodeCoverage.Core;
using KPICodeCoverage.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
        services.AddTransient<ICodeCoverageService, CodeCoverageService>()
            .AddTransient<Func<IGitLabClient>>(_ => () => new GitLabClient(IqviaGitLabSettings.Path, IqviaGitLabSettings.Key))
        )
    
    .Build();

await PrintCoverageForAllProjects(host.Services);


await host.RunAsync();

static async Task PrintCoverageForAllProjects(IServiceProvider services)
{
    using IServiceScope serviceScope = services.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    
    var codeCoverageService = provider.GetRequiredService<ICodeCoverageService>();
    foreach (var projectId in IqviaGitLabSettings.Projects.AllProjectIds)
    {
        Console.WriteLine($"{IqviaGitLabSettings.Projects.ProjectNames[projectId] } - {await codeCoverageService.GetProjectCodeCoverage(projectId)}");
    }
}