using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public abstract class EnemyP : MonoBehaviour {
    public SpriteRenderer sprRend;
    public Collider2D col;
    public Rigidbody2D rb2D;
    public GameObject deathAnimation;

    public Weaponized weaponized;
    public Shootable shootable;

    public Vector2 velocity = new Vector2(0.0f, 0.0f);

    private Boundary boundary;

    void Start()
    {
        boundary = FindObjectOfType<Boundary>();
    }

	// Update is called once per frame
	void Update()
    {
        Move();
        Attack();

        if (boundary.OutsideAndBelowBoundary(transform.position))
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if projectile
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (!projectile || projectile.isEnemyProjectile) return;

        TakeDamage(projectile.damage);
    }

    protected virtual void Move()
    {
        rb2D.velocity = velocity;
    }

    protected virtual void Attack()
    {
        weaponized.Fire(true);
    }

    protected virtual void TakeDamage(int damage)
    {
        shootable.TakeDamage(damage);

        if (shootable.IsDead())
        {
            // Play death animation
            GameObject animationInstance = Instantiate(deathAnimation);
            animationInstance.transform.position = transform.position;
            Destroy(this.gameObject);
        }
    }
}
