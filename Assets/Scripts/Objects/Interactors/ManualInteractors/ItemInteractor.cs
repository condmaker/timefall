using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///Type of ManualInteractor that expects a combination of items to be 
///activated.
///This Interactor stores the added items as children
/// </summary>
public class ItemInteractor : ManualInteractor
{
    /// <summary>
    /// This event is triggered when an item is added
    /// </summary>
    public event Action<ItemData> addData;

    /// <summary>
    /// List of items needed to activate the Interactor
    /// </summary>
    [SerializeField]
    private List<short> unlockers;

    /// <summary>
    /// List of items added by the player
    /// </summary>
    private ICollection<ItemData> itemsAdded;

    /// <summary>
    /// This method is called when the scene starts
    /// </summary>
    public void Awake()
    {
        itemsAdded = new List<ItemData>();
    }

    /// <summary>
    /// Method responsible for handling what happens when the player interacts
    /// with this Interactor
    /// 
    /// This method compares a combination of items added by the player
    /// and the unlockers list and responds accordingly
    /// </summary>
    /// <param name="itemId">Data of the item currently equipped</param>
    /// <param name="position">Current Position of the player</param>
    /// <returns>InteractionResult based on the evaluation</returns>
    public override InteractionResult Toggle(ItemData itemId, Vector3 position)
    {
        if (itemId == null) 
            return InteractionResult.WrongIntMessage;

       
        addData?.Invoke(itemId);

        //Clear the added items list
        itemsAdded.Clear();

        //Check if there was an update
        foreach(Transform t in transform)
        {
            //Add children data to the added list
            itemsAdded.Add(
                t.gameObject.GetComponent<DataHolder>().GetData() as ItemData);
        }

        //Check if the combination of items added was correct
        if (IsCombCorrect(unlockers, itemsAdded))
        {
            ProcessResult();
        }
        else
        {
            ProcessResult(0);
        }
        
        return InteractionResult.UseItem;
    }


    /// <summary>
    /// Method responsible for checking if the corrently added items
    /// correspond to the wanted combination
    /// </summary>
    /// <param name="comb"></param>
    /// <param name="toCheck"></param>
    /// <returns>The result of the operation</returns>
    public bool IsCombCorrect(List<short> comb, ICollection<ItemData> toCheck )
    {
        bool result = true;
        int it = 0;

        //Check if both lists have the same size
        if (comb.Count != toCheck.Count)
        {
            return false;
        }

        //Check if the lists are the same
        foreach(ItemData iS in toCheck)
        {
            if(!(comb[it] == iS.ID))
            {
                result = false;
                break;
            }
            it++;
        }

        return result;
    }


}

