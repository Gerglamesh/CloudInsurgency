using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerP : MonoBehaviour {

	// Use this for initialization
	void Start () {
		for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
	}

    void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Check if projectile
        if (collision.gameObject.tag != "Player") return;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
