using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_ : MonoBehaviour
{
	public GameObject stone;

	private float time = 0;
	private Animator animator;
	private bool attack = false;

	public float Damage = 1;
	public float delayTime = 1;
	public float showTime = 1;

	// Start is called before the first frame update
	void Start()
	{
		time = Time.time;
		animator = GetComponent<Animator>();
		stone.GetComponent<stone>().Damage = Damage;
	}

	// Update is called once per frame
	void Update()
	{
		if (!attack && time + delayTime < Time.time)
		{
			attack = true;
			stone.SetActive(true);
			animator.Play("attack");
			time = Time.time;
		}
		else if (time + showTime < Time.time)
		{
			Destroy(this.gameObject);
		}
	}
}
