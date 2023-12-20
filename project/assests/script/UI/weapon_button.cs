using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class weapon_button : MonoBehaviour
{
    public int weapon_num;
    public Button btn;
    public TMP_Text before, after;
    public Image icon;
    public Sprite[] icon_list;
    public Player_UI player_UI;

	public void setup()
    {
		transform.Find("level").gameObject.SetActive(true);
		icon.sprite = icon_list[weapon_num];
        before.text= playerScript.lvl_weapon[weapon_num].ToString();
        after.text = (playerScript.lvl_weapon[weapon_num] + 1).ToString();
        btn.enabled = true;
        if (playerScript.lvl_weapon[weapon_num] == playerScript.maxlvl) { 
            btn.enabled = false;
            transform.Find("level").gameObject.SetActive(false);
            transform.Find("max").gameObject.SetActive(true);
        }
	}

    public void OnClick()
    {
		playerScript.player.GetComponent<playerScript>().levelUp_weapon(weapon_num);
		player_UI.GameStart();
	}
}
