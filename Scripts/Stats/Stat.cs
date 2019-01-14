using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stat {


	public float baseValue;

	private List<float> modifiers = new List<float>();

	public void Plus(float step){
		baseValue+=step;
	}
	public void Minus(float step){
		baseValue-=step;
	}

	public float GetValue ()
	{
		float finalValue = baseValue;
		modifiers.ForEach(x => finalValue += x);
		return finalValue;
	}

	public float GetBaseValue(){
		return baseValue;
	}

	public void SetValue (float value)
	{
		baseValue=value;
	}

	public void AddModifier (float modifier)
	{
		if (modifier != 0)
			modifiers.Add(modifier);
	}
	
	public void RemoveModifier (float modifier)
	{
		if (modifier != 0)
			modifiers.Remove(modifier);
	}

}