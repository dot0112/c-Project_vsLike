using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class mage_attack_sk : MonoBehaviour
{
    
    public GameObject parentPrefab;
	public GameObject[] childPrefab;

	private float time = 0;
	private Animator animator;
	private bool attack = false;

	public float Damage = 1;
	public float delayTime = 1;
	public float showTime = 1;

	// Start is called before the first frame update
	void Start()
    {

		int randomNumber = Random.Range(0, childPrefab.Length);

		childPrefab[randomNumber].SetActive(true);

		time = Time.time;
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
    {
		if (!attack && time + delayTime < Time.time)
		{
			animator.Play("attack");
			attack = true;
		}
		if (time + delayTime + showTime < Time.time) Destroy(this.gameObject);
	}

	private void OnTriggerStay(Collider other)
	{
		if (attack)
		{
			if (other.CompareTag("Player"))
				other.GetComponent<playerScript>().onDamage(Damage);
		}
	}
}
