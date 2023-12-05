using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class Monster : character
{
	protected GameObject player= playerScript.player;
	protected Animator anim;
	protected Rigidbody rb;

	protected bool follow = true;
	protected float canResTime = 0;

	public bool isDie = false;
	public bool isDie_anim = false;
	public float initHP;
	public float resTime;

	public void Awake()
	{
		gameObject.tag = "Monster";
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
	}

	// Start is called before the first frame update
	protected void Start()
	{
		gameObject.tag = "Monster";
		anim = GetComponent<Animator>();
		rb=GetComponent<Rigidbody>();
	}


	protected void followTarget()
	{
		if (follow)
		{
			if (this.transform.position.y < -5)
			{
				Relocation();
			}
			else
			{
				Vector3 temp = player.transform.position - transform.position;
				temp = temp.normalized;
				temp.y = 0;
				transform.position += temp * speed * Time.deltaTime;
				lookToPlayer();
			}
		}
	}

	protected void lookToPlayer()
	{
		Vector3 playerPosition = player.transform.position;

		// ���⿡�� newYValue�� �÷��̾��� ���� y�� ��ſ� ��������
		playerPosition.y = this.transform.position.y;

		Vector3 direction = playerPosition - transform.position;
		direction.y = 0;

		if (direction != Vector3.zero)
		{
			Quaternion toRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 180 * Time.deltaTime);
		}
	}

	public void Relocation()
	{
		// �׾��� ���͸� ���ġ �ϱ����� �Լ�

		if (canResTime < Time.time)
		{

			tag = "Monster";

			// �ִϸ����� �缳��
			anim.SetTrigger("walk");

			follow = true;
			isDie = false;

			// HP �缳��
			HP = initHP;

			// ��Ȱ ��ġ ����
			// �ϴ� ��, ��, ��, �� �� ����
			Vector3[] loc = { new Vector3(((int) UnityEngine.Random.Range(-40,40)), 0, 90), new Vector3(((int)UnityEngine.Random.Range(-40, 40)), 0, -30), new Vector3(60, 0, ((int)UnityEngine.Random.Range(0, 50))), new Vector3(-60, 0, ((int)UnityEngine.Random.Range(0, 50))) };
			int locIdx = UnityEngine.Random.Range(0, loc.Length);
			var targetPos = player.transform.position;
			targetPos.y = 0;
			this.transform.position = targetPos + loc[locIdx];
			this.GetComponent<Rigidbody>().velocity = Vector3.zero;
		}
	}


	protected override void die()
	{
		isDie = true;
		follow = false;
		canResTime = Time.time + resTime;
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		if (!stateInfo.IsName("die"))
		{
			Debug.Log(Time.time + " monsterDie | anim status " + stateInfo.IsName("die"));
			tag = "Untagged";
			isDie_anim = true;
			anim.Play("die");
		}
		else
		{
			if (stateInfo.normalizedTime >= 1.0f)
			{
				// ���� ���͸� (0, -10, 0) ���� �̵�
				isDie_anim = false;
				this.transform.position = levelManager.waitLoc;
				rb.useGravity = false;
			}
		}
	}


	// Update is called once per frame
	private void Update()
	{

	}

	private void OnCollisionEnter(Collision collision)
	{
	}
}
