using System;
using Items;
using Server;
using UnityEngine;
using TMPro;
using UIs;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Manager
{
    public class ItemInfoManager : MonoBehaviour
    {
        public static ItemInfoManager Instance;

        public Authentication authentication;
        public World world;
        
        public GameObject itemInfoHead;
        
        public Image setItemAvatar;
        public TextMeshProUGUI setItemTitle;
        public TextMeshProUGUI setItemBody;
        public Vector3 setItemInfoPosition;
        
        public Action<string> showItemInfo_Callback;

        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        public void ShowItemInfo(Slot s)
        {
            //init tell player that i need to request item form world server
            Item item = ItemsManager.Instance.item[s.itemID];
            setItemAvatar.sprite = item.sprite;
            setItemBody.text = $"Init Item From World Server Database";

            if (s.result == string.Empty)
            {
                world.TcpSendMessage($"INVENTORY ITEMINFO_REQUEST_ITEM {s.slotID}",null);
                showItemInfo_Callback = (result) =>
                {
                    s.result = result;
                    UpdateItemResult(result,s);
                    //set item name
                };
            }

            if (s.result != string.Empty)
            {
                showItemInfo_Callback = null;
                UpdateItemResult(s.result,s);
            }
            
        }

        public void UpdateItemResult(string result,Slot s)
        {
            string[] r = result.Split(' ');
                
                int _itemId = int.Parse(r[2]);
                int _stack = int.Parse(r[3]);
                string _itemName  = r[4];
                int _slot = int.Parse(r[5]);
                int _itemClass = int.Parse(r[6]);
                int _weight = int.Parse(r[7]);
                int _tier = int.Parse(r[8]);
                int _profession = int.Parse(r[9]);
                string _note = r[10];
                int _quality = int.Parse(r[11]);
                string _description = r[12];
                int _displayId = int.Parse(r[13]);
                int _inventoryType = int.Parse(r[14]);
                int _allowClass = int.Parse(r[15]);
                int _itemLevel = int.Parse(r[16]);
                int _requiredLevel = int.Parse(r[17]);
                int _requiredHonor = int.Parse(r[18]);
                int _requiredRank = int.Parse(r[19]);
                int _setId = int.Parse(r[20]);
                int _itemType = int.Parse(r[21]);
                int _cutLevel = int.Parse(r[22]);
                int _endurance = int.Parse(r[23]);
                int _item_common = int.Parse(r[24]);
                int _item_subclass = int.Parse(r[25]);
                int _item_subtype = int.Parse(r[26]);
                int _item_begin = int.Parse(r[27]);
                int _item_sold = int.Parse(r[28]);
                int _item_pass = int.Parse(r[29]);
                int _item_discarded = int.Parse(r[30]);
                int _gender_type = int.Parse(r[31]);
                int _gender_size_type = int.Parse(r[32]);
                int _item_sets_name = int.Parse(r[33]);
                int _practice = int.Parse(r[34]);
                int _practice_page = int.Parse(r[35]);
                int _increase_hp = int.Parse(r[36]);
                int _increase_mp = int.Parse(r[37]);
                int _increase_sp = int.Parse(r[38]);
                int _hp_overtime = int.Parse(r[39]);
                int _mp_overtime = int.Parse(r[40]);
                int _sp_overtime = int.Parse(r[41]);
                int _str = int.Parse(r[42]);
                int _intel = int.Parse(r[43]);
                int _vit = int.Parse(r[44]);
                int _agi = int.Parse(r[45]);
                int _obs = int.Parse(r[46]);
                int _str_Percentage = int.Parse(r[47]);
                int _intel_Percentage = int.Parse(r[48]);
                int _vit_Percentage = int.Parse(r[49]);
                int _agi_Percentage = int.Parse(r[50]);
                int _obs_Percentage = int.Parse(r[51]);
                int _exAtk = int.Parse(r[52]);
                int _inAtk = int.Parse(r[53]);
                int _exAtk_Percentage = int.Parse(r[54]);
                int _inAtk_Percentage = int.Parse(r[55]);
                int _delay_enemy = int.Parse(r[56]);
                int _hp_consumption_increase = int.Parse(r[57]);
                int _hp_consumption_decrease = int.Parse(r[58]);
                int _mp_consumption_increase = int.Parse(r[59]);
                int _mp_consumption_decrease = int.Parse(r[60]);
                int _sp_consumption_increase = int.Parse(r[61]);
                int _sp_consumption_decrease = int.Parse(r[62]);
                int _weapon_exAtk_lower_limit_increase = int.Parse(r[63]);
                int _weapon_exAtk_lower_limit_decrease = int.Parse(r[64]);
                int _weapon_exAtk_upper_limit_increase = int.Parse(r[65]);
                int _weapon_exAtk_upper_limit_decrease = int.Parse(r[66]);
                int _weapon_inAtk_lower_limit_increase = int.Parse(r[67]);
                int _weapon_inAtk_lower_limit_decrease = int.Parse(r[68]);
                int _weapon_inAtk_upper_limit_increase = int.Parse(r[69]);
                int _weapon_inAtk_upper_limit_decrease = int.Parse(r[70]);
                string _weapon_attack_default_target = r[71];
                int _weapon_attack_default_target_damage = int.Parse(r[72]);
                int _buy_price = int.Parse(r[73]);
                int _sell_price = int.Parse(r[74]);
                int _increase_hp_percentage = int.Parse(r[75]);
                int _increase_mp_percentage = int.Parse(r[76]);
                int _increase_sp_percentage = int.Parse(r[77]);
                int _restore_hp = int.Parse(r[78]);
                int _restore_mp = int.Parse(r[79]);
                int _restore_sp = int.Parse(r[80]);
                int _item_cooldown = int.Parse(r[81]);
                int _item_speed = int.Parse(r[82]);

                int _buy_gold = 0;
                int _buy_sliver = 0;
                int _buy_copper = 0;
                GetGoldSliverCopper(_buy_price,ref _buy_gold, ref _buy_sliver, ref _buy_copper);
                
                int _sell_gold = 0;
                int _sell_sliver = 0;
                int _sell_copper = 0;
                GetGoldSliverCopper(_sell_price,ref _sell_gold, ref _sell_sliver, ref _sell_copper);
                //system

                setItemTitle.text = _itemName;
                setItemTitle.color = GameManager.Instance.GetItemCommonColor(_item_common);
                
                //set item body
                setItemBody.text = $"" +
                                   $"{(_itemLevel > 0 ? $"Level: {_itemLevel}\n" : "")}" +
                                   $"{(_exAtk > 0 ? $"Ex.Atk: {_exAtk}\n" : "")}" +
                                   $"{(_inAtk> 0 ? $"Class: {_inAtk}\n" : "")}" +
                                   $"{(_weight > 0 ? $"Class: {_weight}\n" : "")}" +
                                   $"{(_endurance > 0 ? $"Endurance: {s.enduranceMin}/{s.enduranceMax}\n" : "")}" +
                                   $"{(_requiredLevel > 0 ? $"Required Level: {_requiredLevel}\n" : "")}" +
                                   $"{(_item_begin> 0 ? $"Available for Beginners\n" : "Not Available for Beginners\n")}" +
                                   $"{(_item_pass > 0 ? $"Can Be Pass\n" : "Can't Be Pass\n")}" +
                                   $"{(_item_sold > 0 ? $"Can Be Sold\n" : "Can't Be Sold\n")}" +
                                   $"{(_item_discarded> 0 ? $"Can Be Discarded\n" : "Can't Be Discarded\n")}" +
                                   $"{(_gender_type > 0 ? $"Only Female Wears\n" : "Only Male Wears\n")}" +
                                   $"{(_gender_size_type > 0 ? $"Only Small Female Wears\n" : "")}" +
                                   $"{(_allowClass> 0 ? $"{GameManager.Instance.GetClassType(_allowClass)}\n" : "")}" +
                                   $"{(_note!=string.Empty ? $"{_note}\n" : "")}" +
                                   $"{(_increase_hp_percentage > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"HP Increase by",_increase_hp_percentage,"%\n")}" : "")}" +
                                   $"{(_increase_mp_percentage > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"MP Increase by",_increase_mp_percentage,"%\n")}" : "")}" +
                                   $"{(_increase_sp_percentage > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"HP Increase by",_increase_sp_percentage,"%\n")}" : "")}" +
                                   $"{(_exAtk_Percentage > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"Ex.Atk Increase by",_exAtk_Percentage,"%\n")}" : "")}" +
                                   $"{(_inAtk_Percentage > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"In.Atk Increase by",_inAtk_Percentage,"%\n")}" : "")}" +
                                   $"{(_delay_enemy > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"Enemy Delay",_delay_enemy,"sec\n")}" : "")}" +
                                   $"{(_str > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"Str Increase by",_str,"\n")}" : "")}" +
                                   $"{(_intel > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"Intel Increase by",_intel,"\n")}" : "")}" +
                                   $"{(_vit > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"Vit Increase by",_vit,"\n")}" : "")}" +
                                   $"{(_agi > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"Agi Increase by",_agi,"\n")}" : "")}" +
                                   $"{(_obs > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"Obs Increase by",_obs,"\n")}" : "")}" +
                                   $"{(_str_Percentage > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"Str Increase by",_str_Percentage,"%\n")}" : "")}" +
                                   $"{(_intel_Percentage > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"Intel Increase by",_intel_Percentage,"%\n")}" : "")}" +
                                   $"{(_vit_Percentage > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"Vit Increase by",_vit_Percentage,"%\n")}" : "")}" +
                                   $"{(_agi_Percentage > 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"Agi Increase by",_agi_Percentage,"%\n")}" : "")}" +
                                   $"{(_obs_Percentage> 0 ? $"{GameManager.Instance.GetItemRichTextColorByCommon(_item_common,"Obs Increase by",_obs_Percentage,"%\n")}" : "")}" +
                                   $"{(_exAtk_Percentage > 0 ? $"%\n" : "")}" +
                                   
                                   //item set bull sell
                                   $"{(_buy_price > -1 ? $"Buy Price: <sprite index=0>{_buy_gold}<sprite index=0>{_buy_sliver}<sprite index=0>{_buy_copper}\n" : "")}" +
                                   $"{(_sell_price > -1 ? $"Sell Price:<sprite index=0>{_sell_gold}<sprite index=0>{_sell_sliver}<sprite index=0>{_sell_copper}\n" : "")}" +
                                   $"Description:{_description}";
                itemInfoHead.SetActive(true);
                if (s.slotType == SlotType.Inventory)
                {
                    Vector3 mousePos = new  Vector3(Input.mousePosition.x+setItemInfoPosition.x, Input.mousePosition.y+setItemInfoPosition.y, Input.mousePosition.z+setItemInfoPosition.z);
                    itemInfoHead.transform.position = mousePos;
                }
                
                
        }
        

        public void CloseItemInfo()
        {
            itemInfoHead.SetActive(false);
        }

        private void GetGoldSliverCopper(int defaultGold,ref int _gold, ref int _sliver, ref int _copper)
        {
            _gold = defaultGold / 10000;
            _sliver = (defaultGold / 100) % 100;
            _copper = defaultGold % 100;    
        }
    }
}
