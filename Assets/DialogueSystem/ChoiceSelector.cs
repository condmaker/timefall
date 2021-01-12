using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceSelector : MonoBehaviour
{
    public int ChoiceNumb { get; set; }

    public delegate void ChangeLine(int i);

    public ChangeLine NextLine { get; set; }

    public void SelectChoice()
    {
        NextLine(ChoiceNumb);
    }
}
