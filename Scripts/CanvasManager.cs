using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CanvasManager : MonoBehaviour
{
    private static CanvasManager instance;
    public static CanvasManager Instance
    {
        get
        {
            return instance;
        }
    }

    public static Dictionary<string, Button> UIButtons = new Dictionary<string, Button>();
    public static Dictionary<string, Text> UITextBindings = new Dictionary<string, Text>();



    public void Awake()
    {

        Debug.Log("CanvasManager awaked");
        DontDestroyOnLoad(canvas);
        if (instance == null)
        {
            if (canvas == null)
            {
                Debug.Log("Canvas created");
                canvas = Instantiate(Resources.Load("Ui/Prefarb/Canvas")) as GameObject;


                DontDestroyOnLoad(canvas);
            }
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(canvas);
        }

        Inventory.Instance.onItemUsedCallback += onItemUsedCallback;
        PlayerManager.Instance.Player.GetComponent<PlayerStats>().onStatChangedCallback += onStatChangedCallback;

    }

    void Start()
    {
        Debug.Log("CanvasManager Started");
        initButtons();
        initTexts();
        infoPanel.SetActive(false);
    }

    public void initButtons()
    {
        UIButtons["bsDamagePlus"].onClick.RemoveAllListeners();
        UIButtons["bsDamagePlus"].onClick.AddListener(delegate () { PlayerManager.Instance.Player.GetComponent<PlayerStats>().PlusDamage(5); });

        UIButtons["bsDamageMinus"].onClick.RemoveAllListeners();
        UIButtons["bsDamageMinus"].onClick.AddListener(delegate () { PlayerManager.Instance.Player.GetComponent<PlayerStats>().MinusDamage(5); });

        UIButtons["bsDefencePlus"].onClick.RemoveAllListeners();
        UIButtons["bsDefencePlus"].onClick.AddListener(delegate () { PlayerManager.Instance.Player.GetComponent<PlayerStats>().PlusArmor(5); });

        UIButtons["bsDefenceMinus"].onClick.RemoveAllListeners();
        UIButtons["bsDefenceMinus"].onClick.AddListener(delegate () { PlayerManager.Instance.Player.GetComponent<PlayerStats>().MinusArmor(5); });

        UIButtons["bsHealthPlus"].onClick.RemoveAllListeners();
        UIButtons["bsHealthPlus"].onClick.AddListener(delegate ()
        {
            var stats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();
            stats.PlusHealth(5);
            stats.onGetDamage.Invoke(stats.currentHealth);
			
        });

        UIButtons["bsHealthMinus"].onClick.RemoveAllListeners();
        UIButtons["bsHealthMinus"].onClick.AddListener(delegate ()
        {
            var stats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();
            PlayerManager.Instance.Player.GetComponent<PlayerStats>().MinusHealth(5);
            stats.onGetDamage.Invoke(stats.currentHealth);
        });

        // UIButtons["quit"].onClick.RemoveAllListeners();
        // UIButtons["quit"].onClick.AddListener(delegate () { 
        //     Save save=new Save();
        //     save.SaveGame();

        //     Application.Quit(); 
        // });
    }

    public void initTexts()
    {
        UITextBindings["bsDamage"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().damage.GetBaseValue().ToString();
        UITextBindings["bsDefence"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().armor.GetBaseValue().ToString();
        UITextBindings["bsHealth"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().maxHealth.GetBaseValue().ToString();

        UITextBindings["eqDamage"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().damage.GetValue().ToString();
        UITextBindings["eqDefence"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().armor.GetValue().ToString();
        UITextBindings["eqHealth"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().maxHealth.GetValue().ToString();
        UITextBindings["skillPoints"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().skillPoints.ToString();
    }

    public void onItemUsedCallback()
    {
        UITextBindings["eqDamage"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().damage.GetValue().ToString();
        UITextBindings["eqDefence"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().armor.GetValue().ToString();
        UITextBindings["eqHealth"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().maxHealth.GetValue().ToString();
    }

    public void onStatChangedCallback()
    {
        UITextBindings["bsDamage"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().damage.GetBaseValue().ToString();
        UITextBindings["bsDefence"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().armor.GetBaseValue().ToString();
        UITextBindings["bsHealth"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().maxHealth.GetBaseValue().ToString();

        UITextBindings["eqDamage"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().damage.GetValue().ToString();
        UITextBindings["eqDefence"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().armor.GetValue().ToString();
        UITextBindings["eqHealth"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().maxHealth.GetValue().ToString();

        UITextBindings["skillPoints"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().skillPoints.ToString();

        //StatsCopy.armor = PlayerManager.Instance.Player.GetComponent<PlayerStats>().armor.GetBaseValue();
        //StatsCopy.damage = PlayerManager.Instance.Player.GetComponent<PlayerStats>().damage.GetBaseValue();
        //StatsCopy.maxHealth = PlayerManager.Instance.Player.GetComponent<PlayerStats>().maxHealth.GetBaseValue();
    }

    public bool UiOpened = false;
    public GameObject canvas;
    public GameObject infoPanel;
    public GameObject headSlot;
    public GameObject weaponSlot;
    public GameObject armorSlot;
    public GameObject shieldSlot;
    public GameObject ringSlot;
    public GameObject amuletSlot;
    public GameObject stoneSlot;
    public GameObject Gold;
    public GameObject enemyHealthBar;
    public Image HpBar;
    public Image ExpBar;
    public Text LevelBar;
    public Text ExperiencesBar;

}
