using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttributes : MonoBehaviour
{
    public int attackPower = 10;
    public string attackType = "Physical";
    public float attackSpeed = 1.0f;
    public float attackRange = 5.0f;
    public bool isAreaAttack = false;
    public int chainTargets = 1;
    public int simultaneousAttacks = 1;
    public float criticalRate = 0.1f;
    public float criticalDamage = 1.5f;
    public float accuracy = 0.9f;

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

    public int GetAttackPower() => attackPower;
    public string GetAttackType() => attackType;
    public float GetAttackSpeed() => attackSpeed;
    public float GetAttackRange() => attackRange;
    public bool GetIsAreaAttack() => isAreaAttack;
    public int GetChainTargets() => chainTargets;
    public int GetSimultaneousAttacks() => simultaneousAttacks;
    public float GetCriticalRate() => criticalRate;
    public float GetCriticalDamage() => criticalDamage;
    public float GetAccuracy() => accuracy;

    public void SetAttackPower(int attackPower) => this.attackPower = attackPower;
    public void SetAttackType(string attackType) => this.attackType = attackType;
    public void SetAttackSpeed(float attackSpeed) => this.attackSpeed = attackSpeed;
    public void SetAttackRange(float attackRange) => this.attackRange = attackRange;
    public void SetIsAreaAttack(bool isAreaAttack) => this.isAreaAttack = isAreaAttack;
    public void SetChainTargets(int chainTargets) => this.chainTargets = chainTargets;
    public void SetSimultaneousAttacks(int simultaneousAttacks) => this.simultaneousAttacks = simultaneousAttacks;
    public void SetCriticalRate(float criticalRate) => this.criticalRate = criticalRate;
    public void SetCriticalDamage(float criticalDamage) => this.criticalDamage = criticalDamage;
    public void SetAccuracy(float accuracy) => this.accuracy = accuracy;
}
