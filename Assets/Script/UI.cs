using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI TextField;
    public GameObject TFG;
    public GameObject RSButtons;
    public GameObject OKNOK;
    public GameObject No;

    public GameObject Fadeout;


    string startText;

    private void Start()
    {
        TFG.gameObject.SetActive(false);
        RSButtons.SetActive(true);
        OKNOK.SetActive(false);
        startText = TextField.text;
    }

    public void StartGame()
    {
        TFG.gameObject.SetActive(true);
        RSButtons.SetActive(false);
        OKNOK.SetActive(true);
        No.SetActive(true);
        RSButtons.SetActive(true);
        TextField.text = startText;
    }

    public void OK()
    {
        if (!c)
        {
            TextField.text = "great";
            StartCoroutine(loadScene());
        }
        else
        {
            c = false;
            TFG.gameObject.SetActive(false);
            RSButtons.SetActive(true);

        }
    }

    public void NotOk()
    {
        TextField.text = "Come On, Dont be such a Putin.\nSometimes you have to \n\"just do it!\"\n\n So, are you in?";
        No.SetActive(false);

    }

    bool c = false;

    public void ShowControls()
    {
        c = true;
        TFG.gameObject.SetActive(true);
        TextField.text = "You can drive control\n you turning with A and D.\n\n With the left Mouse Button\n you can use the Boost\n, is you have one.\n\n With the right Mouse Button\n you can drift. It's \nlike drifting in Mario Kart";
        OKNOK.SetActive(true);
        No.SetActive(false);
        RSButtons.SetActive(false);

    }

    public IEnumerator loadScene()
    {
        yield return new WaitForSeconds(.3f);
        Fadeout.SetActive(true);
    }
}
