using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This should be more global, since this will only work with gates. Maybe make
// GateScript inherit from InteractibleScript and then make BridgeScript and
// FireScript inherit from that too
public class Toggler : MonoBehaviour
{
    // Maybe a list isn't that efficient since it will be static
    [SerializeField]
    private List<GameObject> objectToggleList;
    //mudar este nome
    private OpenObject objectToOpen;

    private ObjectData unlocker;
    private InventorySlot inventorySlot;
    private GameObject inventory;
    /*this bool is defined by us in the inspector when the script is placed
    in an object. 
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
    [SerializeField]
    private bool indirectInteraction, isLocked;

    private void Awake()
    {
        if(isLocked)
        unlocker = objectToggleList[0].GetComponent<DataHolder>().GetData;
    }
    public void Toggle()
    {
        if (indirectInteraction)
        {
            //toggle of its own animation needed (lever go left for example)
            if (objectToggleList.Count > 1)
            {
                foreach (GameObject gameobject in objectToggleList)
                {
                    objectToOpen = gameobject.GetComponent<OpenObject>();
                    objectToOpen.Toggle();
                }
            }
            else
            {
                objectToOpen = objectToggleList[0].GetComponent<OpenObject>();
                objectToOpen.Toggle();
            }
        }
        else
        {
            objectToOpen = gameObject.GetComponent<OpenObject>();
            if (isLocked)
            {
                // !!! yes i know... this will change later, just let me work here ok ? 
                //Also intead of looking at this one should wonder why is the
                //inventory on the canvas uh?
                inventory = GameObject.Find("Slot");
                inventorySlot = inventory.GetComponent<InventorySlot>();

                if (unlocker == inventorySlot.currentItem) //erro aqui
                {
                    objectToOpen.Toggle();
                }
                else
                    print("YOU. SHALL NOT. PASS");
                //Se não ? : A Show message with display and play sound, B just play sound?
            }
            else
                objectToOpen.Toggle();
        }
    }
    private void /*bool*/ CheckInteractionType()
    {

    }
    // NOTES : still have quite a bit to make this work 100% yet, its at like 70% now.
    //it doesnt have into account:
    //1 the objects that change other objects states own animations
    //2 this probably isnt narrowing down the unlocker enough tecnicaly speaking...
    //and im not sure if its needed or not as of now, but right now any key will work
    //because they all have the same item data... then again though, maybe thats right
    //and we need more keys.

    //!!!! Very important remeber to switch current item in inventory back to private
    
}
