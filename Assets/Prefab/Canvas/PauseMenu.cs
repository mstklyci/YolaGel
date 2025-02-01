using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu, bilgiMenu;
    [SerializeField] RectTransform pausePanelRect, pauseButtonRect, bilgiPanelRect, bilgiButtonRect, baslatButtonRect;
    [SerializeField] private float topPosY, middlePosY, signbottomPosY, signtopPosY, bilgiTopPosY, bilgiMiddlePosY;
    [SerializeField] private float tweenDuration;
    [SerializeField] CanvasGroup canvasGroup; // Pause menu background canvas group
    [SerializeField] CanvasGroup bilgiCanvasGroup; // Bilgi panel background canvas group
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        AudioManager.instance.musicSource.UnPause();
    }

    public void PauseMenuAcma()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        AudioListener.volume = 0;
        PausePanelIntro();
    }

    public void Home()
    {
        //AudioManager.instance.musicSource.UnPause();
        SceneManager.LoadScene("Main Menu");
        AudioListener.volume = 1;
        Time.timeScale = 1;
    }

    public async void Devam()
    {
        PausePanelOutro(); // Animasyonu başlat.
        await Task.Delay((int)(tweenDuration * 1000)); // Animasyon süresine eşdeğer gecikme.
        pauseMenu.SetActive(false); // Animasyon bittikten sonra menüyü kapat.
        Time.timeScale = 1;
        AudioListener.volume = 1;
    }

    public void YenidenOyna()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioListener.volume = 1;
        Time.timeScale = 1;
    }

    public void BilgiPanelAcma()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        bilgiMenu.SetActive(true);
        Time.timeScale = 0;
        BilgiPanelIntro();
    }

    public async void BilgiPanelKapatma()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        BilgiPanelOutro(); // Animasyonu başlat.
        await Task.Delay((int)(tweenDuration * 1000)); // Animasyon süresine eşdeğer gecikme.
        bilgiMenu.SetActive(false); // Animasyon bittikten sonra menüyü kapat.
        Time.timeScale = 1;
    }

    void PausePanelIntro()
    {
        canvasGroup.DOFade(1, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.OutQuad);

        pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.OutBack);

        pauseButtonRect.DOAnchorPosX(-243, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.OutQuad);

        bilgiButtonRect.DOAnchorPosX(-210, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.OutQuad);

        baslatButtonRect.DOAnchorPosX(300, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.OutQuad);
        

        bilgiPanelRect.DOAnchorPosY(bilgiMiddlePosY, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.OutBack);
    }

    void PausePanelOutro()
    {
        canvasGroup.DOFade(0, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.InQuad);

        pausePanelRect.DOAnchorPosY(topPosY, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.InBack);

        pauseButtonRect.DOAnchorPosX(241, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.InQuad);

        bilgiButtonRect.DOAnchorPosX(191, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.InQuad);

        baslatButtonRect.DOAnchorPosX(-40, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.InQuad);
        

        bilgiPanelRect.DOAnchorPosY(bilgiTopPosY, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.InBack);
    }

    void BilgiPanelIntro()
    {
        bilgiCanvasGroup.DOFade(1, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.OutQuad);

        bilgiPanelRect.DOAnchorPosY(bilgiMiddlePosY, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.OutBack);
        pauseButtonRect.DOAnchorPosX(-243, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.OutQuad);

        bilgiButtonRect.DOAnchorPosX(-210, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.OutQuad);

        baslatButtonRect.DOAnchorPosX(300, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.OutQuad);
        
        
    }

    void BilgiPanelOutro()
    {
        bilgiCanvasGroup.DOFade(0, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.InQuad);

        bilgiPanelRect.DOAnchorPosY(bilgiTopPosY, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.InBack);
        pauseButtonRect.DOAnchorPosX(241, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.InQuad);

        bilgiButtonRect.DOAnchorPosX(191, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.InQuad);

        baslatButtonRect.DOAnchorPosX(-40, tweenDuration)
            .SetUpdate(true)
            .SetEase(Ease.InQuad);
        
    }
}




