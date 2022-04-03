using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class WorldGen : MonoBehaviour
{
    public GameObject Fabrik;
    public Transform Planet;

    public float minDis = 10;
    public float FabrikCount = 2;

    public TextMeshProUGUI Fac;
    public TextMeshProUGUI Timer;

    public List<GameObject> obs = new List<GameObject>();
    public float timer1 = 300;

    public static float realTimer = 0;
    public static float facCount = 0;
    public static float Timer2 = 0;

    public static float Score { get; set; }

    public GameObject enem;
    public GameObject enem2;


    public static float sT;
    public Volume v;

    public bool end = false;

    public LayerMask mask;

    public static bool over = false;

    private void Start()
    {
        enem.SetActive(false);
        enem2.SetActive(false);
        if (PlayerPrefs.GetInt("done") == 1)
        {
            enem.SetActive(true);
            zweiteWelle = true;
            Gen(20);
            Textbubble.addText("Ohh man the climate skeptic\n are here.", 2);
            Textbubble.addText("They´ve sent there wapons. \n We should not collide with them", 1.4f);
            Textbubble.addText("Only if we have the boost", 1.4f);
            Textbubble.addText("They also brought new Factories", 1.4f);
            zweiteWelle = true;
        } else if (PlayerPrefs.GetInt("done") == 2)
        {
            zweiteWelle = true;
            dritteWelle = true;
            Gen(50);
            Textbubble.addText("They are here again.", 2);
            Textbubble.addText("This is the last round", 2);
            enem2.SetActive(true);
            PlayerPrefs.SetInt("done", 2);
        }
        else
        {
            Gen(10);
        }
        timer1 = 60*1;
        sT = timer1;

        Score = 0;
        facCount = 0;
        realTimer = 0;

        over = false;
    }

    private void Update()
    {
        facCount = (obs.Count(o => o != null));

        if (facCount != 0) timer1 -= Time.deltaTime;
        Fac.text = $"Factories: {FabrikCount-obs.Count(o=>o != null)}/{FabrikCount}";
        float t = (int)(timer1 / 60);
        float sec = 60 * ((timer1 / 60) - t);

        Timer2 = timer1;

        realTimer += Time.deltaTime;
        v.weight = 1- (1 / sT) * timer1;
        Timer.text = $"Time: {t}:{(sec < 10 ? "0" : "")}{(int)sec}";

        if(timer1 < 0 && !over)
        {
            over = true;
            Textbubble.addText("Dude, I can´t breath!", 2);
            Textbubble.addText("We failed", 1.3f);
            Textbubble.addText("Atleast we tried", 1.3f);
            Textbubble.addText("This was unavoidable", 1.4f);
            Textbubble.addText("See ya in the next life", 2f);
            Textbubble.addText("bye", 1f);
            StartCoroutine(d());
        }

        if (obs.Count(o => o != null) == 0)
        {
            if (zweiteWelle)
            {
                if (dritteWelle)
                {
                    if (!end)
                    {
                        end = true;
                        Textbubble.addText("We did it!", 1.4f);
                        Textbubble.addText("We saved the Earth", 1.4f);
                        Textbubble.addText("There is only one CO2 producer left", 1.4f);
                        Textbubble.addText("US!!!!", 1.4f);
                        Textbubble.addText("You know what we need to do!", 1.4f);
                        Textbubble.addText("It was unavoidable", 1.4f);
                        Textbubble.addText("We just delayed our dead", 1.4f);
                        Textbubble.addText("Now it´s time", 1.4f);
                        PlayerPrefs.SetInt("done", 0);
                    }
                }
                else
                {
                    dritteWelle = true;
                    Gen(50);
                    Textbubble.addText("They are here again.", 2);
                    Textbubble.addText("This is the last round", 2);
                    enem2.SetActive(true);
                    PlayerPrefs.SetInt("done", 2);
                }
            }
            else
            {
                enem.SetActive(true);
                zweiteWelle = true;
                Gen(20);
                Debug.Log("ant to talk");
                Textbubble.addText("Ohh man the climate skeptic\n are here.", 2);
                Textbubble.addText("They´ve sent there wapons. \n We should not collide with them", 1.4f);
                Textbubble.addText("Only if we have the boost", 1.4f);
                Textbubble.addText("They also brought new Factories", 1.4f);
                PlayerPrefs.SetInt("done", 1);
            }
        }
    }

    public bool zweiteWelle;
    public bool dritteWelle;

    IEnumerator d(float s = 8f)
    {
        yield return new WaitForSeconds(s);
        SceneManager.LoadScene("EndScreen");   
    }
    public void Gen(int c)
    {
        FabrikCount = c;
        float r = Planet.localScale.x;
        for(int i = 0; obs.Count < c; i++)
        {
            int j = 0;
            Vector3 pos = new Vector3();
            do
            {
                pos = new Vector3(Random.Range(-r, r), Random.Range(-r, r), Random.Range(-r, r)).normalized * r;
                j++;
            } while (j < 100 && Physics.CheckSphere(pos, 5,mask));
            GameObject ob = Instantiate(Fabrik, pos, Quaternion.identity);
            obs.Add(ob);
        }
    }
}
