

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;


    // Use this for initialization
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            startButton.GetComponentInChildren<Text>().text = "Continue Game";
        }
        else
        {
            startButton.GetComponentInChildren<Text>().text = "New Game";
        }

        startButton.onClick.RemoveAllListeners();
        startButton.onClick.AddListener(delegate ()
        {
            try
            {
                var stats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();
                stats.Heal((int)stats.maxHealth.GetValue());
            }
            catch { }
            LoadSceneByIndex(1);
        });

        quitButton.onClick.RemoveAllListeners();
        quitButton.onClick.AddListener(delegate ()
        {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadSceneByIndex(int nextSceneIndex) //index of scene to move to
    {
        SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
    }

}
