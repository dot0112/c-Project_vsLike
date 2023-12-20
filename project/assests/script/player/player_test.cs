using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class player_test : character
{

	public int LUK;

	public float invincibleTime;

	public static int[] lvl_weapon = new int[4]; // 1. 근접 망치 2. 권총 3. 기관총 4. 샷건
	public static int maxlvl = 7;

	float T_invincibleTime;
	float hAxis;
	float vAxis;
	Vector3 moveVec;
	public List<GameObject> equipWeapon;
	public static GameObject player;


	Animator anim;


	public void levelUp_weapon(int n)
	{
		if (lvl_weapon[n] == 0)
		{
			equipWeapon[n].SetActive(true);
		}
		lvl_weapon[n]++;
		equipWeapon[n].GetComponent<Weapon>().levelUp(n);
	}


	void GetInput()
	{
		hAxis = Input.GetAxisRaw("Horizontal");
		vAxis = Input.GetAxisRaw("Vertical");
	}

	void Move()
	{
		moveVec = new Vector3(hAxis, 0, vAxis).normalized;

		this.transform.position += moveVec * speed * Time.deltaTime;

		anim.SetBool("isRun", moveVec != Vector3.zero);


	}


	void Turn()
	{
		transform.LookAt(transform.position + moveVec);
	}


	void Attack()
	{
		if (equipWeapon == null)
			return;

		for (int i = 0; i < equipWeapon.Count; i++)
		{
			if (equipWeapon[i].active == true)
			{
				Weapon weapon = equipWeapon[i].GetComponent<Weapon>();
				bool isFireReady = weapon.last_atk + weapon.rate < Time.time;
				if (isFireReady)
				{
					weapon.Use();
					weapon.last_atk = Time.time;
				}
			}
		}
	}


	private void Awake()
	{
		gameObject.tag = "Player";
		player = this.gameObject;
		anim = GetComponentInChildren<Animator>();
	}


	private void Start()
	{
		gameObject.tag = "Player";
		player = this.gameObject;
		anim = GetComponentInChildren<Animator>();
	}


	private void playerDie()
	{
		// 게임 일시정지 및 플레이어 사망에 따른 게임 시스템 조정 필요
	}

	// Update is called once per frame
	void Update()
	{
		GetInput();
		Move();
		Turn();
		Attack();
		GetComponent<Rigidbody>().velocity = Vector3.zero;
		if (HP <= 0) // 플레이어 사망
		{
			playerDie();
		}
	}

	public void onDamage(float damage)
	{
		if (T_invincibleTime + invincibleTime < Time.time)
		{
			Debug.Log("player ondamage");
			// damage -= defense;
			if (damage > 0)
			{
				HP -= damage;
				T_invincibleTime = Time.time;
			}
		}
		if (HP <= 0) die();
	}

	protected override void die()
	{
		Debug.Log("player die");
	}

}
