using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.Serialization;

namespace UIs
{
    public class Header : MonoBehaviour
    {
        public static Header Instance;
        
        public List<GameObject> gameUIHeader = new List<GameObject>();
        public List<Maps> mapHead = new ();
        public List<GameObject> cameraHead = new List<GameObject>();
        
        public Dictionary<int,Maps> mapDict = new Dictionary<int, Maps>();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
            
        }

        private void Start()
        {
            InitMapDict();
        }
        
        public void InitMapDict()
        {
            for (int i = 0; i < mapHead.Count; i++)
            {
                mapDict.Add(mapHead[i].mapID, mapHead[i]);
                mapDict[mapHead[i].mapID].mapIndex = i;
            }
        }
        
        public void OpenGameUIHeader(int menuIndex)
        {
            CloseGameUIHeader();
            gameUIHeader[menuIndex].SetActive(true);
        }

        public void CloseGameUIHeader()
        {
            foreach (var ob in gameUIHeader)
            {
                ob.SetActive(false);
            }
        }

        public void OpenMapHeader(int mapIndex)
        {
            CloseMapHeader();
            mapHead[mapIndex].gameObject.SetActive(true);
        }

        public void CloseMapHeader()
        {
            foreach (var ob in mapHead)
            {
                ob.gameObject.SetActive(false);
            }
        }
        
        public void OpenCameraHeader(int cameraIndex)
        {
            CloseCameraHeader();
            cameraHead[cameraIndex].SetActive(true);
        }
        
        public void CloseCameraHeader()
        {
            foreach (var ob in cameraHead)
            {
                ob.SetActive(false);
            }
        }
    }
}
