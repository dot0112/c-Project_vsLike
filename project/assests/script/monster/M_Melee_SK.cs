using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M_MeleeSK : MonoBehaviour
{

    public GameObject sword;
    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
		System.Random random = new System.Random();
		int randomNumber = random.Next(1, 4);
        switch (randomNumber)
        {
            case 0: // ¸Ç¼Õ
                Destroy(sword.gameObject);
                Destroy(shield.gameObject);
                break;
            case 1: // Ä®
				Destroy(shield.gameObject);
				break;
            case 2: // ¹æÆÐ
				Destroy(sword.gameObject);
				break;
        }
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
