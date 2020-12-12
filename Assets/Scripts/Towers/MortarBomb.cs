using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBomb : Projectile
{

    public float maxHeight;

    private bool reachedMaxHeight;

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
        {
            if(!reachedMaxHeight)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, maxHeight), speed * Time.deltaTime);
                if (transform.position.y >= maxHeight)
                {
                    reachedMaxHeight = true;
                    transform.position = new Vector2(enemy.transform.position.x, transform.position.y);
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
            }
        }
        else
        {
            Destroy(transform.gameObject);
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (reachedMaxHeight && collision.gameObject == enemy)
        {
            var chars = GetCharactersInRange(AOERange);

            foreach(var chr in chars)
            {
                var chrScript = chr.GetComponent<MonoBehaviour>() as Character;
                chrScript.health -= damage;
            }

            Destroy(transform.gameObject);
        }
    }

    List<GameObject> GetCharactersInRange(float range)
    {
        var list = new List<GameObject>();
        var chars = GameObject.FindGameObjectsWithTag("Character");

        foreach(var obj in chars)
            if (Vector2.Distance(transform.position, obj.transform.position) <= range)
                list.Add(obj);

        return list;
    }

}
