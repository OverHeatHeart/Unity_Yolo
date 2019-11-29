using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Obstacle
{
    public int killScore = 20;
    public DamageType weakPoint = DamageType.Fist;

    private int hp = 10;
    public void GetDamage(DamageType type)
    {
        if (type == weakPoint)
            hp -= 10;
        else
            hp -= 3;
        if (hp < 0)
            DestroyEnemy();
    }
    private void DestroyEnemy()
    {
        TextManager.instance.GetScore(killScore);
        GameManager.instance.GetDeathTicle(transform.position);
        hp = 10;
        base.Reset();
    }
}
