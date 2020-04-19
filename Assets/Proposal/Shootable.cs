using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shootable {

    public int maxHealth = 10;
    public int hitPoints = 10;
    public int armor = 2; // Maybe Regenerating armor to force focused fire on certain enemies?

    public void TakeDamage(int damage)
    {
        // Insert cool damage calculations here!
        hitPoints -= damage;
    }

    public bool IsDead()
    {
        return hitPoints <= 0;
    }
}
