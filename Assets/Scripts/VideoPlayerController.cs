using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace ldjam_hellevator
{
    public class VideoPlayerController : MonoBehaviour
    {
        [SerializeField] private VideoPlayer _videoPlayer;

        void Awake()
        {
            _videoPlayer = GetComponent<VideoPlayer>();
            _videoPlayer.loopPointReached += OnMovieFinished;
        }
        
        void OnMovieFinished(VideoPlayer vp)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
