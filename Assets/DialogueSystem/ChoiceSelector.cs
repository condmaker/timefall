using UnityEngine;

/// <summary>
/// Class responsible for managing the behaviours of a Choice Button
/// </summary>
public class ChoiceSelector : MonoBehaviour
{
    /// <summary>
    /// Property that defines the number of the choice in the Node
    /// </summary>
    public int ChoiceNumb { get; set; }

    /// <summary>
    /// Delegate that stores the numder of this choice
    /// </summary>
    /// <param name="i">Number of this choice</param>
    public delegate void ChangeLine(int i);

    /// <summary>
    /// Property that is used when the ChoiceSelector needs to
    /// change to the enxt line
    /// </summary>
    public ChangeLine NextLine { get; set; }

    /// <summary>
    /// Method triggered when this Choice Selector is selected
    /// </summary>
    public void SelectChoice()
    {
        NextLine(ChoiceNumb);
    }
}
