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

    // 마지막 공격 시간 (단위:초)
    public float last_atk = 0;

    // {레벨 :{{데미지, 공격 주기, 총알 크기, 총알 속도},{무기 종류},{무기 종류}}}
    // 총알 크기 * localScale 이기 때문에 변화가 있을때만 1 이상 그 외에는 1
    public float[,,] level_stat ={
        {{3,2,0,0 }, {1,1,1,1 }, {1,0.5f,1,1 }},
        {{5,2,0,0 }, {2,1,1.2f,1 }, {1,0.4f,1,1.2f }},
        {{7,2,0,0 }, {3,0.9f,1,1.1f }, {1,0.3f,1,1.2f }},
        {{8,1.7f,0,0 }, {4,0.9f,1.2f,1 }, {2,0.3f,1,1 }},
        {{13, 1.7f, 0, 0}, {4, 0.8f, 1.2f, 1.2f}, {3,0.3f,1,1 }},
        {{13, 1.5f, 0, 0}, {4, 0.5f, 1, 1.2f}, {4, 0.3f, 1.2f, 1}},
        {{15, 1.5f, 0, 0}, {7, 0.5f, 1, 1}, {4, 0.1f, 1, 2}},
    };

    // 1: 근접 해머 2: 권총 3: SMG
    public void levelUp(int i)
    {
        int level = playerScript.lvl_weapon[i] - 1;
        switch (i)
        {
            case 0: 
                // 해머
                break;
			case 1:
                // 권총
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
	// Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인 루틴

	IEnumerator Shot()
	{
        Debug.Log(Time.time + " shot");

		//1
		yield return new WaitForSeconds(0.01f);

		float yRotation = rotate.transform.rotation.eulerAngles.y;
		float newYRotation = (yRotation > 180f) ? yRotation - 360f : yRotation;

		// Quaternion.Euler 함수를 사용하여 새로운 회전값 생성
		Quaternion newRotation = Quaternion.Euler(0f, newYRotation-90f, 0f);

		GameObject newBullet = Instantiate(bullet, firePos.position, newRotation);

        newBullet.GetComponent<bulletManager>().initBullet(damage, 1, 1, true);
        // initBullet(데미지, 속도, 크기, (true: 플레이어 발사 | false: 몬스터 발사));
        // 레벨업 기능 구현시 레벨업 메소드에서 bullet 오브젝트에 적용해야함

        newBullet.transform.localScale *= 0.2f;
        // 크기 조정
        newBullet.SetActive(true);

		yield return new WaitForSeconds(0.01f);


	}
}
    // Start is called before the first frame update
    