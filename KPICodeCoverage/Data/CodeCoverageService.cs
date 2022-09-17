using GitLabApiClient;
using GitLabApiClient.Internal.Paths;
using GitLabApiClient.Models.Pipelines;
using KPICodeCoverage.Core;

namespace KPICodeCoverage.Data;

public class CodeCoverageService : ICodeCoverageService
{
    private readonly Func<IGitLabClient> _clientFactory;

    public CodeCoverageService(Func<IGitLabClient> clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<string> GetProjectCodeCoverage(ProjectId projectId)
    {
        var client = _clientFactory();
        
        var pipelines = await client.Pipelines.GetAsync(projectId, (options) =>
        {
            options.Status = PipelineStatus.Success;
            options.UpdatedAfter = DateTime.Today.AddDays(-5);

        });
        pipelines = pipelines.OrderBy(x => x.CreatedAt).ToList();

            
        var pipelineDetail = await client.Pipelines.GetAsync(projectId, pipelines.Last().Id);
        var lastCodeCoverage = pipelineDetail.Coverage;
        var i = pipelines.Count;

            
        while (string.IsNullOrWhiteSpace(lastCodeCoverage) && i > 1)
        {
            pipelineDetail = await client.Pipelines.GetAsync(projectId, pipelines[--i].Id);
            lastCodeCoverage = pipelineDetail.Coverage;
        }

        return lastCodeCoverage ?? "0";

    }
}