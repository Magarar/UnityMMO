using System;
using Server;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class LoadingManager : MonoBehaviour
    {
        public static LoadingManager Instance;
        
        public Authentication authentication;
        public World world;
        
        public Slider loadingProgress;

        public bool isProgressBarFull => loadingProgress.value >= loadingProgress.maxValue;

        private void Awake()
        {
            if  (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        public void ResetProgressBar()
        {
            loadingProgress.value = 0;
        }
        
        public void SetLoadingProgressMax(int max)
        {
            loadingProgress.maxValue = max;
        }

        public void SetLoadingProgressMin(int min)
        {
            loadingProgress.value += min;
        }
    }
}
