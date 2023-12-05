using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class outsideManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        tag = "Respawn";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerStay(Collider other)
	{
        if (other.tag == "Monster")
        {
            other.GetComponent<Monster>().Relocation();
        }
	}
}
