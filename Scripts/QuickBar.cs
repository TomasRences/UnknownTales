using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickBar : MonoBehaviour
{
    public int NextSceneIndex;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadSceneByIndex(int nextSceneIndex) //index of scene to move to
    {
        SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
    }

    public void SaveAndQuit()
    {
        Save save = new Save();
        save.SaveGame();

        Application.Quit(); 
    }

    public void GoToTown() //index of scene to move to
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
        PlayerManager.Instance.Player.transform.position = new Vector3(-12, 0.5f, -10);
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCanvas"));
    }

    public void HealPlayer()
    {

        var currNum = int.Parse(CanvasManager.UITextBindings["potionsNumber"].text.ToString());

        if (currNum > 0)
        {
            Debug.Log("Plyer healed");
            PlayerManager.Instance.playerStats.Heal(20);
            PlayerManager.Instance.playerStats.numOfPotions -= 1;
            CanvasManager.UITextBindings["potionsNumber"].text = (currNum - 1).ToString();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        LoadSceneByIndex(NextSceneIndex);
    }

}
