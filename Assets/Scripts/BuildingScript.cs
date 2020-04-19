using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public GameObject deathAnimation;
    public Weaponized weaponized;
    public Shootable shootable;
    public bool canShoot;
    public bool shotDirectionTurns;
    //public GameObject weaponToTurn; //To make turn

    private bool animHasPlayed = false; //Flag to make animation play only once
    private Animator animator;
    private BoxCollider2D goCollider;
    //private Transform goWeaponTransform; //To make turn

    // Use this for initialization
    void Start ()
    {
        animator = GetComponent<Animator>();
        goCollider = GetComponent<BoxCollider2D>();
        //goWeaponTransform = weaponToTurn.GetComponent(typeof(Transform)) as Transform; //To make turn
    }

    // Update is called once per frame
    void Update ()
    {
        if (canShoot)
        {
            Attack();
        }

        //Trigger damaged animation
        if (animator.GetBool("IsDamaged") == false && shootable.hitPoints < (shootable.maxHealth / 2))
        {
            print("Half Dead"); //For debug purposes
            animator.SetTrigger("IsDamaged");
        }

        //Trigger dead animation
        if (animator.GetBool("IsDead") == false && shootable.hitPoints <= 0)
        {
            print("Dead"); //For debug purposes
            animator.SetTrigger("IsDead");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if projectile
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (!projectile || projectile.isEnemyProjectile) return;

        TakeDamage(projectile.damage);
    }

    protected virtual void TakeDamage(int damage)
    {
        shootable.TakeDamage(damage);

        if (shootable.IsDead() && animHasPlayed == false)
        {
            animHasPlayed = true; //Set flag

            //Disable collider to allow projectiles to pass
            goCollider.enabled = false;

            // Play death animation
            GameObject animationInstance = Instantiate(deathAnimation);
            animationInstance.transform.position = transform.position;
        }
    }

    protected virtual void Attack()
    {
        weaponized.Fire(true);

        //To make turn
        /*if (shotDirectionTurns)
        {
            goWeaponTransform.Rotate(90, 0, 0);
        }*/
    }
}
