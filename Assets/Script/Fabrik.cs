using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Fabrik : MonoBehaviour
{
    public GameObject Partikel;
    public GameObject audio;

    void Start()
    {
        transform.Rotate(0, Random.Range(0,360), 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static bool ein = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FindObjectOfType<WorldGen>().obs.Remove(transform.parent.gameObject);
            if(!ein) Textbubble.addText("Yeah", 0.2f);
            ein = true;
            StartCoroutine(scale());
            Destroy(GetComponent<Collider>());
            Partikel.SetActive(true);
            Partikel.GetComponent<VisualEffect>().Play();
            audio.SetActive(true);
            CS cs = FindObjectOfType<CS>();
            cs.Dur = .1f;

            StartCoroutine(TSU2());

            collision.gameObject.GetComponent<Player>().hf();
        }
    }

    IEnumerator scale()
    {
        for (int i = 0; i < 200; i++)
        {
            yield return new WaitForSeconds(0.001f);
            transform.localScale = transform.localScale * 0.8f;
        }
        Destroy(transform.parent.gameObject);
    }


    IEnumerator TSU2()
    {
        WorldGen wg = FindObjectOfType<WorldGen>();
        for (int i = 0; i < 50; i++)
        {

            wg.Fac.transform.localScale = Vector3.Lerp(wg.Fac.transform.localScale, new Vector3(1.3f, 1.3f, 1.3f), 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < 50; i++)
        {
            wg.Fac.transform.localScale = Vector3.Lerp(wg.Fac.transform.localScale, new Vector3(1, 1, 1), 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
