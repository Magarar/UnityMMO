using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UIs
{
    public class ToonSelection : MonoBehaviour
    {
        //API
        public TextMeshProUGUI setPlayerID;
        public TextMeshProUGUI setPlayerFreeLevel;
        public TextMeshProUGUI setPlayerCreateDate;
        
        public string guid { get; set;}
    
        public string account {get; set;}
    
        public string realm {get; set;}
    
        public string name {get; set;}
    
        public string race {get; set;}
    
        public string class_ {get; set;}
    
        public string gender {get; set;}
    
        public string level {get; set;}
    
        public string xp {get; set;}
    
        public string money {get; set;}
    
        public string positionX  {get; set;}
    
        public string positionY  {get; set;}
    
        public string positionZ  {get; set;}
    
        public string rotation  {get; set;}
    
        public string map  {get; set;}
    
        public string dungeonDifficulty  {get; set;}
    
        public string online  {get; set;}
    
        public string totaltime  {get; set;}
    
        public string leveltime  {get; set;}
    
        public string canvasHp_Header  {get; set;}

        public string createDate;
        
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

        public void SelectThisToon()
        {
            GameManager.Instance.characters_guid = guid;
            GameManager.Instance.characters_account = account;
            GameManager.Instance.characters_realm = realm;
            GameManager.Instance.characters_name = name;
            GameManager.Instance.characters_race = race;
            GameManager.Instance.characters_class_ = class_;
            GameManager.Instance.characters_gender = gender;
            GameManager.Instance.characters_level = level;
            GameManager.Instance.characters_xp = xp;
            GameManager.Instance.characters_money = money;
            GameManager.Instance.characters_positionX = positionX;
            GameManager.Instance.characters_positionY = positionY;
            GameManager.Instance.characters_positionZ = positionZ;
            GameManager.Instance.characters_rotation = rotation;
            GameManager.Instance.characters_map = map;
            GameManager.Instance.characters_dungeonDifficulty = dungeonDifficulty;
            GameManager.Instance.characters_online = online;
            GameManager.Instance.characters_totaltime = totaltime;
            GameManager.Instance.characters_leveltime = leveltime;
            GameManager.Instance.characters_canvasHp_Header = canvasHp_Header;
            GameManager.Instance.characters_createDate = createDate;
            
            CharactersManager.Instance.ChooseRaceSelection(int.Parse(race));
            
            GameManager.Instance.characterStat_cid = characterStat_cid;
            GameManager.Instance.characterStat_str = characterStat_str;
            GameManager.Instance.characterStat_intel = characterStat_intel;
            GameManager.Instance.characterStat_vit = characterStat_vit;
            GameManager.Instance.characterStat_agi = characterStat_agi;
            GameManager.Instance.characterStat_obs = characterStat_obs;
            GameManager.Instance.characterStat_property = characterStat_property;
            GameManager.Instance.characterStat_attack_time = characterStat_attack_time;
            GameManager.Instance.characterStat_wait_time = characterStat_wait_time;
            GameManager.Instance.characterStat_reload_time = characterStat_reload_time;
            
            //player attack time init
            GameManager.Instance.attackTime = 0;
            GameManager.Instance.waitTime = characterStat_wait_time;
            GameManager.Instance.reloadTime = characterStat_reload_time;
            
            
        }

        public void InizCompoents()
        {
            
        }
    }
}
