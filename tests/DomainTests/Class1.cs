﻿
using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace Arentheym.ParkingBarrier.Domain.Tests;

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