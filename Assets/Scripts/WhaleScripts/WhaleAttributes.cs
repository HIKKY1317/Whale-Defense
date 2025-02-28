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
    public float attackRange = 1.0f;
    public float attackSpeed = 5.0f;
    public int rewardMoney = 50;

    public string GetWhaleName()
    {
        return whaleName;
    }

    public int GetMaxHp()
    {
        return maxHp;
    }

    public int GetHp()
    {
        return hp;
    }

    public float GetSwimSpeed()
    {
        return swimSpeed;
    }

    public int GetAttackPower()
    {
        return attackPower;
    }

    public float GetAttackRange()
    {
        return attackRange;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public int GetRewardMoney()
    {
        return rewardMoney;
    }

    public void SetWhaleName(string whaleName)
    {
        this.whaleName = whaleName;
    }

    public void SetMaxHp(int maxHp)
    {
        this.maxHp = maxHp;
    }

    public void SetHp(int hp)
    {
        this.hp = hp;
    }

    public void SetSwimSpeed(float swimSpeed)
    {
        this.swimSpeed = swimSpeed;
    }

    public void SetAttackPower(int attackPower)
    {
        this.attackPower = attackPower;
    }

    public void SetAttackRange(float attackRange)
    {
        this.attackRange = attackRange;
    }

    public void SetAttackSpeed(float attackSpeed)
    {
        this.attackSpeed = attackSpeed;
    }

    public void SetRewardMoney(int rewardMoney)
    {
        this.rewardMoney = rewardMoney;
    }
}
