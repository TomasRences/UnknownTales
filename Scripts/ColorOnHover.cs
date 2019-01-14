
using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
/* Tint the object when hovered. */

public class ColorOnHover : MonoBehaviour
{

    public Color color;
    public Renderer meshRenderer;

    List<Color> originalColours = new List<Color>();

    void Start()
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                var meshR = child.GetComponent<SkinnedMeshRenderer>();
                if (meshR != null)
                {
                    for (int i = 0; i < meshRenderer.materials.Length; i++)
                    {
                        if (meshR.materials[i].HasProperty("_Color"))
                            originalColours.Add(meshR.materials[i].color);

                    }
                }
            }
        }
        else
        {
            if (meshRenderer == null)
            {
                meshRenderer = GetComponent<MeshRenderer>();
            }

            if (meshRenderer != null)
            {
                for (int i = 0; i < meshRenderer.materials.Length; i++)
                {
                    if (meshRenderer.materials[i].HasProperty("_Color"))
                        originalColours.Add(meshRenderer.materials[i].color);

                }
            }
        }

    }

    void OnMouseEnter()
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                var meshR = child.GetComponent<SkinnedMeshRenderer>();
                if (meshR != null)
                {
                    foreach (Material mat in meshR.materials)
                    {
                        if (mat.HasProperty("_Color"))
                            mat.color *= color;
                    }
                }
            }
        }
        else
        {
            foreach (Material mat in meshRenderer.materials)
            {
                if (mat.HasProperty("_Color"))
                    mat.color *= color;
            }
        }
    }

    void OnMouseExit()
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                var meshR = child.GetComponent<Renderer>();
                if (meshR != null)
                {
                    foreach (var color in originalColours)
                    {
                        foreach (var mat in meshR.materials)
                        {
                            mat.color = color;
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < originalColours.Count; i++)
            {
                meshRenderer.materials[i].color = originalColours[i];
            }
        }
    }
}