using UnityEngine;

namespace Manager
{
    public class CharacterCharactersManager : MonoBehaviour
    {
        public static CharacterCharactersManager Instance;
        
        public GameObject characterEquipmentHead;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }
    }
}
