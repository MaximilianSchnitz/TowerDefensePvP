using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Trap : Turret
{

    public Trap(float range, float atkSpeed) 
        : base(range, atkSpeed)
    {

    }

    public override bool CanHit(float enemyposx, float enemyposy)
    {
        if(Mathf.Floor(enemyposx) == Mathf.Floor(transform.position.x) && Mathf.Floor(enemyposy) == Mathf.Floor(transform.position.y))
            return true;
        return false;
    }
}
