using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public Stat experiences;

    public Stat nextLevel;

    public Stat maxNumOfPotions;

    public int skillPoints = 0;

    public int numOfPotions;


    public delegate void OnGetExperiences(int addedExps);
    public OnGetExperiences onGetExperiences;

    public delegate void OnLevelUp(int level);
    public OnLevelUp onLevelUp;

    public delegate void OnStatChanged();
    public OnStatChanged onStatChangedCallback;
    public delegate void OnPotionAdded(int numOfPotions);
    public OnPotionAdded onPotionAdded;


    private void Update()
    {


        if (Input.GetKeyDown(KeyCode.E))
        {
            onGetExperiences.Invoke(244);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LevelUp((int)level.GetValue() + 1);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Inventory.Instance.onGoldChangedCallback.Invoke(200);
        }

    }

    public void SetHealthMax(){
        this.currentHealth=this.maxHealth.GetValue();
    }

    void Awake()
    {
        base.Awake();
        StartCoroutine(PotionsRoutine(15));
        StartCoroutine(HealRoutine(4));
    }

    void Start()
    {
        EquipmentManager.Instance.onEquipmentChanged += OnEquipmentChanged;
        onGetDamage += OnGetDamageCallback;
        onGetExperiences += OnGetExperiencesCallback;

        nextLevel.SetValue(ComputeNexLevel((int)level.GetValue()));


        onGetExperiences.Invoke(0);

        numOfPotions = (int)maxNumOfPotions.GetValue();

        onPotionAdded += OnPotionAddedCallback;
    }
    IEnumerator HealRoutine(float delay)
    {
        while (true)
        {
            //Debug.Log("numOfPotions: " + numOfPotions.ToString());
            if (currentHealth != maxHealth.GetValue())
            {
                currentHealth += 1;
                if (onGetDamage != null)
                    onGetDamage.Invoke(currentHealth);
            }
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator PotionsRoutine(float delay)
    {
        while (true)
        {
            //Debug.Log("numOfPotions: " + numOfPotions.ToString());
            if (numOfPotions != maxNumOfPotions.GetValue())
            {
                numOfPotions += 1;
                //Debug.Log("Potion added");
                if (onPotionAdded != null)
                {
                    onPotionAdded.Invoke(numOfPotions);
                }
            }
            yield return new WaitForSeconds(delay);
        }

    }

    void OnPotionAddedCallback(int numOfPotions)
    {
        CanvasManager.UITextBindings["potionsNumber"].text = numOfPotions.ToString();
    }

    public void SetLevel(int newLevel)
    {
        level.SetValue(newLevel);
        CanvasManager.UITextBindings["level"].text = newLevel.ToString();
        CanvasManager.UITextBindings["avatarLevel"].text = newLevel.ToString();
        nextLevel.SetValue(ComputeNexLevel((int)level.GetValue()));

        var stats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

        StatsCopy.armor = stats.armor.GetBaseValue();
        StatsCopy.damage = stats.damage.GetBaseValue();
        StatsCopy.maxHealth = stats.maxHealth.GetBaseValue();
        StatsCopy.skillPoints = stats.skillPoints;
    }

    public void SetExperiences(int exps)
    {

        experiences.SetValue(exps);

        var currentExps = experiences.GetValue();
        var diff = nextLevel.GetValue() - currentExps;

        CanvasManager cm = CanvasManager.Instance;
        cm.ExpBar.fillAmount = ((currentExps * 100) / nextLevel.GetValue()) / 100.0f;
        cm.ExperiencesBar.text = string.Format("{0}/{1}", experiences.GetValue(), nextLevel.GetValue());
    }

    public void LevelUp(int newLevel)
    {
        level.SetValue(newLevel);
        CanvasManager.UITextBindings["level"].text = newLevel.ToString();
        CanvasManager.UITextBindings["avatarLevel"].text = newLevel.ToString();
        nextLevel.SetValue(ComputeNexLevel((int)level.GetValue()));
        skillPoints += 15;
        var stats = PlayerManager.Instance.Player.GetComponent<PlayerStats>();

        CanvasManager.UITextBindings["skillPoints"].text = stats.skillPoints.ToString();

        if (onLevelUp != null)
            onLevelUp.Invoke(newLevel);

        StatsCopy.armor = stats.armor.GetBaseValue();
        StatsCopy.damage = stats.damage.GetBaseValue();
        StatsCopy.maxHealth = stats.maxHealth.GetBaseValue();
        StatsCopy.skillPoints = stats.skillPoints;

        //Debug.Log(StatsCopy.toString());
    }

    public int ComputeNexLevel(int level)
    {
        return Mathf.FloorToInt(level * (4.0f / 3.0f) * 575);
    }

    public void OnGetDamageCallback(float currentHp)
    {
        //Debug.Log("player get damage");
        CanvasManager.Instance.HpBar.fillAmount = currentHp / maxHealth.GetValue();
    }

    void OnGetExperiencesCallback(int exps)
    {
        var expGained = Utilities.GetLevelMultiplierExps((int)level.GetValue(), exps);
        Debug.Log("expGained: " + expGained);
        experiences.SetValue(experiences.GetValue() + expGained);

        if (experiences.GetValue() >= nextLevel.GetValue())
        {
            LevelUp((int)level.GetValue() + 1);
        }

        var currentExps = experiences.GetValue(); // 522
        //nextLevel.GetValue();   //1333
        var diff = nextLevel.GetValue() - currentExps; //811

        //Debug.Log(((currentExps * 100) / nextLevel.GetValue()) / 100.0f);
        CanvasManager cm = CanvasManager.Instance;
        cm.ExpBar.fillAmount = ((currentExps * 100) / nextLevel.GetValue()) / 100.0f;
        cm.ExperiencesBar.text = string.Format("{0}/{1}", experiences.GetValue(), nextLevel.GetValue());
        //CanvasManager.UITextBindings["experiencesBar"].text = string.Format("{0}/{1}", experiences.GetValue(), nextLevel.GetValue());

    }

    public override void Die()
    {
        base.Die();
        try
        {
            GetComponents<AudioSource>()[1].Play();
        }
        catch
        {

        }
        
        //Destroy(gameObject);

        GameManager.instance.GameOver();
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {

        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armorModifier);
            damage.RemoveModifier(oldItem.damageModifier);
        }
    }

    public void PlusArmor(int step)
    {
        if (!(skillPoints >= 5)) return;
        skillPoints -= 5;
        armor.Plus(step);
        if (onStatChangedCallback != null)
            onStatChangedCallback.Invoke();
    }

    public void MinusArmor(int step)
    {
        Debug.Log("MinusArmor: " + (armor.baseValue - step));
        Debug.Log("StatsCopy.armor: " + StatsCopy.armor);

        if ((armor.baseValue - step) < StatsCopy.armor) return;

        armor.Minus(step);

        skillPoints += 5;
        if (onStatChangedCallback != null)
            onStatChangedCallback.Invoke();
    }

    public void PlusDamage(int step)
    {
        if (!(skillPoints >= 5)) return;
        skillPoints -= 5;
        damage.Plus(step);
        if (onStatChangedCallback != null)
            onStatChangedCallback.Invoke();
    }

    public void MinusDamage(int step)
    {
        Debug.Log("MinusDamage: " + (damage.baseValue - step));
        Debug.Log("StatsCopy.damage: " + StatsCopy.damage);

        if ((damage.baseValue - step) < StatsCopy.damage) return;
        damage.Minus(step);
        skillPoints += 5;
        if (onStatChangedCallback != null)
            onStatChangedCallback.Invoke();
    }

    public void PlusHealth(int step)
    {
        if (!(skillPoints >= 5)) return;
        skillPoints -= 5;
        maxHealth.Plus(step);

        onGetDamage.Invoke(currentHealth);

        if (onStatChangedCallback != null)
            onStatChangedCallback.Invoke();
    }

    public void MinusHealth(int step)
    {
        if ((maxHealth.baseValue - step) < StatsCopy.maxHealth) return;
        maxHealth.Minus(step);
        skillPoints += 5;

        onGetDamage.Invoke(currentHealth);

        if (onStatChangedCallback != null)
            onStatChangedCallback.Invoke();
    }
}
