using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CreatePlayer : MonoBehaviour
{

    public static float HP ;
    public static float Damage ;
    public static float defense ;
    public static float speed ;
    public static int LUK;

    int count_reroll = 5;
    int count_statChange = 5;


    public TMP_Text HP_text;
    public TMP_Text Damage_text;
    public TMP_Text defense_text;
    public TMP_Text speed_text;
    public TMP_Text LUK_text;
    public TMP_Text count_text;


	void Start()
    {
        HP = 100;
        Damage = 1;
        defense = 1;
        speed = 10;
        LUK = 0;

        for (int i = 0; i < count_statChange; i++)
        {
            int a = UnityEngine.Random.Range(0,5);
            switch (a)
            {
                case 0:
                    HP += 1;
                    break;

                case 1:
                    Damage += 1;
                    break;

                case 2:
                    defense += 1;
                    break;

                case 3:
                    speed += 1;
                    break;
                case 4:
                    LUK += 1;
                    break;
                    
            }
        }

        HP_text.text = string.Format("{0}", HP);
        Damage_text.text = string.Format("{0}", Damage);
        defense_text.text = string.Format("{0}", defense);
        speed_text.text = string.Format("{0}", speed);
        LUK_text.text=string.Format("{0}", LUK);


    }


    // Update is called once per frame
    void Update()
    {
        count_text.text = string.Format("{0}",count_reroll);
    }

    public void reStat()
    {
       if (count_reroll > 0)
        {
            count_reroll--;

            HP = 100;
            Damage = 1;
            defense = 1;
            speed = 10;
            LUK=0;

            for (int i = 0; i < count_statChange; i++)
            {
				int a = UnityEngine.Random.Range(0, 5);
				switch (a)
                {
                    case 0:
                        HP += 1;
                        break;

                    case 1:
                        Damage += 1;
                        break;

                    case 2:
                        defense += 1;
                        break;

                    case 3:
                        speed += 1;
                        break;
					case 4:
						LUK += 1;
						break;

				}
			}

            HP_text.text = string.Format("{0}", HP);
            Damage_text.text = string.Format("{0}", Damage);
            defense_text.text = string.Format("{0}", defense);
            speed_text.text = string.Format("{0}", speed);
			LUK_text.text = string.Format("{0}", LUK);
		}

    }

    void Awake()
    {

    }
}

