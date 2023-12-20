using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_boss_bug_jump : MonoBehaviour
{
    public int Damage;
    public bool attack = false;

	private void OnTriggerStay(Collider other)
	{
		if (attack)
		{
            if (other.tag=="Player")   
            {
                other.GetComponent<playerScript>().onDamage(Damage);
            }
        }
	}
}
