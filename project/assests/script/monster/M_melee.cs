using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_melee : Monster
{

	// Start is called before the first frame update
	new void Start()
    {
		base.Start();
	}

    // Update is called once per frame
    void Update()
    {
        if (!isDie)
        {
            followTarget();

        } else
        {
            if (isDie_anim) die();
            else
                Relocation(); // 후에 게임 시스템에서 relocate 시키게 변경 예정
        }
    }

	private void OnCollisionStay(Collision collision)
	{
        if (!isDie)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<playerScript>().onDamage(Damage);
                Debug.Log("attack melee");
            }
        }
	}
}
