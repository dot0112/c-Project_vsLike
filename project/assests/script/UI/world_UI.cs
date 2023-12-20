using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class world_UI : MonoBehaviour
{

    public TMP_Text world_num;


	void Update()
    {
		if (levelManager.level == 10 || levelManager.level == 20)
		{
			world_num.SetText("?");
		}
		else
			world_num.SetText(((levelManager.world) + 1).ToString());
    }
}
