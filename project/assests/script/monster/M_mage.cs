using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_mage : M_Range
{
	public float castingTime;
	public float runSpeed;
	public GameObject attackMagic;


	bool isAttack = false;
	bool animSet = false;

	// Start is called before the first frame update
	new void Start()
	{
		base.Start();
		anim.SetBool("player_alive", true);
	}

	void Attack()
	{
		t_Attack = Time.time;

		Quaternion q = new Quaternion();
		Vector3 loc = player.transform.position;
		loc.y = 0;
		GameObject newMagic=Instantiate(attackMagic, loc, q);
		newMagic.GetComponent<mageAttackManager>().initMagic(Damage);
	}

	void runawayTarget()
	{
		Vector3 temp = transform.position - player.transform.position;
		temp = temp.normalized;
		temp.y = this.transform.position.y;
		transform.position += temp * speed * Time.deltaTime;

		lookToPlayer();
	}

	// Update is called once per frame
	void Update()
	{
		if (!isDie) {
			if (isAttack)
			{
				if (animSet)
				{
					Attack();
					anim.SetTrigger("attack");
					animSet = false;
				}
				if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
				{
					canAttack = false;
					isAttack = false;
					anim.SetTrigger("walk");
				}
			}
			else
			{
				if (!canAttack)
				{
					if (t_Attack + attackCycle < Time.time) { follow = true; canAttack = true; t_Attack = 0; }
					runawayTarget();
				}
				else
				{
					var dis = Vector3.Distance(player.transform.position, transform.position);
					if (canAttack) // 공격 가능
					{
						if (dis > attackRange) { if (follow) followTarget(); } // 따라감
						else if (dis <= attackRange)
						{
							canAttack = false;
							animSet = true;
							isAttack = true;
							follow = false;
						} // 공격
					}
				}
			}
		}
		else
		{
			Relocation();
		}
	}
}