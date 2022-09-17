using GitLabApiClient.Internal.Paths;

namespace KPICodeCoverage.Core;

    public static class IqviaGitLabSettings
    {
        public static class Projects
        {
            public static ProjectId Gateway = 2037;
            public static ProjectId Dashboard = 9120;
            public static ProjectId Configuration = 1956;
            public static ProjectId WidgetTemplateEditor = 6198;
            public static ProjectId Statistics = 4023;
            public static ProjectId Migration = 4024;

            public static ProjectId[] AllProjectIds { get; } = { Gateway, Dashboard, Configuration, WidgetTemplateEditor, Statistics, Migration };

            public static Dictionary<ProjectId, string> ProjectNames { get; } = new()
            {
                { Configuration, "Configuration" },
                { Dashboard, "Dashboard" },
                { Gateway, "Gateway" },
                { Migration, "Migration" },
                { Statistics, "Statistics" },
                { WidgetTemplateEditor, "WidgetTemplateEditor" }
            };
        }

        public static string Path = "https://gitlab.ims.io/";
        public static string Key = "some_gitlab_key";
    }
