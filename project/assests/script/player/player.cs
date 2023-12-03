using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class playerScript : character
{

	public float invincibleTime;


    float T_invincibleTime;
	float hAxis;
	float vAxis;
	float fireDelay;
	bool isFireReady;
	Vector3 moveVec;
	public Weapon equipWeapon;
	float autoAttackTimer = 0f;


	Animator anim;


	void GetInput()
	{
		hAxis = Input.GetAxisRaw("Horizontal");
		vAxis = Input.GetAxisRaw("Vertical");
	}

	void Move()
	{
		moveVec = new Vector3(hAxis, 0, vAxis).normalized;

		transform.position += moveVec * speed * Time.deltaTime;

		anim.SetBool("isRun", moveVec != Vector3.zero);

		
	}

	void Turn()
	{
		transform.LookAt(transform.position + moveVec);
	}
	void AutoAttackTimer()
	{

		// ���� ������ ���� (��: 1�ʸ��� ����)
		float attackInterval = 1.0f;

		// Ÿ�̸Ӹ� ������Ʈ
		autoAttackTimer += Time.deltaTime;

		// ���� ���ݸ��� Attack �޼��� ȣ��
		if (autoAttackTimer >= attackInterval)
		{
			Attack();
			autoAttackTimer = 0f; // Ÿ�̸� �ʱ�ȭ
		}
	}
	void Attack()
	{
		if (equipWeapon == null)
			return;

		fireDelay += Time.deltaTime;
		isFireReady = equipWeapon.rate / 1000.0f < fireDelay;
		Debug.Log(equipWeapon.rate + " " + fireDelay);

		if (isFireReady)
		{
			equipWeapon.Use();
			anim.SetTrigger("doSwing");
			fireDelay = 0;
		}
	}
    private void Awake()
	{
		gameObject.tag = "Player";
        anim = GetComponentInChildren<Animator>();
    }

	private void Start()
	{

	}
	

	private void playerDie()
	{
		// ���� �Ͻ����� �� �÷��̾� ����� ���� ���� �ý��� ���� �ʿ�
	}

	// Update is called once per frame
	void Update()
    {
		GetInput();
		Move();
		Turn();
		AutoAttackTimer();
		if (HP <= 0) // �÷��̾� ���
		{
			playerDie();
		}

	

		


    }

	public new void onDamage(float damage)
	{
		if (T_invincibleTime + invincibleTime < Time.time)
		{
			Debug.Log("player ondamage");
			HP -= damage;
            T_invincibleTime = Time.time;
		}
		if (HP <= 0) die();
	}

	protected override void die()
	{
		Debug.Log("player die");
	}

}
