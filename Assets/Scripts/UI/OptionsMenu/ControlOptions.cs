using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlOptions : MonoBehaviour
{
    [SerializeField]
    private OptionSelector strafeSelector;
    [SerializeField]
    private KeyBindings playerBinds;


    private void Awake()
    {
        strafeSelector.onSelect += playerBinds.UpdateStrafe;
    }

}
