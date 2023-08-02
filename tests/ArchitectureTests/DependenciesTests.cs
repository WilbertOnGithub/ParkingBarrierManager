using System.Diagnostics;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Arentheym.ParkingBarrier.Architecture.Tests;

public class DependenciesTests
{
    private readonly Assembly uiAssembly;
    private readonly Assembly applicationAssembly;
    private readonly Assembly domainAssembly;
    private readonly Assembly infrastructureAssembly;

    public DependenciesTests()
    {
        var path = "../../../../../src/UI/bin/";
        path += Debugger.IsAttached ? "Debug" : "Release";
        path += "/net7.0/";
        path = Fallback(Path.GetFullPath(path));

        uiAssembly = Assembly.LoadFile(Path.Combine(path, "UITests.dll"));
        applicationAssembly = Assembly.LoadFile(Path.Combine(path, "ApplicationTests.dll"));
        domainAssembly = Assembly.LoadFile(Path.Combine(path, "DomainTests.dll"));
        infrastructureAssembly = Assembly.LoadFile(Path.Combine(path, "InfrastructureTests.dll"));
    }

    [Fact]
    public void UIAssemblyIsCompositeRoot()
    {
        uiAssembly.Should().Reference(applicationAssembly);
        uiAssembly.Should().Reference(infrastructureAssembly);
    }

    [Fact]
    public void InfrastructureMustOnlyReferenceDomain()
    {
        infrastructureAssembly.Should().Reference(domainAssembly);
        infrastructureAssembly.Should().NotReference(uiAssembly);
        infrastructureAssembly.Should().NotReference(applicationAssembly);
    }

    [Fact]
    public void ApplicationMustOnlyReferenceDomainAndApplication()
    {
        // TODO: Uncomment when having actually referenced domain
        //applicationAssembly.Should().Reference(domainAssembly);
        // TODO: Uncomment when having actually referenced domain
        // applicationAssembly.Should().Reference(infrastructureAssembly);
        applicationAssembly.Should().NotReference(uiAssembly);
    }

    [Fact]
    public void DomainMustReferenceNothing()
    {
        domainAssembly.Should().NotReference(uiAssembly);
        domainAssembly.Should().NotReference(applicationAssembly);
        domainAssembly.Should().NotReference(infrastructureAssembly);
    }

    private static string Fallback(string path)
    {
        const string Debug = "Debug";
        const string Release = "Release";

        if (!File.Exists(path))
        {
            return path.Contains(Debug, StringComparison.Ordinal) ?
                path.Replace(Debug, Release, StringComparison.Ordinal) : path.Replace(Release, Debug, StringComparison.Ordinal);
        }

        return path;
    }
}