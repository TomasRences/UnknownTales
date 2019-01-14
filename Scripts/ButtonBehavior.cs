using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ButtonBehavior : MonoBehaviour
{
    public string BindingName;

    public Button ButtonComponent;
    

    // Use this for initialization
    void Awake()
    {
        if(CanvasManager.UIButtons.ContainsKey(BindingName))return;
        Debug.Log(BindingName);
        CanvasManager.UIButtons.Add(BindingName, ButtonComponent);
    }
}
