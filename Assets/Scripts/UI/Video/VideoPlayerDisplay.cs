using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerDisplay : MonoBehaviour
{
    [Header("Video Requirements")]
    [SerializeField] TextMeshProUGUI TotalMinutes;
    [SerializeField] TextMeshProUGUI TotalSeconds;
    [SerializeField] TextMeshProUGUI CurrentMinutes;
    [SerializeField] TextMeshProUGUI CurrentSeconds;
    [SerializeField] Image PlayPauseImage;
    [SerializeField] Sprite PlayIcon;
    [SerializeField] Sprite PauseIcon;
    [SerializeField] TextMeshProUGUI VideoDescription;
    

    VideoPlayer _videoplayer;

    void Awake()
    {
        _videoplayer = GetComponent<VideoPlayer>();
    }

    public void PlayPause()
    {
        if (_videoplayer.isPlaying)
        {
            _videoplayer.Pause();
            PlayPauseImage.sprite = PlayIcon;
        }
        else
        {
            _videoplayer.Play();
            PlayPauseImage.sprite = PauseIcon;
        }
    }

    void SetCurrentTime(){
        CurrentMinutes.text = Mathf.Floor((int)_videoplayer.time/60).ToString("00");
        CurrentMinutes.text = ((int)_videoplayer.time % 60).ToString("00");

    }
    void SetTotalTime(){
        TotalMinutes.text = Mathf.Floor((int)_videoplayer.clip?.length/60).ToString("00");
        TotalSeconds.text = ((int)_videoplayer.clip?.length % 60).ToString("00");
    }

}
