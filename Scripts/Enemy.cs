using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable {

	PlayerManager playerManager;

	CharacterStats objStats;

	
	void Start(){
		playerManager=PlayerManager.Instance;
		objStats=GetComponent<CharacterStats>();

	}

	public override void Interact(){
		base.Interact();
		CharacterCombat playerCombat = playerManager.Player.GetComponent<CharacterCombat>();

		if(playerCombat!=null){
			playerCombat.Attack(objStats);
		}
	}
}
