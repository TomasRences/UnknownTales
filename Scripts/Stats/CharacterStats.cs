using UnityEngine;


public enum CharacterType
{
    Npc, Enemy
}

public class CharacterStats : MonoBehaviour
{

    public Stat maxHealth;
    public float currentHealth { get; protected set; }
    //public CharacterType characterType;

    public Stat damage;
    public Stat level;
    public Stat armor;
    public event System.Action<float, float> OnHealthChanged;

    public bool IsDied = false;

    public delegate void OnGetDamage(float currentHealt);
    public OnGetDamage onGetDamage;

    public event System.Action OnHealthReachedZero;
    public delegate void OnDie();
    public OnDie onDieCallback;

    public virtual void Awake()
    {
        currentHealth = maxHealth.GetValue();
    }


    private void Update()
    {

    }

    public virtual void Start()
    {

    }

    public void TakeDamage(float damage)
    {

        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        if (damage == 0) damage = 1;

        currentHealth -= damage;
        if (onGetDamage != null)
            onGetDamage.Invoke(currentHealth);

        //Debug.Log(transform.name + " takes " + damage + " damage.");

        if (OnHealthChanged != null)
        {
            OnHealthChanged(maxHealth.GetValue(), currentHealth);
        }

        if (currentHealth <= 0)
        {
            if (OnHealthReachedZero != null)
            {
                OnHealthReachedZero();
            }

            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth.GetValue());
        if (onGetDamage != null)
            onGetDamage.Invoke(currentHealth);
    }


    public virtual void Die()
    {
        //Debug.Log("DIE");
        IsDied = true;

        if (onDieCallback != null)
            onDieCallback.Invoke();
    }



}