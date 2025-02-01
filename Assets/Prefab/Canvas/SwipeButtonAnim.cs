using System;
using UnityEngine;
using UnityEngine.UI;

public class SwipeButtonAnim : MonoBehaviour
{
    Button button;
    Vector3 upScale = new Vector3(1.2f, 1.2f, 1f);

    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(Anim);
    }

    void Anim()
    {
        // Sequence kullanımı ile animasyonları sıraya koy
        LeanTween.sequence()
            .append(LeanTween.scale(gameObject, upScale, 0.1f).setIgnoreTimeScale(true))
            .append(LeanTween.scale(gameObject, Vector3.one, 0.1f).setIgnoreTimeScale(true));
    }
}

