using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    public GameObject[] tutorialPanels; // Tutorial paneller dizisi
    public GameObject winPanel;         // Win panel referansı
    public GameObject pauseMenu;        // Duraklatma ekranı
    public GameObject looseMenu;        // Kaybetme ekranı
    public Button pauseRestartButton;   // Duraklatma ekranındaki restart butonu
    public Button looseRestartButton;   // Loose ekranındaki restart butonu
    public Button winRestartButton;     // Win ekranındaki restart butonu

    private int currentPanelIndex = 0; // Hangi panelin aktif olduğunu takip eder
    private bool hasWon = false;       // Win durumunu takip eder

    public List<DragObject> dragObjects;

    private void Start()
    {
        // Başlangıçta tüm panelleri devre dışı bırak
        foreach (GameObject panel in tutorialPanels)
        {
            panel.SetActive(false);
        }

        // Win, pause ve loose ekranlarını kapat
        winPanel.SetActive(false);
        pauseMenu.SetActive(false);
        looseMenu.SetActive(false);

        int tutorial = PlayerPrefs.GetInt("Tutorial", 0);

        if (tutorial == 0)
        {
            foreach (var item in dragObjects)
            {
                item.enabled = false;
            }
            ShowPanel(0);
        }

        // Tutorial panellerini yalnızca oyun kazanılmadıysa göster
        if (!hasWon)
        {
            //ShowPanel(0);
        }

        // Restart butonlarını dinle
        pauseRestartButton.onClick.AddListener(RestartLevel);
        looseRestartButton.onClick.AddListener(RestartLevel);
        winRestartButton.onClick.AddListener(RestartLevelFromWin);
    }

    private void Update()
    {
        // Eğer win durumuna ulaşılmışsa tutorial panelleri kontrol etmeye gerek yok
        if (hasWon) return;

        // Tıklama algılama
        if (Input.GetMouseButtonDown(0) && PlayerPrefs.GetInt("Tutorial") == 0)
        {
            NextPanel();
        }


    }

    private void ShowPanel(int index)
    {
        if (index >= 0 && index < tutorialPanels.Length)
        {
            tutorialPanels[index].SetActive(true);
        }
    }

    private void HidePanel(int index)
    {
        if (index >= 0 && index < tutorialPanels.Length)
        {
            tutorialPanels[index].SetActive(false);
        }
    }

    private void NextPanel()
    {
        // Şu anki paneli kapat
        HidePanel(currentPanelIndex);

        // Bir sonraki panele geç
        currentPanelIndex++;

        // Eğer son paneli geçtiysek tüm panelleri kapalı bırak
        if (currentPanelIndex >= tutorialPanels.Length)
        {
            foreach (var dragObject in dragObjects)
            {
                dragObject.enabled = true;
            }
            return;
        }

        // Yeni paneli göster
        ShowPanel(currentPanelIndex);
    }

    public void WinLevel()
    {
        // Win durumuna geçtiğimizi belirt
        hasWon = true;

        // Tüm tutorial panellerini kapat
        foreach (GameObject panel in tutorialPanels)
        {
            panel.SetActive(false);
        }

        // Win paneli aktif et
        winPanel.SetActive(true);

        // Pause ve Loose menülerini kapat
        pauseMenu.SetActive(false);
        looseMenu.SetActive(false);
    }

    public void RestartLevel()
    {
        if (hasWon)
        {
            // Eğer oyun kazanılmışsa tutorial panellerini açma
            foreach (GameObject panel in tutorialPanels)
            {
                panel.SetActive(false);
            }

            // Win panelini kapat
            winPanel.SetActive(false);

            // Pause ve Loose ekranlarını kapat
            pauseMenu.SetActive(false);
            looseMenu.SetActive(false);

            // Level'i sıfırla (tutorial açılmadan)
            currentPanelIndex = 0;
            return;
        }

        // Eğer oyun kazanılmamışsa tutorial panellerini sıfırla
        currentPanelIndex = 0;
        foreach (GameObject panel in tutorialPanels)
        {
            panel.SetActive(false);
        }

        // İlk tutorial panelini yeniden göster
        ShowPanel(0);

        // Pause ve Loose ekranlarını kapat
        pauseMenu.SetActive(false);
        looseMenu.SetActive(false);
        winPanel.SetActive(false); // Win paneli kapat
    }

    public void RestartLevelFromWin()
    {
        // Win durumunda leveli sıfırla ama tutorial açma

        foreach (GameObject panel in tutorialPanels)
        {
            panel.SetActive(false);
        }

        // Pause, Loose ve Win ekranlarını kapat
        pauseMenu.SetActive(false);
        looseMenu.SetActive(false);
        winPanel.SetActive(false);

        // Level sıfırlandı ama tutorial açılmamalı
        currentPanelIndex = 0;
    }
}
