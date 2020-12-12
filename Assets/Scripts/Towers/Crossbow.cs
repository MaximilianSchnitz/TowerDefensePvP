using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class Crossbow : Turret
{
    public int rotation;
    public Crossbow(float range, float atkSpeed, float dmgPerHit, bool aOE, int rotation)
        : base(range, atkSpeed, dmgPerHit, aOE)
    {
        this.range = 10;
        this.atkSpeed = 0.5f;
        this.dmgPerHit = 5;
        this.aOE = false;
        // Methode zum Runden ?
        this.rotation = rotation;
    }

    //elegante Lösung
    public override bool CanHit(float enemyposx, float enemyposy)
    {
        double angle;
        if (enemyposx - transform.position.x == 0)
        {
            angle = 0;
        }
        else
        {
            angle = Math.Atan(Math.Abs(enemyposy - transform.position.y) / Mathf.Abs(enemyposx - transform.position.x)) * Mathf.Rad2Deg;
        }
        if (enemyposx >= transform.position.x && enemyposy < transform.position.y)
        {
            angle = angle + 270;
        }

        else if (enemyposx <= transform.position.x && enemyposy <= transform.position.y)
        {
            angle = angle + 180;
        }

        else if (enemyposx <= transform.position.x && enemyposy > transform.position.y)
        {
            angle = angle + 90;
        }

        Debug.Log(angle);


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
                if (angle >= 315 && angle <= 360 || angle <= 45 && angle >= 0)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public override bool CanHit(Vector2 enemy)
    {
        var pos = transform.position;

        //float angle = Mathf.Atan(pos.y - enemy.y / pos.x - enemy.x) * Mathf.Deg2Rad;

        var newEnemyPos = GameHandler.RotationVector(enemy, rotation * Mathf.Deg2Rad);


        float b1 = pos.y - pos.x;
        float b2 = pos.y + pos.x;

        if(newEnemyPos.x > transform.position.x)
        {
            float y1 = newEnemyPos.x + b1;
            float y2 = newEnemyPos.x + b2;

            if (newEnemyPos.y <= y1 && newEnemyPos.y >= y2)
                return true;
        }

        return false;
    }
}
