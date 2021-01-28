using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that changes a camera's aspect ratio.
/// </summary>
public class AspectChange : MonoBehaviour
{
    /// <summary>
    /// The camera instance.
    /// </summary>
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.aspect = 1.2f;
    }

}
