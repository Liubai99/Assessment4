using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartPacmanScene : MonoBehaviour {

    public Button GameStartPacmanButton;
    public Button Level02Button;

    public float MaxScaleNum = 1.2f;
    public float MixScaleNum = 0.8f;
    public bool IsScaleBig = false;

    public Text BestScoreText01;
    public Text BestTimeText01;
   // public Text BestScoreText02;
   // public Text BestTimeText02;

    public static float Levelm01 = 0;
    public static float levels01 = 0;
    public static float Leveltimer01 = 0;

    // Use this for initialization
    void Start ()
    {
        GameStartPacmanButton.onClick.AddListener(GameStartPacmanListener);
        Level02Button.onClick.AddListener(Level02ButtonLis);

        BestScoreText01.text = GhostManager.Level01BestScore.ToString();
        // BestTimeText01.text = GhostManager.Level01BestTime.ToString();
        BestTimeText01.text = Levelm01.ToString("00") + " : " + levels01.ToString("00") + " : " + Leveltimer01.ToString("00");

        // BestScoreText02.text = GhostManager.Level02BestScore.ToString();
        // BestTimeText02.text = GhostManager.Level02BestTime.ToString();


    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GameStartPacmanButton.gameObject.transform.localScale.y < MaxScaleNum && IsScaleBig == true)
        {
            GameStartPacmanButton.gameObject.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
            Level02Button.gameObject.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
        }
        else
        {
            IsScaleBig = false;
        }

        if (GameStartPacmanButton.gameObject.transform.localScale.y > MixScaleNum && IsScaleBig == false)
        {
            GameStartPacmanButton.gameObject.transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
            Level02Button.gameObject.transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);
        }
        else
        {
            IsScaleBig = true;
        }
    }

    public  void GameStartPacmanListener()
    {
        SceneManager.LoadScene("Pac-manScene-Level01");
    }

     public void Level02ButtonLis()
    {
        SceneManager.LoadScene("Pac-manScene-Level01");
    }

}
