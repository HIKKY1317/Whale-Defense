using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttributes : MonoBehaviour
{
    public int hp = 100;
    public float attackSpeed = 1.0f;
    public int attackPower = 10;
    public float attackRange = 5.0f;

    public int GetHp()
    {
        return hp;
    }

    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public int GetAttackPower()
    {
        return attackPower;
    }

    public float GetAttackRange()
    {
        return attackRange; 
    }

    public void SetHp(int hp)
    {
        this.hp = hp;
    }

    public void SetAttackSpeed(float attackSpeed)
    {
        this.attackSpeed = attackSpeed;
    }

    public void SetAttackPower(int  attackPower)
    {
        this.attackPower = attackPower;
    }

    public void SetAttackRange(float attackRange)
    {
        this.attackRange = attackRange;
    }
}
