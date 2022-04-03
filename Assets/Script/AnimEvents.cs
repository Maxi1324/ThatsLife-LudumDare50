using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimEvents : MonoBehaviour
{
    public void loadScene(string str)
    {
        SceneManager.LoadScene(str);
    }

    public void elfDestroy()
    {
        Destroy(gameObject);
    }
}
