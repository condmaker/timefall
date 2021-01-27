/// <summary>
/// Enum that defines the type of iteration that can be applied to
/// a StateHandler
/// </summary>
public enum IterationType
{
    /// <summary>
    /// Iterate to the next state
    /// </summary>
    Next = 1,

    /// <summary>
    /// Iterate to the previous state
    /// </summary>
    Previous = -1,

    /// <summary>
    /// Iterate to the last state
    /// </summary>
    Last
}
