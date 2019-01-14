using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
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

    public void GoToTown() //index of scene to move to
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        //DontDestroyOnLoad(GameObject.FindGameObjectWithTag("MainCanvas"));
    }

    void OnTriggerEnter(Collider other)
    {
        LoadSceneByIndex(NextSceneIndex);
    }

}
