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

    Currency currency;

    // Start is called before the first frame update
    void Start()
    {
        var gameHandler = GameObject.FindGameObjectWithTag("GameController");
        currency = gameHandler.GetComponent<Currency>();
    }

    float cooldown;

    void Update()
    {
        if(cooldown <= 0)
        {
            currency.currency += 1;
            cooldown = 1 / increase;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }
    }

}
