using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

abstract class Turret : MonoBehaviour
{
    public Animator animator;

    public int price;
    public float range;
    public float atkSpeed;
    public bool aOE;
    public int rotation;
    public bool rotatable;

    [NonSerialized]
    public bool isPlaced = false;

    public GameObject projectile;

    public Turret(float range, float atkSpeed, int price)
    {
        this.range = range;
        this.atkSpeed = atkSpeed;
        this.price = price;
    }

    public Vector2 BottomLeft
    {
        get
        {
            float x = transform.position.x - transform.localScale.x / 2;
            float y = transform.position.y - transform.localScale.y / 2;

            return new Vector2(x, y);
        }
    }

    public Vector2 TopRight
    {
        get
        {
            float x = transform.position.x + transform.localScale.x / 2;
            float y = transform.position.y + transform.localScale.y / 2;

            return new Vector2(x, y);
        }
    }

    void Start()
    {
        transform.position = new Vector2(Mathf.Floor(transform.position.x) + 0.5f, Mathf.Floor(transform.position.y) + 0.5f);

        if (rotation == 360)
            rotation = 0;
    }

    public abstract bool CanHit(float enemyposx, float enemyposy);

    private float cooldownLeft = 0;

    // Update is called once per frame
    void Update()
    {
        if (!isPlaced)
            return;

        Target(GetClosestCharacter());
        if (cooldownLeft > 0)
            cooldownLeft -= Time.deltaTime;
    }

    public void Target(GameObject enemy)
    {
        if (enemy == null || !CanHit(enemy.transform.position.x, enemy.transform.position.y))
        {
            animator.SetBool("Firing", false);
            return;
        }

        Attack(enemy);
    }

    public void Attack(GameObject enemy)
    {
        if (cooldownLeft <= 0)
        {
            cooldownLeft = 1 / atkSpeed;
            animator.SetBool("Firing", true);
            CreateProjectile(enemy);
        }
    }

    public void CreateProjectile(GameObject enemy)
    {
        var proj = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, 1), transform.rotation);
        var projScript = proj.GetComponent<MonoBehaviour>() as Projectile;
        projScript.enemy = enemy;
    }

    public GameObject GetClosestCharacter()
    {
        var chars = GameObject.FindGameObjectsWithTag("Character");
        if (chars.Length == 0)
            return null;

        var closest = chars[0];
        var closestDistance = Vector2.Distance(transform.position, closest.transform.position);
        foreach (var c in chars)
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
