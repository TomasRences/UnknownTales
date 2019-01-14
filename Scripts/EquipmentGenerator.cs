using UnityEngine;

public class EquipmentGenerator : MonoBehaviour
{
    static string[] weapons = new string[] { "Mace", "LumberjackAxe", "CheapKnife", "BasicSword" };
    static string[] shields = new string[] { "SkullShield", "WoodenShield" };
    static string[] helmets = new string[] { "HelmetOfFire", "HelmetOfProtection" };


    static string[] amulets_names = new string[] { "Amulet of Sun", "Amulet of Fire", "Poisoned amulet", "Neclace of Power" };
    static string[] swords_names = new string[] { "Sword of Destiny", "Undead\'s sword", "Silver sword of Protection", "Sword bastard" };
    static string[] maces_names = new string[] { "Mace of Apocalipse", "Doom stick", "Wooden mace" };
    static string[] axes_names = new string[] { "Murderer's axe", "Blooded axe", "Big axe" };
    static string[] knifes_names = new string[] { "Thief's knife", "Knife", "Assassin's blade", "Wolf's fang" };
    static string[] shields_names = new string[] { "Fantastic shield", "Shield of Undead Baron", "Shield of Death" };
    static string[] helmets_names = new string[] { "Helmet of destiny", "Old helmet of Bravery", "Helmet", "Wooden helmet" };
    static string[] rings_names = new string[] { "Gold ring", "Magic ring of Poison", "Perfect ring of Curse", "Ring of Peace" };
    static string[] stones_names = new string[] { "Stone of Magic", "Stone of Acrobacy", "Stone of Health", "Stone of Immortality" };
    static string[] armors_names = new string[] { "Wooden armor", "Armor of Protection", "Skull armor", "Samuraj's armor" };


    public static void GenerateRandomItemObj()
    {
        int index = Random.Range(0, 5);
        Debug.Log("GenerateRandomItemObj: "+index);
        
        switch (index)
        {
            case 0:
                {
                    GenerateRandomWeaponObj();
                    break;
                }
            case 1:
                {
                    GenerateRandomShieldObj();
                    break;
                }
            case 2:
                {
                    GenerateRandomHelmetObj();
                    break;
                }
            case 3:
                {
                    GenerateRandomWeaponObj();
                    break;
                }
            case 4:
                {
                    GenerateRandomShieldObj();
                    break;
                }
            case 5:
                {
                    GenerateRandomHelmetObj();
                    break;
                }
        }
    }

    public static Equipment GenerateRandomAmulet()
    {
        Equipment item = new Equipment();

        item.equipSlot = EquipmentSlot.Amulet;
        Sprite[] items = Resources.LoadAll<Sprite>("Ui/Sprites/Items/Amulets");
        Debug.Log(items.Length);

        int icon = Random.Range(0, items.Length - 1);
        item.icon = items[icon];
        int name = Random.Range(0, amulets_names.Length - 1);
        item.name = amulets_names[name];

        item.armorModifier=Random.Range(1,10) + (int)Utilities.GetLevelMultiplierArmor((int)PlayerManager.Instance.playerStats.level.GetValue()); 
        item.damageModifier=Random.Range(1,5) + (int)Utilities.GetLevelMultiplierDamage((int)PlayerManager.Instance.playerStats.level.GetValue()); 
        
        return item;
    }

    public static Equipment GenerateRandomArmor()
    {
        Equipment item = new Equipment();

        item.equipSlot = EquipmentSlot.Chest;
        Sprite[] items = Resources.LoadAll<Sprite>("Ui/Sprites/Items/Armors");
        Debug.Log(items.Length);
        int icon = Random.Range(0, items.Length - 1);
        item.icon = items[icon];
        int name = Random.Range(0, armors_names.Length - 1);
        item.name = armors_names[name];

        item.armorModifier=Random.Range(1,20) + (int)Utilities.GetLevelMultiplierArmor((int)PlayerManager.Instance.playerStats.level.GetValue()); 
        
        return item;
    }

    public static Equipment GenerateRandomHelmet()
    {
        Equipment item = new Equipment();

        item.equipSlot = EquipmentSlot.Head;
        Sprite[] items = Resources.LoadAll<Sprite>("Ui/Sprites/Items/Helmets");
        Debug.Log(items.Length);
        int icon = Random.Range(0, items.Length - 1);
        item.icon = items[icon];
        int name = Random.Range(0, helmets_names.Length - 1);
        item.name = helmets_names[name];

        item.armorModifier=Random.Range(1,10) + (int)Utilities.GetLevelMultiplierArmor((int)PlayerManager.Instance.playerStats.level.GetValue()); 
        item.damageModifier=Random.Range(1,10) + (int)Utilities.GetLevelMultiplierDamage((int)PlayerManager.Instance.playerStats.level.GetValue()); 
        

        return item;
    }

