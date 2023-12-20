using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atk_aura : atk_base
{

	static int level = 0;

	private Collider myCollider;

	/*
     * 레벨 당 변화 값 
    */
	private float[] Damage_lvl = { 1, 1 };      // + 연산
	private float[] bulletSize_lvl = { 1.0f, 1.0f };       // * 연산
	private float[] atkCycle_lvl = { 1.0f, 1.0f };         // * 연산

	// Start is called before the first frame update
	void Start()
    {
		initStat(1, 0, 1, 1, 2);
		myCollider = GetComponent<Collider>();
	}

    // Update is called once per frame
    void Update()
    {
		if (t_attack + atkCycle * t_atkCycle < Time.time)
		{
			t_attack = Time.time;
			attack();
		}
	}

	void levelUp()
	{
		Damage = Damage_lvl[level];
		atkCycle = atkCycle_lvl[level];
		bulletSize = bulletSize_lvl[level];
		this.transform.localScale = this.transform.localScale * bulletSize_lvl[level];
	}

	void activeCollider()
	{
		myCollider.enabled = true;
	}

	void deactiveCollider()
	{
		myCollider.enabled = false;
	}

	override protected void attack()
	{
		activeCollider();
		Invoke("deactiveCollider", 0.5f);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Monster")
		{
			other.gameObject.GetComponent<character>().onDamage(Damage, false);
		}
	}
}
