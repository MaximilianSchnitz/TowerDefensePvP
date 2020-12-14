using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class Canon : Turret
{

    public Canon(float range, float atkSpeed, int price, int rotation)
        : base(range, atkSpeed, price)
    {
        this.rotation = rotation;
    }

    public override bool CanHit(float enemyposx, float enemyposy)
    {
        switch (rotation)
        {
            //rechts
            case 0:
                if (enemyposy >= transform.position.y - 0.5 && enemyposy <= transform.position.y + 0.5 && enemyposx > transform.position.x && enemyposx - transform.position.x < range)
                {
                    return true;
                }
                break;
            //unten
            case 90:
                if (enemyposx >= transform.position.x - 0.5 && enemyposx <= transform.position.x + 0.5 && enemyposy < transform.position.y && transform.position.y - enemyposy < range)
                {
                    return true;
                }
                break;
            //links
            case 180:
                if (enemyposy >= transform.position.y - 0.5 && enemyposy <= transform.position.y + 0.5 && enemyposx < transform.position.x && transform.position.x - enemyposx < range)
                {
                    return true;
                }
                break;
            //oben
            case 270:
                if (enemyposx >= transform.position.x - 0.5 && enemyposx <= transform.position.x + 0.5 && enemyposy > transform.position.y && enemyposy - transform.position.y < range)
                {
                    return true;
                }
                break;
        }
        return false;
    }
}
