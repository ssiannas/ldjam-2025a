using System;
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
            _videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "Opening_Animation.mp4");
            _videoPlayer.loopPointReached += OnMovieFinished;
            _videoPlayer.started += PlayAudio;
        }

        private void Start()
        {
            _videoPlayer.Prepare();
            _videoPlayer.Play();
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
