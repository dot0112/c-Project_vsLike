using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{

    public GameObject canvas;
    public GameObject gameOption;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickExitgame()
    {
        gameOption.SetActive(false);
        SceneManager.LoadScene("Main");   //플레이어 생성
    }

    public void OnClickCreateButton()
    {
        canvas.SetActive(false);
        SceneManager.LoadScene("InGame");   //플레이어 생성
    }


}
