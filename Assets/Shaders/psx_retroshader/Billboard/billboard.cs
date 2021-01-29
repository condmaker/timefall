using UnityEngine;
using System.Collections;

/// <summary>
/// Class used in the PSX retroshader.
/// </summary>
/// <remarks>
/// The shader and addiotional info can be found 
/// <a href="https://github.com/dsoft20/psx_retroshader">
/// here </a>.
/// </remarks>
public class billboard : MonoBehaviour
{

	void Start ()
    {
        if (billboard.cam == null)
        {
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
        }
    }

    public static Transform cam;
    public Vector3 freeRotation = Vector3.one;
    Vector3 eangles = Vector3.zero;

    void LateUpdate()
    {
        this.transform.LookAt(billboard.cam);
        transform.Rotate(0, 180, 0);
        eangles = transform.eulerAngles;
        eangles.x *= freeRotation.x;
        eangles.y *= freeRotation.y;
        eangles.z *= freeRotation.z;
        transform.eulerAngles = eangles;
    }
}
