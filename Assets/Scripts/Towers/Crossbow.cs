using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class Crossbow : Turret
{
    public Crossbow(float range, float atkSpeed, int price)
        : base(range, atkSpeed, price)
    {
        // Methode zum Runden ?
    }

    //elegante Lösung
    public override bool CanHit(float enemyposx, float enemyposy)
    {
        double angle;
        if (enemyposx - transform.position.x == 0)
        {
            angle = 90;
        }
        else
        {
            angle = Math.Atan(Math.Abs(enemyposy - transform.position.y) / Mathf.Abs(enemyposx - transform.position.x)) * Mathf.Rad2Deg;
        }

        if (enemyposx >= transform.position.x && enemyposy < transform.position.y)
        {
            angle = 360 - angle;
        }
        else if (enemyposx <= transform.position.x && enemyposy <= transform.position.y)
        {
            angle = angle + 180;
        }
        else if (enemyposx < transform.position.x && enemyposy > transform.position.y)
        {
            angle = 180 - angle;
        }

        if (Vector2.Distance(transform.position, new Vector2(enemyposx, enemyposy)) <= range)
        {
            if(rotation != 0)
            {
                if (angle >= rotation - 45 && angle <= rotation + 45)
                {
                    return true;
                }
            }
            else
            {
                if (angle >= 315 && angle < 360 || angle <= 45 && angle >= 0)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
