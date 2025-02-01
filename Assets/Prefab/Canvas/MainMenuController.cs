using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject SeviyelerMenu;
    public GameObject AyarlarMenu;
    public GameObject YapimcilarMenu;
    public GameObject BasarimlarMenu;
    public GameObject CikisMenu;

    [SerializeField] RectTransform SeviyelerAnaPanelRect, AyarlarAnaPanelRect, YapimcilarAnaPanelRect, BasarimlarAnaPanelRect, CikisAnaPanelRect;
    [SerializeField] private float topPosY, middlePosY;
    [SerializeField] private float tweenDuration;
    [SerializeField] CanvasGroup seviyelerBackgroundCanvas, ayarlarBackgroundCanvas, yapimcilarBackgroundCanvas, basarimlarBackgroundCanvas, cikisBackgroundCanvas;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.musicSource.UnPause();
    }

    public void SeviyelerButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        ShowPanel(SeviyelerMenu, SeviyelerAnaPanelRect, seviyelerBackgroundCanvas);
    }

    public void AyarlarButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        ShowPanel(AyarlarMenu, AyarlarAnaPanelRect, ayarlarBackgroundCanvas);
    }

    public void YapimcilarButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        ShowPanel(YapimcilarMenu, YapimcilarAnaPanelRect, yapimcilarBackgroundCanvas);
    }

    public void BasarimlarButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        ShowPanel(BasarimlarMenu, BasarimlarAnaPanelRect, basarimlarBackgroundCanvas);
    }

    public void CikisSoruButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        ShowPanel(CikisMenu, CikisAnaPanelRect, cikisBackgroundCanvas);
    }

    public async void SeviyeGeriButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        await HidePanel(SeviyelerAnaPanelRect, seviyelerBackgroundCanvas);
        SeviyelerMenu.SetActive(false);
    }

    public async void AyarlarGeriButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        await HidePanel(AyarlarAnaPanelRect, ayarlarBackgroundCanvas);
        AyarlarMenu.SetActive(false);
    }

    public async void YapimcilarGeriButton()
    {
        await HidePanel(YapimcilarAnaPanelRect, yapimcilarBackgroundCanvas);
        YapimcilarMenu.SetActive(false);
    }

    public async void BasarimlarGeriButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        await HidePanel(BasarimlarAnaPanelRect, basarimlarBackgroundCanvas);
        BasarimlarMenu.SetActive(false);
    }

    public async void CikisHayirButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        await HidePanel(CikisAnaPanelRect, cikisBackgroundCanvas);
        CikisMenu.SetActive(false);
    }

    public void CikisEvetButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        Application.Quit();
    }

    private void ShowPanel(GameObject menu, RectTransform panelRect, CanvasGroup canvasGroup)
    {
        menu.SetActive(true);
        canvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        panelRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true);
    }

    private async Task HidePanel(RectTransform panelRect, CanvasGroup canvasGroup)
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        canvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
        await panelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true).AsyncWaitForCompletion();
    }

    public void CutSceneLoader()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        AudioManager.instance.musicSource.Pause();
        SceneManager.LoadScene("CutScene");
    }
}

