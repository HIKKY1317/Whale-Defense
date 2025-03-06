using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAttributes : MonoBehaviour
{
    public string whaleName = "Whale";
    public int maxHp = 200;
    public int hp = 200;
    public float swimSpeed = 2.0f;
    public int attackPower = 5;
    public string attackType = "Physical";
    public int defense = 5;
    public int magicDefense = 5;
    public float attackSpeed = 1.0f;
    public float attackRange = 5.0f;
    public bool isAreaAttack = false;
    public int chainTargets = 1;
    public int simultaneousAttacks = 1;
    public float criticalRate = 0.1f;
    public float criticalDamage = 1.5f;
    public float accuracy = 0.9f;
    public float evasion = 0.1f;
    public string turretElement = "Fire";
    public int rewardMoney = 50;

    private Dictionary<string, int> activeEffects = new Dictionary<string, int>();

    public void ApplyEffect(string effectName, int level)
    {
        if (activeEffects.ContainsKey(effectName))
        {
            activeEffects[effectName] = Mathf.Max(activeEffects[effectName], level);
        }
        else
        {
            activeEffects.Add(effectName, level);
        }
    }

    public void RemoveEffect(string effectName)
    {
        if (activeEffects.ContainsKey(effectName))
        {
            activeEffects.Remove(effectName);
        }
    }

    public int GetEffectLevel(string effectName)
    {
        return activeEffects.ContainsKey(effectName) ? activeEffects[effectName] : 0;
    }

    public string GetWhaleName() => whaleName;
    public int GetMaxHp() => maxHp;
    public int GetHp() => hp;
    public float GetSwimSpeed() => swimSpeed;
    public int GetAttackPower() => attackPower;
    public string GetAttackType() => attackType;
    public int GetDefense() => defense;
    public int GetMagicDefense() => magicDefense;
    public float GetAttackSpeed() => attackSpeed;
    public float GetAttackRange() => attackRange;
    public bool GetIsAreaAttack() => isAreaAttack;
    public int GetChainTargets() => chainTargets;
    public int GetSimultaneousAttacks() => simultaneousAttacks;
    public float GetCriticalRate() => criticalRate;
    public float GetCriticalDamage() => criticalDamage;
    public float GetAccuracy() => accuracy;
    public float GetEvasion() => evasion;
    public string GetTurretElement() => turretElement;
    public int GetRewardMoney() => rewardMoney;

    public void SetWhaleName(string value) => whaleName = value;
    public void SetMaxHp(int value) => maxHp = value;
    public void SetHp(int value) => hp = value;
    public void SetSwimSpeed(float value) => swimSpeed = value;
    public void SetAttackPower(int value) => attackPower = value;
    public void SetAttackType(string value) => attackType = value;
    public void SetDefense(int value) => defense = value;
    public void SetMagicDefense(int value) => magicDefense = value;
    public void SetAttackSpeed(float value) => attackSpeed = value;
    public void SetAttackRange(float value) => attackRange = value;
    public void SetIsAreaAttack(bool value) => isAreaAttack = value;
    public void SetChainTargets(int value) => chainTargets = value;
    public void SetSimultaneousAttacks(int value) => simultaneousAttacks = value;
    public void SetCriticalRate(float value) => criticalRate = value;
    public void SetCriticalDamage(float value) => criticalDamage = value;
    public void SetAccuracy(float value) => accuracy = value;
    public void SetEvasion(float value) => evasion = value;
    public void SetTurretElement(string value) => turretElement = value;
    public void SetRewardMoney(int value) => rewardMoney = value;
}
