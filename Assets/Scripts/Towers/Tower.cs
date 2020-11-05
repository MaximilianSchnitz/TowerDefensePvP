using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    public int price;
    [SerializeField]
    public float range;
    [SerializeField]
    public float damage;
    [SerializeField]
    public float cooldown;
    [SerializeField]
    public Vector2Int bottomLeft;
    [SerializeField]
    public Vector2Int topRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private float cooldownLeft = 0;

    // Update is called once per frame
    void Update()
    {
        Target(GetClosestCharacter());
        if (cooldownLeft > 0)
            cooldownLeft -= Time.deltaTime;
    }

    public void Target(GameObject enemy)
    {
        if (enemy == null || Vector2.Distance(transform.position, enemy.transform.position) > range)
            return;

        Attack(enemy);
    }

    public void Attack(GameObject enemy)
    {
        if(cooldownLeft <= 0)
        {
            var c = enemy.GetComponent<MonoBehaviour>() as Character;
            c.health -= damage;
            cooldownLeft = cooldown;
        }
    }

    public GameObject GetClosestCharacter()
    {
        var chars = GameObject.FindGameObjectsWithTag("Character");
        if (chars.Length == 0)
            return null;

        var closest = chars[0];
        var closestDistance = Vector2.Distance(transform.position, closest.transform.position);
        foreach(var c in chars)
        {
            float distance;
            if ((distance = Vector2.Distance(transform.position, c.transform.position)) < closestDistance)
            {
                closest = c;
                closestDistance = distance;
            }
        }
        return closest;
    }

}
