using UnityEngine;
using UnityEngine.Serialization;

namespace Character.Creatures
{
    public enum CreatureType
    {
        Normal,
        Friend,
        Friendly,
        Rare,
        Elite,
        Boss
    }

    public enum CreatureWhatNpc
    {
        None,
        Guard,
        QuestionHelper,
        Vending,
        Teleporter,
        Schooler,
        Creature,
        
    }
    public class CreaturesDatabase : MonoBehaviour
    {
        public CreatureType creatureType;
        public CreatureWhatNpc creatureWhatNpc;
        
        public int creature_guid;
    
        public int creature_realm ;
    
        public string creature_name;
    
        public int creature_race;
    
        public int creature_npc_class;
    
        public int creature_gender ;
    
        public int creature_level;
    
        public int creature_xp;
        
        public int creature_money ;
    
        public string creature_dropItemID ;
    
        public string creature_positionX ;
    
        public string creature_positionY ;
    
        public string creature_positionZ ;
    
        public string creature_rotation ;
    
        public int creature_map ;
    
        public string creature_canvasHp_Header ;
    
        public int creature_nameType  ;
    
        public int creature_useType ;
    
        public int creature_maxHp  ;
    
        public int creature_minHp ;
    
        public int creature_maxMp ;
    
        public int creature_minMp ;
    
        public int creature_maxSp ;
    
        public int creature_minSp ;
    
        public int creature_str ;
    
        public int creature_intel ;
    
        public int creature_vit ;
    
        public int creature_agi ;
    
        public int creature_obs ;
        
        public int creature_switchPosTime ;
        
        public int creature_respawnTime ;
    
        public int creature_attackTime ;
    
        public int creature_reloadTime;
        
        public float creature_move_speed;
        
        
        
        public float creature_respawnTime_max ;
    
        public float creature_attackTime_max ;
    
        public float creature_reloadTime_max;
        
        public float creature_respawnTime_min ;
    
        public float creature_attackTime_min ;
    
        public float creature_reloadTime_min;
        
    }
}
