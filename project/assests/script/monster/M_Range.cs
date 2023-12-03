using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Range : Monster
{
	public float attackRange;
	public float attackCycle;
	public bool canAttack = true;
	public float bulletSize;
	public float bulletSpeed;
	public GameObject bullet;
	public Transform firePos;

	protected float t_Attack;

	private bool animSet = false;

	// Start is called before the first frame update
	new void Start()
	{
		base.Start();
	}

	void Attack()
	{
		anim.Play("attack");
		Quaternion q = Quaternion.LookRotation(player.transform.position - transform.position);
		GameObject newBullet = Instantiate(bullet, firePos.position, q);
		newBullet.GetComponent<bulletManager>().initBullet(Damage, bulletSpeed, bulletSize, false);
		canAttack = false;
		t_Attack = Time.time;
		Debug.Log("range attack");
	}

	// Update is called once per frame
	void Update()
	{
		if (!isDie)
		{
			if (animSet)
			{
				if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
				{
					animSet = false;
					anim.Play("idle");
				}
			}
			else
			{
				if (!canAttack)
				{
					if (t_Attack + attackCycle < Time.time)
					{
						canAttack = true;
					}
				}
				else
				{
					var dis = Vector3.Distance(player.transform.position, transform.position);
					if (dis <= attackRange)
					{
						animSet = true;
						Attack();
					}
					else
					{
						anim.SetTrigger("walk");
						if (follow) followTarget();
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
