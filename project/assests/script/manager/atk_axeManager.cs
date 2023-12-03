using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atk_axeManager : MonoBehaviour
{
    // 도끼 생성시 플레이어의 정면으로 나옴 | 수정 필요

    public GameObject Axe;

	public float Speed;
	public float Damage;
	public float bulletSize;

	private Animator anim;
    private bool attack_anim = false;
    private AnimatorStateInfo stateInfo;

	readonly Quaternion initRot = Quaternion.Euler(0, -60, 0);

	// Start is called before the first frame update
	void Start()
    {
        anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        if (attack_anim)
        {
			stateInfo = anim.GetCurrentAnimatorStateInfo(0);
			if (stateInfo.IsName("attack")&& stateInfo.normalizedTime >= 1.0f)
            {
                Debug.Log(Time.time + " : end axe");
                Debug.Log(stateInfo.normalizedTime);
                end();
            }
		}
    }

    // 공격 | 공격 스크립트에서 호출 필요
    public void attack()
    {
        Debug.Log(Time.time + " : atk_axeM attack");

        // 시작
        Axe.SetActive(true);
		attack_anim = true;

        // 회전 애니메이션 실행
        anim.SetTrigger("attack");
    }

    void end()
    {
		attack_anim = false;
		Axe.SetActive(false);
        anim.SetTrigger("end");
	}

    public void changeStat(float d, float s,float bs)
    {
        Damage = d; Speed = s; bulletSize = bs;
    }
}
