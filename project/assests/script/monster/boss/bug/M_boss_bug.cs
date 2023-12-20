using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class M_boss_bug : Monster
{
	// Start is called before the first frame update


	public float late_jump, late_charge, late_magic, charge_speed;
	float[] last_attack = new float[3];
	bool[] c = new bool[3];

	public GameObject warning_charge, warning_jump, warning_jump_clone, magic;

	new public void Awake()
	{
		base.Awake();
	}

	new public void Start()
	{
		base.Start();
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position.y < -5) Relocation();
		if (player != null)
		{
			if (!isDie) // 살아있을 때
			{
				var dis = Vector3.Distance(player.transform.position, transform.position);

				if (dis > 20 && last_attack[0] + late_charge <= Time.time || c[0])
				{
					if (!c[0])
					{
						// 돌진 공격
						last_attack[0] = Time.time;
						StartCoroutine(charge_attack());
					}
				}
				else if (dis > 20 && last_attack[1] + late_magic <= Time.time || c[1])
				{
					if (!c[1])
					{
						// 마법 공격
						last_attack[1] = Time.time;
						StartCoroutine(cast_magic());
					}
				}
				else if (dis < 50 && last_attack[2] + late_jump <= Time.time || c[2]) 
				{
					if (!c[2])
					{
						// 점프 광역 공격
						last_attack[2] = Time.time;
						StartCoroutine(jump_attack());
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
					GameObject UI = GameObject.FindWithTag("MainUI");
					UI.GetComponent<Player_UI>().roundEnd();
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
		// 위험 지대 표시 및 충전 애니메이션 표시
		c[0] = true;
		Vector3 pos_before = transform.position;
		rb.velocity = Vector3.zero;
		warning_charge.SetActive(true);
		anim.Play("charge_1");
		while (last_attack[0] + 1f < Time.time)
		{
			// 1 초 동안 플레이어의 방향을 따라감
			lookToPlayer();
			yield return null;
		}
		GameObject warning_charge_clone = Instantiate(warning_charge,warning_charge.transform.position, warning_charge.transform.rotation);
		warning_charge_clone.transform.localScale = 5 * warning_charge_clone.transform.localScale;
		warning_charge.SetActive(false);
		warning_charge_clone.SetActive(true);
		yield return new WaitForSeconds(0.5f);


		// 실제로 움직이는 부분
		anim.Play("charge_2");
		while (true)
		{
			if (Vector3.Distance(pos_before, transform.position) >= 40) break;
			transform.position += Time.deltaTime * transform.forward * charge_speed;
			yield return null;
		}
		Destroy(warning_charge_clone);

		anim.Play("idle");
		yield return new WaitForSeconds(2f);
		c[0] = false;
		// 종료
	}

	IEnumerator cast_magic()
	{
		// 1 번에서 5 번 랜덤하게 공격
		c[1] = true;
		int rand = UnityEngine.Random.Range(1, 6);
		for (int i = 0; i < rand; i++)
		{
			anim.Play("cast_magic");
			while (true)
			{
				if (!anim.GetCurrentAnimatorStateInfo(0).IsName("cast_magic"))
				{
					anim.Play("cast_magic");
				}
				else
				{
					if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) break;
				}
				yield return null;
			}
			GameObject newMagic = Instantiate(magic, player.transform.position, Quaternion.identity);
			newMagic.SetActive(true);
			yield return new WaitForSeconds(0.3f);
			anim.Play("idle");
		}
		yield return new WaitForSeconds(0.5f);
		c[1] = false;
		// 종료
	}

	IEnumerator jump_attack()
	{
		// 애니메이션 재생 시간 수정 필요
		// 위험 지대 표시 및 진행
		c[2] = true;
		warning_jump_clone=Instantiate(warning_jump, player.transform.position, Quaternion.identity);
		warning_jump_clone.transform.localScale = warning_jump_clone.transform.localScale * 4;
		warning_jump_clone.SetActive(true);
		while (true)
		{
			if (!anim.GetCurrentAnimatorStateInfo(0).IsName("jump"))
			{
				anim.Play("jump");
			}
			else
			{
				if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f) break;
			}
			yield return null;
		}
		warning_jump_clone.SetActive(false);
		Destroy(warning_jump_clone.gameObject);
		anim.Play("idle");
		yield return new WaitForSeconds(2f);
		c[2] = false;
		// 종료
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

	public void jump_attack_damage_start()
	{
		warning_jump_clone.GetComponent<M_boss_bug_jump>().attack = true;
	}

	public void jump_attack_damage_end()
	{
		warning_jump_clone.GetComponent<M_boss_bug_jump>().attack = false;
	}

	public void OnStateExit()
	{
		this.transform.position = warning_jump_clone.transform.position;
	}

	private void MovePrefabToTarget()
	{
		LeanTween.move(this.gameObject, warning_jump_clone.transform.position, anim.GetCurrentAnimatorStateInfo(0).length).setEase(LeanTweenType.easeInOutQuad);
	}

	protected override void die()
	{
		isDie = true;
	}
}
