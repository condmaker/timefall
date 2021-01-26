using System.Collections;
using UnityEngine;


public class EnterNextLevel : MonoBehaviour
{
    private ObjectStateHandler osh;

    public void Awake()
    {
        osh = GetComponent<ObjectStateHandler>();
        if (osh != null)
            osh.OnChangeState += LoadNextScene;
    }

    public void LoadNextScene(ObjectStateHandler oSH, short state)
    {
       
    }
}
