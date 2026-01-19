using System;
using System.Net.Sockets;
using System.Text;
using Items;
using Manager;
using UIs;
using UnityEngine;

namespace Server
{
    public class World : MonoBehaviour
    {
        [Header("Components")]
        public Authentication authentication;
        
        [Header("Auth Server")]
        private Socket client;
        private byte[] data;
        private int receiveBufferSize = 4096;
        
        [Tooltip("服务器响应结果")]
        public string result{get;set;}

        public bool IsServerAlive{get; set; }
        
        private void Awake()
        {
            ByteInit();
        }

        private void Update()
        {
            if (client != null)
            {
                client.BeginReceive(data, 0, receiveBufferSize, SocketFlags.None, new AsyncCallback(TcpReceiveMessage), client);
                UpdatePackets();
                if (!client.Connected && IsServerAlive)
                {
                    Debug.Log("World Server Disconnected");
                    client = null;
                    IsServerAlive = false;
                }
            }
                
        }

        /// <summary>
        /// 数据包处理中心
        /// </summary>
        public void UpdatePackets()
        {
            if (!string.IsNullOrEmpty(result))
            {
                Debug.Log("World Server Received: " + result);
                //Packet Execution
                string[] r = result.Split(' ');

                if (r[0] == "PREPARE")
                {
                    
                }

                if (r[0] == "SET")
                {
                    if (r[1] == "CHARACTERS")
                    {
                        CharactersManager.Instance.SpawnToon(result);
                    }

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
                        
                        ItemsManager.Instance.item[_itemId].curItemType = (Item_Type)_itemType;
                        ItemsManager.Instance.item[_itemId].InitItemType(_item_subclass,  _item_subtype);
                        ItemInfoManager.Instance.showItemInfo_Callback?.Invoke(result);
                    }
                }

                if (r[0] == "SPAWN")
                {
                    if (r[1] == "CHARACTERS")
                    {
                        CharactersManager.Instance.SpawnForBot(result);
                    }

                    if (r[1] == "CREATURE")
                    {
                        CharactersManager.Instance.SpawnCreature(result);
                    }
                }

                if (r[0] == "RETRIEVE_LOGIN")   
                {
                    if (r[1] == "DONE")
                    {
                        Debug.Log("Login Success");
                        MessageBoxManager.Instance.Okay();
                        Header.Instance.OpenMapHeader(1);
                        Header.Instance.OpenGameUIHeader(3);
                        CharactersManager.Instance.CheckCharactersCount();
                    }
                    if (r[1] == "DONE_INGAME")
                    {

                        MessageBoxManager.Instance.Okay();
                        Header.Instance.OpenMapHeader(1);
                        Header.Instance.OpenGameUIHeader(3);
                        Header.Instance.OpenCameraHeader(1);
                        CharactersManager.Instance.CheckCharactersCount();
                        
                        //change ost
                        AudioManager.Instance.PlayOst(0);
                    }
                }

                if (r[0] == "QUIT")
                {
                    if (r[1] == "GAME")
                    {
                        if (CharactersManager.Instance.spawnPlayerDict.ContainsKey(r[2]))
                        {
                            Destroy(CharactersManager.Instance.spawnPlayerDict[r[2]].gameObject);
                            CharactersManager.Instance.spawnPlayerDict.Remove(r[2]);
                        }
                    }
                }
                if (r[0] == "ERROR")
                {
                    if (r[1] == "DUPLICATED_NAME")
                    {
                        MessageBoxManager.Instance.OpenMessageBox("Duplicated Name",null);
                        Header.Instance.OpenGameUIHeader(4);
                    }
                }

                if (r[0] == "GETSET")
                {
                    if (r[1] == "CHARACTERS")
                    {
                        CharactersManager.Instance.GetSetCharacters(result);
                    }
                }

                if (r[0] == "INVENTORY")
                {
                    if (r[1] == "FULL")
                    {
                        MessageBoxManager.Instance.OpenMessageBox("Inventory is full",null);
                    }

                    if (r[1] == "SET")
                    {
                        InventorySystemManager.Instance.SetItem(result);
                        //destroy item call back

                    }

                    if (r[1] == "DROP_DIFF_ITEM")
                    {
                        MessageBoxManager.Instance.OpenMessageBox("Inventory is different",null);
                    }

                    if (r[1] =="DROP_STACK_ITEM")
                    {
                        MessageBoxManager.Instance.OpenMessageBox("Target item is full stack",null);
                    }

                    if (r[1] == "SPAWN_ITEM")
                    {
                        InventorySystemManager.Instance.SpawnItem(result);
                    }
                }

                if (r[0] == "ENEMY")
                {
                    
                }

                if (r[0] == "ITEM")
                {
                    if (r[1] == "RESERVED")
                    {
                        Debug.LogError("You can not loot this item");
                    }
                }

                if (r[0] == "ITEM_CONSUMABLE_USE_POTION")
                {
                    if (r[1] == GameManager.Instance.characters_name)
                    {
                        CharactersManager.Instance.spawnPlayerDict[r[1]].SpawnCreatureDamagePopUp(int.Parse(r[2]),  int.Parse(r[3]));
                    }
                    
                }

                if (r[0] == "CHATTING")
                {
                    if (r[1] == "ALL")
                    {
                        ChatSystemManager.Instance.SetAllChannelTxt(result);
                    }

                    if (r[1] == "PRIVATE")
                    {
                        ChatSystemManager.Instance.SetPrivateChannelTxt(result);
                    }

                    if (r[1] == "GUILD")
                    {
                        ChatSystemManager.Instance.SetGuildChannelTxt(result);
                    }

                    if (r[1] == "SCHOOL")
                    {
                        ChatSystemManager.Instance.SetSchoolChannelTxt(result);
                    }

                    if (r[1] == "SYSTEM")
                    {
                        ChatSystemManager.Instance.SetSystemChannelTxt(result);
                    }

                    if (r[1] == "WORLD")
                    {
                        ChatSystemManager.Instance.SetWorldChannelTxt(result);
                    }

                    if (r[1] == "WHISPER")
                    {
                        ChatSystemManager.Instance.SetWhisperTxt(result);
                    }
                }

                if (r[0] == "ATTACK")
                {
                    if (r[1] == "PLAYER")
                    {
                        if (CharactersManager.Instance.spawnPlayerDict.ContainsKey(r[2]))
                        {
                            int dpsEvent = int.Parse(r[3].Split('_')[0]);
                            int totalDamage = int.Parse(r[3].Split('_')[1]);
                            CharactersManager.Instance.spawnPlayerDict[r[2]].SpawnPlayerDamagePopUp(dpsEvent,totalDamage);
                        }
                    }
                }

                if (r[0] == "QUEUE")
                {
                    if (r[1] == "TIME")
                    {
                        MessageBoxManager.Instance.Okay();
                        QueueManager.Instance.OpenQueue($"Realm is Full\nPosition in queue: {r[2]}\nEstimated wait time: {r[3]}");
                        GameManager.Instance.gameSettings = 6;
                    }
                    
                    if (r[1] == "COMPLETED")
                    {
                        QueueManager.Instance.queueHeader.SetActive(false);
                    }
                }
                
                
                result = string.Empty;
            }
        }

