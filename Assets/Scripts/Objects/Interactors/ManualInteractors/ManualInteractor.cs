using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Type of Interactor that the player can interact 
/// by going near it and pressing the "Interact" key
/// </summary>
public abstract class ManualInteractor : Interactor
{
    /// <summary>
    /// Method responsible for handling what happens when the player interacts
    /// with this Interactor
    /// </summary>
    /// <param name="itemId">Data of the item currently equipped</param>
    /// <param name="position">Current Position of the player</param>
    /// <returns>InteractionResult based on the evaluation</returns>
    public abstract InteractionResult 
        Toggle(ItemData itemId, Vector3 position);
}
