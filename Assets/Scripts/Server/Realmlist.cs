using System;
using Manager;
using UIs;
using UnityEngine;
using UnityEngine.UI;

namespace Server
{
    public class Realmlist : MonoBehaviour
    {
        public RealmSelection realm;
        public Transform spawnRealmlistPosition;

        public Button backButton;

        public void SpawnRealmlist(string setRealmlist)
        {
            string[] r = setRealmlist.Split(' ');
            RealmSelection rs= Instantiate(realm, spawnRealmlistPosition,false);
            HLManager.Instance.allRealmList.Add(rs);
            rs.transform.localScale = Vector3.one;
            
            rs.realmlist_guid = r[2];
            rs.realmlist_name = r[3];
            rs.realmlist_ipaddress = r[4];
            rs.realmlist_port = r[5];
            rs.realmlist_flag = r[6];
            rs.realmlist_dynamicFlag = r[7];
            rs.realmlist_staticFlag = r[8];
            rs.realmlist_online = r[9];
            rs.realmlist_uptime = r[10];
            rs.realmlist_restartTime = r[11];
            
            rs.serverName.text = rs.realmlist_name;
            
        }

        public void ConfirmAcceptRealmlist()
        {
            if (GameManager.Instance.realmlist_guid == String.Empty)
            {
                MessageBoxManager.Instance.OpenMessageBox("Please select a realmlist");
            }
            else
            {
                Header.Instance.OpenGameUIHeader(2);
                LoginManager.Instance.userNameInput.Select();
                GameManager.Instance.gameSettings = 1;
            }
        }

        public void GoBack()
        {
            Header.Instance.OpenGameUIHeader(0);
            backButton.interactable = false;
        }
    }
}
