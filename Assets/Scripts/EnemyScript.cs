using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    public int gunDamage = 1;
    public int collisionDamage = 4;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    public void IsDead(int hitPoints)
    {
        if (hitPoints <= 0)
        {
            hitPoints = 0;

            Destroy(this.gameObject);
        }
    }
}

