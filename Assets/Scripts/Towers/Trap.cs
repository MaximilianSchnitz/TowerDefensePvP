using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Trap : Turret
{

    public Trap(float range, float atkSpeed, int price) 
        : base(range, atkSpeed, price)
    {

    }

    public override bool CanHit(float enemyposx, float enemyposy)
    {
        if(Mathf.Floor(enemyposx) == Mathf.Floor(transform.position.x + 0.5f) && Mathf.Floor(enemyposy) == Mathf.Floor(transform.position.y + 0.5f))
            return true;
        return false;
    }
}
