using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_Range : Monster
{
	public float attackRange;
	public float attackCycle;
	public bool canAttack = true;
	public GameObject bullet;
	public Transform firePos;

	protected float t_Attack;

	public bool animSet = false;

	public int Damage_col = 1;

	// Start is called before the first frame update
	new void Start()
	{
		base.Start();
	}

	void Attack()
	{
		Quaternion q = Quaternion.LookRotation(player.transform.position - transform.position);
		GameObject newBullet = Instantiate(bullet, firePos.position, q);
		newBullet.SetActive(true);
		canAttack = false;
		t_Attack = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		if (!isDie)
		{
			Vector3 t = transform.position;
			t.y = 0;
			this.transform.position = t;
			lookToPlayer();
			if (animSet)
			{
				if (anim.GetCurrentAnimatorStateInfo(0).IsName("attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
				{
					Attack();
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
						anim.Play("attack");
					}
					else
					{
						anim.Play("walk");
						followTarget();
					}
				}
			}
		}
		else
		{
			if (isDie_anim) die();
			else
				Relocation();
		}

	}

	private void OnCollisionStay(Collision collision)
	{
		if (!isDie)
		{
			if (collision.gameObject.tag == "Player")
			{
				collision.gameObject.GetComponent<playerScript>().onDamage(Damage_col);
			}
		}
	}
}
