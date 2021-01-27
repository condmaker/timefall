using UnityEngine;

/// <summary>
///Type of ManualInteractor where the equipped item is used 
///to directly jump to its assigned state  
/// </summary
public class DirectItemInteractor : ManualInteractor
{
    /// <summary>
    /// List of items id and the corresponding state to jump to
    /// </summary>
    [SerializeField]
    private ItemStatePar[] par;

    //// <summary>
    /// Method responsible for handling what happens when the player interacts
    /// with this Interactor
    /// 
    /// This method compares the equipped item id with the list of 
    /// ItemStatePars signals to jump to the appropriate state    
    /// </summary>
    /// <param name="itemId">Data of the item currently equipped</param>
    /// <param name="position">Current Position of the player</param>
    /// <returns>InteractionResult based on the evaluation</returns>
    public override InteractionResult Toggle(ItemData itemId, Vector3 position)
    {
        //Loops through the list of pares
        foreach(ItemStatePar i in par)
        {
            //Compare the equipped item with the current item id of the Par
            if (i.ID == itemId.ID)
            {
                ProcessResult(i.State);
                return InteractionResult.UseItem;

            }
        }

        return InteractionResult.WrongIntMessage;

    }


}
