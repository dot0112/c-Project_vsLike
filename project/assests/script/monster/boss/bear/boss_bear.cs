using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class boss_bear : Monster
{
	public float late_charge, late_melee
		, charge_speed;
	float[] last_attack = new float[2];
	bool[] c = new bool[2];
	bool mF = false;

	public GameObject[] warning_melee;
	public GameObject warning_charge;


	new public void Start()
	{
		base.Start();
	}

	new public void Awake()
	{
		base.Awake();
	}


	void Update()
	{
		if(transform.position.y<-5) Relocation();
		if (player != null)
		{
			if (!isDie)
			{
				var dis = Vector3.Distance(player.transform.position, transform.position);
				if (dis > 10 && last_attack[0] + late_charge <= Time.time || c[0])
				{
					if (!c[0])
					{
						// 돌진 공격
						last_attack[0] = Time.time;
						StartCoroutine(charge_attack());
					}
				}
				else if (dis < 15 && last_attack[1] + late_melee <= Time.time || c[1])
				{
					if (!c[1])
					{

						last_attack[1] = Time.time;
						StartCoroutine(melee_attack());
					}
				}
				else
				{
					AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
					string s = stateInfo.shortNameHash.ToString();
					// 플레이어 쫓기
					if (!s.Equals("walk"))
						anim.Play("walk");
					followTarget();
				}
			}
			else
			{
				base.die();
				if (!isDie_anim)    // die 애니메이션 종료 이후
				{
					//라운드 종료 기능 추가 필요
					GameObject UI = GameObject.FindWithTag("MainUI");
					UI.GetComponent<Player_UI>().roundEnd();
					Destroy(gameObject);
				}
			}
		}
		else
		{
			player = GameObject.FindWithTag("Player");
		}
	}

	IEnumerator charge_attack()
	{
		c[0] = true;
		// 3 번에서 5 번 
		int rand = UnityEngine.Random.Range(3, 6);
		for (int i = 0; i < rand; i++)
		{
			Vector3 pos_before = transform.position;
			//rb.velocity = Vector3.zero;
			warning_charge.SetActive(true);
			while (true)
			{
				if (!anim.GetCurrentAnimatorStateInfo(0).IsName("charge_1"))
				{
					anim.Play("charge_1");
				}
				else
				{
					lookToPlayer();
					if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) break;
				}
				yield return null;
			}
			GameObject warning_charge_clone = Instantiate(warning_charge, warning_charge.transform.position, warning_charge.transform.rotation);
			warning_charge_clone.transform.localScale = warning_charge_clone.transform.localScale * 5;
			warning_charge_clone.SetActive(true);
			warning_charge.SetActive(false);
			yield return new WaitForSeconds(0.2f);
			anim.Play("charge_2");
			while (true)
			{
				if (Vector3.Distance(pos_before, transform.position) >= 40) break;
				transform.position += Time.deltaTime * transform.forward * charge_speed;
				yield return null;
			}
			Destroy(warning_charge_clone);
			anim.Play("idle");
			yield return new WaitForSeconds(0.5f);
		}

		yield return new WaitForSeconds(1f);
		c[0] = false;
	}

	IEnumerator melee_attack()
	{
		c[1] = true;

		// 공격 전 따라가기
		warning_melee[0].SetActive(true);
		while (last_attack[1] + 1f >= Time.time)
		{
			AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
			string s = stateInfo.shortNameHash.ToString();
			if (!s.Equals("walk"))
				anim.Play("walk");
			followTarget();
			yield return null;
		}

		// 1 번 공격
		while (true)
		{
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("melee_1"))
			{
				Vector3 direction = player.transform.position - transform.position;
				transform.rotation = Quaternion.LookRotation(direction);
				anim.Play("melee_1");
			}
			else
				if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) break;
			if (mF) moveForward(20);
			yield return null;
		}
		warning_melee[0].SetActive(false);
		yield return new WaitForSeconds(0.1f);

		// 2 번 공격
		warning_melee[0].SetActive(true);
		yield return new WaitForSeconds(0.1f);
		while (true)
		{
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("melee_2"))
			{
				Vector3 direction = player.transform.position - transform.position;
				transform.rotation = Quaternion.LookRotation(direction);
				anim.Play("melee_2");
			}
			else
				if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) break;
			if (mF) moveForward(20);
			yield return null;
		}
		warning_melee[0].SetActive(false);
		yield return new WaitForSeconds(0.1f);

		// 3 번 공격
		warning_melee[1].SetActive(true);
		yield return new WaitForSeconds(0.1f);
		while (true)
		{
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("melee_3"))
			{
				Vector3 direction = player.transform.position - transform.position;
				transform.rotation = Quaternion.LookRotation(direction);
				anim.Play("melee_3");
			}
			else
				if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) break;
			if (mF) moveForward(20);
			yield return null;
		}
		warning_melee[1].SetActive(false);
		yield return new WaitForSeconds(0.1f);

		warning_melee[2].SetActive(true);
		yield return new WaitForSeconds(0.1f);
		while (true)
		{
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("melee_4"))
			{
				Vector3 direction = player.transform.position - transform.position;
				transform.rotation = Quaternion.LookRotation(direction);
				anim.Play("melee_4");
			}
			else
				if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) break;
			if (mF) moveForward(20);
			yield return null;
		}
		warning_melee[2].SetActive(false);
		yield return new WaitForSeconds(1.5f);
		c[1] = false;
	}

	private void OnCollisionStay(Collision collision)
	{
		if (!isDie)
		{
			if (collision.gameObject.tag == "Player")
			{
				collision.gameObject.GetComponent<playerScript>().onDamage(Damage);
			}
		}
	}

	public void moveForward(float speed)
	{
		transform.position += transform.forward * speed * Time.deltaTime;
	}

	public void attack_active(int n)
	{
		warning_melee[n].GetComponent<melee_warning>().attack_active();
	}

	public void attack_deactive(int n)
	{
		warning_melee[n].GetComponent<melee_warning>().attack_deactive();
	}

	public void setMF()
	{
		mF = true;
	}

	public void resetMF()
	{
		mF = false;
	}

	protected override void die()
	{
		isDie = true;
	}
}
