using System.Collections.Generic;
using UnityEngine;

namespace ldjam_hellevator
{
    public class AudioManager : MonoBehaviour
    {
        public List<Sound> sounds;
        [SerializeField] private AudioChannel channel;

        private static AudioManager _instance;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            channel.OnAudioRequested += Play;
            channel.OnAudioStopped += Stop;
            channel.OnIsAudioPlaying = IsPlaying;

            foreach (var s in sounds)
            {
                var source = gameObject.AddComponent<AudioSource>();
                source.clip = s.clip;
                source.volume = s.volume;
                source.pitch = s.pitch;
                source.loop = s.loop;
                s.source = source;
            }

            DontDestroyOnLoad(gameObject);
        }

        public void Play(string soundName)
        {
            var s = sounds.Find(sound => sound.soundName == soundName);
            s.source.Play();
        }

        public void Stop(string soundName)
        {
            var s = sounds.Find(sound => sound.soundName == soundName);
            s.source.Stop();
        }

        public bool IsPlaying(string soundName)
        {
            var s = sounds.Find(sound => sound.soundName == soundName);
            return s.source.isPlaying;
        }

        private void OnDestroy()
        {
            channel.OnAudioRequested -= Play;
            channel.OnAudioStopped -= Stop;
            channel.OnIsAudioPlaying = null;

            foreach (var s in sounds)
            {
                s.source.Stop();
            }
        }
    }
}
