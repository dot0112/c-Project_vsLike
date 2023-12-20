using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee_warning : MonoBehaviour
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

    public void attack_active()
    {
        attack = true;
    }

	public void attack_deactive()
	{
		attack = false;
	}

}
