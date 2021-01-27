using UnityEngine;


/// <summary>
/// Class responsible for instatiating an item when the GameObject this script 
/// is attached changes states
/// </summary>
public class InstatiateItem : MonoBehaviour
{
    /// <summary>
    /// The State Handler of the GameObject this script is attached
    /// </summary>
    private ObjectStateHandler osh;

    /// <summary>
    /// Position offset from the center of the object
    /// </summary>
    [SerializeField]
    private Vector3 positionOffset;

    /// <summary>
    /// Object to be instatiated
    /// </summary>
    [SerializeField]
    private GameObject item = null;

    /// <summary>
    /// Method called when the scene starts
    /// </summary>
    public void Awake()
    {
        osh = GetComponent<ObjectStateHandler>();
        if(osh != null)
            osh.OnChangeState += Instatiate;
    }

    /// <summary>
    /// Method responsible for instatiating the object
    /// </summary>
    /// <param name="oSH"></param>
    /// <param name="state"></param>
    public void Instatiate(ObjectStateHandler oSH, short state)
    {
        GameObject _item;
        _item = Instantiate(item, transform.position + positionOffset, item.transform.rotation);
        _item.transform.SetParent(gameObject.transform.GetChild(1));
    }
}
