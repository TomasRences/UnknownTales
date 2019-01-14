

using UnityEngine;

public static class Utilities
{
    public static float GetLevelMultiplierDamage(int level)
    {
        return Mathf.Log10(level) * 1.56f;
    }

    public static float GetLevelMultiplierArmor(int level)
    {
        return Mathf.Log10(level) * 6.88f;
    }

    public static float GetHealthMultiplierArmor(int level)
    {
        return Mathf.Log10(level) * 12;
    }

    public static int ComputePrice(Equipment item){
        return (item.armorModifier*52)+(item.damageModifier*53)+Random.Range(3,9);
    }

    public static float GetCriticalHitMultiplier(int level, float chance)
    {
        var rand = Random.Range(1, 100);
        float criticalHit = 0f;
        if (rand >= chance)
        {
            criticalHit = 3.56f;
        }
        return Mathf.Log10(level)*5*criticalHit;
    }

    public static int GetLevelMultiplierExps(int level, int exps)
    {   
        if(level==1){
            return exps;
        }
        
        return (int)((Mathf.Log10(level)*5) + 1.28f * exps);
    }
}