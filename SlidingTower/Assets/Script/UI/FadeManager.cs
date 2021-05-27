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
        FadeOut(fadeImage, fadeOutTime);
    }

    public void FadeIn(Image image, float time)
    {
        image.CrossFadeAlpha(1, time, false);
    }

    public void FadeOut(Image image, float time)
    {
        image.CrossFadeAlpha(0, time, false);
    }
}
