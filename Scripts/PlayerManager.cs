using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void Awake()
    {
        if (instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            if (Player == null)
            {
                Player = Instantiate(Resources.Load("Character/Player/Prefarb/PlayerPrefarb"))/*, new Vector3(-12, 0.5f, -10), Quaternion.identity)*/ as GameObject;
                Player.tag = "Player";

                DontDestroyOnLoad(Player);
            }
            instance = this;

            playerStats = Player.GetComponent<PlayerStats>();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    public void ResetPlayersPosition()
    {
        Player.transform.position = new Vector3(-12, 0.5f, -10);
        var agent = Player.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        agent.ResetPath();
    }

    public GameObject Player;
    public PlayerStats playerStats;

}
