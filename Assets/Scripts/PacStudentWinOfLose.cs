using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PacStudentWinOfLose : MonoBehaviour
{

    public Button PacmanGameAgainButton;
    public Button PacmanGameOverButton;

    // Use this for initialization
    void Start()
    {

        PacmanGameAgainButton.onClick.AddListener(PacmanGameAgainListener);
        PacmanGameOverButton.onClick.AddListener(PacmanGameOverClickListener);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PacmanGameOverClickListener()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void PacmanGameAgainListener()
    {
        SceneManager.LoadScene("StartScene");
    }

}
