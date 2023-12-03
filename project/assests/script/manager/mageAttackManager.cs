using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class mageAttackManager : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
		if(!attack && time + delayTime < Time.time)
		{
			animator.SetBool("attack", true);
		}
		if (time + delayTime + showTime < Time.time) Destroy(this.gameObject);
	}

	private void OnTriggerStay(Collider other)
	{
		if (attack)
		{
			if (other.CompareTag("Player"))
			{
				other.GetComponent<playerScript>().onDamage(Damage);
				attack= false;
			}
		}
	}

	private void setAttack()
	{
		attack = true;
	}

	public void initMagic(float d)
	{
		Damage = d;
	}
}
