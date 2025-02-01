// File: VideoSceneController.cs
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneController : MonoBehaviour
{
    public string nextSceneName; // Bir sonraki sahnenin ismini buraya yazın veya Inspector'dan atayın.
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Video Player bileşenini al
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null)
        {
            // Video bittiğinde tetiklenecek olayı bağla
            videoPlayer.loopPointReached += OnVideoEnd;
        }
        else
        {
            Debug.LogError("Video Player bileşeni bulunamadı!");
        }
    }

    // Video bittiğinde çağrılacak fonksiyon
    void OnVideoEnd(VideoPlayer vp)
    {
        // Sahneyi yükle
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Sonraki sahne adı belirtilmemiş!");
        }
    }
}

