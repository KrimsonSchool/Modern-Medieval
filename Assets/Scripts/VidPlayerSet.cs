using UnityEngine;
using UnityEngine.Video;

public class VidPlayerSet : MonoBehaviour
{
    VideoPlayer videoPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(videoPlayer.targetCamera == null)
        {
            videoPlayer.targetCamera = Camera.main;
        }
    }
}
