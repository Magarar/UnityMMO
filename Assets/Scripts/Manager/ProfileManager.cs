using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Manager
{
    public class ProfileManager : MonoBehaviour
    {
        public static ProfileManager Instance;

        public Slider hpBar;
        public Slider mpBar;
        public Slider spBar;
        public Slider xpBar;

        public TextMeshProUGUI xpTxt;
        public TextMeshProUGUI hpTxt;
        public TextMeshProUGUI mpTxt;
        public TextMeshProUGUI spTxt;
        
        public TextMeshProUGUI levelTxt;
        public TextMeshProUGUI preLevelTxt;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }
    }
}
