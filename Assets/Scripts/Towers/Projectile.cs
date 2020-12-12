using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public float damage;
    public float speed;
    public float AOERange;
    public GameObject enemy;

    // Update is called once per frame
    void Update()
    {
        if (enemy != null)
            transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ALARM");
        if (collision.gameObject == enemy)
        {
            var chr = enemy.GetComponent<MonoBehaviour>() as Character;
            chr.health -= damage;
            Destroy(transform.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ALARM");
        if (collision.gameObject == enemy)
        {
            var chr = enemy.GetComponent<MonoBehaviour>() as Character;
            chr.health -= damage;
            Destroy(transform.gameObject);
            Destroy(this);
        }
    }

}
