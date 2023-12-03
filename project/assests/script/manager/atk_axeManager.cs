using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atk_axeManager : MonoBehaviour
{
    // ���� ������ �÷��̾��� �������� ���� | ���� �ʿ�

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

    // ���� | ���� ��ũ��Ʈ���� ȣ�� �ʿ�
    public void attack()
    {
        Debug.Log(Time.time + " : atk_axeM attack");

        // ����
        Axe.SetActive(true);
		attack_anim = true;

        // ȸ�� �ִϸ��̼� ����
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
