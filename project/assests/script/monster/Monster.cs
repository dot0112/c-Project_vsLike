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
    protected GameObject player;
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
	}

	// Start is called before the first frame update
	protected void Start()
    {
		setTarget();
		gameObject.tag = "Monster";
		anim = GetComponent<Animator>();
		anim.SetBool("player_alive", true);	// player ��ü�� Ȯ�� �޼ҵ� �ʿ�
	}

    protected void setTarget()
    {
		player = GameObject.FindWithTag("Player");

	}

    protected void followTarget()
    {
		if (follow)
		{
			var targetPos = player.transform.position;
			targetPos.y=this.transform.position.y;
			var heading = targetPos - this.transform.position;
			this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * speed);
			lookToPlayer();
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

	protected void Relocation()
	{
		// �׾��� ���͸� ���ġ �ϱ����� �Լ�

		if (canResTime < Time.time)
		{

			// �ִϸ����� �缳��
			anim.SetTrigger("walk");

			follow = true;
			isDie = false;

			// HP �缳��
			HP = initHP;

			// ��Ȱ ��ġ ����
			// �ϴ� ��, ��, ��, �� �� ����
			Vector3[] loc = { new Vector3(-30, 0, 0), new Vector3(30, 0, 0), new Vector3(0, 0, -30), new Vector3(0, 0, 90) };
			int locIdx=UnityEngine.Random.Range(0, loc.Length);
			var targetPos=player.transform.position;
			targetPos.y = 0;
			this.transform.position = targetPos + loc[locIdx];
		}
	}

	
	protected override void die()
	{	
		isDie = true;
		follow = false;
		canResTime = Time.time+resTime;
		AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
		if (!stateInfo.IsName("die"))
		{
			Debug.Log(Time.time+" monsterDie");
			isDie_anim = true;
			anim.SetTrigger("die");
		} else
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
		if (collision.collider.tag == "underGround")
		{
			Relocation();
		}
	}
}
