using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GhostManager : MonoBehaviour
{
   
    private static GhostManager _instance;
    public static GhostManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public int LevelNum = 1;
    public static int Level01BestScore = 0;
    public static float Level01BestTime = 0;
    public static int Level02BestScore = 330;
    public static float Level02BestTime = 80;

    public Text BestScoreText;
    public Text BestTimeText;

    public AudioSource GetSource;
    public AudioSource GetSuperSource;

    public GameObject pacman;
    public GameObject Enemy01;
    public GameObject Enemy02;
    public GameObject Enemy03;
    public GameObject Enemy04;

    public Text GhostremainText;
    public Text GhostnowText;
    public Text GhostscoreText;

    public bool isSuperPacman = false;
    public List<int> usingIndex = new List<int>();
    public List<int> rawIndex = new List<int> { 0, 1, 2, 3 };
    private List<GameObject> pacdotGos = new List<GameObject>();
    private int pacdotNum = 0;
    private int nowEat = 0;
    public int score = 0;
    private int count = 0;

    public bool IsRunTime = false;
    public float TimeDead = 10;
    public Text TimeDeadText;
    


    private void Awake()
    {
        _instance = this;

        Screen.SetResolution(1024, 768, false);

        int GhosttempCount = rawIndex.Count;
        for (int i = 0; i < GhosttempCount; i++)
        {
            int tempIndex = Random.Range(0, rawIndex.Count);
            usingIndex.Add(rawIndex[tempIndex]);
            rawIndex.RemoveAt(tempIndex);
        }
        foreach (Transform t in GameObject.Find("MazeScene").transform)
        {
            pacdotGos.Add(t.gameObject);
        }
        pacdotNum = GameObject.Find("MazeScene").transform.childCount;
    }

    private void Start()
    {
        float Time_Ran = Random.Range(0,10);
        Invoke("CreatePacStudentpowerpellet", Time_Ran);

        GetComponent<AudioSource>().Play();

        if (LevelNum == 1)
        {
            BestScoreText.text = Level01BestScore.ToString();
            BestTimeText.text = Level01BestTime.ToString();
        }
        else
        {
            BestScoreText.text = Level02BestScore.ToString();
            BestTimeText.text = Level02BestTime.ToString();
        }

    }

    private void Update()
    {
        if(IsRunTime == true)
        {
            TimeDead -= Time.deltaTime;

            TimeDeadText.text = TimeDead.ToString("0");
        }

       GhostremainText.text = (pacdotNum - nowEat).ToString();
        GhostnowText.text =  nowEat.ToString();
        GhostscoreText.text =  score.ToString();

        if (nowEat == pacdotNum)
        {
            if (LevelNum == 1)
            {
                if (score > Level01BestScore)
                {
                    Level01BestScore = score;
                }

                if (PacmanManager.TimeNum < Level01BestTime)
                {

                    Level01BestTime = PacmanManager.TimeNum;

                    StartPacmanScene.Levelm01 = PacmanManager.m01;
                    StartPacmanScene.levels01 = PacmanManager.s01;
                    StartPacmanScene.Leveltimer01 = PacmanManager.timer01;

                }
            }
            else
            {
                if (score > Level02BestScore)
                {
                    Level02BestScore = score;
                }
               
                if (PacmanManager.TimeNum < Level02BestTime)
                {
                    Level02BestTime = PacmanManager.TimeNum;
                }
            }

            SceneManager.LoadScene("Pac-manWinScene");
        }
        count++;
        
    }

    public void OnEatPacdot(GameObject go)
    {
        nowEat++;
        score += 1;
        pacdotGos.Remove(go);

        GetSource.Play();
    }

    public void OnEatpowerpellet()
    {
        score += 10;
        Invoke("CreatePacStudentpowerpellet", 8f);
        isSuperPacman = true;
        GhostFreezeEnemy();
        StartCoroutine(PacStudentRecoveryEnemy());

        GetSuperSource.Play();
    }

    IEnumerator PacStudentRecoveryEnemy()
    {
        yield return new WaitForSeconds(10f);
        GhostDisFreezeEnemy();
        isSuperPacman = false;
    }

    public void CreatePacStudentpowerpellet()
    {
        if (pacdotGos.Count < 5)
        {
            return;
        }


        int tempIndex = Random.Range(0, pacdotGos.Count);

        pacdotGos[tempIndex].transform.localScale = new Vector3(0.07f, 0.07f, 2f);

        pacdotGos[tempIndex].GetComponent<PacStudent>().ispowerpellet = true;
        float index = Random.Range(0, 1);
        pacdotGos[tempIndex].gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.white, index);
   

        if (count % 2 == 0)

        {
            Renderer render = pacdotGos[tempIndex].GetComponent<Renderer>();

            render.material.color = Color.red;
        }
        else
        {
            Renderer render = pacdotGos[tempIndex].GetComponent<Renderer>();

            render.material.color = Color.blue;
        }


    }

   public void GhostFreezeEnemy()
    {
        TimeDeadText.gameObject.SetActive(true);
        TimeDead = 10;
        TimeDeadText.text = TimeDead.ToString("0");
        IsRunTime = true;

        BgAudiosControler.Instance.OpneBg02();

        Enemy01.GetComponent<PacStudentEnemyMove>().enabled = false;
        Enemy02.GetComponent<PacStudentEnemyMove>().enabled = false;
        Enemy03.GetComponent<PacStudentEnemyMove>().enabled = false;
        Enemy04.GetComponent<PacStudentEnemyMove>().enabled = false;
        Enemy01.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 0.8f);
        Enemy02.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 0.8f);
        Enemy03.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 0.8f);
        Enemy04.GetComponent<SpriteRenderer>().color = new Color(0.8f, 0.8f, 0.8f, 0.8f);
    }

  public void GhostDisFreezeEnemy()
    {
        TimeDead = 0;
        TimeDeadText.text = TimeDead.ToString("0");
        IsRunTime = false;
        TimeDeadText.gameObject.SetActive(false);

        BgAudiosControler.Instance.OpneBg01();

        Enemy01.GetComponent<PacStudentEnemyMove>().enabled = true;
        Enemy02.GetComponent<PacStudentEnemyMove>().enabled = true;
        Enemy03.GetComponent<PacStudentEnemyMove>().enabled = true;
        Enemy04.GetComponent<PacStudentEnemyMove>().enabled = true;
        Enemy01.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        Enemy02.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        Enemy03.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        Enemy04.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }

    
}
