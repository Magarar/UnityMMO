using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        public List<AudioClip> soundOst = new List<AudioClip>();
        public List<AudioClip> soundInterface = new List<AudioClip>();

        public AudioSource asOst;
        public AudioSource asInterface;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        public void PlayOst(int ostIndex)
        {
            asOst.clip = soundOst[ostIndex];
            asOst.Play();
        }

        public void PlayInterface(int interfaceIndex)
        {
            asInterface.clip = soundInterface[interfaceIndex];
            asInterface.Play();
        }
    }
}
