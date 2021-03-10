using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class sawControl : MonoBehaviour
{
    GameObject[] gidilecekNoktalar;
    bool mesafeyiBirKereAl = true;
    Vector3 mesafe;
    int mesafeSayaci = 0;
    bool ileriGeri=true;

    void Start()
    {
        gidilecekNoktalar = new GameObject[transform.childCount];
        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
    }

    
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.Rotate(0,0,5);
        noktalaraGit();
    }

    void noktalaraGit()
    {
        if (mesafeyiBirKereAl)
        {
            mesafe = (gidilecekNoktalar[mesafeSayaci].transform.position - transform.position).normalized;
            mesafeyiBirKereAl = false;
        }
        
        float uzaklik = Vector3.Distance(transform.position, gidilecekNoktalar[mesafeSayaci].transform.position);
        transform.position += mesafe * Time.fixedDeltaTime * 10;
        
        if (uzaklik < 0.5f)
        {
            mesafeyiBirKereAl = true;
            if (mesafeSayaci==gidilecekNoktalar.Length-1)
            {
                ileriGeri = false;
            }
            else if (mesafeSayaci==0)
            {
                ileriGeri = true;
            }

            if (ileriGeri)
            {
                mesafeSayaci++;
            }
            else
            {
                mesafeSayaci--;
            }
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount-1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i+1).transform.position);
        }
    }
#endif
}

#if UNITY_EDITOR //bu kodların içine yazılanlar oyun build edildiğinde çalışmıyor. unity ekranında yani editor de çalışıyor.
[CustomEditor(typeof(sawControl))]
[System.Serializable]
class sawEditor : Editor
{
    public override void OnInspectorGUI()
    {
        sawControl script = (sawControl)target;
        if (GUILayout.Button("Create", GUILayout.MinWidth(100),GUILayout.Width(100)))
        {
            GameObject obje = new GameObject();
            obje.transform.parent = script.transform;
            obje.transform.position = script.transform.position;
            obje.name = script.transform.childCount.ToString();
        }
    }
}
#endif