using TMPro;
using UnityEngine;

namespace Manager
{
    public class QueueManager : MonoBehaviour
    {
        public static QueueManager Instance;

        public GameObject queueHeader;

        public TextMeshProUGUI queueInformationTxt;

        public bool IsQueueOpen => queueHeader.activeSelf;

        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        public void OpenQueue(string queueMessage)
        {
            queueHeader.SetActive(true);
            queueInformationTxt.text = queueMessage;
            
        }

        public void ChangeServer()
        {
            queueHeader.SetActive(false);
            LoginManager.Instance.ChangeServerWhileQueue();
        }
    }
}
