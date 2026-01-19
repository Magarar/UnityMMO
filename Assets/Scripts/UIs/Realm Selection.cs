using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    public class RealmSelection : MonoBehaviour
    {
        //realmlist
        public string realmlist_guid{get;set;} = string.Empty;
        
        public string realmlist_name{get;set;} = string.Empty;
        
        public string realmlist_ipaddress{get;set;} = string.Empty;
        
        public string realmlist_port{get;set;} = string.Empty;
        
        public string realmlist_flag{get;set;} = string.Empty;
        
        public string realmlist_dynamicFlag{get;set;} = string.Empty;
        
        public string realmlist_staticFlag{get;set;} = string.Empty;
        
        public string realmlist_online{get;set;} = string.Empty;
        
        public string realmlist_uptime{get;set;} = string.Empty;
        
        public string realmlist_restartTime{get;set;} = string.Empty;
        
       
        
        public Image HL;
        public TextMeshProUGUI serverName;
        
        public string realmName{get;set;} = string.Empty;

        public void SelectThisRealm()
        {
            HLManager.Instance.DisableRealmListHL();
            HL.enabled = true;
            
            GameManager.Instance.realmlist_guid = realmlist_guid;
            GameManager.Instance.realmlist_name = realmlist_name;
            GameManager.Instance.realmlist_ipaddress = realmlist_ipaddress;
            GameManager.Instance.realmlist_port = realmlist_port;
            GameManager.Instance.realmlist_flag = realmlist_flag;
            GameManager.Instance.realmlist_dynamicFlag = realmlist_dynamicFlag;
            GameManager.Instance.realmlist_staticFlag = realmlist_staticFlag;
            GameManager.Instance.realmlist_online = realmlist_online;
            GameManager.Instance.realmlist_uptime = realmlist_uptime;
            GameManager.Instance.realmlist_restartTime = realmlist_restartTime;
            
            LoginManager.Instance.loginRealmlistHeader.text = realmlist_name;
            
        }

        private void Start()
        {
            
        }
    }
}
