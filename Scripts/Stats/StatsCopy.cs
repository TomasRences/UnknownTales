using UnityEngine;

public static class StatsCopy
{

    public static float maxHealth;
    public static float damage;
    public static float armor;
    public static int skillPoints;
    

    public static string toString(){
        return string.Format("maxHealth: {0} damage: {1} armor: {2} skillPoints: {3}", maxHealth, damage, armor, skillPoints);
    }

    // public static StatsCopy instance;


    // private void Awake()
    // {

    //     if (instance == null)
    //     {
    //         DontDestroyOnLoad(gameObject);
    //         instance = this;
    //         SceneManager.sceneLoaded += OnSceneLoaded;
    //     }

    // }

}