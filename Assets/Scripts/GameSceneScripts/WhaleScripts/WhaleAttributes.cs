using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleAttributes : MonoBehaviour
{
    public string whaleName = "Whale";
    public int maxHp = 200;
    public int hp = 200;
    public float swimSpeed = 2.0f;
    public int defense = 5;
    public int magicDefense = 5;
    public float evasion = 0.1f;
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
    public int GetDefense() => defense;
    public int GetMagicDefense() => magicDefense;
    public float GetEvasion() => evasion;
    public int GetRewardMoney() => rewardMoney;

    public void SetWhaleName(string value) => whaleName = value;
    public void SetMaxHp(int value) => maxHp = value;
    public void SetHp(int value) => hp = value;
    public void SetSwimSpeed(float value) => swimSpeed = value;
    public void SetDefense(int value) => defense = value;
    public void SetMagicDefense(int value) => magicDefense = value;
    public void SetEvasion(float value) => evasion = value;
    public void SetRewardMoney(int value) => rewardMoney = value;
}
