using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static bool SceneWasLoaded = false;

    private void Awake()
    {

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }


        Debug.Log("GameManager Awaked");
    }

    void OnEnable()
    {

    }

    IEnumerator Sleep(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);


        CanvasManager.Instance.Awake();
        PlayerManager.Instance.Awake();
        SceneWasLoaded = true;
        //StartCoroutine(Sleep(1));
        //PlayerManager.Instance.Player.transform.position = new Vector3(-12,0.5f,-10);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(5);
    }
    
    void Start()
    {
        Save save = new Save();
        save.LoadGame();

        var stats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

        if (!File.Exists(Application.persistentDataPath + "/gamesave.save"))
            Inventory.Instance.onGoldChangedCallback.Invoke(250);
        
    }

    void Update()
    {
        if (SceneWasLoaded)
        {
            PlayerManager.Instance.ResetPlayersPosition();
            SceneWasLoaded = false;
        }
    }
}