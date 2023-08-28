using System.Threading.Tasks;

namespace Arentheym.ParkingBarrier.UI;

/// <summary>
/// Marks a type as requiring asynchronous initialization and provides the result of that initialization.
/// </summary>
/// <see also="https://blog.stephencleary.com/2013/01/async-oop-2-constructors.html"/>
public interface IAsyncInitialization
{
    /// <summary>
    /// The result of the asynchronous initialization of this instance.
    /// </summary>
    Task Initialization { get; }
}