using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndUI : MonoBehaviour
{
    public TextMeshProUGUI DidorDidN;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Factory;
    public TextMeshProUGUI Time;

    public GameObject lLoad;
    private void Start()
    {
        string str = "";
        int fC = (int)WorldGen.facCount;
        switch (fC)
        {
            case (0):
                str = "You did it. You saved Earth!. \nYou can be proud of yourself!";
                break;
            case (1):
            case (2):
            case (3):
                str = "You were almost there. \nUnfortunately you did not succeed:(\nNext time you´ll be better";
                break;
            default:
                str = "You were not able to save us.\n We are all dead, due to your lack of skills!";
                break;
        }
        bool eines = false;
        str += "\n";
        if(WorldGen.Timer2 <= 0)
        {
            str += "\nYou lost, because no time was left.";
            eines = true;
        }
        if(fC != 0)
        {
            str += "\nYou lost, because you didn´t destroy all Factories";
        }
        if(eines == false && fC != 0)
        {
            str += "\nYou lost, because you crashed into something";
        }
        str += "\n\nThis Game was made for the LudumDare 50. If you liked this Game, pleace rate it there:)";
        DidorDidN.text = str;

        float t = (int)(WorldGen.realTimer / 60);
        float sec = 60 * ((WorldGen.realTimer / 60) - t);
        Time.text = $"{t}:{(sec < 10 ? "0" : "")}{(int)sec}";

        Score.text = WorldGen.Score.ToString();
        Factory.text = WorldGen.facCount.ToString();
    }

    public void l()
    {
        lLoad.SetActive(true);
    }
}
