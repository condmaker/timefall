using UnityEngine;


/// <summary>
///Type of ManualInteractor where the equipped item is used when 
///calculating the interaction result
/// </summary>
public class HandInteractor : ManualInteractor
{
    /// <summary>
    /// Variable that defines if the Interacter needs an item to be activated
    /// </summary>
    [SerializeField]
    private bool needsItem;

    /// <summary>
    /// Id of the item that activates the Interactor
    /// </summary>
    [SerializeField]
    private short unlockerId;

    /// <summary>
    /// Method is called before the first fram of the update
    /// </summary>
    private void Start() { /*Empty to activate enabling*/ }

    /// <summary>
    /// Method responsible for handling what happens when the player interacts
    /// with this Interactor
    /// 
    /// This method compares the equipped item id with the unlockerId
    /// and activates according with its evaluation 
    /// </summary>
    /// <param name="itemId">Data of the item currently equipped</param>
    /// <param name="position">Current Position of the player</param>
    /// <returns>InteractionResult based on the evaluation</returns>
    public override InteractionResult Toggle(ItemData itemId, Vector3 position)
    {

        //Compare the unlockerId with id of the equipped item
        if (unlockerId == itemId?.ID || !needsItem)
        {
            //Activate based on the Iteration
            ProcessResult();

            if(!needsItem)
                return InteractionResult.Activate;
            return InteractionResult.UseItem;
        }

        return InteractionResult.WrongIntMessage;
    }

}

