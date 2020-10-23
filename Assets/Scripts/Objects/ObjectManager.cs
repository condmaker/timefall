using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public ObjectData data;
    void Start()
    {
        if (data != null)
            LoadObject(data);
    }

    void LoadObject(ObjectData _data)
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
