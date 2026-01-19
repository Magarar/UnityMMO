using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Character.Creatures;
using Character.Player;
using Map;
using Player;
using Server;
using TMPro;
using UIs;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Manager
{
    public class CharactersManager : MonoBehaviour
    {
        public static CharactersManager Instance;
        
        public PlayerManager playerManager;
        public World world;
        public Authentication authentication;

        public GameObject selectCharactersPanel;
        public GameObject createCharactersPanel;
        
        public ToonSelection toonSelection;
        public Transform spawnCharacterPosition;
        
        public Transform spawnCharacterPositionBase;
        
        //string=playerName
        public Dictionary<string,PlayerManager> spawnPlayerDict = new Dictionary<string, PlayerManager>();
        public Dictionary<string,PlayerManager> spawnCreatureDict = new Dictionary<string, PlayerManager>();

        public TMP_InputField playerIdInput;

        public List<GameObject> specialButton = new List<GameObject>();
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        public void SpawnToon(string result)
        {
            string[] e = result.Split('|');
            string[] split = e[0].Split(' ');

            ToonSelection ts = Instantiate(toonSelection, spawnCharacterPosition,false);
            ts.transform.localScale = Vector3.one;
            HLManager.Instance.allToonList.Add(ts);
            
            //character
            ts.guid = split[2];
            ts.account = split[3];
            ts.realm = split[4];
            ts.name = split[5];
            ts.race = split[6];
            ts.class_ = split[7];
            ts.gender = split[8];
            ts.level = split[9];
            ts.xp = split[10];
            ts.money = split[11];
            ts.positionX = split[12];
            ts.positionY = split[13];
            ts.positionZ = split[14];
            ts.rotation = split[15];
            ts.map = split[16];
            ts.dungeonDifficulty = split[17];
            ts.online = split[18];
            ts.totaltime = split[19];
            ts.leveltime = split[20];
            ts.canvasHp_Header = split[21];
            ts.createDate = split[22]+" "+split[23];
            
            ts.setPlayerID.text = ts.name;
            ts.setPlayerFreeLevel.text = $"{GameManager.Instance.GetClassType(int.Parse(ts.class_))} level:{ts.level}";
            ts.setPlayerCreateDate.text = ts.createDate;
            
            string[] g = e[1].Split(' ');
            
            //character stat
            ts.characterStat_cid = g[2];
            ts.characterStat_str = g[3];
            ts.characterStat_intel = g[4];
            ts.characterStat_vit = g[5];
            ts.characterStat_agi = g[6];
            ts.characterStat_obs = g[7];
            ts.characterStat_property = g[8];
            ts.characterStat_attack_time = float.Parse(g[9]);
            ts.characterStat_wait_time = float.Parse(g[10]);
            ts.characterStat_reload_time = float.Parse(g[11]);
        }

        public void SpawnCharacters(string result)
        {
            string[] r = result.Split(' ');
            
        }
        
        private void CloseAllRace()
        {
            foreach (var race in playerManager.raceList)
            {
                race.SetActive(false);
            }
        }

        public void EnableSpecialButton()
        {
            foreach (var button in specialButton)
                button.SetActive(true);
        }

        public void DisableSpecialButton()
        {
            foreach (var button in specialButton)
                button.SetActive(false);
        }
        
        /// <summary>
        /// choose race(use in create character)
        /// </summary>
        /// <param name="raceID"></param>
        public void ChooseRace(int raceID)
        {
            CloseAllRace();
            playerManager.raceList[raceID].SetActive(true);
            GameManager.Instance.characters_race_enum = raceID;
        }

        public void ChooseRaceSelection(int raceID)
        {
            CloseAllRace();
            playerManager.raceList[raceID].SetActive(true);
        }

        #region spawnCharacter
        private void SpawnForMyself()
        {
            //clear all item
            InventorySystemManager.Instance.DestroyAllItemJunk();
            //clear all creatures
            DeleteAllSpawnCreatures();
            //open map
            Header.Instance.OpenMapHeader(Header.Instance.mapDict[int.Parse(GameManager.Instance.characters_map)].mapIndex);

            PlayerManager player = null;
            //missing set pos && rotation from database
            float positionX = float.Parse(GameManager.Instance.characters_positionX);
            float positionY = float.Parse(GameManager.Instance.characters_positionY);
            float positionZ = float.Parse(GameManager.Instance.characters_positionZ);
            
            Vector3 curPos = new Vector3(positionX, positionY, positionZ);
            if (curPos != Vector3.zero)
            {
                //have rotation_x
                float rotationX = float.Parse(GameManager.Instance.characters_rotation.Split('@')[0]);
                float rotationY = float.Parse(GameManager.Instance.characters_rotation.Split('@')[1]);
                float rotationZ = float.Parse(GameManager.Instance.characters_rotation.Split('@')[2]);
                float rotationW = float.Parse(GameManager.Instance.characters_rotation.Split('@')[3]);
                Quaternion curRot = new Quaternion(rotationX, rotationY, rotationZ, rotationW);
                player = Instantiate(playerManager, curPos, curRot);
            }
            else
            {
                Maps myMap = Header.Instance.mapDict[int.Parse(GameManager.Instance.characters_map)];
                player = Instantiate(playerManager, myMap.spawnPlayerPosition.position, Quaternion.identity);
            }
            //init checkAnimation
            player.CheckAnimation();
            
            player.gameObject.name = GameManager.Instance.characters_name;

            
            
            spawnPlayerDict.Add(GameManager.Instance.characters_name, player);
            
            player.IsLocalPlayer = true;
            player.playerCamera.gameObject.SetActive(true);
            player.playerCamera.transform.SetParent(null);
            player.playerCamera.GetComponent<PlayerCamera>().targetPlayer = player.transform;
            
            player.canvasHp.gameObject.SetActive(true);
            player.canvasHp.playerName.text = GameManager.Instance.characters_name;
            player.canvasHp.playerName.color = Color.cyan;

            
            player.OpenRace(int.Parse(GameManager.Instance.characters_race));
            GameManager.Instance.Player = player;
            
            HLManager.Instance.ClearToonList();
            
            //switch ost
            AudioManager.Instance.PlayOst(1);
            player.playerVFX.OpenParticleSystem(0);
        }

        public void SpawnForBot(string result)
        {
            string[] e = result.Split('|');
            string[] r = e[0].Split(' ');
            if(spawnPlayerDict.ContainsKey(r[5]))
               return;
            
            
            PlayerManager player = Instantiate(playerManager, Vector3.zero, Quaternion.identity);
            spawnPlayerDict.Add(r[5], player);
            player.gameObject.name = r[5];
            player.canvasHp.gameObject.SetActive(true);
            player.canvasHp.playerName.text = r[5];
            player.OpenRace(int.Parse(r[6]));
            
            //character
            player.characters.characters_guid= r[2];
            player.characters.characters_account = r[3];
            player.characters.characters_realm = r[4];
            player.characters.characters_name = r[5];
            player.characters.characters_race = r[6];
            player.characters.characters_class_ = r[7];
            player.characters.characters_gender = r[8];
            player.characters.characters_level = r[9];
            player.characters.characters_xp = r[10];
            player.characters.characters_money = r[11];
            player.characters.characters_positionX = r[12];
            player.characters.characters_positionY = r[13];
            player.characters.characters_positionZ = r[14];
            player.characters.characters_rotation = r[15];
            player.characters.characters_map = r[16];
            player.characters.characters_dungeonDifficulty = r[17];
            player.characters.characters_online = r[18];
            player.characters.characters_totaltime = r[19];
            player.characters.characters_leveltime = r[20];
            player.characters.characters_createDate = r[21]+" "+r[22];
            
            //character stat
            string[] g = e[1].Split(' ');
            player.characterStat.characterStat_cid = g[2];
            player.characterStat.characterStat_str = g[3];
            player.characterStat.characterStat_intel = g[4];
            player.characterStat.characterStat_vit = g[5];
            player.characterStat.characterStat_agi = g[6];
            player.characterStat.characterStat_obs = g[7];
            player.characterStat.characterStat_property = g[8];
            player.characterStat.characterStat_attack_time = float.Parse(g[9]);
            player.characterStat.characterStat_wait_time = float.Parse(g[10]);
            player.characterStat.characterStat_reload_time = float.Parse(g[11]);
            
            player.CheckAnimation();
            player.playerVFX.OpenParticleSystem(2);
            
            //add bot point
            StartCoroutine(WaitForSpawn(1, () =>
            {
                player.playerNet.UdpSendMessage($"BOT JOIN {GameManager.Instance.realmlist_guid} {r[5]}", 
                    GameManager.Instance.realmlist_ipaddress, int.Parse(GameManager.Instance.realmlist_port));
            } ));

        }

        public void SpawnCreature(string result)
        {
            string[] r = result.Split(' ');
            
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
            
            
            

            PlayerManager creature = Instantiate(playerManager);
            creature.gameObject.name = _name;
            creature.OpenRace(_race);
            creature.CheckAnimation();
            spawnCreatureDict.Add(_guid.ToString(),creature);

            float posX = _positionX!="0"?float.Parse(_positionX):0;
            float posY = _positionY!="0"?float.Parse(_positionY):0;
            float posZ = _positionZ!="0"?float.Parse(_positionZ):0;
            
            Vector3 curPos = new Vector3(posX, posY, posZ);
            creature.transform.localPosition = curPos;

            if (_rotation.Contains("@"))
            {
                string[] g = _rotation.Split('@');
                float rotX = float.Parse(g[0]);
                float rotY = float.Parse(g[1]);
                float rotZ = float.Parse(g[2]);
                float rotW = float.Parse(g[3]);
                
                var curRot = new Vector3(rotX, rotY, rotZ);
                creature.transform.localEulerAngles = curRot;

            }
            else
            {
                creature.transform.rotation = Quaternion.identity;
            }

            
            creature.canvasHp.gameObject.SetActive(true);
            creature.canvasHp.GetComponent<RectTransform>().localPosition = new Vector3(0, float.Parse(_canvasHp_Header), 0);
            creature.setName.text = _name;
            
            creature.setName.color = GameManager.Instance.GetColorNameType(_nameType);
            creature.creaturesDatabase.creatureType = (CreatureType)(_nameType);
            
            creature.creaturesDatabase.creatureWhatNpc = (CreatureWhatNpc)(_useType);

            //set creature type
            switch (creature.creaturesDatabase.creatureType)
            {
                case CreatureType.Normal:
                    break;
                case CreatureType.Friend:
                    break;
                case CreatureType.Friendly:
                    break;
                case CreatureType.Rare:
                    break;
                case CreatureType.Elite:
                    creature.IsCreature = true;
                    break;
                case CreatureType.Boss:
                    creature.IsCreature = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            

            if (creature.creaturesDatabase.creatureWhatNpc==CreatureWhatNpc.None)
            {
                creature.setGuild.gameObject.SetActive(false);
            }
            else
            {
                creature.setGuild.gameObject.SetActive(true);
                string name = creature.creaturesDatabase.creatureWhatNpc.ToString();
                string re = char.ToUpper(name[0]) + name.Substring(1);
                creature.setGuild.text = $"{re}";
            }
            
            creature.HpBar.maxValue = _maxHp;
            creature.HpBar.value = _minHp;  
            
            //set creature database
            creature.creaturesDatabase.creature_guid = _guid;
            creature.creaturesDatabase.creature_realm  =  _realm;
            creature.creaturesDatabase.creature_name = _name;
            creature.creaturesDatabase.creature_race = _race;
            creature.creaturesDatabase.creature_npc_class = _npc_class;
            creature.creaturesDatabase.creature_gender = _gender;
            creature.creaturesDatabase.creature_level = _level;
            creature.creaturesDatabase.creature_xp = _xp;
            creature.creaturesDatabase.creature_money = _money;
            creature.creaturesDatabase.creature_dropItemID = _dropItemID;
            creature.creaturesDatabase.creature_positionX = _positionX;
            creature.creaturesDatabase.creature_positionY = _positionY;
            creature.creaturesDatabase.creature_positionZ = _positionZ;
            creature.creaturesDatabase.creature_rotation = _rotation;
            creature.creaturesDatabase.creature_canvasHp_Header = _canvasHp_Header;
            creature.creaturesDatabase.creature_map = _map;
            creature.creaturesDatabase.creature_nameType = _nameType;
            creature.creaturesDatabase.creature_useType = _useType;
            creature.creaturesDatabase.creature_maxHp = _maxHp;
            creature.creaturesDatabase.creature_minHp = _minHp;
            creature.creaturesDatabase.creature_maxMp = _maxMp;
            creature.creaturesDatabase.creature_minMp = _minMp;
            creature.creaturesDatabase.creature_maxSp = _maxSp;
            creature.creaturesDatabase.creature_minSp = _minSp;
            creature.creaturesDatabase.creature_str = _str;
            creature.creaturesDatabase.creature_intel = _intel;
            creature.creaturesDatabase.creature_vit = _vit;
            creature.creaturesDatabase.creature_agi = _agi;
            creature.creaturesDatabase.creature_obs = _obs;
            
            creature.creaturesDatabase.creature_respawnTime = _respawnTime;
            creature.creaturesDatabase.creature_attackTime= _attackTime;
            creature.creaturesDatabase.creature_reloadTime = _reloadTime;
            
            creature.creaturesDatabase.creature_respawnTime_max = _respawnTime;
            creature.creaturesDatabase.creature_attackTime_max = _attackTime;
            creature.creaturesDatabase.creature_reloadTime_max = _reloadTime;
            creature.creaturesDatabase.creature_respawnTime_min = _respawnTime;
            creature.creaturesDatabase.creature_attackTime_min = _attackTime;
            creature.creaturesDatabase.creature_reloadTime_min = _reloadTime;
            
            
            
            creature.creaturesDatabase.creature_switchPosTime = _switchPosTime;
            creature.creaturesDatabase.creature_move_speed = _move_speed;
            
            
            
            //check if cur creature is die
            if (creature.HpBar.value <= 0)
            {
                creature.canvasHp.gameObject.SetActive(false);
                creature.myCollider.enabled = false;
                creature.PlayAnimation("Death");
            }
            
            //loading progress
            int loadingProgressMax = int.Parse(r[36]);
            LoadingManager.Instance.SetLoadingProgressMax(loadingProgressMax);
            LoadingManager.Instance.SetLoadingProgressMin(1);
            
            //network
            creature.playerVFX.OpenParticleSystem(0);
            StartCoroutine(WaitForSpawn(1, () =>
            {
                creature.playerNet.UdpSendMessage($"NPC JOIN {GameManager.Instance.realmlist_guid} {r[2]} {creature.transform.position.x} {creature.transform.position.y} {creature.transform.position.z}", 
                    GameManager.Instance.realmlist_ipaddress, int.Parse(GameManager.Instance.realmlist_port));
            } ));

            if (LoadingManager.Instance.isProgressBarFull)
            {
                foreach (var c in spawnCreatureDict.Values.ToList())
                {
                    c.gameObject.SetActive(false);
                }

                StartCoroutine(WaitForSpawn(0.5f, () =>
                {
                    foreach (var c in spawnCreatureDict.Values.ToList())
                    {
                        c.gameObject.SetActive(true);
                    }
                    Header.Instance.OpenGameUIHeader(6);
                } ));
            }
                
            
            
            
            
            

            
        }

        public void DeleteAllSpawnCreatures()
        {
            if(spawnCreatureDict.Count <= 0)
                return;
            foreach (var spawnCreature in spawnCreatureDict.Keys)
                Destroy(spawnCreatureDict[spawnCreature].gameObject);
            spawnCreatureDict.Clear();
        }
        
        
        public void DeleteAllSpawnCharacters()
        {
            foreach (var key in spawnPlayerDict.Keys)
            {
                Destroy(spawnPlayerDict[key].gameObject);
            }
            spawnPlayerDict.Clear();
        }

        private IEnumerator WaitForSpawn(float t, Action c)
        {
            yield return new WaitForSeconds(t);
            c?.Invoke();
        }
        #endregion
        
        //select characters panel
        public void StartGameCharacter()
        {
            if(GameManager.Instance.characters_name == string.Empty)
                MessageBoxManager.Instance.OpenMessageBox("Please select your character.");
            else
            {
                GameManager.Instance.Is_Start_Game = true;
                MessageBoxManager.Instance.Okay();
                Header.Instance.OpenGameUIHeader(5);

                //spawn for myself
                SpawnForMyself();
                
                //spawn for someone else
                world.TcpSendMessage($"SPAWN CHARACTER {GameManager.Instance.characters_name}", () =>
                {
                    MessageBoxManager.Instance.OpenMessageBox("World Server is down!.");
                    return;
                });
                StartCoroutine(WaitForSpawn(1, () =>
                {
                    //enable localPlayer
                    GameManager.Instance.Player.IsLocalPlayer = true;
                    //add player endpoint
                    GameManager.Instance.Player.playerNet.UdpSendMessage(
                        $"PLAYER JOIN {GameManager.Instance.realmlist_guid} {GameManager.Instance.characters_name}",
                        GameManager.Instance.realmlist_ipaddress, int.Parse(GameManager.Instance.realmlist_port));
                }));

            }
        }

        public void CreateCharacters()
        {
            if (spawnCharacterPosition.childCount == GameManager.Instance.MAX_CREATE_CHARACTER_COUNT)
            {
                MessageBoxManager.Instance.OpenMessageBox("You have reached the maximum number of characters.");
                return;
            }
            
            Header.Instance.OpenGameUIHeader(4);
            HLManager.Instance.ClearToonList();
            HLManager.Instance.SelectFirstCreateToon();
            
            playerIdInput.Select();
        }

        public void DeleteCharacters()
        {
            MessageBoxManager.Instance.OpenMessageBox("The [Delete] function is not working at the moment");
        }

        public void RetrieveCharacters()
        {
            MessageBoxManager.Instance.OpenMessageBox("The [Retrieve] function is not working at the moment");

        }

        public void BackCharacters()
        {
            HLManager.Instance.ClearToonList();
            world.Disconnect();
            Header.Instance.OpenGameUIHeader(0);
            Header.Instance.OpenMapHeader(0);
            
            LoginManager.Instance.userNameInput.text = string.Empty;
            LoginManager.Instance.passwordInput.text = string.Empty;
            
            playerManager.CloseRace();
        }

        public void CheckCharactersCount()
        {
            if (spawnCharacterPosition.childCount > 0)
            {
                EnableSpecialButton();
                HLManager.Instance.SelectFirstToon();
                
            }
            else
            {
                DisableSpecialButton();
            }
        }
        
        //create characters panel
        public void JustEnterGameCharacters()
        {
            //create character and insert into database
            if (GameManager.Instance.characters_race_enum == 999)
            {
                MessageBoxManager.Instance.OpenMessageBox("Please choose your race.");
                return;
            }

            if (playerIdInput.text == string.Empty)
            {
                MessageBoxManager.Instance.OpenMessageBox("Please enter your player ID.");
                return;
            }
            MessageBoxManager.Instance.OpenMessageBox("Creating character...", null);
            Header.Instance.CloseGameUIHeader();    
            world.TcpSendMessage($"CHAT CREATE_CHARACTER {GameManager.Instance.account_guid} {GameManager.Instance.realmlist_guid} {playerIdInput.text} {GameManager.Instance.characters_race_enum}",() =>
            {
                MessageBoxManager.Instance.OpenMessageBox("World Server is down",null);
            });
            

            //loading screen
            //handle player movement
            //handle spawn player
            
        }

        public void BackCreateCharacters()
        {
            Header.Instance.OpenGameUIHeader(3);
            
            GameManager.Instance.characters_race_enum = 999;
            playerIdInput.text = string.Empty;
            HLManager.Instance.SelectFirstToon();
        }

        public void GetSetCharacters(string result)
        {
            string[] e = result.Split('|');
            
            string[] characters = e[0].Split(' ');
            GameManager.Instance.characters_guid = characters[2];
            GameManager.Instance.characters_account = characters[3];
            GameManager.Instance.characters_realm = characters[4];
            GameManager.Instance.characters_name = characters[5];
            GameManager.Instance.characters_race = characters[6];
            GameManager.Instance.characters_class_ = characters[7];
            GameManager.Instance.characters_gender = characters[8];
            GameManager.Instance.characters_level = characters[9];
            GameManager.Instance.characters_xp = characters[10];
            GameManager.Instance.characters_money = characters[11];
            GameManager.Instance.characters_positionX = characters[12];
            GameManager.Instance.characters_positionY = characters[13];
            GameManager.Instance.characters_positionZ = characters[14];
            GameManager.Instance.characters_rotation = characters[15];
            GameManager.Instance.characters_map = characters[16];
            GameManager.Instance.characters_dungeonDifficulty = characters[17];
            GameManager.Instance.characters_online = characters[18];
            GameManager.Instance.characters_totaltime = characters[19];
            GameManager.Instance.characters_leveltime = characters[20];
            GameManager.Instance.characters_canvasHp_Header = characters[21];
            GameManager.Instance.characters_createDate = characters[22]+" "+characters[23];

            string[] characterStat = e[1].Split(' ');
            GameManager.Instance.characterStat_cid = characterStat[2];
            GameManager.Instance.characterStat_str = characterStat[3];
            GameManager.Instance.characterStat_intel = characterStat[4];
            GameManager.Instance.characterStat_vit = characterStat[5];
            GameManager.Instance.characterStat_agi = characterStat[6];
            GameManager.Instance.characterStat_obs = characterStat[7];
            GameManager.Instance.characterStat_property = characterStat[8];
            GameManager.Instance.characterStat_attack_time = float.Parse(characterStat[9]);
            GameManager.Instance.characterStat_wait_time = float.Parse(characterStat[10]);
            GameManager.Instance.characterStat_reload_time = float.Parse(characterStat[11]);
            
            //spawn character
            StartGameCharacter();
        }
        
        
        
        
        
    }
}
