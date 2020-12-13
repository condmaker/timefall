using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should be more global, since this will only work with gates. Maybe make
// GateScript inherit from InteractibleScript and then make BridgeScript and
// FireScript inherit from that too

/* Made some changes to this script, its purpose now is to work on most world objects
you must select on the inspector if the object islocked or not and then you need to
drag the item you wish to use to unlock said object into the objectToggleList.
you must also select if the object you are placing the script on main purpose 
is to toggle other objects(lever) or to toggle itself(door).
*/

public class Toggler : MonoBehaviour
{
    [SerializeField]
    private short maxStates;

    private short state;
    public short State
    {
        get
        {
            return state;
        }
        set
        {
            if (state == maxStates - 1)
                state = 0;
            else
                state = value;
        }
    }
    
    [SerializeField]
    private List<Toggable> toggables;


    //mudar este nome
    private OpenObject objectToOpen;
    private OpenObject selfToggle;

    [SerializeField]
    private short unlockerId;
    private InventorySlot inventorySlot;
    private GameObject inventory; 
    //this is unecessary because i could get inventorySlot
    //in just 1 line, ill deal with this later

    [SerializeField]
    private bool isLocked;
    /*indirectInteraction bool is defined by us in the inspector when the script is placed
    in an object. Or according to the prefab
    Its defines if the script is in an object that changes its own state
    or changes some other objects state, this should later be switched to
    work with item ID's or some kind of tag on WorldObjects... however that
    hasnt been done yet and i want to make things move... there is also the
    problem of objects that can do both... so this actually works well
    for the time being, check GDD anyway.

    isLocked defines if the object needs some other item to be opened pretty much
    */
    // Should this be in 2 separate scripts ? I like it like this actually
    // you pick things in the inspector since the script is very global


    private void Awake()
    {
        selfToggle = gameObject.GetComponent<OpenObject>();
    }


    //muda ITemData para um short
    public void Toggle(ItemData item = null)
    {
        //this is in case the object toggles other things, lever for eg.
 
        if (toggables.Count > 0)
        {
            foreach (Toggable g in toggables)
            {
                State++;
                g.CheckCombinations();
            }
        }

        //this is for things like doors, chests or torches that dont change other things
        if (isLocked)
        {
            if (item == null)
            {
                print("YOU. SHALL NOT. PASS");
                return;
            }

            if (unlockerId == item.ID) //erro aqui
            {
                selfToggle.Toggle();
            }
            else
                print("YOU. SHALL NOT. PASS");
            //A - Show message with display and play locked sound
            //B - just play locked sound?*/
        }

        
    }


    private void /*bool*/ CheckInteractionType()
    {

    }
    // NOTES : still have quite a bit to make this work 100% yet, its at like 90% now.
    //it doesnt have into account:
    //the fact that this probably isnt narrowing down the unlocker enough tecnicaly 
    //speaking... and im not sure if its needed or not as of now, but right now any 
    //key will work because they all have the same item data... then again though,
    //maybe thats right and we need more keys.

    //!!!! Very important remeber to switch current item in inventory back to private
    
}
