using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player_UI : MonoBehaviour
{

    public GameObject GameMenu;
    public GameObject roundMenu;
    public GameObject Player;
    public GameObject GameOver;

    public playerScript playerScript;
    public M_melee meleeScript;

    public TMP_Text Player_Hp;
    public TMP_Text timeText;
    public TMP_Text roundText;



    public float playTime;


    float Max_HP;

    int round = 1;
    float second = 60;
    protected bool stop = false;


    void Start()
    {
        Max_HP = CreatePlayer.HP;

        playerScript.HP = CreatePlayer.HP;
        playerScript.Damage = CreatePlayer.Damage;
        playerScript.defense = CreatePlayer.defense;
        playerScript.speed = CreatePlayer.speed;

        Time.timeScale = 1;
    }


    public void GameStart() 
    {
        GameMenu.SetActive(true);
        roundMenu.SetActive(false);
        Player.SetActive(true);

        Time.timeScale = 1;

        Player.GetComponent<playerScript>().enabled = true;
    }

    void Update()
    {
        if (stop == false)
        {
            second -= Time.deltaTime;
        }

        if (round == 9 || round == 19)
        {
            if (string.Format("{0:000}", second) == "000")
            {
                round++;
                GameMenu.SetActive(false);
                roundMenu.SetActive(true);
                Player.SetActive(false);

                Time.timeScale = 0;
                Player.GetComponent<playerScript>().enabled = false;

                second = 120;
            }
        }
        else 
        {
            if (string.Format("{0:000}", second) == "000")
            {
                round++;
                GameMenu.SetActive(false);
                roundMenu.SetActive(true);
                Player.SetActive(false);

                Time.timeScale = 0;
                Player.GetComponent<playerScript>().enabled = false;

                second = 60;
            }
        }

        if (playerScript.HP <= 0) { 
            GameOver.SetActive(true);
            Time.timeScale = 0;
            Player.GetComponent<playerScript>().enabled = false;
        }
    }


    void LateUpdate()
    {
        Player_Hp.text = string.Format("{0}", playerScript.HP) + " / " + string.Format("{0}", Max_HP);

        if (round == 10 || round == 20)
            timeText.text = string.Format("{0:000}", second);
        else
            timeText.text = string.Format("{0:00}", second);
        roundText.text = "Round " + round; 
    }

    public void stopSet()
    {
        Player.GetComponent<playerScript>().enabled = false;
        stop = true;

        Time.timeScale = 0;
    }

    public void startSet()
    {
        Player.GetComponent<playerScript>().enabled = true;
        stop = false;

        Time.timeScale = 1;
    }

    public void OnClickReStart()
    {
        SceneManager.LoadScene("CreatePlayer");
    }

    public void OnClickEXIT()
    {
        SceneManager.LoadScene("Main");
    }
}
