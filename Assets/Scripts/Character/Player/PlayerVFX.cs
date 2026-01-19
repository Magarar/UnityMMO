using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Player
{
    public class PlayerVFX : MonoBehaviour
    {
        public PlayerManager playerManager;
        public PlayerNet playerNet;

        public List<ParticleSystem> myPar = new();

        public void OpenParticleSystem(int particleIndex)
        {
            myPar[particleIndex].gameObject.SetActive(true);
            myPar[particleIndex].Play();
            
            StartCoroutine(WaitForPlayingVFX(1f,(() =>
            {
                myPar[particleIndex].gameObject.SetActive(false);
                myPar[particleIndex].Stop();
            })));
        }

        private IEnumerator WaitForPlayingVFX(float t, Action vfx_callback)
        {
            yield return new WaitForSeconds(t);
            vfx_callback?.Invoke();
        }

    }
}
