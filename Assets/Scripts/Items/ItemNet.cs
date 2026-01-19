using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Character.Player;
using Manager;
using UnityEngine;

namespace Items
{
    public class ItemNet : MonoBehaviour
    {
        public ItemPickUp itemPickUp;
        
        public float syncTime;
        public float syncTimeReset;
        
        private Socket client;
        private EndPoint recvEndPoint;
        private int byteCount;
        private int recvDataBufferSize = 4096;
        private byte[] data;
        public string result {get; set;} = string.Empty;

        public void InitItem()
        {
            ByteInit();
            
            ReceiveEndPointInit();
            if (ItemsManager.Instance.item[itemPickUp.itemID].itemName == string.Empty)
            {
                UdpSendMessage($"ITEM REQUEST {GameManager.Instance.realmlist_guid} {itemPickUp.netID} {itemPickUp.itemID}", GameManager.Instance.realmlist_ipaddress, int.Parse(GameManager.Instance.realmlist_port));
            }
            else
            {
                UdpSendMessage($"ITEM ADD_ENDPOINT {GameManager.Instance.realmlist_guid} {itemPickUp.netID} {itemPickUp.itemID}", GameManager.Instance.realmlist_ipaddress, int.Parse(GameManager.Instance.realmlist_port));
            }

        }

        private void ByteInit()
        {
            data = new byte[recvDataBufferSize];
        }

        private void ReceiveEndPointInit()
        {
            recvEndPoint = new IPEndPoint(IPAddress.Any, 0);
        }

        public void UdpSendMessage(string message, string ipaddress, int port)
        {
            if (client == null)
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            }
            else
            {
                
            }
            byte[] sender = Encoding.ASCII.GetBytes(message);
            var endPoint = new IPEndPoint(IPAddress.Parse(ipaddress), port);
            client.BeginSendTo(sender, 0, sender.Length, SocketFlags.None, endPoint, null, null);
        }

        private void UdpReceiveMessage(IAsyncResult ar)
        {
            var c = (Socket)ar.AsyncState;
            byteCount = c.EndReceive(ar);
            byte[] newBuffers = new byte[byteCount];
            Array.Copy(data, 0, newBuffers, 0, byteCount);
            result = Encoding.ASCII.GetString(newBuffers);
        }

        private void Update()
        {
            if (client != null)
            {
                client.BeginReceiveFrom(data, 0, recvDataBufferSize, SocketFlags.None, ref recvEndPoint, UdpReceiveMessage, client);
                UpdatePackets();
            }
        }

