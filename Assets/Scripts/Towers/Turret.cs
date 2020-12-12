using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

abstract class Turret : MonoBehaviour
{
    public Animator animator;

    public float range;
    public float atkSpeed;
    public float dmgPerHit;
    public bool aOE;

    public GameObject projectile;

    public Turret(float range, float atkSpeed, float dmgPerHit, bool aOE)
    {
        this.range = range;
        this.atkSpeed = atkSpeed;
        this.dmgPerHit = dmgPerHit;
        this.aOE = aOE;
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

    public abstract bool CanHit(float enemyposx, float enemyposy);
    public abstract bool CanHit(Vector2 enemy);

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
        var proj = Instantiate(projectile, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
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
