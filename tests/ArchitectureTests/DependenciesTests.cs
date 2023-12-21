using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Arentheym.ParkingBarrier.Architecture.Tests;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit testing naming")]
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
        path += "/net8.0/";
        path = Fallback(Path.GetFullPath(path));

        uiAssembly = Assembly.LoadFile(Path.Combine(path, "UI.dll"));
        applicationAssembly = Assembly.LoadFile(Path.Combine(path, "Application.dll"));
        domainAssembly = Assembly.LoadFile(Path.Combine(path, "Domain.dll"));
        infrastructureAssembly = Assembly.LoadFile(Path.Combine(path, "Infrastructure.dll"));
    }

    [Fact]
    public void UIAssembly_is_compositeroot()
    {
        uiAssembly.Should().Reference(applicationAssembly);
        uiAssembly.Should().Reference(infrastructureAssembly);
    }

    [Fact]
    public void Infrastructure_can_only_reference_domain_and_application()
    {
        infrastructureAssembly.Should().Reference(domainAssembly);
        infrastructureAssembly.Should().Reference(applicationAssembly);
        infrastructureAssembly.Should().NotReference(uiAssembly);
    }

    [Fact]
    public void Application_can_only_reference()
    {
        applicationAssembly.Should().Reference(domainAssembly);
        applicationAssembly.Should().NotReference(uiAssembly);
    }

    [Fact]
    public void Domain_must_stand_alone()
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