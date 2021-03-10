using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menuControl : MonoBehaviour
{        
    GameObject chapter, locks;

    void Start()
    {
        //PlayerPrefs.DeleteAll(); //bütün kayıtları siler.

        chapter = GameObject.Find("Chapters");
        locks = GameObject.Find("Locks");

        for (int i = 0; i < chapter.transform.childCount; i++)
        {
            chapter.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < locks.transform.childCount; i++)
        {
            locks.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < PlayerPrefs.GetInt("hangiLevel"); i++)
        {
            chapter.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }        
    }
    
    public void butonSec(int gelenButon)
    {
        if (gelenButon==1)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("hangiLevel"));
        }
        else if (gelenButon == 2)
        {
            for (int i = 0; i < locks.transform.childCount; i++)
            {
                locks.transform.GetChild(i).gameObject.SetActive(true);
            }
            for (int i = 0; i < chapter.transform.childCount; i++)
            {
                chapter.transform.GetChild(i).gameObject.SetActive(true);
            }

            for (int i = 0; i < PlayerPrefs.GetInt("hangiLevel"); i++)
            {
                locks.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if (gelenButon == 3)
        {
            Application.Quit();
        }
    }

    public void levellerButon(int gelenLevel)
    {
        SceneManager.LoadScene(gelenLevel);
    }
}
