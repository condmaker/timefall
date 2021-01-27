/// <summary>
/// Types of results the interaction can have
/// </summary>
public enum InteractionResult 
{
    /// <summary>
    /// Simply activate the object
    /// </summary>
    Activate,

    /// <summary>
    /// Activate the object and use the equipped item
    /// </summary>
    UseItem,

    /// <summary>
    /// Don't activate the object
    /// </summary>
    WrongIntMessage
}
