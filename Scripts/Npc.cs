using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class Npc : Interactable {

	PlayerManager playerManager;

	CharacterStats objStats;

	
	void Start(){
		playerManager=PlayerManager.Instance;
		objStats=GetComponent<CharacterStats>();

	}

	public override void Interact(){
		base.Interact();
		
		if(CanvasManager.Instance.canvas.GetComponent<ShopUI>().shopUI.activeSelf)return;
		
		CanvasManager.Instance.canvas.GetComponent<ShopUI>().ToggleShop();
	}
}
