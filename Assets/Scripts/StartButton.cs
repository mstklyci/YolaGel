using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private bool AlltrafficSignFull;
    [SerializeField] private Button startBtn;
    private Image buttonImage;
    [SerializeField] private Sprite startOff,startOn;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        buttonImage = startBtn.GetComponent<Image>();
        buttonImage.sprite = startOff;
    }
    public void startButton()
    {
        AlltrafficSignFull = true;

        GameObject[] dropZones = GameObject.FindGameObjectsWithTag("DropZone");
        foreach (GameObject dropZone in dropZones)
        {
            TrafficSign dropZoneSc = dropZone.GetComponent<TrafficSign>();
            if (dropZoneSc != null && dropZoneSc.empty == true)
            {
                AlltrafficSignFull = false;
                break;
            }
        }

        if (AlltrafficSignFull)
        {
            audioManager.PlaySFX(audioManager.playSound);

            GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");

            foreach (GameObject car in cars)
            {
                Car carMoveSc = car.GetComponent<Car>();

                if (carMoveSc != null)
                {
                    carMoveSc.enabled = true;
                }
            }

            GameObject[] humans = GameObject.FindGameObjectsWithTag("Human");

            foreach (GameObject human in humans)
            {
                Human humanMoveSc = human.GetComponent<Human>();

                if (humanMoveSc != null)
                {
                    humanMoveSc.enabled = true;
                }
            }

            GameObject[] signs = GameObject.FindGameObjectsWithTag("Sign");
            foreach (GameObject sign in signs)
            {
                DragObject dragObject = sign.GetComponent<DragObject>();
                if (dragObject != null)
                {
                    dragObject.gameStarted = true;
                }
            }

            buttonImage.sprite = startOn;
        }
        else
        {
            audioManager.PlaySFX(audioManager.buttonClick);
        }
    }

    public void NetLevelButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void RestartLevel()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        AchievementManager.Instance.IncreaseTryCount(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        SceneManager.LoadScene(0);
    }

    public void LastLevelButton()
    {
        audioManager.PlaySFX(audioManager.buttonClick);
        AudioManager.instance.musicSource.Pause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}