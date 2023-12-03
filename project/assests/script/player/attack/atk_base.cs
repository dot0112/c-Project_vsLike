using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class atk_base : MonoBehaviour
{

	public float Damage;
	public float Speed;
	public float bulletSize;

	public float atkCycle;   // 공격 주기 비
	public float t_atkCycle; // 공격 주기 시간
									// atkCycle * t_atkCycle 을 공격 주기로 사용 | 기본적으로 공격 주기 비 를 변경

	/*
     * 레벨 당 변화 값 
     * 시스템 스크립트
     * lvl 0 -> 1 : 해당 무기 스크립트 활성화
     * lvl n -> n + 1 : static 변수를 이용한 능력치 상승
    */

	private float[] Damage_lvl = {};      // + 연산
	private float[] Speed_lvl = {};    // * 연산
	private float[] bulletSize_lvl = {};       // * 연산
	private float[] atkCycle_lvl = {};         // * 연산

	protected float t_attack;
	protected bool can_attack = true;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	protected void initStat(float d, float s, float bs, float ac, float tac)
	{
		Damage = d; Speed=s; bulletSize = bs;
		atkCycle = ac; t_atkCycle=tac;
	}

	abstract protected void attack();
}
