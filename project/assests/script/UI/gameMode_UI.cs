using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class gameMode_UI : MonoBehaviour
{
	public Image icon;
	public Sprite[] icon_list;

	private void Update()
	{
		if (levelManager.level == 10 || levelManager.level == 20) {
			icon.sprite = icon_list[4];
		}
		else
			icon.sprite = icon_list[levelManager.gameMode];
	}
}
