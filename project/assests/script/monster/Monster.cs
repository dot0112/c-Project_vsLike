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

		// 여기에서 newYValue를 플레이어의 원래 y값 대신에 넣으세요
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
		// 죽었던 몬스터를 재배치 하기위한 함수

		if (canResTime < Time.time)
		{

			tag = "Monster";

			// 애니메이터 재설정
			anim.SetTrigger("walk");

			follow = true;
			isDie = false;

			// HP 재설정
			HP = initHP;

			// 부활 위치 설정
			// 일단 좌, 우, 하, 상 만 설정
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
				// 죽은 몬스터를 (0, -10, 0) 으로 이동
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
