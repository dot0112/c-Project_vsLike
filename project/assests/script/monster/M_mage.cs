using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_mage : M_Range
{
	public float runSpeed;
	public GameObject attackMagic;

	// Start is called before the first frame update
	new void Start()
	{
		base.Start();
		canAttack = true;
	}
	
	void Attack()
	{
		t_Attack = Time.time;

		Quaternion q = new Quaternion();
		Vector3 loc = player.transform.position;
		loc.y = 0;
		GameObject newMagic = Instantiate(attackMagic, loc, q);
		newMagic.SetActive(true);
	}

	void runawayTarget()
	{
		anim.Play("walk");
		Vector3 temp = transform.position - player.transform.position;
		temp = temp.normalized;
		temp.y = 0;
		transform.position += temp * speed * Time.deltaTime;

		lookToPlayer();
	}

	// Update is called once per frame
	void Update()
	{
		if (!isDie) {
			
				if (!canAttack)
				{
					if (animSet)
					{
						if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
						{
							Attack();
							animSet = false;
						}
					}
					else
					{
						if (t_Attack + attackCycle < Time.time) { follow = true; canAttack = true; t_Attack = 0; }
						runawayTarget();
					}
				}
				else
				{
					var dis = Vector3.Distance(player.transform.position, transform.position);
					if (canAttack) // 공격 가능
					{
						if (dis > attackRange) { if (follow) followTarget(); } // 따라감
						else if (dis <= attackRange)
						{
							anim.Play("attack");
							canAttack = false;
							animSet = true;
							follow = false;
						} // 공격
					}
				}
			}
		else
		{
			Relocation();
		}
	}
}