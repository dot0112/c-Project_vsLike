using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mage_attack_pa : MonoBehaviour
{

	public GameObject[] giftBox;
	public GameObject attackBox;

	private float time = 0; // 내부적으로 지연시간 계산
	private Animator animator;
	private bool attack = false;

	public float Damage = 1;    // 데미지
	public float delayTime = 1; // 공격 전 딜레이
	public float showTime = 1;  // 공격 후 표시되는 시간

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();

		int tmp = Random.Range(0, giftBox.Length);
		giftBox[tmp].transform.position = attackBox.transform.position;
		giftBox[tmp].transform.parent= attackBox.transform;
		giftBox[tmp].SetActive(true);
		time = Time.time;
	}

	// Update is called once per frame
	void Update()
	{
		if (!attack && time + delayTime < Time.time)
		{
			attack = true;
			attackBox.SetActive(true);
			animator.Play("attack");
			time = Time.time;
		}
		else if (time + showTime < Time.time)
		{
			Destroy(this.gameObject);
		}
	}


	private void OnTriggerStay(Collider other)
	{
		if (attack)
		{
			if (other.tag == "Player")
			{
				other.GetComponent<playerScript>().onDamage(Damage);
			}
		}
	}
}
