using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class chracterControl : MonoBehaviour
{
    public Sprite[] beklemeAnim;
    public Sprite[] ziplamaAnim;
    public Sprite[] yurumeAnim;
    SpriteRenderer sprtRend;
    float horizontal = 0;
    Rigidbody2D fizik;
    Vector2 vec;
    bool birKereZipla=true;

    int beklemeAnimSayac=0;
    int yurumeAnimSayac=0;
    float beklemeAnimZaman = 0;
    float yurumeAnimZaman = 0;

    Vector3 kameraSonPoz;
    Vector3 kameraIlkPoz;
    GameObject kamera;

    public Image siyahResim;
    //float resimSayaci = 0;
    float menuyeDonmeZamani=0;

    public Text canTxt;
    public int can = 20;
    int goldCount=0;
    public Text goldTxt;
        
    createParts crtParts;    
    public bool canKontrol = true;
    
    void Start()
    {
        Time.timeScale = 1;
        sprtRend = GetComponent<SpriteRenderer>();
        fizik= GetComponent<Rigidbody2D>();

        kamera = GameObject.FindGameObjectWithTag("MainCamera");
        kameraIlkPoz = kamera.transform.position - transform.position;

        canTxt.text = "Can: " + can;

        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("hangiLevel"))
        {
            PlayerPrefs.SetInt("hangiLevel", SceneManager.GetActiveScene().buildIndex);
        }

        //Debug.Log(crtParts.GetComponent<createParts>().deneme); //direk veriyi çekiyor. Örneğin "public int x=10;"      
        
        crtParts = GetComponent<createParts>();
        
        //int a = crtParts.GetComponent<createParts>().deneme;
        //Debug.Log("Deneme: " + a);        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (birKereZipla)
            {
                fizik.AddForce(new Vector2(0, 500));
                birKereZipla = false;
            }            
        }
    }

    private void OnCollisionEnter2D(Collision2D col) //bir yüzeye temas ettiğinde tetikleniyor
    {
        birKereZipla = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag=="kursun_tag")
        {
            can--;
            canTxt.text = "Can: " + can; 
        }
        if (col.tag == "dusman_tag")
        {
            can-=10;
            canTxt.text = "Can: " + can;
        }
        if (col.tag == "saw_tag")
        {
            can -= 10;
            canTxt.text = "Can: " + can;
        }
        if (col.tag == "finish_tag")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (col.tag == "chest_tag")
        {
            can += 10;
            canTxt.text = "Can: " + can;
            col.GetComponent<BoxCollider2D>().enabled = false;
            col.GetComponent<giveHeal>().enabled = true;
            Destroy(col.gameObject, 3);
        }
        if (col.tag == "gold_tag")
        {
            goldCount++;
            goldTxt.text = "Gold: " + goldCount;
            Destroy(col.gameObject);
        }
        if (col.tag == "water_tag")
        {
            can = 0;
        }
    }

    void FixedUpdate()
    {
        karakterHaraket();
        Animasyonu();
        
        if (can<=0)
        {
            canKontrol = false;
            sprtRend.enabled = false;
            crtParts.crtParts();
            //redPlayer.SetActive(false);
            Time.timeScale = 0.5f;
            canTxt.enabled = false;
            //resimSayaci += 0.03f;
            //siyahResim.color = new Color(0, 0, 0, resimSayaci);
            menuyeDonmeZamani += Time.fixedDeltaTime;
            if (menuyeDonmeZamani>10)
            {
                SceneManager.LoadScene("Ana Menu");
            }
        }
    }
   

    private void LateUpdate()
    {
        kameraKontrol();
    }

    void kameraKontrol()
    {
        kameraSonPoz = kameraIlkPoz + transform.position;
        //kamera.transform.position = kameraSonPoz;
        kamera.transform.position = Vector3.Lerp(kamera.transform.position, kameraSonPoz, 0.1f);
    }

    void karakterHaraket()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vec = new Vector2(horizontal*10, fizik.velocity.y);
        fizik.velocity = vec;
    }

    void Animasyonu()
    {
        if (birKereZipla)
        {
            if (horizontal == 0)
            {
                beklemeAnimZaman += Time.deltaTime;
                if (beklemeAnimZaman > 0.05f)
                {
                    sprtRend.sprite = beklemeAnim[beklemeAnimSayac++];
                    if (beklemeAnimSayac == beklemeAnim.Length)
                    {
                        beklemeAnimSayac = 0;
                    }
                    beklemeAnimZaman = 0;
                }
            }

            else if (horizontal > 0)
            {
                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.01f)
                {
                    sprtRend.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    yurumeAnimZaman = 0;
                }
                transform.localScale = new Vector2(1, 1);
            }

            else if (horizontal < 0)
            {

                yurumeAnimZaman += Time.deltaTime;
                if (yurumeAnimZaman > 0.01f)
                {
                    sprtRend.sprite = yurumeAnim[yurumeAnimSayac++];
                    if (yurumeAnimSayac == yurumeAnim.Length)
                    {
                        yurumeAnimSayac = 0;
                    }
                    yurumeAnimZaman = 0;
                }
                transform.localScale = new Vector2(-1, 1);
            }
        }

        else
        {
            if (fizik.velocity.y > 0)
            {
                sprtRend.sprite = ziplamaAnim[0];
            }

            else
            {
                sprtRend.sprite = ziplamaAnim[1];
            }

            if (horizontal>0)
            {
                transform.localScale = new Vector2(1, 1);
            }
           
            else if (horizontal<0)
            {
                transform.localScale = new Vector2(-1, 1);
            }
        }
    }
}
