using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarBomb : Projectile
{

    public float maxHeight;
    private bool reachedMaxHeight;

    Vector2 lastPos;

    // Update is called once per frame
    void Update()
    {
        if (!reachedMaxHeight)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, maxHeight), speed * Time.deltaTime);
            if (transform.position.y >= maxHeight)
            {
                reachedMaxHeight = true;
            }
            if(enemy != null)
                lastPos = enemy.transform.position;
        }
        else
        {
            if(enemy != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
                lastPos = enemy.transform.position;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, lastPos, speed * Time.deltaTime);
                if ((Vector2) transform.position == lastPos)
                    OnTriggerEnter2D(null);
            }
        }
        //else if (enemy != null && reachedMaxHeight)
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
        //    lastPos = enemy.transform.position;
        //}
        //else
        //{
        //    transform.position = Vector2.MoveTowards(transform.position, lastPos, speed * Time.deltaTime);
        //    if ((Vector2) transform.position == lastPos)
        //        OnTriggerEnter2D(null);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision is null || collision.gameObject.tag == "Character")
        {
            if (reachedMaxHeight)
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
