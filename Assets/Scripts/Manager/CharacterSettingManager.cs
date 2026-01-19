using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Manager
{
    public class CharacterSettingManager : MonoBehaviour
    {
        public static CharacterSettingManager Instance;

        public List<TextMeshProUGUI> charactersTxt = new List<TextMeshProUGUI>();
        public List<GameObject> charactersPanel = new List<GameObject>();
        
        public List<GameObject> characterSettingOne = new List<GameObject>();
        public List<GameObject> characterSettingTwo = new List<GameObject>();
        [FormerlySerializedAs("characterSettingPanel")] public List<GameObject> characterPanelHeader = new List<GameObject>();

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }
        
        public void OpenCharacterSetting(GameObject o)
        {
            o.SetActive(true);
        }

        public void CloseCharacterSetting(GameObject o)
        {
            o.SetActive(false);
        }

        public void OpenCharactersPanel(int value)
        {
            foreach (var character in charactersPanel)
                character.SetActive(false);
            foreach (var character in charactersTxt)
                character.color = Color.white;
            charactersPanel[value].SetActive(true);
            charactersTxt[value].color = Color.red;
        }

        public void OpenPanelHeader(int value)
        {
            
        }

        public void CloseAllPanelHeader()
        {
            foreach (var p in characterPanelHeader)
                p.SetActive(false);
        }
    }
}
