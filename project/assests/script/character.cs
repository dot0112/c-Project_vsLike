using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class character : MonoBehaviour
{
    public float HP;
    public float Damage;
    public float defense;
    public float speed;

    public void onDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0) die();
    }

    protected abstract void die();
}
