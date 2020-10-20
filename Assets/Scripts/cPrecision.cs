using System;
using UnityEngine;

[ExecuteInEditMode]
public class cPrecision : MonoBehaviour
{
    [SerializeField]
    private Material material;
    public int colorPrecision = 16;
    public bool usePalette = true;
    public Texture2D palette;
   
    void Awake()
    {
        material = new Material(Shader.Find("Hidden/cPrecision"));
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (usePalette)
        {
            material.SetFloat("_usePalette", 1);
            Debug.Log("Cope");
        }
        else
        {
            material.SetFloat("_usePalette", 0);
        }

        material.SetFloat("_Colors", colorPrecision);
        material.SetTexture("_Palette", palette);
        Graphics.Blit(source, destination, material);
    }
}