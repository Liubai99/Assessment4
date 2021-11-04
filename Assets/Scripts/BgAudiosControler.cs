using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgAudiosControler : MonoBehaviour
{


    private static BgAudiosControler _instance;
    public static BgAudiosControler Instance
    {
        get
        {
            return _instance;
        }
    }

    public GameObject[] BGSound;

    private void Awake()
    {
        _instance = this;
    }

        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CloseBgSound()
    {
        for (int n = 0;n<3;n++)
        {
            BGSound[n].SetActive(false);
        }
    }

    public void OpneBg01()
    {
        CloseBgSound();
        BGSound[0].SetActive(true);
    }

    public void OpneBg02()
    {
        CloseBgSound();
        BGSound[1].SetActive(true);
    }

    public void OpneBg031()
    {
        BGSound[2].gameObject.AddComponent<AudioSource>().volume = 1;
    }

    public void OpneBg032()
    {
        BGSound[2].gameObject.AddComponent<AudioSource>().volume = 0;
    }

}