        private void UpdatePackets()
        {
            if (!string.IsNullOrEmpty(result))
            {
                
                string[] r = result.Split(' ');

                if (r[0] == "ITEM")
                {
                    if (r[1]=="TAKEN")
                    {
                        Destroy(gameObject);
                    }
                }

                if (r[0] == "SET")
                {
                    if (r[1] == "ITEM_INFO")
                    {
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
                        //system
                        int _enduranceMax = int.Parse(r[83]);
                        int _enduranceMin  = int.Parse(r[84]);
                        
                        ItemsManager.Instance.item[_itemId].itemName = _itemName;
                        ItemsManager.Instance.item[_itemId].slot = _slot;
                        ItemsManager.Instance.item[_itemId].itemClass = _itemClass;
                        ItemsManager.Instance.item[_itemId].weight = _weight;
                        ItemsManager.Instance.item[_itemId].tier = _tier;
                        ItemsManager.Instance.item[_itemId].profession = _profession;
                        ItemsManager.Instance.item[_itemId].note = _note;
                        ItemsManager.Instance.item[_itemId].quality = _quality;
                        ItemsManager.Instance.item[_itemId].description = _description;
                        ItemsManager.Instance.item[_itemId].displayId = _displayId;
                        ItemsManager.Instance.item[_itemId].inventoryType = _inventoryType;
                        ItemsManager.Instance.item[_itemId].allowClass = _allowClass;
                        ItemsManager.Instance.item[_itemId].itemLevel = _itemLevel;
                        ItemsManager.Instance.item[_itemId].requiredLevel = _requiredLevel;
                        ItemsManager.Instance.item[_itemId].requiredHonor = _requiredHonor;
                        ItemsManager.Instance.item[_itemId].requiredRank = _requiredRank;
                        ItemsManager.Instance.item[_itemId].setId = _setId;
                        ItemsManager.Instance.item[_itemId].itemType = _itemType;
                        ItemsManager.Instance.item[_itemId].cutLevel = _cutLevel;
                        ItemsManager.Instance.item[_itemId].endurance = _endurance;
                        ItemsManager.Instance.item[_itemId].item_common = _item_common;
                        ItemsManager.Instance.item[_itemId].item_subclass = _item_subclass;
                        ItemsManager.Instance.item[_itemId].item_subtype = _item_subtype;
                        ItemsManager.Instance.item[_itemId].item_begin = _item_begin;
                        ItemsManager.Instance.item[_itemId].item_sold = _item_sold;
                        ItemsManager.Instance.item[_itemId].item_pass = _item_pass;
                        ItemsManager.Instance.item[_itemId].item_discarded = _item_discarded;
                        ItemsManager.Instance.item[_itemId].gender_type = _gender_type;
                        ItemsManager.Instance.item[_itemId].gender_size_type = _gender_size_type;
                        ItemsManager.Instance.item[_itemId].item_sets_name = _item_sets_name;
                        ItemsManager.Instance.item[_itemId].practice = _practice;
                        ItemsManager.Instance.item[_itemId].practice_page = _practice_page;
                        ItemsManager.Instance.item[_itemId].increase_hp = _increase_hp;
                        ItemsManager.Instance.item[_itemId].increase_mp = _increase_mp;
                        ItemsManager.Instance.item[_itemId].increase_sp = _increase_sp;
                        ItemsManager.Instance.item[_itemId].hp_overtime = _hp_overtime;
                        ItemsManager.Instance.item[_itemId].mp_overtime = _mp_overtime;
                        ItemsManager.Instance.item[_itemId].sp_overtime = _sp_overtime;
                        ItemsManager.Instance.item[_itemId].str = _str;
                        ItemsManager.Instance.item[_itemId].intel = _intel;
                        ItemsManager.Instance.item[_itemId].vit = _vit;
                        ItemsManager.Instance.item[_itemId].agi = _agi;
                        ItemsManager.Instance.item[_itemId].obs = _obs;
                        ItemsManager.Instance.item[_itemId].str_Percentage = _str_Percentage;
                        ItemsManager.Instance.item[_itemId].intel_Percentage = _intel_Percentage;
                        ItemsManager.Instance.item[_itemId].vit_Percentage = _vit_Percentage;
                        ItemsManager.Instance.item[_itemId].agi_Percentage = _agi_Percentage;
                        ItemsManager.Instance.item[_itemId].obs_Percentage = _obs_Percentage;
                        ItemsManager.Instance.item[_itemId].exAtk = _exAtk;
                        ItemsManager.Instance.item[_itemId].inAtk = _inAtk;
                        ItemsManager.Instance.item[_itemId].exAtk_Percentage = _exAtk_Percentage;
                        ItemsManager.Instance.item[_itemId].inAtk_Percentage = _inAtk_Percentage;
                        ItemsManager.Instance.item[_itemId].delay_enemy = _delay_enemy;
                        ItemsManager.Instance.item[_itemId].hp_consumption_increase = _hp_consumption_increase;
                        ItemsManager.Instance.item[_itemId].mp_consumption_increase = _mp_consumption_increase;
                        ItemsManager.Instance.item[_itemId].sp_consumption_increase = _sp_consumption_increase;
                        ItemsManager.Instance.item[_itemId].hp_consumption_decrease = _hp_consumption_decrease;
                        ItemsManager.Instance.item[_itemId].mp_consumption_decrease = _mp_consumption_decrease;
                        ItemsManager.Instance.item[_itemId].sp_consumption_decrease = _sp_consumption_decrease;
                        ItemsManager.Instance.item[_itemId].weapon_exAtk_lower_limit_increase = _weapon_exAtk_lower_limit_increase;
                        ItemsManager.Instance.item[_itemId].weapon_exAtk_lower_limit_decrease = _weapon_exAtk_lower_limit_decrease;
                        ItemsManager.Instance.item[_itemId].weapon_exAtk_upper_limit_increase = _weapon_exAtk_upper_limit_increase;
                        ItemsManager.Instance.item[_itemId].weapon_exAtk_upper_limit_decrease = _weapon_exAtk_upper_limit_decrease;
                        ItemsManager.Instance.item[_itemId].weapon_inAtk_lower_limit_increase = _weapon_inAtk_lower_limit_increase;
                        ItemsManager.Instance.item[_itemId].weapon_inAtk_lower_limit_decrease = _weapon_inAtk_lower_limit_decrease;
                        ItemsManager.Instance.item[_itemId].weapon_inAtk_upper_limit_increase = _weapon_inAtk_upper_limit_increase;
                        ItemsManager.Instance.item[_itemId].weapon_inAtk_upper_limit_decrease = _weapon_inAtk_upper_limit_decrease;
                        ItemsManager.Instance.item[_itemId].weapon_attack_default_target = _weapon_attack_default_target;
                        ItemsManager.Instance.item[_itemId].weapon_attack_default_target_damage = _weapon_attack_default_target_damage;
                        ItemsManager.Instance.item[_itemId].sell_price = _sell_price;
                        ItemsManager.Instance.item[_itemId].buy_price = _buy_price;
                        ItemsManager.Instance.item[_itemId].increase_hp_percentage = _increase_hp_percentage;
                        ItemsManager.Instance.item[_itemId].increase_mp_percentage = _increase_mp_percentage;
                        ItemsManager.Instance.item[_itemId].increase_sp_percentage = _increase_sp_percentage;
                        ItemsManager.Instance.item[_itemId].restore_hp = _restore_hp;
                        ItemsManager.Instance.item[_itemId].restore_mp = _restore_mp;
                        ItemsManager.Instance.item[_itemId].restore_sp = _restore_sp;
                        ItemsManager.Instance.item[_itemId].item_cooldown = _item_cooldown;
                        ItemsManager.Instance.item[_itemId].item_speed = _item_speed;
                        
                        ItemsManager.Instance.item[_itemId].InitItemType(_item_subclass,  _item_subtype);
                        ItemsManager.Instance.item[_itemId].curItemType = (Item_Type)_itemType;
                        
                    }
                }
                
                result = string.Empty;
            }
        }
    }
}
