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
    // Maybe a list isn't that efficient since it will be static
    [SerializeField]
    private List<GameObject> objectToggleList;
    //mudar este nome
    private OpenObject objectToOpen;
    private OpenObject selfToggle;

    private ObjectData unlocker;
    private InventorySlot inventorySlot;
    private GameObject inventory; 
    //this is unecessary because i could get inventorySlot
    //in just 1 line, ill deal with this later

    [SerializeField]
    private bool indirectInteraction, isLocked;
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
        if (isLocked)
        unlocker = objectToggleList[0].GetComponent<DataHolder>().GetData;
    }
    public void Toggle()
    {
        //this is in case the object toggles other things, lever for eg.
        if (indirectInteraction)
        {
            if (objectToggleList.Count > 1)
            {
                foreach (GameObject gameobject in objectToggleList)
                {
                    objectToOpen = gameobject.GetComponent<OpenObject>();
                    objectToOpen.Toggle();
                    print("door was opened");
                }
            }
            else
            {
                objectToOpen = objectToggleList[0].GetComponent<OpenObject>();
                objectToOpen.Toggle();
                print("door was opened");
            }
            //this toggles the object itself, a lever, button, torch, wtv...
            //selfToggle.Toggle(); Lever doesnt yet have animations....
        }
        //this is for things like doors, chests or torches that dont change other things
        else
        {
            if (isLocked)
            {
                // !!! yes i know... this will change later, just let me work here ok ? 
                //Also intead of looking at this one should wonder why is the
                //inventory on the canvas uh?
                inventory = GameObject.Find("Slot");
                inventorySlot = inventory.GetComponent<InventorySlot>();

                if (unlocker == inventorySlot.currentItem) //erro aqui
                {
                    selfToggle.Toggle();
                }
                else
                    print("YOU. SHALL NOT. PASS");
                //A - Show message with display and play locked sound
                //B - just play locked sound?
            }
            else
                selfToggle.Toggle();
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
