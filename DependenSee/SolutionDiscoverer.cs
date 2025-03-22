using net.r_eg.MvsSln;
using net.r_eg.MvsSln.Core;

namespace DependenSee;

internal static class SolutionDiscoverer
{
    internal static IEnumerable<string> GetProjects(string solutionFullName)
    {
        using var sln = new Sln(solutionFullName, SlnItems.Projects);

        var projectInfos = sln.Result?.ProjectItems ?? Enumerable.Empty<ProjectItem>();

        var projectFiles = projectInfos
            .Where(IsSharpOrBasic)
            .Select(info => info.fullPath)
            .ToList();

        return projectFiles;
    }

    private static bool IsSharpOrBasic(ProjectItem info)
    {
        switch (info.EpType)
        {
            case ProjectType.Cs:
            case ProjectType.CsSdk:
            case ProjectType.Vb:
            case ProjectType.VbSdk:
                {
                    return true;
                }
            default:
                {
                    return false;
                }
        }
    }
}