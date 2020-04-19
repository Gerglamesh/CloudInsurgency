using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

    public float moveSpeed = 5.0f; // TODO: Should this be here?
    public Boundary boundary; // TODO: This should be accessed differently
    public bool activateBoundary; // To bypass when testing other stuff
    public Weaponized weaponized;
    public Shootable shootable;

    private BoxCollider2D boxCollider;
    private float filterThreshold = 0.01f;
    private Rigidbody2D rb2D; // TODO: Never Used?? *H*

	// Use this for initialization
	void Start () {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        // Get input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement vector
        Vector3 moveVector = new Vector3(horizontalInput, verticalInput, 0.0f);
        
        // Clamp moveVector between [0, 1], this avoids diagonal "speed boost"
        if (moveVector.magnitude > 1.0f)
        {
            moveVector.Normalize();
        }

        // Apply movement speed
        moveVector *= moveSpeed * Time.deltaTime;

        // Check where new position would be
        Rect updatedCollider = new Rect(boxCollider.bounds.min + moveVector, boxCollider.bounds.max - boxCollider.bounds.min);

        // Stay inside bounds
        if (activateBoundary)
        {
            if (updatedCollider.xMin <= boundary.GetXMin()) moveVector += new Vector3(boundary.GetXMin() - boxCollider.bounds.min.x, 0.0f, 0.0f); // Left
            if (updatedCollider.xMax >= boundary.GetXMax()) moveVector += new Vector3(boundary.GetXMax() - boxCollider.bounds.max.x, 0.0f, 0.0f); // Right
            if (updatedCollider.yMin <= boundary.GetYMin()) moveVector += new Vector3(0.0f, boundary.GetYMin() - boxCollider.bounds.min.y, 0.0f); // Bottom
            if (updatedCollider.yMax >= boundary.GetYMax()) moveVector += new Vector3(0.0f, boundary.GetYMax() - boxCollider.bounds.max.y, 0.0f); // Top
        }

        // Filter out some movement noise
        if(Mathf.Abs(moveVector.x) < filterThreshold) moveVector.x = 0.0f;
        if(Mathf.Abs(moveVector.y) < filterThreshold) moveVector.y = 0.0f;

        // Finally perform translation
        transform.Translate(moveVector);

        if (Input.GetButton("Fire1"))
        {
            weaponized.Fire(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if projectile
        Projectile projectile = collision.gameObject.GetComponent<Projectile>();
        if (!projectile || !projectile.isEnemyProjectile) return;

        TakeDamage(projectile.damage);
    }

    void TakeDamage(int damage)
    {
        shootable.TakeDamage(damage);

        if (shootable.IsDead())
        {
            // Play death animation or whatever
            Destroy(this.gameObject);
        }
    }
}
