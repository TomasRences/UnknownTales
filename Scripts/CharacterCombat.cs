using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    private float attackDelay = 1f;

    CharacterStats objStats;

    public event System.Action OnAttack;

    void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    void Start()
    {
        objStats = GetComponent<CharacterStats>();
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
            {
                OnAttack();
            }

            attackCooldown = 1f / attackSpeed;
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        var critical = Utilities.GetCriticalHitMultiplier((int)objStats.level.GetValue(), 26f);
        stats.TakeDamage(objStats.damage.GetValue() + critical);

        try
        {
            GetComponents<AudioSource>()[2].Play();
        }
        catch { }
    }
}
