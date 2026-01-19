using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Character.Creatures;
using Manager;
using UnityEngine;

namespace Character.Player
{
    public class PlayerNet : MonoBehaviour
    {
        public PlayerManager playerManager;
        public PlayerVFX playerVFX;
        
        public CreaturesDatabase creaturesDatabase;

        public float syncTime;
        public float syncTimeReset;
        
        private Socket client;
        private EndPoint recvEndPoint;
        private int byteCount;
        private int recvDataBufferSize = 4096;
        private byte[] data;
        public string result {get; set;} = string.Empty;

        private void Awake()
        {
            ByteInit();
            ReceiveEndPointInit();
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
            UpdateSyncPlayer();
        }

        private void UpdatePackets()
        {
            if (!string.IsNullOrEmpty(result))
            {
                
                string[] r = result.Split(' ');
                if (r[0] == "SET")
                {
                    if (r[1] == "BOT")
                    {
                        float px = float.Parse(r[4]);
                        float py = float.Parse(r[5]);
                        float pz = float.Parse(r[6]);
                
                        float rx = float.Parse(r[7]);
                        float ry = float.Parse(r[8]);
                        float rz = float.Parse(r[9]);
                        float rw = float.Parse(r[10]);
                        
                        int maxHp = int.Parse(r[12]);
                        int minHp = int.Parse(r[13]);
                        int maxMp = int.Parse(r[14]);
                        int minMp = int.Parse(r[15]);
                        int maxSp = int.Parse(r[16]);
                        int minSp = int.Parse(r[17]);
                        int maxExAtk = int.Parse(r[18]);
                        int minExAtk = int.Parse(r[19]);
                        int maxInAtk = int.Parse(r[20]);
                        int minInAtk = int.Parse(r[21]);
                        float accuracy = float.Parse(r[22]);
                        int exDef = int.Parse(r[23]);
                        int inDef = int.Parse(r[24]);
                        float dodge = float.Parse(r[25]);
                        float critical = float.Parse(r[26]);
                        int flee = int.Parse(r[27]);
                        int aspd = int.Parse(r[28]);
                            
                        int str = int.Parse(r[29]);
                        int intel = int.Parse(r[30]);
                        int vit = int.Parse(r[31]);
                        int agi = int.Parse(r[32]);
                        int obs = int.Parse(r[33]);
                        int property = int.Parse(r[34]);
                        float attack_time = float.Parse(r[35]);
                        float wait_time = float.Parse(r[36]);
                        float reload_time = float.Parse(r[37]);
                            
                        int maxXp = int.Parse(r[38]);
                        int minXp = int.Parse(r[39]);
                            
                        int level = int.Parse(r[40]);
                        int preLevel = int.Parse(r[41]);
                        
                        int money = int.Parse(r[42]);
                        int weight = int.Parse(r[43]);
                        int weightMin = int.Parse(r[44]);
                        int ember = int.Parse(r[45]);
                        float playerMoveSpeed = float.Parse(r[46]);
                        if (playerManager.IsLocalPlayer)
                        {
                            //player
                            CharacterEquipmentManager.Instance.hpText.text = $"{minHp}/{maxHp}";
                            CharacterEquipmentManager.Instance.mpText.text = $"{minMp}/{maxMp}";
                            CharacterEquipmentManager.Instance.spText.text = $"{minSp}/{maxSp}";
                            CharacterEquipmentManager.Instance.exAtkText.text = $"{minExAtk}-{maxExAtk}";
                            CharacterEquipmentManager.Instance.inAtkText.text = $"{minInAtk}-{maxInAtk}";
                            CharacterEquipmentManager.Instance.accuracyText.text = $"{accuracy:f1}%";
                            CharacterEquipmentManager.Instance.exDefText.text = $"{exDef}";
                            CharacterEquipmentManager.Instance.inDefText.text = $"{inDef}";
                            CharacterEquipmentManager.Instance.dodgeText.text = $"{dodge:f1}%";
                            CharacterEquipmentManager.Instance.criticalText.text = $"{critical:f1}%";
                            CharacterEquipmentManager.Instance.fleeText.text = $"{flee}";
                            CharacterEquipmentManager.Instance.aspdText.text = $"{aspd}";
                            
                            CharacterEquipmentManager.Instance.stats[0].text = $"{str}";
                            CharacterEquipmentManager.Instance.stats[1].text = $"{intel}";
                            CharacterEquipmentManager.Instance.stats[2].text = $"{vit}";
                            CharacterEquipmentManager.Instance.stats[3].text = $"{agi}";
                            CharacterEquipmentManager.Instance.stats[4].text = $"{obs}";
                            
                            CharacterEquipmentManager.Instance.property.text = $"{property}";
                            
                            
                            CharacterEquipmentManager.Instance.curExp.text = $"{minXp}";
                            CharacterEquipmentManager.Instance.levelUpExp.text = $"{maxXp}";

                            if (property > 0)
                            {
                                CharacterEquipmentManager.Instance.OpenPlusMinusStat();
                            }
                            
                            ProfileManager.Instance.xpBar.maxValue = maxXp;
                            ProfileManager.Instance.xpBar.value = minXp;
                            
                            float percentage = ((float)minXp / maxXp) * 100f;
                            percentage = Mathf.Clamp(percentage, 0, 100);
                            ProfileManager.Instance.xpTxt.text = $"{minXp} ({percentage}%)";

                            if (level > int.Parse(ProfileManager.Instance.levelTxt.text))
                            {
                                playerVFX.OpenParticleSystem(1);
                            }
                            ProfileManager.Instance.levelTxt.text = $"{level}";
                            ProfileManager.Instance.preLevelTxt.text = $"{preLevel}";
                            
                            ProfileManager.Instance.hpBar.maxValue = maxHp;
                            ProfileManager.Instance.hpBar.value = minHp;
                            ProfileManager.Instance.hpTxt.text = $"{minHp}/{maxHp}";
                            
                            ProfileManager.Instance.mpBar.maxValue = maxMp;
                            ProfileManager.Instance.mpBar.value = minMp;
                            ProfileManager.Instance.mpTxt.text = $"{minMp}/{maxMp}";
                            
                            ProfileManager.Instance.spBar.maxValue = maxSp;
                            ProfileManager.Instance.spBar.value = minSp;
                            ProfileManager.Instance.spTxt.text = $"{minSp}/{maxSp}";
                            
                            //set player hp
                            GameManager.Instance.Player.HpBar.maxValue = maxHp;
                            GameManager.Instance.Player.HpBar.value = minHp;
                            
                            GameManager.Instance.characterStat_attack_time = attack_time;
                            GameManager.Instance.characterStat_wait_time = wait_time;
                            GameManager.Instance.characterStat_reload_time = reload_time;
                            
                            InventorySystemManager.Instance.SetPlayerMoney(money);
                            InventorySystemManager.Instance.SetPlayerWeight(weight, weightMin);
                            InventorySystemManager.Instance.SetEmber(ember);
                            GameManager.Instance.Player.agent.speed = playerMoveSpeed;


                        }
                        else
                        {
                            //bot
                
                            Vector3 setPosition = new Vector3(px, py, pz);
                            Quaternion setRotation = new Quaternion(rx, ry, rz, rw);
                
                            transform.position = setPosition;
                            transform.rotation = setRotation;
                        
                            playerManager.PlayAnimation(r[11]);
                            
                            playerManager.HpBar.maxValue = maxHp;
                            playerManager.HpBar.value = minHp;
                        }
                    }

                    if (r[1] == "CREATURE")
                    {
                        int _guid = int.Parse(r[2]);
                        int _realm = int.Parse(r[3]);
                        string _name = r[4];
                        int _race = int.Parse(r[5]);
                        int _npc_class = int.Parse(r[6]);
                        int _gender = int.Parse(r[7]);
                        int _level = int.Parse(r[8]);
                        int _xp = int.Parse(r[9]);
                        int _money = int.Parse(r[10]);
                        string _dropItemID = r[11];
                        string _positionX = r[12];
                        string _positionY = r[13];
                        string _positionZ = r[14];
                        string _rotation = r[15];
                        int _map = int.Parse(r[16]);
                        string _canvasHp_Header = r[17];
                        int _nameType = int.Parse(r[18]);
                        int _useType = int.Parse(r[19]);
                        int _maxHp = int.Parse(r[20]);
                        int _minHp = int.Parse(r[21]);
                        int _maxMp = int.Parse(r[22]);
                        int _minMp = int.Parse(r[23]);
                        int _maxSp = int.Parse(r[24]);
                        int _minSp = int.Parse(r[25]);
                        int _str = int.Parse(r[26]);
                        int _intel = int.Parse(r[27]);
                        int _vit = int.Parse(r[28]);
                        int _agi = int.Parse(r[29]);
                        int _obs = int.Parse(r[30]);
                        int _respawnTime = int.Parse(r[31]);
                        int _attackTime = int.Parse(r[32]);
                        int _reloadTime = int.Parse(r[33]);
                        int _switchPosTime = int.Parse(r[34]);
                        float _move_speed = float.Parse(r[35]);
                        
                        playerManager.HpBar.value = _minHp;

                        string[] _totalDamage = r[36].Split('_');
                        string _playerTarget = r[37];
                        
                        playerManager.SpawnCreatureDamagePopUp(int.Parse(_totalDamage[0]),int.Parse(_totalDamage[1]));
                        playerManager.playerTarget = _playerTarget;
                        if (playerManager.HpBar.value <= 0)
                        {
                            playerManager.PlayAnimation("Death");
                        }
                    }

                    if (r[1] == "RESPAWN")
                    {
                        int _guid = int.Parse(r[2]);
                        int _realm = int.Parse(r[3]);
                        string _name = r[4];
                        int _race = int.Parse(r[5]);
                        int _npc_class = int.Parse(r[6]);
                        int _gender = int.Parse(r[7]);
                        int _level = int.Parse(r[8]);
                        int _xp = int.Parse(r[9]);
                        int _money = int.Parse(r[10]);
                        string _dropItemID = r[11];
                        string _positionX = r[12];
                        string _positionY = r[13];
                        string _positionZ = r[14];
                        string _rotation = r[15];
                        int _map = int.Parse(r[16]);
                        string _canvasHp_Header = r[17];
                        int _nameType = int.Parse(r[18]);
                        int _useType = int.Parse(r[19]);
                        int _maxHp = int.Parse(r[20]);
                        int _minHp = int.Parse(r[21]);
                        int _maxMp = int.Parse(r[22]);
                        int _minMp = int.Parse(r[23]);
                        int _maxSp = int.Parse(r[24]);
                        int _minSp = int.Parse(r[25]);
                        int _str = int.Parse(r[26]);
                        int _intel = int.Parse(r[27]);
                        int _vit = int.Parse(r[28]);
                        int _agi = int.Parse(r[29]);
                        int _obs = int.Parse(r[30]);
                        int _respawnTime = int.Parse(r[31]);
                        int _attackTime = int.Parse(r[32]);
                        int _reloadTime = int.Parse(r[33]);
                        int _switchPosTime = int.Parse(r[34]);
                        
                        Vector3 myBasePosition = new Vector3(float.Parse(_positionX), float.Parse(_positionY), float.Parse(_positionZ));
                        transform.position = myBasePosition;
                        
                        //reset hp
                        playerManager.HpBar.maxValue = _maxHp;
                        playerManager.HpBar.value = _minHp;
                        
                        playerManager.playerTarget = string.Empty;
                        
                        playerManager.canvasHp.gameObject.SetActive(true);
                        playerManager.myCollider.enabled = true;
                        playerManager.PlayAnimation("IDLE");
                        playerVFX.OpenParticleSystem(0);
                    }

                    if (r[1] == "ATTACKPLAYER")
                    {
                        string _playerTarget = r[2];
                        if (CharactersManager.Instance.spawnPlayerDict.ContainsKey(r[2]))
                        {
                            playerManager.playerTarget = _playerTarget;
                        }
                        if (playerManager.HpBar.value <= 0)
                        {
                            playerManager.PlayAnimation("Death");
                        }
                    }

                    if (r[1] == "RESETPLAYER")
                    {
                        playerManager.playerTarget = string.Empty;
                        if (playerManager.HpBar.value <= 0)
                        {
                            playerManager.PlayAnimation("Death");
                        }
                    }
                }

                if (r[0] == "ENEMY")
                {
                    if (r[1] == "SPAWN_ITEM")
                    {
                        InventorySystemManager.Instance.SpawnItemForCreature(result,transform.position);
                    }
                }
                
                
                
                result = string.Empty;
            }
        }
        
        /// <summary>
        /// sync player position and rotation
        /// send
        /// </summary>
        private void UpdateSyncPlayer()
        {   
            if(!playerManager.IsLocalPlayer)
                return;
            
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;

            string setBot = $"SET BOT {GameManager.Instance.realmlist_guid} {GameManager.Instance.characters_name} {position.x} {position.y} {position.z} {rotation.x} {rotation.y} {rotation.z} {rotation.w} " +
                            $"{GameManager.Instance.animationName}";
            syncTime -= Time.deltaTime;
            if (syncTime <= 0)
            {
                syncTime = syncTimeReset;
                UdpSendMessage(setBot, GameManager.Instance.realmlist_ipaddress, int.Parse(GameManager.Instance.realmlist_port));
            }
        }
    }
}