    public static Equipment GenerateRandomWeapon()
    {
        Equipment item = new Equipment();

        item.equipSlot = EquipmentSlot.Weapon;
        int w_type = Random.Range(0, 3);

        if (w_type == 0)
        {
            Sprite[] items = Resources.LoadAll<Sprite>("Ui/Sprites/Items/Weapons/Axes");
            Debug.Log(items.Length);
            int icon = Random.Range(0, items.Length - 1);
            item.icon = items[icon];
            int name = Random.Range(0, axes_names.Length);
            item.name = axes_names[name];
        }
        if (w_type == 1)
        {
            Sprite[] items = Resources.LoadAll<Sprite>("Ui/Sprites/Items/Weapons/Swords");
            Debug.Log(items.Length);
            int icon = Random.Range(0, items.Length - 1);
            item.icon = items[icon];
            int name = Random.Range(0, swords_names.Length - 1);
            item.name = swords_names[name];
        }
        if (w_type == 2)
        {
            Sprite[] items = Resources.LoadAll<Sprite>("Ui/Sprites/Items/Weapons/Maces");
            Debug.Log(items.Length);
            int icon = Random.Range(0, items.Length - 1);
            item.icon = items[icon];
            int name = Random.Range(0, maces_names.Length - 1);
            item.name = maces_names[name];
        }
        if (w_type == 3)
        {
            Sprite[] items = Resources.LoadAll<Sprite>("Ui/Sprites/Items/Weapons/Knifes");
            Debug.Log(items.Length);
            int icon = Random.Range(0, items.Length - 1);
            item.icon = items[icon];
            int name = Random.Range(0, knifes_names.Length - 1);
            item.name = knifes_names[name];
        }

        item.damageModifier=Random.Range(1,12) + (int)Utilities.GetLevelMultiplierDamage((int)PlayerManager.Instance.playerStats.level.GetValue()); 

        return item;
    }

    public static Equipment GenerateRandomRing()
    {
        Equipment item = new Equipment();

        item.equipSlot = EquipmentSlot.Ring;
        Sprite[] items = Resources.LoadAll<Sprite>("Ui/Sprites/Items/Rings");
        Debug.Log(items.Length);
        int icon = Random.Range(0, items.Length - 1);
        item.icon = items[icon];
        int name = Random.Range(0, rings_names.Length - 1);
        item.name = rings_names[name];

        item.armorModifier=Random.Range(1,5) + (int)Utilities.GetLevelMultiplierArmor((int)PlayerManager.Instance.playerStats.level.GetValue()); 
        item.damageModifier=Random.Range(1,5) + (int)Utilities.GetLevelMultiplierDamage((int)PlayerManager.Instance.playerStats.level.GetValue()); 
        
        return item;
    }

    public static Equipment GenerateRandomStone()
    {
        Equipment item = new Equipment();

        item.equipSlot = EquipmentSlot.Stone;
        Sprite[] items = Resources.LoadAll<Sprite>("Ui/Sprites/Items/Stones");
        Debug.Log(items.Length);
        int icon = Random.Range(0, items.Length - 1);
        item.icon = items[icon];
        int name = Random.Range(0, stones_names.Length - 1);
        item.name = stones_names[name];

        item.armorModifier=Random.Range(1,6) + (int)Utilities.GetLevelMultiplierArmor((int)PlayerManager.Instance.playerStats.level.GetValue()); 
        item.damageModifier=Random.Range(1,4) + (int)Utilities.GetLevelMultiplierDamage((int)PlayerManager.Instance.playerStats.level.GetValue()); 

        return item;
    }

    public static Equipment GenerateRandomShield()
    {
        Equipment item = new Equipment();

        item.equipSlot = EquipmentSlot.Shield;
        Sprite[] items = Resources.LoadAll<Sprite>("Ui/Sprites/Items/Shields");
        Debug.Log(items.Length);
        int icon = Random.Range(0, items.Length - 1);
        item.icon = items[icon];
        int name = Random.Range(0, shields_names.Length - 1);
        item.name = shields_names[name];

        item.armorModifier=Random.Range(1,8) + (int)Utilities.GetLevelMultiplierArmor((int)PlayerManager.Instance.playerStats.level.GetValue()); 

        return item;
    }

    public static Equipment GenerateRandomItem()
    {
        Equipment item = new Equipment();

        int type = Random.Range(0, 6);
        switch (type)
        {
            case 0:
                {
                    item = GenerateRandomAmulet();
                    break;
                }
            case 1:
                {
                    item = GenerateRandomArmor();
                    break;
                }
            case 2:
                {
                    item = GenerateRandomHelmet();
                    break;
                }
            case 3:
                {
                    item = GenerateRandomRing();
                    break;
                }
            case 4:
                {
                    item = GenerateRandomShield();
                    break;
                }
            case 5:
                {
                    item = GenerateRandomStone();
                    break;
                }
            case 6:
                {
                    item = GenerateRandomWeapon();
                    break;
                }
        }

        return item;
    }


    public static void GenerateRandomWeaponObj()
    {
        int index = Random.Range(0, weapons.Length - 1);

        GameObject item = Instantiate(Resources.Load(string.Format("Props/Items/Prefarbs/{0}", weapons[index]))) as GameObject;
        item.transform.position = new Vector3(
                PlayerManager.Instance.Player.transform.position.x + 2f,
                PlayerManager.Instance.Player.transform.position.y,
                PlayerManager.Instance.Player.transform.position.z + 2f
            );
    }

    public static void GenerateRandomShieldObj()
    {
        int index = Random.Range(0, shields.Length - 1);

        GameObject item = Instantiate(Resources.Load(string.Format("Props/Items/Prefarbs/{0}", shields[index]))) as GameObject;
        item.transform.position = new Vector3(
                PlayerManager.Instance.Player.transform.position.x + 2f,
                PlayerManager.Instance.Player.transform.position.y,
                PlayerManager.Instance.Player.transform.position.z + 2f
            );
    }

    public static void GenerateRandomHelmetObj()
    {
        int index = Random.Range(0, helmets.Length - 1);

        GameObject item = Instantiate(Resources.Load(string.Format("Props/Items/Prefarbs/{0}", helmets[index]))) as GameObject;
        item.transform.position = new Vector3(
                PlayerManager.Instance.Player.transform.position.x + 2f,
                PlayerManager.Instance.Player.transform.position.y,
                PlayerManager.Instance.Player.transform.position.z + 2f
            );
    }
}