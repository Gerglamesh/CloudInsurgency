using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float lifeTime = 1.0f;
    public float speed = 1.0f;
    public int damage = 1;
    public bool isEnemyProjectile;
    public bool destroyedOnCollision = true; //Not always the case, ex. with gas cloud
    public GameObject deathAnimation;

    private Vector3 velocity;
    private float timeSoFar;
    private Rigidbody2D rb2D;
    private Animator deathAnimator;

	// Use this for initialization
	void Start () {
        timeSoFar = 0.0f;
        rb2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        timeSoFar += Time.deltaTime;
        if (timeSoFar >= lifeTime)
        {
            GameObject animationInstance = Instantiate(deathAnimation);
            animationInstance.transform.position = transform.position;
            Destroy(this.gameObject);
        }
	}

    //Destroy projectile on hit
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyedOnCollision)
        {
            if (collision.gameObject.CompareTag("Enemy") && !isEnemyProjectile)
            {
                GameObject animationInstance = Instantiate(deathAnimation);
                animationInstance.transform.position = transform.position;
                Destroy(this.gameObject);
            }

            if (collision.gameObject.CompareTag("Building") && !isEnemyProjectile)
            {
                GameObject animationInstance = Instantiate(deathAnimation);
                animationInstance.transform.position = transform.position;
                Destroy(this.gameObject);
            }

            if (collision.gameObject.CompareTag("Player") && isEnemyProjectile)
            {
                GameObject animationInstance = Instantiate(deathAnimation);
                animationInstance.transform.position = transform.position;
                Destroy(this.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        if(rb2D != null) rb2D.velocity = velocity;
    }

    public void FireProjectile(Vector3 position, Vector3 direction, bool isEnemyFiring)
    {
        isEnemyProjectile = isEnemyFiring;
        transform.position = position;
        velocity = direction * speed;
    }
}
