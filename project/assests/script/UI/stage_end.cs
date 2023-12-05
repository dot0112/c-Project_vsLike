using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stage_end : MonoBehaviour
{
    public weapon_button[] btn;
    public int weapon_count = 3;

    public void setWeapon()
    {
        List<int> list = new List<int>();
        for(int i = 0; i < weapon_count; i++)
        {
            list.Add(i);
        }

        for(int i = 0; i < 3; i++)
        {
            int random = UnityEngine.Random.Range(0, list.Count);
            btn[i].weapon_num = list[random];
            btn[i].setup();
            list.RemoveAt(random);
        }
    }
}
