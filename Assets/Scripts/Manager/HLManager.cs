using System.Collections.Generic;
using UIs;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class HLManager : MonoBehaviour
    {
        public static HLManager Instance;
        //Realmlist
        public List<RealmSelection> allRealmList = new List<RealmSelection>();
        //CreateToon
        public List<CreateToonSelection> allCreateToonList = new List<CreateToonSelection>();
        //Toon
        public List<ToonSelection> allToonList = new List<ToonSelection>();

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

        public void DisableRealmListHL()
        {
            foreach (var r in allRealmList)
            {
                if(r!=null)
                    r.HL.enabled = false;
            }
            
        }

        public void ClearRealmList()
        {
            foreach (var r in allRealmList)
            {
                if(r!=null)
                    Destroy(r.gameObject);
            }
            allRealmList.Clear();
        }
        
        //Character
        public void DisableCreateToonListHL()
        {
            
        }

        public void ClearCreateToonList()
        {
            foreach (var r in allCreateToonList)
            {
                if(r!=null)
                    Destroy(r.gameObject);
            }
            allCreateToonList.Clear();
        }

        public void DisableToonListHL()
        {
            
        }

        public void SelectFirstCreateToon()
        {
            if (allCreateToonList.Count > 0)
            {
                allCreateToonList[0].gameObject.GetComponent<Button>()?.onClick.Invoke();
            }
        }

        public void ClearToonList()
        {
            foreach (var r in allToonList)
            {
                if(r!=null)
                    Destroy(r.gameObject);
            }
            allToonList.Clear();
        }

        public void SelectFirstToon()
        {
            if (allToonList.Count > 0)
                allToonList[0].gameObject.GetComponent<Button>()?.onClick.Invoke();
        }
    }
}
