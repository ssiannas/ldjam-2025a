using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace ldjam_hellevator
{
    public class VideoPlayerController : MonoBehaviour
    {
        [SerializeField] private VideoPlayer _videoPlayer;
        [SerializeField] private AudioChannel _audioChannel;
        void Awake()
        {
            _videoPlayer = GetComponent<VideoPlayer>();
            _videoPlayer.loopPointReached += OnMovieFinished;
            _videoPlayer.started += PlayAudio;
        }
        
        void PlayAudio(VideoPlayer vp)
        {
            _audioChannel.PlayAudio(SoundNames.OpeningTheme);
        }
        void OnMovieFinished(VideoPlayer vp)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
