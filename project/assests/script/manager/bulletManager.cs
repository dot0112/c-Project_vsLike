using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class bulletManager : MonoBehaviour
{
    public float Speed=1;
    public float Damage;
    public float bulletSize;

    public bool playerBullet = false;

    // Start is called before the first frame update
    void Start()
    {
        float speed = 10 * Speed;
        if (playerBullet) this.GetComponent<Rigidbody>().velocity = transform.right * speed;
        else this.GetComponent<Rigidbody>().velocity = transform.forward * speed;
		// źȯ ���Ⱚ * �ʱⰪ 10 * speed ���� = źȯ �ӵ�
		// źȯ �������� ���ư� ä�� �����Ǿ����� -> transform.right �� �ذ�
	}

    // Update is called once per frame
    void Update()
    {
        //this.GetComponent<Rigidbody>().AddForce(transform.right * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
		if (!playerBullet)
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<character>().onDamage(Damage, true);
                Destroy(this.gameObject);
            }
        }
        else
        {
            if(other.gameObject.tag == "Monster")
            {
                float damage = Damage;
                if(Random.Range(0, 100) < playerScript.LUK)
                {
                    damage *= 1.5f;
                }
				other.gameObject.GetComponent<character>().onDamage(damage,false);
				Destroy(this.gameObject);
			}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Map")
        {
			Destroy (this.gameObject);
        }
    }

	public void initBullet(float d,float s, float bs, bool ws)
    {
        Damage = d;
        Speed *= s;
        this.transform.localScale = this.transform.localScale * bs;
        playerBullet = ws;
    }
}
