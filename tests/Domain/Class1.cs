namespace Arentheym.ParkingBarrier.Domain.Tests;

using AutoFixture;
using AutoFixture.AutoNSubstitute;

public class Class1
{
    private readonly IFixture fixture;

    /// <summary>
    /// Constructor
    /// </summary>
    public Class1()
    {
        fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
    }
}