using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "VideoData", menuName = "Video/Data")]
public class VideoPlayerData : ScriptableObject
{
    public VideoClip Clip;
    public string ClipDescription;
}
