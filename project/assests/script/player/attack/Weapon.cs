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
    