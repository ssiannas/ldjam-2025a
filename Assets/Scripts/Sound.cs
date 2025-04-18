using UnityEngine;

namespace ldjam_hellevator
{
    [System.Serializable]
    public class Sound
    {
        [HideInInspector] public AudioSource source;

        public AudioClip clip;
        public string soundName;
        [Range(0f, 1f)] public float volume = 0.7f;
        [Range(0f, 3f)] public float pitch = 1f;
        public bool loop = false;
    }
}
