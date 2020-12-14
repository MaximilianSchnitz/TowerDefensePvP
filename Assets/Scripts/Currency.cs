using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Currency : MonoBehaviour
{
    [SerializeField]
    public int startCurrency;

    [System.NonSerialized]
    public int currency;

    public float more;

    Text currencyText;

    private float moneyCooldown;

    void Start()
    {
        var textUI = GameObject.FindGameObjectWithTag("Currency");
        currencyText = textUI.GetComponent<Text>();

        currency = startCurrency;

        moneyCooldown = 1 / more;
    }


    void Update()
    {
        if (currencyText.text != currency.ToString())
            currencyText.text = currency.ToString();

        if(moneyCooldown <= 0)
        {
            currency++;
            moneyCooldown = 1 / more;
        }
        else
        {
            moneyCooldown -= Time.deltaTime;
        }
     
    }

}
