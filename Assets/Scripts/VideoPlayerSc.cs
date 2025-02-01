using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoPlayerSc : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();

        if (videoPlayer != null && audioSource != null)
        {
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.SetTargetAudioSource(0, audioSource);

            videoPlayer.loopPointReached += OnVideoEnd;

            videoPlayer.Play();
        }
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        gameObject.SetActive(false);
    }

    public void OnSkipButtonClicked()
    {
        videoPlayer.Stop();
        gameObject.SetActive(false);
    }
}