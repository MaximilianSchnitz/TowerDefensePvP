using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class Mortar : Turret
{
    public Mortar(float range, float atkSpeed, float dmgPerHit, bool aOE)
       : base(range, atkSpeed, dmgPerHit, aOE)
    {
        this.range = 15;
        this.atkSpeed = 1;
        this.dmgPerHit = 5;
        this.aOE = true;

    }

    public override bool CanHit(float enemyposx, float enemyposy)
    {
        if (Vector2.Distance(transform.position, new Vector2(enemyposx, enemyposy)) <= range)
        {
            return true;
        }
        return false;
    }

    public override bool CanHit(Vector2 enemy)
    {
        throw new NotImplementedException();
    }
}
