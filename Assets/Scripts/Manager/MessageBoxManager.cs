using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Manager
{
    public class MessageBoxManager : MonoBehaviour
    {
        public static MessageBoxManager Instance;
        
        public GameObject messageBoxUIHeader;
        public List<GameObject> buttonActionList = new List<GameObject>();
        public TextMeshProUGUI messageBoxText;
        
        private Action ok_Callback;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        
        public void OpenMessageBox(string message,Action _ok_Callback = null)
        {
            messageBoxUIHeader.SetActive(true);
            messageBoxText.text = message;
            if (_ok_Callback != null)
            {
                ok_Callback = _ok_Callback;
                buttonActionList[0].SetActive(false);//ok
                buttonActionList[1].SetActive(true);//ok
                buttonActionList[2].SetActive(true);//cancel
            }
            else
            {
                buttonActionList[0].SetActive(true);//ok
                buttonActionList[1].SetActive(false);//ok
                buttonActionList[2].SetActive(false);//cancel
            }
                
            
        }
        
        
        
        public void Okay()
        {
            if (ok_Callback != null)
            {
                ok_Callback?.Invoke();
                ok_Callback = null;
            }
            messageBoxUIHeader.SetActive(false);
        }

        public void Cancel()
        {
            messageBoxUIHeader.SetActive(false);
            ok_Callback = null;
        }
    }
}
