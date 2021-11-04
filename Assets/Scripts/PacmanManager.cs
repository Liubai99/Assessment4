using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PacmanManager : MonoBehaviour
{

    private static PacmanManager _instance;
    public static PacmanManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject[] icons;
    public int life = 3;

    public AudioSource hurtSound;

    public Transform Resurrectionpoints;
    public GameObject Player;



    public float CountDownTime;
    private float GameTime;
    private float timer = 0;

    public Text TimeText;

    public string NameLevel;

    public static float TimeNum = 0;
    public static  float m01 = 0;
    public static float s01 = 0;
    public static float timer01 = 0;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameTime = CountDownTime;

        CleanTime();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        timer += (Time.deltaTime * 60);

        TimeText.text =  m01.ToString("00") + " : " + s01.ToString("00") + " : " + timer.ToString("00");
        if (timer >= 60f)
        {
            timer = 0;
            s01++;
            TimeNum++;

            if (s01 >= 60)
            {
                s01 = 0;
                m01++;
            }
        }
    }

    public void OnClickPlay()
    {
        Time.timeScale = 1;
    }

    public void OnClickStop()
    {
        Time.timeScale = 0;
    }

    public void GameOverButtonClickListener()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    public void GameAgianButtonClickListener()
    {
        CleanTime();
        Time.timeScale = 1;
        SceneManager.LoadScene(NameLevel);
    }


    public void UpdateLife()
    {
        life--;
        if (life < 0)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Pac-manFailureScene");
        }

        for (int i = 0; i < icons.Length; i++)
        {
            hurtSound.Play();

            if (i < life) icons[i].SetActive(true);
            else icons[i].SetActive(false);

            PacStudentMove.Instance.BackPosition();
            Player.transform.position = Resurrectionpoints.transform.position;
        }
    }

    public void NameLevel01()
    {
        CleanTime();
        Time.timeScale = 1;
        SceneManager.LoadScene("Pac-manScene-Level01");
    }

    public void NameLevel02()
    {
        CleanTime();
        Time.timeScale = 1;
        SceneManager.LoadScene("Pac-manScene-Level01");
    }

    public void CleanTime()
    {
        TimeNum = 0;
        m01 = 0;
        s01 = 0;
        timer01 = 0;
    }

}
