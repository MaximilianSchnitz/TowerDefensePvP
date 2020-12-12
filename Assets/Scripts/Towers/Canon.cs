using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

class Canon : Turret
{

    public int rotation;
    public Canon(float range, float atkSpeed, float dmgPerHit, bool aOE, int rotation)
        : base(range, atkSpeed, dmgPerHit, aOE)
    {
        this.range = 5;
        this.atkSpeed = 1;
        this.dmgPerHit = 15;
        this.aOE = true;
        this.rotation = rotation;
    }

    private void Start()
    {
        transform.Rotate(new Vector3(0, 0, 360 - rotation));
        transform.position = new Vector2(Mathf.Floor(transform.position.x) + 0.5f, Mathf.Floor(transform.position.y) + 0.5f);
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
                if (enemyposx >= enemyposx - 0.5 && enemyposx <= enemyposx + 0.5 && enemyposy > transform.position.y && enemyposy - transform.position.y < range)
                {
                    return true;
                }
                break;
        }
        return false;
    }

    public override bool CanHit(Vector2 enemy)
    {
        throw new NotImplementedException();
    }
}
