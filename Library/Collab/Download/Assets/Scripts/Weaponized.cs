using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaponized : MonoBehaviour {
    public Weapon[] weapons;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Fire all weapons
    public bool Fire()
    {
        foreach (Weapon w in weapons)
        {
            if (w.isActive)
            {
                //w.transform.position = transform.position;
                w.FireWeapon();
            }
        }

        return false;
    }
}
