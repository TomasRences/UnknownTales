

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public static Dictionary<string, Button> UIButtons = new Dictionary<string, Button>();
    public static Dictionary<string, Text> UITextBindings = new Dictionary<string, Text>();


    private void Awake()
    {
        Debug.Log("UIManager awaked");
        if (instance == null)
        {
            //DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void initButtons()
    {
        UIButtons["bsDamagePlus"].onClick.AddListener(delegate () { PlayerManager.Instance.Player.GetComponent<PlayerStats>().PlusDamage(5); });
        UIButtons["bsDamageMinus"].onClick.AddListener(delegate () { PlayerManager.Instance.Player.GetComponent<PlayerStats>().MinusDamage(5); });

        UIButtons["bsDefencePlus"].onClick.AddListener(delegate () { PlayerManager.Instance.Player.GetComponent<PlayerStats>().PlusArmor(5); });
        UIButtons["bsDefenceMinus"].onClick.AddListener(delegate () { PlayerManager.Instance.Player.GetComponent<PlayerStats>().MinusArmor(5); });

        UIButtons["bsHealthPlus"].onClick.AddListener(delegate () { PlayerManager.Instance.Player.GetComponent<PlayerStats>().PlusHealth(5); });
        UIButtons["bsHealthMinus"].onClick.AddListener(delegate () { PlayerManager.Instance.Player.GetComponent<PlayerStats>().MinusHealth(5); });

        UIButtons["quit"].onClick.AddListener(delegate () { Application.Quit(); });
    }

    public void initTexts()
    {
        Debug.Log(UITextBindings.Count);
        UITextBindings["bsDamage"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().damage.GetBaseValue().ToString();
        UITextBindings["bsDefence"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().armor.GetBaseValue().ToString();
        UITextBindings["bsHealth"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().maxHealth.GetBaseValue().ToString();

        UITextBindings["eqDamage"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().damage.GetValue().ToString();
        UITextBindings["eqDefence"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().armor.GetValue().ToString();
        UITextBindings["eqHealth"].text = PlayerManager.Instance.Player.GetComponent<PlayerStats>().maxHealth.GetValue().ToString();
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
    }
}