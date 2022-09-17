using GitLabApiClient.Internal.Paths;

namespace KPICodeCoverage.Core;

public interface ICodeCoverageService
{
    Task<string> GetProjectCodeCoverage(ProjectId projectId);
}