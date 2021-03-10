using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class enemyControl : MonoBehaviour
{
    GameObject[] gidilecekNoktalar;
    bool mesafeyiBirKereAl = true;
    Vector3 mesafe;
    int mesafeSayaci = 0;
    bool ileriGeri = true;

    GameObject karakter;
    RaycastHit2D ray;
    public LayerMask layerMask;
    public  int hiz = 5;
    public Sprite onTaraf;
    public Sprite arkaTaraf;
    SpriteRenderer spriteRenderer;

    public GameObject kursun;
    float atesZamani = 0;

    void Start()
    {
        karakter = GameObject.FindGameObjectWithTag("Player");
        gidilecekNoktalar = new GameObject[transform.childCount];
        for (int i = 0; i < gidilecekNoktalar.Length; i++)
        {
            gidilecekNoktalar[i] = transform.GetChild(0).gameObject;
            gidilecekNoktalar[i].transform.SetParent(transform.parent);
        }
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Debug.Log(karakter.GetComponent<chracterControl>().canKontrol);
    }

    void FixedUpdate()
    {
        beniGordumu();
        
        if (ray.collider.tag == "Player" && karakter.GetComponent<chracterControl>().canKontrol==true)
            {
                hiz = 8;
                spriteRenderer.sprite = onTaraf;
                atesEt();
            }
         else
            {
                hiz = 4;
                spriteRenderer.sprite = arkaTaraf;
            }        
        
        noktalaraGit();
    }

    void atesEt()
    {
        atesZamani += Time.fixedDeltaTime;
        if (atesZamani>Random.Range(0.2f,1))
        {
            Instantiate(kursun, transform.position, Quaternion.identity);
            atesZamani = 0;
        }
    }

    void beniGordumu()
    {
        Vector3 rayYonum = karakter.transform.position - transform.position;
        ray = Physics2D.Raycast(transform.position, rayYonum, 1000, layerMask);
        Debug.DrawLine(transform.position, ray.point, Color.magenta);
    }

    void noktalaraGit()
    {
        if (mesafeyiBirKereAl)
        {
            mesafe = (gidilecekNoktalar[mesafeSayaci].transform.position - transform.position).normalized;
            mesafeyiBirKereAl = false;
        }
        float uzaklik = Vector3.Distance(transform.position, gidilecekNoktalar[mesafeSayaci].transform.position);
        transform.position += mesafe * Time.fixedDeltaTime * hiz;
        if (uzaklik < 0.5f)
        {
            mesafeyiBirKereAl = true;
            if (mesafeSayaci == gidilecekNoktalar.Length - 1)
            {
                ileriGeri = false;
            }
            else if (mesafeSayaci == 0)
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

    public Vector2 getYon()
    {
        return (karakter.transform.position - transform.position).normalized;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}

#if UNITY_EDITOR //bu kodların içine yazılanlar oyun build edildiğinde çalışmıyor. unity ekranında yani editor de çalışıyor.
[CustomEditor(typeof(enemyControl))]
[System.Serializable]
class enemyControlEditor : Editor
{
    public override void OnInspectorGUI()
    {
        enemyControl script = (enemyControl)target;
        EditorGUILayout.Space();
        if (GUILayout.Button("Create", GUILayout.MinWidth(100), GUILayout.Width(100)))
        {
            GameObject obje = new GameObject();
            obje.transform.parent = script.transform;
            obje.transform.position = script.transform.position;
            obje.name = script.transform.childCount.ToString();
        }

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layerMask"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("onTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("arkaTaraf"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("kursun"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}
#endif