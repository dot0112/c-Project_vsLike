using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletField : MonoBehaviour
{
	// Start is called before the first frame update
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "bullet")
		{
			Destroy(other.gameObject);
		}
	}
}