        private void ByteInit()
        {
            data = new byte[receiveBufferSize];
        }

        /// <summary>
        /// TCP消息接收回调函数
        /// </summary>
        /// <param name="ar">异步操作状态</param>
        public void TcpReceiveMessage(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            int byteCount = socket.EndReceive(ar);
            byte[] newData = new byte[byteCount];
            Array.Copy(data, 0, newData, 0, byteCount);
            result  = Encoding.ASCII.GetString(newData);
            
        }

        /// <summary>
        /// TCP消息发送入口
        /// </summary>
        /// <param name="message">要发送的字符串消息</param>
        public void TcpSendMessage(string message,Action connectCallback)
        {
            if (client == null)
            {
                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //try to connect to authentication server
                if (!client.Connected)
                {
                    try
                    {
                        client.Connect(GameManager.Instance.realmlist_ipaddress, Convert.ToInt32(GameManager.Instance.realmlist_port));
                        SendMessage(message);
                    }
                    catch (SocketException e)
                    {
                        connectCallback?.Invoke();
                    }
                }
                else
                {
                    //try to send
                    SendMessage(message);
                }
            }
            else
            {
                //try to connect to authentication server
                if (!client.Connected)
                {
                    try
                    {
                        client.Connect(GameManager.Instance.realmlist_ipaddress, Convert.ToInt32(GameManager.Instance.realmlist_port));
                        SendMessage(message);
                    }
                    catch (SocketException e)
                    {
                        connectCallback?.Invoke();
                    }
                }
                else
                {
                    //try to send
                    SendMessage(message);
                }
            }
                
        }
        

        private void SendMessage(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            client.Send(data);
            IsServerAlive = true;
        }

        public void Disconnect()
        {
            client.Disconnect(false);
            client.Close();
            client = null;
        }
    }
}
