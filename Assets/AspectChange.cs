using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AspectChange : MonoBehaviour
{
    Camera cam;
    // Start is called before the first frame update
    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.aspect = 1.2f;
    }

}
