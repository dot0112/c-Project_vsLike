using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axeManager : MonoBehaviour
{
    public atk_axeManager parentPrefab;

    // Start is called before the first frame update
    void Start()
    {
        parentPrefab=transform.parent.GetComponent<atk_axeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Monster")
        {
            other.GetComponent<character>().onDamage(parentPrefab.Damage);
        }
	}
}
