using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TextBindingBehavior : MonoBehaviour
{
    public string BindingName;
    public string TextInitialValue;

    public Text TextComponent;

    
    public string TextValue
    {
        get
        {
            if (TextComponent == null)
            {
                TextComponent = this.gameObject.GetComponent<Text>();
            }
            return TextComponent.text;
        }
        set
        {
            if (TextComponent == null)
            {
                TextComponent = this.gameObject.GetComponent<Text>();
            }
            TextComponent.text = value;
        }
    }

    // Use this for initialization
    void Awake()
    {
        if(CanvasManager.UITextBindings.ContainsKey(BindingName))return;
        TextValue = TextInitialValue;
        CanvasManager.UITextBindings.Add(BindingName, TextComponent);
    }
}
