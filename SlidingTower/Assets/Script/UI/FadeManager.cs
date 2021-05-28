using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;

    public float fadeInTime;
    public float fadeOutTime;

    public Image fadeImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Multiple FadeManager");
        }
    }

    private void Start()
    {
        FadeOut(fadeImage, fadeOutTime, false);
    }

    public void FadeIn(Image image, float time, bool state)
    {
        image.CrossFadeAlpha(1, time, state);
    }

    public void FadeOut(Image image, float time, bool state)
    {
        image.CrossFadeAlpha(0, time, state);
    }
}
