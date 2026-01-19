using System.Collections.Generic;
using Server;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class CharacterEquipmentManager : MonoBehaviour
    {
        public Authentication authentication;
        public World world;
        
        public static CharacterEquipmentManager Instance;
        
        public GameObject characterEquipmentHead;
        
        public TextMeshProUGUI hpText;
        public TextMeshProUGUI mpText;
        public TextMeshProUGUI spText;
        public TextMeshProUGUI exAtkText;
        public TextMeshProUGUI inAtkText;
        public TextMeshProUGUI accuracyText;
        public TextMeshProUGUI exDefText;
        public TextMeshProUGUI inDefText;
        public TextMeshProUGUI dodgeText;
        public TextMeshProUGUI criticalText;
        public TextMeshProUGUI fleeText;
        public TextMeshProUGUI aspdText;

        public List<TextMeshProUGUI> stats = new List<TextMeshProUGUI>();
        public List<Button> plusStat = new List<Button>();
        public List<Button> minusStat = new List<Button>();
        public List<Button> confirmAndCancel = new List<Button>();
        public TextMeshProUGUI property;

        public TextMeshProUGUI curExp;
        public TextMeshProUGUI levelUpExp;
        
        //system
        private void Awake()
        {
            if  (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        public void Increment_Stat(int value)
        {
            switch (value)
            {
                case 0:
                    world.TcpSendMessage($"INCREMENT_STAT STR",null);
                    break;
                case 1:
                    world.TcpSendMessage($"INCREMENT_STAT INTEL",null);
                    break;
                case 2:
                    world.TcpSendMessage($"INCREMENT_STAT VIT",null);
                    break;
                case 3:
                    world.TcpSendMessage($"INCREMENT_STAT AGI",null);
                    break;
                case 4:
                    world.TcpSendMessage($"INCREMENT_STAT OBS",null);
                    break;
            }
            OpenConfirmAndCancel();
        }
        
        public void Decrement_Stat(int value)
        {
            switch (value)
            {
                case 0:
                    world.TcpSendMessage($"DECREMENT_STAT STR",null);
                    break;
                case 1:
                    world.TcpSendMessage($"DECREMENT_STAT INTEL",null);
                    break;
                case 2:
                    world.TcpSendMessage($"DECREMENT_STAT VIT",null);
                    break;
                case 3:
                    world.TcpSendMessage($"DECREMENT_STAT AGI",null);
                    break;
                case 4:
                    world.TcpSendMessage($"DECREMENT_STAT OBS",null);
                    break;
            }
            OpenConfirmAndCancel();
        }

        public void OpenPlusMinusStat()
        {
            foreach (var ob in plusStat)
                ob.gameObject.SetActive(true);

            foreach (var ob in minusStat)
                ob.gameObject.SetActive(true);
        }

        public void ClosePlusMinusStat()
        {
            foreach (var ob in plusStat)
                ob.gameObject.SetActive(false);
            foreach (var ob in minusStat)
                ob.gameObject.SetActive(false);
        }

        public void OpenConfirmAndCancel()
        {
            foreach (var ob in confirmAndCancel)
                ob.gameObject.SetActive(true);
        }

        public void CloseConfirmAndCancel()
        {
            foreach (var ob in confirmAndCancel)
                ob.gameObject.SetActive(false);
        }

        public void Confirm_Stat()
        {
            world.TcpSendMessage("CONFIRM_STATS ",null);
            ClosePlusMinusStat();
        }

        public void Cancel_Stat()
        {
            world.TcpSendMessage("CANCEL_STATS ",null);
        }


    }
}
