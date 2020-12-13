using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class Mortar : Turret
{
    public Mortar(float range, float atkSpeed, int price)
       : base(range, atkSpeed, price)
    {

    }

    public override bool CanHit(float enemyposx, float enemyposy)
    {
        if (Vector2.Distance(transform.position, new Vector2(enemyposx, enemyposy)) <= range)
        {
            return true;
        }
        return false;
    }

}
