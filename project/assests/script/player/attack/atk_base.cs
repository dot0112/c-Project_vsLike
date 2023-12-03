using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class atk_base : MonoBehaviour
{

	public float Damage;
	public float Speed;
	public float bulletSize;

	public float atkCycle;   // ���� �ֱ� ��
	public float t_atkCycle; // ���� �ֱ� �ð�
									// atkCycle * t_atkCycle �� ���� �ֱ�� ��� | �⺻������ ���� �ֱ� �� �� ����

	/*
     * ���� �� ��ȭ �� 
     * �ý��� ��ũ��Ʈ
     * lvl 0 -> 1 : �ش� ���� ��ũ��Ʈ Ȱ��ȭ
     * lvl n -> n + 1 : static ������ �̿��� �ɷ�ġ ���
    */

	private float[] Damage_lvl = {};      // + ����
	private float[] Speed_lvl = {};    // * ����
	private float[] bulletSize_lvl = {};       // * ����
	private float[] atkCycle_lvl = {};         // * ����

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
