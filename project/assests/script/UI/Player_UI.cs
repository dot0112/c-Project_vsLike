using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.Build;

public class Player_UI : MonoBehaviour
{

    public GameObject GameMenu;
    public GameObject roundMenu;
    public GameObject Player;
    public GameObject GameOver;
    public GameObject levelManager;

    public TMP_Text Player_Hp;
    public TMP_Text timeText;
    public TMP_Text roundText;



    public float playTime;


    float Max_HP;

    int round = 0;
	public float stageTime = 10;
    float second;
    protected bool stop = false;


    void Start()
    {

	    Max_HP = CreatePlayer.HP;

        playerScript.HP = CreatePlayer.HP;
        playerScript.Damage = CreatePlayer.Damage;
        playerScript.defense = CreatePlayer.defense;
        playerScript.speed = CreatePlayer.speed;
        playerScript.LUK = CreatePlayer.LUK;
        roundUp();

        Time.timeScale = 1;
        second = stageTime;
	}

	private void Awake()
	{
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
				roundUp();
				GameMenu.SetActive(false);
                roundMenuActive();

				Player.SetActive(false);

                Time.timeScale = 0;
                Player.GetComponent<playerScript>().enabled = false;

                second = stageTime * 2;
            }
        }
        else 
        {
            if (string.Format("{0:000}", second) == "000")
            {
				roundUp();
				GameMenu.SetActive(false);
                roundMenuActive();

				Player.SetActive(false);

                Time.timeScale = 0;
                Player.GetComponent<playerScript>().enabled = false;

                second = stageTime;
            }
        }

        if (playerScript.HP <= 0) { 
            GameOver.SetActive(true);
            Time.timeScale = 0;
            Player.GetComponent<playerScript>().enabled = false;
        }
    }

    void roundUp()
    {
        round++;
        levelManager.GetComponent<levelManager>().levelUp(round);
    }

    void roundMenuActive()
    {
        roundMenu.SetActive(true);
        roundMenu.GetComponent<stage_end>().setWeapon();
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
