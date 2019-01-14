
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class Save
{
    public Stat experiences;
    public Stat nextLevel;
    public Stat maxNumOfPotions;
    public int skillPoints = 0;
    public int numOfPotions;
    public Stat maxHealth;
    public Stat damage;
    public Stat level;
    public Stat armor;

    [SerializeField]
    int gold;

    [SerializeField]
    string[] currentEquipment;

    [SerializeField]
    List<string> items;


    private Save CreateSaveGameObject()
    {
        Save save = new Save();

        var stats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

        save.experiences = stats.experiences;
        save.skillPoints = stats.skillPoints;
        save.level = stats.level;
        save.damage = stats.damage;
        save.armor = stats.armor;
        save.maxHealth = stats.maxHealth;
        save.gold=Inventory.Instance.Gold;

        var currEq = EquipmentManager.Instance.GetCurrentEquipment();

        save.currentEquipment = new string[currEq.Length];
        for (int i = 0; i < currEq.Length; i++)
        {
            if (currEq[i] == null)
                continue;

            EquipmentProxy eqp = new EquipmentProxy(currEq[i]);

            save.currentEquipment[i] = JsonUtility.ToJson(eqp);
        }

        save.items = new List<string>();

        foreach (var item in Inventory.Instance.items)
        {
            EquipmentProxy eqp = new EquipmentProxy(item as Equipment);
            save.items.Add(JsonUtility.ToJson(eqp));
        }

        return save;
    }

    public void SaveGame()
    {
        Save save = CreateSaveGameObject();

        Debug.Log(Application.persistentDataPath);

        Debug.Log(save.gold);
        string jsonFile = JsonUtility.ToJson(save);

        Debug.Log(jsonFile);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/gamesave.save", jsonFile);

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {

            string loadedData = File.ReadAllText(Application.persistentDataPath + "/gamesave.save");
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            Save save = JsonUtility.FromJson<Save>(loadedData);

            var stats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

            if (save.level != null)
                stats.SetLevel((int)save.level.GetValue());
            if (save.experiences != null)
                stats.SetExperiences((int)save.experiences.GetValue());
            if (save.damage != null)
                stats.damage = save.damage;
            if (save.armor != null)
                stats.armor = save.armor;
            if (save.maxHealth != null)
                stats.maxHealth = save.maxHealth;
            if (save.skillPoints != 0)
                stats.skillPoints = save.skillPoints;

            if (save.currentEquipment != null)
            {
                foreach (var eq in save.currentEquipment)
                {
                    try
                    {
                        EquipmentProxy eqp = JsonUtility.FromJson<EquipmentProxy>(eq);
                        EquipmentManager.Instance.Equip(eqp.ToEquipment());
                    }
                    catch { }
                }
            }

            if (save.items != null)
            {
                foreach (var jItem in save.items)
                {
                    EquipmentProxy eqp = JsonUtility.FromJson<EquipmentProxy>(jItem);
                    Inventory.Instance.items.Add(eqp.ToEquipment());
                }
                //Inventory.Instance.items = save.items
            }

            if (save.gold != 0)
            {
                Inventory.Instance.Gold = save.gold;
                if (Inventory.Instance.onGoldChangedCallback != null)
                {
                    Inventory.Instance.onGoldChangedCallback.Invoke(save.gold);
                }
            }

            if (Inventory.Instance.onItemChangedCallback != null)
            {
                Inventory.Instance.onItemChangedCallback.Invoke();
            }


            if (stats.onStatChangedCallback != null)
            {
                stats.onStatChangedCallback.Invoke();
            }

            Debug.Log("Game Loaded");

        }
        else
        {
            Debug.Log("No game saved!");
        }
    }
}