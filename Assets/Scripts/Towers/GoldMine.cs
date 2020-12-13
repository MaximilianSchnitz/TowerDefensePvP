using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class GoldMine : Turret
{

    public GoldMine(float range, float atkSpeed, int price, int increase)
        :base(range, atkSpeed, price)
    {
        this.increase = increase;
    }

    public float increase;

    public override bool CanHit(float enemyposx, float enemyposy)
    {
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        var gameHandler = GameObject.FindGameObjectWithTag("GameController");
        var currency = gameHandler.GetComponent<Currency>();
        currency.more += increase;
    }
}
