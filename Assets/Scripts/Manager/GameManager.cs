using System;
using Character.Player;
using Unity.VisualScripting;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        
        //acount
        public string account_guid{get;set;} = string.Empty;
        
        public string account_username{get;set;} = string.Empty;
        
        public string account_password{get;set;} = string.Empty;
        
        public string account_email{get;set;} = string.Empty;
        
        public string account_ipaddress{get;set;} = string.Empty;
        
        public string account_oldIpaddress{get;set;} = string.Empty;
        
        public string account_newIpaddress{get;set;} = string.Empty;
        
        public string account_security{get;set;} = string.Empty;
        
        public string account_emberssion{get;set;} = string.Empty;
        
        public string account_expansion{get;set;} = string.Empty;
        
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
        
        //Characters
        public string characters_guid { get; set;}
    
        public string characters_account {get; set;}
    
        public string characters_realm {get; set;}
    
        public string characters_name {get; set;}
    
        public string characters_race {get; set;}
    
        public string characters_class_ {get; set;}
    
        public string characters_gender {get; set;}
    
        public string characters_level {get; set;}
    
        public string characters_xp {get; set;}
    
        public string characters_money {get; set;}
    
        public string characters_positionX  {get; set;}
    
        public string characters_positionY  {get; set;}
    
        public string characters_positionZ  {get; set;}
    
        public string characters_rotation  {get; set;}
    
        public string characters_map  {get; set;}
    
        public string characters_dungeonDifficulty  {get; set;}
    
        public string characters_online  {get; set;}
    
        public string characters_totaltime  {get; set;}
    
        public string characters_leveltime  {get; set;}
    
        public string characters_canvasHp_Header  {get; set;}

        public string characters_createDate;
        
        //character stat
        public string characterStat_cid { get; set; }
        
        public string characterStat_str{ get; set; }
        
        public string characterStat_intel{ get; set; }
        
        public string characterStat_vit{ get; set; }
        
        public string characterStat_agi{ get; set; }
        
        public string characterStat_obs { get; set; }
        
        public string characterStat_property { get; set; }
        
        public float characterStat_attack_time { get; set; }
        
        public float characterStat_wait_time { get; set; }
        
        public float characterStat_reload_time { get; set; }
        
        //playerAttackTime
        public float attackTime;
        public float waitTime;
        public float reloadTime;
        
        
        /// <summary>
        /// check if characters_race_enum doesn't eqaul to 999
        /// </summary>
        public int characters_race_enum { get; set; } = 999;
        
        /// <summary>
        /// max number for create character
        /// </summary>
        public int MAX_CREATE_CHARACTER_COUNT { get; set; } = 6;
        
        
        /// <summary>
        /// use to look everythings while chat window is actived
        /// </summary>
        public bool IsChatting {get; set;}

        
        /// <summary>
        /// use to send packet request later
        /// </summary>
        public int curUseChannel { get; set; }
        
        /// <summary>
        /// use for store ChatInput.Text
        /// </summary>
        public string currentWhoWhisper { get; set; } = string.Empty;
        

        /// <summary>
        /// get class type
        /// </summary>
        /// <param name="classID"></param>
        /// <returns></returns>
        public string GetClassType(int classID)
        {
            switch (classID)
            {
                case 0:
                    return "Free";
                case 1:
                    return "TungTungTungSahur";
                case 2:
                    return "TralaleroTralala";
                case 3:
                    return "Liangziliangzi Zheyikuainayikuai Bigweidai";
                case 4:
                    return "Lirili Larila";
                case 5:
                    return "Cappuccino Assassino";
                case 6:
                    return "Shimpanzini Bananini";
                case 7:
                    return "CbLaiLou";
                default:
                    return "Free";
                
            }
        }

        /// <summary>
        /// get cur name type color text
        /// </summary>
        /// <param name="nameType"></param>
        /// <returns></returns>
        public Color GetColorNameType(int nameType)
        {
            switch (nameType)
            {
                case 0:
                    return Color.white;
                case 1:
                    return Color.green;
                case 2:
                    return Color.yellow;
                case 3:
                    return Color.blue;
                case 4:
                    return Color.red;
                case 5:
                    return Color.red;
            }
            return Color.white;
        }

        public string GetWhatNpcUseType(int useType)
        {
            switch (useType)
            {
                case 0:
                    return "Guard";
                case 1:
                    return "QuestHelper";
                case 2:
                    return "Vending";
                case 3:
                    return "Teleporter";
                case 4:
                    return "Schooler";
                case 5:
                    return "Normal Creature";
            }
            return string.Empty;
        }

        /// <summary>
        /// get item color by item common
        /// </summary>
        /// <param name="itemCommon"></param>
        /// <returns></returns>
        public Color32 GetItemCommonColor(int itemCommon)
        {
            switch (itemCommon)
            {
                case 0://poor
                    return new Color32(157,157,157,255);
                case 1://common
                    return new Color32(255,255,255,255);
                case 2://uncommon
                    return new Color32(0,255,0,255);
                case 3://rare
                    return new Color32(0,0,255,255);
                case 4://epic
                    return new Color32(255,0,255,255);
                case 5://legendary
                    return new Color32(255,128,0,255);
                case 6://artifact
                    return new Color32(230,204,128,255);
                case 7://heirloon
                    return new Color32(0,204,255,255);
                case 8://wow taken
                    return new Color32(0,204,255,255);
            }
            return new Color32(255,255,255,255);
        }
        
        /// <summary>
        /// get item common name by item common
        /// </summary>
        /// <param name="itemCommon"></param>
        /// <returns></returns>
        public string GetItemCommonName(int itemCommon)
        {
            switch (itemCommon)
            {
                case 0://poor
                    return "Poor";
                case 1://common
                    return "Common";
                case 2://uncommon
                    return "Uncommon";
                case 3://rare
                    return "Rare";
                case 4://epic
                    return "Epic";
                case 5://legendary
                    return "Legendary";
                case 6://artifact
                    return "Artifact";
                case 7://heirloon
                    return "Heirloon";
                case 8://wow taken
                    return "WowTaken";
            }
            return "Common";
        }

        /// <summary>
        /// get item increase color by item common
        /// </summary>
        /// <param name="itemCommon"></param>
        /// <param name="itemIncrease"></param>
        /// <returns></returns>
        public string GetItemRichTextColorByCommon(int itemCommon,string header,int itemIncrease,string ending)
        {
            switch (itemCommon)
            {
                case 0://poor
                    return $"<color=#9D9D9D>{header} {itemIncrease} {ending}</color>";
                case 1://common
                    return $"<color=#FFFFFF>{header} {itemIncrease} {ending}</color>";
                case 2://uncommon
                    return $"<color=#1EFF00>{header} {itemIncrease} {ending}</color>";
                case 3://rare
                    return $"<color=#0070DD>{header} {itemIncrease} {ending}</color>";
                case 4://epic
                    return $"<color=#A335EE>{header} {itemIncrease} {ending}</color>";
                case 5://legendary
                    return $"<color=#FF8000>{header} {itemIncrease} {ending}</color>";
                case 6://artifact
                    return $"<color=#E6CC80>{header} {itemIncrease} {ending}</color>";
                case 7://heirloon
                    return $"<color=#00CCFF>{header} {itemIncrease} {ending}</color>";
                case 8://wow taken
                    return $"<color=#00CCFF>{header} {itemIncrease} {ending}</color>";
            }
            return $"<color=#9D9D9D>{header} {itemIncrease} {ending}</color>";
        }
        
        
        
        
        //Player profile
        public PlayerManager Player{get; set;}
        public PlayerManager Target{get; set;}
        
        //cur using animation
        public string animationName;
        
        //GameSetting
        /// <summary>
        /// 0:none
        /// 1:login
        /// 2:character
        /// 3:inGame
        /// 6:inqueue
        /// </summary>
        public int gameSettings;
        
        /// <summary>
        /// use for any system when the game is started
        /// </summary>
        public bool Is_Start_Game{get; set;}
        
        /// <summary>
        /// use for lock character when enter any setting
        /// </summary>
        public bool Is_Lock_Character { get; set; }
        
        /// <summary>
        ///   use to check if player is in city range or outside
        /// </summary>
        /// <returns></returns>
        public bool Is_Inside_City { get; set; } = true;
        
        



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
        
        
    }
}
