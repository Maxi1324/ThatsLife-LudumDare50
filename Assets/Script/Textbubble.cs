using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Textbubble : MonoBehaviour
{
    public static List<TextInfo> Texts = new List<TextInfo>();

    public TextMeshProUGUI Text;

    private void Start()
    {
        Texts.Clear();
        StartCoroutine(dings());
    }


    private void Update()
    {
      
    }

    public static void addText(string str, float t)
    {

        Texts.Add(new TextInfo() { Text = str, showTime = t });
    }

    IEnumerator dings()
    {
        while (true)
        {
            if(Texts.Count > 0)
            {
                if (gameObject.transform.localScale.y != 1)
                {
                    gameObject.transform.localScale = new Vector3();
                    for(int j = 0; j < 20;j++)
                    {
                        gameObject.transform.localScale += new Vector3(0.05f, 0.05f, 0.05f);
                        yield return new WaitForSeconds(0.01f);
                    }
                    gameObject.transform.localScale = new Vector3(1, 1, 1);
                }
                TextInfo i = Texts[0];
                Text.text = i.Text;
                yield return new WaitForSeconds(i.showTime);
                Texts.Remove(i);
            }
            else
            {
                gameObject.transform.localScale *= 0.9f;
            }
            yield return null;
        }

    }
}

public struct TextInfo{

    public string Text;
    public float showTime;

}
