using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageDisplay : MonoBehaviour
{
    private EntityDetection eD;
    private GameObject entityObject;
    private TextMeshProUGUI textToDisplay;

    void Start()
    {
        textToDisplay = GetComponent<TextMeshProUGUI>();
        entityObject = GameObject.Find("EntityDetection");
        eD = entityObject.GetComponent<EntityDetection>();
    }

    void Update()
    {
        if (eD.ObjectIsGrabable)
            textToDisplay.text = ("pick-up " + eD.objectTouchedName);
        else if (eD.ObjectIsUsable)
            textToDisplay.text = ("Use " + eD.objectTouchedName);
        else if (eD.ObjectIsNPC)
            textToDisplay.text = ("Talk to " + eD.objectTouchedName);
        else
            textToDisplay.text = " ";
    }
}
