using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatiateItem : MonoBehaviour
{
    private ObjectStateHandler osh;

    [SerializeField]
    private Vector3 positionOffset;
    [SerializeField]
    private GameObject item;

    public void Awake()
    {
        osh = GetComponent<ObjectStateHandler>();
        if(osh != null)
            osh.OnChangeState += Instatiate;
    }

    public void Instatiate(ObjectStateHandler oSH, short state)
    {
        GameObject _item;
        _item = Instantiate(item, transform.position + positionOffset, item.transform.rotation);
        _item.transform.SetParent(gameObject.transform);
    }
}
