using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weaponized {
    public Weapon[] weapons;

    // Fire all weapons
    public bool Fire(bool isEnemyFiring)
    {
        foreach (Weapon w in weapons)
        {
            if (w.isActive)
            {
                w.FireWeapon(isEnemyFiring);
            }
        }

        return false;
    }
}
