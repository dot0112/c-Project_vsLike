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

    int count = 5;

    public TMP_Text HP_text;
    public TMP_Text Damage_text;
    public TMP_Text defense_text;
    public TMP_Text speed_text;
    public TMP_Text count_text;


    void Start()
    {

        HP = 100;
        Damage = 1;
        defense = 1;
        speed = 10;

        var randomObj = new System.Random();

        for (int i = 0; i < 5; i++)
        {
            int a = randomObj.Next(0, 4);
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

            }
        }

        HP_text.text = string.Format("{0}", HP);
        Damage_text.text = string.Format("{0}", Damage);
        defense_text.text = string.Format("{0}", defense);
        speed_text.text = string.Format("{0}", speed);


    }


    // Update is called once per frame
    void Update()
    {
        count_text.text = string.Format("{0}",count);
    }

    public void reStat()
    {
        var randomObj = new System.Random();
        if (count > 0)
        {
            count--;

            HP = 100;
            Damage = 1;
            defense = 1;
            speed = 10;

            for (int i = 0; i < 5; i++)
            {
                int a = randomObj.Next(0, 4);
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

                }
            }

            HP_text.text = string.Format("{0}", HP);
            Damage_text.text = string.Format("{0}", Damage);
            defense_text.text = string.Format("{0}", defense);
            speed_text.text = string.Format("{0}", speed);

        }

    }

    void Awake()
    {

    }
}

