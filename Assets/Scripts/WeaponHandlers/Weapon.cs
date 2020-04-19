using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Projectile projectile;
    public int numProjectiles = 1;
    public float cooldown = 0.5f;
    public bool isActive = true;
    public bool weaponIsHoming = false;

    private Vector3 worldUp;

    private float cooldownSoFar;
    private bool isReady;
    private Animator anm; //To use attached animator-controller *H*
    private int goChildrenAmnt; //To store amnt of GameObject Children *H*
    private Transform closestEnemyTransform;
    private Transform weaponTransform;

	// Use this for initialization
	void Start ()
    {
        worldUp.Set(0, -1, 0);

        goChildrenAmnt = transform.childCount; //Get amnt of Game Object Children *H*
        weaponTransform = GetComponent<Transform>();

        cooldownSoFar = 0.0f;
        isReady = true;

        anm = gameObject.GetComponent<Animator>(); //Assign Animator-Controller *H*
        //Adjusted to take into account lack of animator component *H*
        if (anm != null)
        {
            anm.SetTrigger("IdleTrigger"); //Set animation to Idle *H*
        }
    }

	// Update is called once per frame
	void Update ()
    {
        if (!isReady)
        {
            //To make weapon turn towards closest enemy if weapon is homing. *H*
            if (weaponIsHoming)
            {
                GameObject closestEnemy = FindClosestEnemy();

                if (closestEnemy != null)
                {
                    //Calculates angle towards enemy.
                    Transform closestEnemyTransform = closestEnemy.GetComponent<Transform>();
                    Vector3 direction = closestEnemyTransform.position - weaponTransform.position;
                    float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) -90;
                    weaponTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); //Turns weapon towards enemy.
                }
                else
                {
                    weaponTransform.rotation = Quaternion.LookRotation(Vector3.forward);
                }
            }

            if (cooldownSoFar >= cooldown)
            {
                isReady = true;
                cooldownSoFar = 0.0f;
            }
            else
            {
                cooldownSoFar += Time.deltaTime;
            }
        }
	}

    //Changed method to spawn projectile at child transforms so 
    //spawnpoints can be assigned in weapon GameObject *H*
    public bool FireWeapon(bool isEnemyFiring)
    {
        if (isReady)
        {
            //If any, then play animation
            if (anm != null)
            {
                anm.SetTrigger("FiringTrigger"); //To trigger the firing animation *H*
            }

            //Instantiate projectile
            for (int i = 0; i < goChildrenAmnt; ++i)
            {
                    Projectile projectileInstance = Instantiate(projectile);
                    projectileInstance.FireProjectile(transform.GetChild(i).position, transform.up, isEnemyFiring);
                    //projectileInstance.FireProjectile(transform.position, Vector3.up); //*F*
            }

            isReady = false;
        }
        return false;
    }

    //Finding the closest game object with tag "Enemy", and shoot at it *H*
    public GameObject FindClosestEnemy()
    {
        GameObject[] gameObjects;
        gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gameObjects)
        {
            Vector3 diff = go.transform.position - position;
            float currentDistance = diff.sqrMagnitude;
            if (currentDistance < distance)
            {
                closest = go;
                distance = currentDistance;
            }
        }
        return closest;
    }
}
