using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Weapon : MonoBehaviour
{

    public enum Type { Melee, Range };
    public Type type;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public Transform firePos;
    public GameObject bullet;
    public GameObject rotate;
    
    public GameObject rightHand;
    public GameObject leftHand;

    // ������ ���� �ð� (����:��)
    public float last_atk = 0;

    // {���� :{{������, ���� �ֱ�, �Ѿ� ũ��, �Ѿ� �ӵ�},{���� ����},{���� ����}}}
    // �Ѿ� ũ�� * localScale �̱� ������ ��ȭ�� �������� 1 �̻� �� �ܿ��� 1
    public float[,,] level_stat ={
        {{3,2,0,0 }, {1,1,1,1 }, {1,0.5f,1,1 }},
        {{5,2,0,0 }, {2,1,1.2f,1 }, {1,0.4f,1,1.2f }},
        {{7,2,0,0 }, {3,0.9f,1,1.1f }, {1,0.3f,1,1.2f }},
        {{8,1.7f,0,0 }, {4,0.9f,1.2f,1 }, {2,0.3f,1,1 }},
        {{13, 1.7f, 0, 0}, {4, 0.8f, 1.2f, 1.2f}, {3,0.3f,1,1 }},
        {{13, 1.5f, 0, 0}, {4, 0.5f, 1, 1.2f}, {4, 0.3f, 1.2f, 1}},
        {{15, 1.5f, 0, 0}, {7, 0.5f, 1, 1}, {4, 0.1f, 1, 2}},
    };

    // 1: ���� �ظ� 2: ���� 3: SMG
    public void levelUp(int i)
    {
        int level = playerScript.lvl_weapon[i] - 1;
        switch (i)
        {
            case 0: 
                // �ظ�
                break;
			case 1:
                // ����
                if (level == 0)
                {
                    rightHand.SetActive(false);
                }
                goto default;
            case 2:
				// SMG
				if (level == 0)
				{
					leftHand.SetActive(false);
				}
                goto default;
            default:
                rate = level_stat[level, i, 1];
                damage = (int)level_stat[level, i, 0];
				bullet.GetComponent<bulletManager>().initBullet(damage, level_stat[level, i, 3], level_stat[level, i, 2], true);
				break;
        }
    }

    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        } else if(type == Type.Range)
        {
			StopCoroutine("Shot");
			StartCoroutine("Shot");
		}
    }
    IEnumerator Swing()
    {
        //1
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
      
        //2
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
      

    }
	// Use() ���η�ƾ -> Swing() �����ƾ -> Use() ���� ��ƾ

	IEnumerator Shot()
	{
        Debug.Log(Time.time + " shot");

		//1
		yield return new WaitForSeconds(0.01f);

		float yRotation = rotate.transform.rotation.eulerAngles.y;
		float newYRotation = (yRotation > 180f) ? yRotation - 360f : yRotation;

		// Quaternion.Euler �Լ��� ����Ͽ� ���ο� ȸ���� ����
		Quaternion newRotation = Quaternion.Euler(0f, newYRotation-90f, 0f);

		GameObject newBullet = Instantiate(bullet, firePos.position, newRotation);

        newBullet.GetComponent<bulletManager>().initBullet(damage, 1, 1, true);
        // initBullet(������, �ӵ�, ũ��, (true: �÷��̾� �߻� | false: ���� �߻�));
        // ������ ��� ������ ������ �޼ҵ忡�� bullet ������Ʈ�� �����ؾ���

        newBullet.transform.localScale *= 0.2f;
        // ũ�� ����
        newBullet.SetActive(true);

		yield return new WaitForSeconds(0.01f);


	}
}
    // Start is called before the first frame update
    