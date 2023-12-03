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
    