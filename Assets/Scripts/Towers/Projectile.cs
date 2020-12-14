using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float damage;
    public float speed;
    public float AOERange;

    [System.NonSerialized]
    public GameObject enemy;

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
            transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
        else
        {
            Destroy(transform.gameObject);
            Destroy(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == enemy)
        {
            var chr = enemy.GetComponent<MonoBehaviour>() as Character;
            chr.health -= damage;
            Destroy(transform.gameObject);
        }
    }

}
