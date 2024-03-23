using System;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.Xunit;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class Build : NukeBuild
{
    public static int Main() => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution]
    readonly Solution Solution;

    AbsolutePath SourceDirectory => RootDirectory / "src";
    AbsolutePath TestResultsDirectory => RootDirectory / "TestResults";

    Target Clean =>
        _ =>
            _.Before(Restore)
                .Executes(() =>
                {
                    TestResultsDirectory.CreateOrCleanDirectory();
                    SourceDirectory.GlobDirectories("**/{obj,bin}").DeleteDirectories();
                });

    Target Restore =>
        _ =>
            _.Executes(() =>
            {
                DotNetRestore(s => s.SetProjectFile(Solution));
            });

    Target Compile =>
        _ =>
            _.DependsOn(Restore)
                .DependsOn(Clean)
                .Executes(() =>
                {
                    DotNetBuild(s => s.SetProjectFile(Solution).SetConfiguration(Configuration));
                });

    Target Tests =>
        _ =>
            _.DependsOn(Compile)
                .Executes(() =>
                {
                    DotNetTest(s => s.SetProjectFile(Solution).SetConfiguration(Configuration));
                });
}
