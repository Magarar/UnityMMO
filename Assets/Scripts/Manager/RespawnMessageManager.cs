using System;
using TMPro;
using UnityEngine;

namespace Manager
{
    public class RespawnMessageManager : MonoBehaviour
    {
        public static RespawnMessageManager Instance;

        public GameObject respawnHeader;
        public TextMeshProUGUI respawnInformation;
        
        public Action respawnCallback;
        

        public void Awake()
        {
            if  (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        public void RespawnSet(string message, Action callback)
        {
            respawnHeader.SetActive(true);
            respawnInformation.text = message;
            respawnCallback = callback;
        }

        public void RespawnClick()
        {
            respawnCallback?.Invoke();
            respawnHeader.SetActive(false);
        }
    }
}
