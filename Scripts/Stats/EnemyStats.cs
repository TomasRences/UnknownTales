using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    bool animationExecuted = false;
    Animator animator;
    AudioSource[] audioSource;
    public Stat experiencesFrom;
    public string CharacterLabel;

    void Start()
    {
        audioSource = GetComponents<AudioSource>();
        animator = GetComponent<Animator>();

        if (PlayerManager.Instance.Player != null)
        {
            PlayerManager.Instance.Player.GetComponent<PlayerStats>().onLevelUp += OnPlayerLevelUp;

            int playersLevel = (int)PlayerManager.Instance.Player.GetComponent<PlayerStats>().level.GetValue();
            int levelMUltiplier = (int)(PlayerManager.Instance.Player.GetComponent<PlayerStats>().level.GetValue() - 1) / 2;

            this.level.SetValue(this.level.GetValue() + levelMUltiplier);

            var damageMultiplier = Utilities.GetLevelMultiplierDamage(playersLevel);
            damage.SetValue(damage.GetValue() + damageMultiplier);

            var armorMultiplier = Utilities.GetLevelMultiplierArmor(playersLevel);
            armor.SetValue(armor.GetValue() + armorMultiplier);

            var healthMultiplier = Utilities.GetHealthMultiplierArmor(playersLevel);
            maxHealth.SetValue(maxHealth.GetValue() + healthMultiplier);
            currentHealth = maxHealth.GetValue();

            Debug.Log(CharacterLabel + ": " + level.GetValue());
        }
    }

    void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (IsDied)
        {
            animator.SetBool("Died", false);
        }
    }

    public void OnPlayerLevelUp(int level)
    {

        var damageMultiplier = Utilities.GetLevelMultiplierDamage(level);
        damage.SetValue(damage.GetValue() + damageMultiplier);

        var armorMultiplier = Utilities.GetLevelMultiplierArmor(level);
        armor.SetValue(armor.GetValue() + armorMultiplier);

        bool setCurrentHealth = false;

        if (maxHealth.GetValue() == currentHealth)
        {
            setCurrentHealth = true;
        }
        var healthMultiplier = Utilities.GetHealthMultiplierArmor(level);
        maxHealth.SetValue(maxHealth.GetValue() + healthMultiplier);

        if (setCurrentHealth)
        {
            currentHealth = maxHealth.GetValue();
        }

        if (level % 2 == 0)
        {
            this.level.SetValue(this.level.GetValue() + 1);
        }
    }

    IEnumerator PlayDeath(float delay)
    {
        animator.SetBool("Died", true);
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsWalking", false);
        animator.SetInteger("Action", 0);
        animationExecuted = true;

        yield return new WaitForSeconds(delay);
    }

    IEnumerator LootCoroutine()
    {

        yield return new WaitForSeconds(1);

        GameObject obj = Instantiate(Resources.Load("Props/Items/Prefarbs/Gold")) as GameObject;
        obj.transform.position = gameObject.transform.position;

        EquipmentGenerator.GenerateRandomItemObj();

        Destroy(gameObject);
    }

    public override void Die()
    {
        // Destroy(gameObject);
        if (!animationExecuted)
        {
            base.Die();
            try
            {
                audioSource[1].Play();
            }
            catch
            {

            }
            
            StartCoroutine(PlayDeath(1));
            StartCoroutine(LootCoroutine());
            PlayerManager.Instance.playerStats.onGetExperiences.Invoke((int)experiencesFrom.GetValue());
        }

    }
}