using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class atk_front : atk_base
{

	public GameObject bullet;
	public Transform firePos;

	/*
     * 레벨 당 변화 값 
    */
	private float[] Damage_lvl = { 1, 1 };      // + 연산
    private float[] Speed_lvl= { 1.0f,1.2f};    // * 연산
    private float[] bulletSize_lvl = {1.0f,1.0f };       // * 연산
    private float[] atkCycle_lvl = {1.0f,1.0f };         // * 연산

    void Start()
    {
        initStat(1, 3, 0.5f, 1, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (!can_attack) {
            if(t_attack + atkCycle * t_atkCycle < Time.time)
            {
                can_attack = true;
            }
        }
        else
        {
            // 공격
            attack();
            can_attack = false;
            t_attack = Time.time;
        }
    }

    override protected void attack()
    {
        GameObject newBullet = Instantiate(bullet, firePos.position, firePos.rotation);
        newBullet.GetComponent<bulletManager>().initBullet(Damage, Speed, bulletSize, true);
        Debug.Log("asdf");
    }
}
