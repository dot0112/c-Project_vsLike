using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atk_axe : atk_base
{

	public GameObject axePrefab;

	protected GameObject axe;

	private atk_axeManager M_axe;

	/*
     * 레벨 당 변화 값 
    */
	private float[] Damage_lvl = { 1, 1 };      // + 연산
	private float[] Speed_lvl = { 1.0f, 1.2f };    // * 연산
	private float[] bulletSize_lvl = { 1.0f, 1.0f };       // * 연산
	private float[] atkCycle_lvl = { 1.0f, 1.0f };         // * 연산

	// Start is called before the first frame update
	void Start()
    {
		initStat(10, 1, 1, 1, 4);
		activeWeapon();
    }

    // Update is called once per frame
    void Update()
    {
		if (!can_attack)
		{
			if (t_attack + atkCycle * t_atkCycle < Time.time)
			{
				can_attack = true;
			}
		} else
		{
			attack();
			can_attack = false;
			t_attack = Time.time;
		}
    }

	protected override void attack()
	{
		M_axe.attack();
	}

	public void activeWeapon()
	{
		axe = Instantiate(axePrefab, transform.position, transform.rotation);
		axe.transform.parent = transform;
		M_axe = axe.GetComponent<atk_axeManager>();
		M_axe.changeStat(Damage, Speed, bulletSize);
	}
}
