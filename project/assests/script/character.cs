using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class character : MonoBehaviour
{
    public float HP;
    public float Damage;
    public float defense;
    public float speed;

    public void onDamage(float damage, bool isPlayer)
    {
        if(!isPlayer)
        {
            int randNUM = UnityEngine.Random.Range(0, 100) + 1;
            if (randNUM <= playerScript.LUK) damage *= 2;
        }
        damage -= defense;
        if (damage > 0)
        {
            HP -= damage;
        }
        if (HP <= 0) die();
    }

    protected abstract void die();
}
