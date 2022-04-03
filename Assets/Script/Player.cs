using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float s = 0.1f;
    public float fSpeed = 4;

    public float rot = 0;

    public AudioSource CarIdle;
    public AudioSource CarKurve;

    public Camera cam;

    public GameObject DieSound;
    public GameObject Partikel;

    public GameObject P2;

    public GameObject car;

    private bool started = false;
    private bool started1 = false;
    private bool started2 = false;

    public GameObject AutoModell;

    private bool isDrifting;
    private float driftDir;
    private float driftTimer = 0;

    private bool die;

    private float starts;

    public List<VisualEffect> Sparks = new List<VisualEffect>();

    public AudioSource DriftSound;
    public AudioSource BoostSound;

    public TextMeshProUGUI Scoret;

    public GameObject MultOb;
    public TextMeshProUGUI MultText;


    private void Start()
    {
        starts = s;
        Textbubble.addText("You know how to drive this thing. Right?",2);
        Textbubble.addText("We should try to Destroy this little Factories",2);
        Scoret.text = "Score: " + WorldGen.Score;
        multiplikator = 1;
    }
    void Update()
    {
        if (die || WorldGen.over) return;
        if (!(new Vector3(transform.position.x, 0, transform.position.z).magnitude < 0.7f))
        {
            transform.Rotate(new Vector3(0, 0, rot));
        }
        transform.position += transform.up * fSpeed * Time.deltaTime;

        float hori = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Fire2"))
        {
            StartDrift(hori);
        }
        if (Input.GetButtonUp("Fire2"))
        {
            StopDrifting();
        }

        if (Input.GetButtonDown("Fire1") && HasOrb)
        {
            Boost();
            HasOrb = false;
            StopCoroutine("Sup4");
            StartCoroutine(Sup4(new Vector3(0,0,0), false));
        }
        if (!isDrifting)
        {
            Sparks.ForEach((s) => s.Stop());

            if (hori < -0.2f)
            {
                rot += -s * hori;
                if (!started1)
                {
                    StopCoroutine("ToOne");
                    StopCoroutine("ToZero");
                    StartCoroutine(ToOne(CarKurve));
                    StartCoroutine(ToZero(CarIdle));
                    started1 = true;
                }
                started = false;
                started2 = false;
            }
            else if (hori > 0.2f)
            {
                rot += -s * hori;
                if (!started2)
                {
                    StopCoroutine("ToOne");
                    StopCoroutine("ToZero");
                    StartCoroutine(ToOne(CarKurve));
                    StartCoroutine(ToZero(CarIdle));
                    started2 = true;
                }
                started1 = false;
                started = false;
            }
            else
            {
                if (!started)
                {
                    StopCoroutine("ToOne");
                    StopCoroutine("ToZero");
                    StartCoroutine(ToZero(CarKurve));
                    StartCoroutine(ToOne(CarIdle));
                    started = true;
                }
                started1 = false;
                started2 = false;
            }
        }
        else
        {
            WhileDrifting();
        }
        multTimer += Time.deltaTime;
    }

    public void WhileDrifting()
    {
        float hori = Input.GetAxis("Horizontal");
        rot += -s * (hori+driftDir);
        driftTimer += Time.deltaTime;
    }

    public void StopDrifting()
    {
        isDrifting = false;
        s = starts;
        if(driftTimer > 1.4f)
        {
            Boost();
        }
        StopCoroutine("rotto");
        StartCoroutine(rotto(-1));
        Sparks.ForEach((s) => s.Stop());
        DriftSound.Stop();
    }

    public void StartDrift(float hori)
    {
        if (isDrifting && Mathf.Abs(hori) < .3f) return;
        driftTimer = 0;
        isDrifting = true;
        driftDir = hori>0?1:-1;
        s = starts * 1.35f;
        StopCoroutine("rotto");
        StartCoroutine(rotto());
        Sparks.ForEach((s) => s.Play());
        DriftSound.Play();

        if (!PBC) {
            Textbubble.addText("Please be careful", 1);
            PBC = true;
        }
    }

    bool PBC = false;

    IEnumerator rotto(int dir = 1)
    {
        for (int i = 0; i < 20; i++)
        {
            AutoModell.transform.Rotate(0, 1f * driftDir*dir, 0);
            yield return new WaitForSeconds(0.001f);
        }
        if(dir == -1)
        {
            AutoModell.transform.localRotation = Quaternion.Euler(new Vector3());
        }
    }

    IEnumerator d(float s = 7.5f)
    {
        yield return new WaitForSeconds(s);
        SceneManager.LoadScene("EndScreen");
    }

    public void Boost()
    {
        StartCoroutine(Boost1());
        if (!YNWD)
        {
            Textbubble.addText("Yeah, now we´re driving", 1.2f);
            YNWD = true;
        }
    }

    bool YNWD = false;

    public bool hasBoost;

    IEnumerator Boost1()
    {
        hasBoost = true;
        StartCoroutine(ToOne(BoostSound));
        P2.SetActive(true);
        P2.GetComponent<VisualEffect>().Play();
        for(int i = 0;i < 80; i++)
        {
            cam.fieldOfView += .3f;
            fSpeed -= 0.1f;

            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(2);
        P2.GetComponent<VisualEffect>().Stop();
        StartCoroutine(ToZero(BoostSound));
        for (int i = 0; i < 80; i++)
        {
            cam.fieldOfView -= .3f;
            fSpeed += 0.1f;
            yield return new WaitForSeconds(0.005f);
        }
        P2.SetActive(false);

        hasBoost = false;


    }

    public void Die()
    {
        die = true;
        Textbubble.addText("This was unavoidable", 2);
        StartCoroutine(scale());
        Destroy(GetComponent<Collider>());
        Destroy(GetComponent<Rigidbody>());
        Destroy(GetComponent<OnPlanetMovement>());
        Partikel.GetComponent<VisualEffect>().Play();
        Partikel.SetActive(true);
        DieSound.SetActive(true);
        CS cs = FindObjectOfType<CS>();
        cs.Dur = .1f;
        StartCoroutine(d(3));
    }

    public int multiplikator;
    public float multTimer;

    public void hf()
    {
        if (multTimer > 2)
        {
            multiplikator = 1;
        }
        else
        {
            multiplikator *= 2;
        }
        multTimer = 0;
        StopCoroutine("TSU");
        StartCoroutine(TSU());
        WorldGen WG = FindObjectOfType<WorldGen>();
        WorldGen.Score += 1 * multiplikator;
        Scoret.text = "Score: " + WorldGen.Score;
        WG.timer1 += 2;
        MultText.text = multiplikator + "x";
    }

    public Color MultColor;

    IEnumerator TSU() {

        for(int i = 0; i < 50; i++)
        {
            Scoret.transform.localScale = Vector3.Lerp(Scoret.transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), 0.1f);
            MultText.color = Color.Lerp(MultText.color, MultColor,0.05f);
            MultOb.GetComponent<Image>().color = Color.Lerp(MultText.color, MultColor,0.05f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 50; i++)
        {
            Scoret.transform.localScale = Vector3.Lerp(Scoret.transform.localScale, new Vector3(1,1,1), 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 50; i++)
        {
            MultText.color = Color.Lerp(MultText.color, new Color(0, 0, 0, 0), 0.05f);
            MultOb.GetComponent<Image>().color = Color.Lerp(MultText.color, new Color(0, 0, 0, 0), 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
    }



    IEnumerator ToZero(AudioSource s)
    {
        for (int i = 0; i < 25; i++)
        {
            if (s.volume == 0) break;
            
            yield return new WaitForSeconds(0.005f);
            s.volume -= 0.01f;
        }
        s.volume = 0;
    }

    IEnumerator ToOne(AudioSource s)
    {
        for (int i = 0; i < 25; i++)
        {
            if (s.volume == 0.25f) break;
            yield return new WaitForSeconds(0.005f);
            s.volume += 0.01f;
        }
        s.volume = 0.25f;
    }

    IEnumerator scale()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.001f);
            car.transform.localScale = car.transform.localScale * 0.8f;
        }
        Destroy(car);
    }

    private bool HasOrb;
    public GameObject OrbImage;
    public GameObject OrbImage2;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy" )
        {
            if (!hasBoost)
            {
                Die();
            }
            else
            {
                StartCoroutine(TSU2());
                WorldGen WG = FindObjectOfType<WorldGen>();
                WorldGen.Score += (450/50) * multiplikator;
            }
        }
        if(collision.transform.tag == "Die")
        {
            Die();
        }
        
    }


    IEnumerator Sup4(Vector3 v, bool b)
    {
        OrbImage.SetActive(b);
        for (int i = 0; i < 40;i++)
        {
            OrbImage.transform.localScale = Vector3.Lerp(OrbImage.transform.localScale, v, 0.1f);
            OrbImage2.transform.localScale = Vector3.Lerp(OrbImage.transform.localScale, v, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Orb")
        {
            if (!SO)
            {
                Textbubble.addText("This is a speed Orb", 1.5f);
                Textbubble.addText("We can use Them to speed us up", 1.5f);
            }
            SO = true;
            StartCoroutine(TSU2());
            WorldGen WG = FindObjectOfType<WorldGen>();
            WorldGen.Score += (200/50) * multiplikator;
            if (HasOrb)
            {
                Boost();
            }
            HasOrb = true;
            StopCoroutine("Sup4");
            StartCoroutine(Sup4(new Vector3(1, 1, 1), true));
            StartCoroutine(scale11(other.gameObject));
        }
    }

    bool SO = false;

    IEnumerator scale11(GameObject ob)
    {
        for (int i = 0; i < 200; i++)
        {
            yield return new WaitForSeconds(0.001f);
            ob.transform.localScale = ob.transform.localScale * 0.9f;
        }
        Destroy(ob);
    }

    IEnumerator TSU2()
    {
        for (int i = 0; i < 50; i++)
        {

            Scoret.transform.localScale = Vector3.Lerp(Scoret.transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 50; i++)
        {
            Scoret.transform.localScale = Vector3.Lerp(Scoret.transform.localScale, new Vector3(1, 1, 1), 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    //bUst Gegner eleminieren
}
