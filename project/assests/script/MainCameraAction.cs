using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera_Action : MonoBehaviour
{
	public GameObject Target;               // ī�޶� ����ٴ� Ÿ��

	public float offsetX = 0.0f;            // ī�޶��� x��ǥ
	public float offsetY = 12.0f;           // ī�޶��� y��ǥ
	public float offsetZ = -15.0f;          // ī�޶��� z��ǥ

	public float CameraSpeed = 1.0f;       // ī�޶��� �ӵ�
	Vector3 TargetPos;                      // Ÿ���� ��ġ

	private void Start()
	{
		Target = GameObject.FindWithTag("Player");
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		// Ÿ���� x, y, z ��ǥ�� ī�޶��� ��ǥ�� ���Ͽ� ī�޶��� ��ġ�� ����
		TargetPos = new Vector3(
			Target.transform.position.x + offsetX,
			Target.transform.position.y + offsetY,
			Target.transform.position.z + offsetZ
			);

		// ī�޶��� �������� �ε巴�� �ϴ� �Լ�(Lerp)
		transform.position = Vector3.Lerp(transform.position, TargetPos, Time.deltaTime * CameraSpeed);
	}
}