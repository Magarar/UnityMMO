using System;
using System.Collections.Generic;
using Config;
using Server;
using TMPro;
using UIs;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Manager
{
    public class InGameManager : MonoBehaviour
    {
        public static InGameManager Instance;
        public World world;
        public Authentication authentication;

        [Header("System")]
        public Slider backgroundOST;
        public Slider backgroundUIInterface;
        public Toggle windowsMode;
        public Toggle fullMode;
        public List<int> wScreen,hScreen,rScreen = new List<int>();
        public TMP_Dropdown screenRes;
        public Toggle luminance;
        public Toggle allTrading;
        public Toggle hideAround;

        
        
        [HideInInspector] public float saveOriginalValueOst;
        [HideInInspector] public float saveOriginalValueInterface;
        [HideInInspector] public bool saveWindowMode;
        [HideInInspector] public bool saveFullMode;
        [HideInInspector] public int saveScreenIndex;
        [HideInInspector] public bool saveLuminance;
        [HideInInspector] public bool saveAllowTrading;
        [HideInInspector] public bool saveHideAround;

        [Header("Return to Character")]
        private float returnCharactersTime = 15f;
        private bool returnCharactersBool;
        public TextMeshProUGUI returnCharactersInfo;
        
        [Header("Return to exit")]
        private float returnExitTime = 15f;
        private bool returnExitBool;
        public TextMeshProUGUI returnExitInfo;
        
        [FormerlySerializedAs("systemSetting")] public GameObject systemMenu;
        public List<GameObject> settingLayer = new List<GameObject>();
        
        //verify that user open system
        private bool IsOpenSystem;

        


        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        private void Start()
        {
            //test
            backgroundOST.maxValue = 1;
            backgroundOST.value = AudioManager.Instance.asOst.volume;
            
            //set dropdown screen 
            screenRes.ClearOptions();
            List<string> srl = new List<string>();
            for (int i = 0; i < wScreen.Count; i++)
            {
                string s_srl = $"{wScreen[i]}x{hScreen[i]} {rScreen[i]} rate";
                srl.Add(s_srl);
            }
            screenRes.AddOptions(srl);
            screenRes.RefreshShownValue();

            screenRes.value = 0;
            saveScreenIndex = screenRes.value;
        }
        
        private void Update()
        {
            OpenSystemSetting();
            Update_ReturnCharacters();
            Update_ReturnExit();
            Update_System();
        }

        
        private void OpenSystemSetting()
        {
            if(!GameManager.Instance.Is_Start_Game)
                return;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                systemMenu.SetActive(true);
                OpenSystemSetting(0);
                GameManager.Instance.Is_Lock_Character = true;
                
                //block player from moving when click on esc enter system menu
                GameManager.Instance.Player.agent.isStopped = true;
                GameManager.Instance.Player.curPosition = Vector3.zero;
                GameManager.Instance.Player.PlayAnimation("IDLE");
                GameManager.Instance.animationName = "IDLE";
                ChatSystemManager.Instance.ResetChat();
                //close all ui panel
                CharacterSettingManager.Instance.CloseAllPanelHeader();
                //reset drag
                DragDropManager.Instance.DragReset();
                DragDropManager.Instance.dragDropSlot.gameObject.SetActive(false);
                SplitManager.Instance.SplitItemReset();
                SplitManager.Instance.dragDropSlot.gameObject.SetActive(false);
                SplitManager.Instance.splitItemPanel.SetActive(false);
                SplitManager.Instance.isArrow = false;
                SplitManager.Instance.splitArrow.gameObject.SetActive(false);
                if (IsOpenSystem)
                {
                    
                    IsOpenSystem = false;
                    ReturnToGame_Confirm_System();
                }
                
            }
        }

        /// <summary>
        /// 1 = system
        /// 2 = return to character
        /// 3 = offline manager
        /// 4 = exit
        /// </summary>
        /// <param name="settingID"></param>
        public void OpenSystemSetting(int settingID)
        {
            
            foreach (var panel in settingLayer)
            {
                panel.SetActive(false);
            }

            if (settingID == 0)
            {
                settingLayer[settingID].SetActive(true);
                return;
            }

            if (settingID == 1)
            {
                settingLayer[settingID].SetActive(true);
                saveOriginalValueOst = backgroundOST.value;
                saveOriginalValueInterface = backgroundUIInterface.value;
                
                saveWindowMode = windowsMode.isOn;
                saveFullMode = fullMode.isOn;
                saveScreenIndex = screenRes.value;
                
                saveLuminance = luminance.isOn;
                
                saveAllowTrading = allTrading.isOn;
                saveHideAround = hideAround.isOn;
                
                IsOpenSystem = true;
                
                return;
            }

            if (settingID == 2)
            {
                ReturnToGame();
                GameManager.Instance.Is_Start_Game = false;
                GameManager.Instance.Is_Lock_Character = false;
                GameManager.Instance.Player.IsLocalPlayer = false;
                
            
                GameManager.Instance.characters_race_enum = 999;
                CharactersManager.Instance.playerIdInput.text = string.Empty;
                
                CharactersManager.Instance.DeleteAllSpawnCharacters();
                CharactersManager.Instance.DeleteAllSpawnCreatures();
                InventorySystemManager.Instance.ClearAllInventory();
                LoadingManager.Instance.ResetProgressBar();
                MessageBoxManager.Instance.OpenMessageBox("Retrieve Characters...");
                world.TcpSendMessage("RETRIEVE INGAME", () =>
                {
                    MessageBoxManager.Instance.OpenMessageBox("World Server is down");
                });
                return;
            }


            if (settingID == 4 && GameManager.Instance.Is_Inside_City)
            {
                ForceExit();
                return;
            }

            if (settingID == 4 && !GameManager.Instance.Is_Inside_City)
            {
                settingLayer[settingID].SetActive(true);
                returnExitBool = true;
                returnExitTime = 15f;
                return;
            }
            settingLayer[settingID].SetActive(true);
        }

        public void DisableSystemSetting()
        {
            
        }

        public void CloseSystemSetting()
        {
            
        }

        public void ReturnToGame()
        {
            systemMenu.SetActive(false);
            foreach (var panel in settingLayer)
            {
                panel.SetActive(false);
            }
            GameManager.Instance.Is_Lock_Character = false;
            GameManager.Instance.Player.agent.isStopped = false;

        }

        #region return to character
        
        public void ReturnToGame_Cancel_ReturnToCharacters()
        {
            returnCharactersBool = false;
            returnCharactersTime = 15f;
            
            ReturnToGame();
        }

        private void Update_ReturnCharacters()
        {
            if (returnCharactersBool)
            {
                returnCharactersInfo.text = $"{Mathf.RoundToInt(returnCharactersTime)} sec until return to characters";
                returnCharactersTime -= Time.deltaTime;
                if (returnCharactersTime <= 0)
                {
                    returnCharactersTime = 15;
                    returnCharactersBool = false;
                    
                    GameManager.Instance.Is_Start_Game = false;
                    GameManager.Instance.Is_Lock_Character = false;
                    GameManager.Instance.Player.IsLocalPlayer = false;
                
                    CharactersManager.Instance.DeleteAllSpawnCharacters();
                    CharactersManager.Instance.DeleteAllSpawnCreatures();
                    InventorySystemManager.Instance.ClearAllInventory();
                    LoadingManager.Instance.ResetProgressBar();
                    
                    Header.Instance.OpenGameUIHeader(3);
            
                    GameManager.Instance.characters_race_enum = 999;
                    CharactersManager.Instance.playerIdInput.text = string.Empty;
                    
                    MessageBoxManager.Instance.OpenMessageBox("Retrieve Characters...");
                    world.TcpSendMessage("RETRIEVE INGAME", () =>
                    {
                        MessageBoxManager.Instance.OpenMessageBox("World Server is down");
                    });

                }
            }
        }
        #endregion

        public void ReturnToGame_Cancel_ReturnToExit()
        {
            returnExitBool = false;
            returnExitTime = 15f;
            
            ReturnToGame();
        }

        private void Update_ReturnExit()
        {
            if (returnExitBool)
            {
                returnExitInfo.text = $"{Mathf.RoundToInt(returnExitTime)} sec until return to exit";
                returnExitTime -= Time.deltaTime;
                if (returnExitTime <= 0)
                {
                    returnExitTime = 15;
                    returnExitBool = false;
                    Application.Quit();
                }
            }
        }
        
        //offline manager
        public void Confirm_OfflineManager(int jobIndex)
        {
            if (jobIndex == 0)
            {
                //click confirm
            }
            
            if (jobIndex == 1)
            {
                //click cancel
            }

            
        }

        public void ForceExit()
        {
            Application.Quit();
        }

        #region System
        //system
        private void Update_System()
        {
            AudioManager.Instance.asOst.volume = Mathf.Clamp(backgroundOST.value, 0, 1);
            AudioManager.Instance.asInterface.volume = Mathf.Clamp(backgroundUIInterface.value, 0, 1);
            if (GameManager.Instance.Player != null)
            {
                GameManager.Instance.Player.asInterface.volume =  Mathf.Clamp(backgroundUIInterface.value, 0, 1);
            }
        }

        public void ReturnToGame_Cancel_System()
        {
            OpenSystemSetting(0);
            
            backgroundOST.value = saveOriginalValueOst;
            backgroundUIInterface.value = saveOriginalValueInterface;
            
            windowsMode.isOn = saveWindowMode;
            fullMode.isOn = saveFullMode;
            screenRes.value = saveScreenIndex;
            
            luminance.isOn = saveLuminance;
            
            allTrading.isOn = saveAllowTrading;
            hideAround.isOn = saveHideAround;
        }

        public void ReturnToGame_Confirm_System()
        {
            OpenSystemSetting(0);
            
            int resIndex = screenRes.value;
            int ws = wScreen[resIndex];
            int hs = hScreen[resIndex];
            int rs = rScreen[resIndex];

            if (windowsMode.isOn)
            {
                Screen.SetResolution(ws, hs, FullScreenMode.Windowed,rs);
            }

            if (fullMode.isOn)
            {
                Screen.SetResolution(ws, hs, FullScreenMode.FullScreenWindow,rs);
            }

            GameManager.Instance.Player.asInterface.volume = backgroundUIInterface.value;
            //missing luminance in (Game Appearance)
            
            //missing interface (Game Interface)
            
            //missing (Other Setting)
            
            //start change config
            SystemConfig.Instance.WriteChangeConfig();
            
        }

        public void SystemDefaultSettings()
        {
            List<string> cfg = SystemConfig.Instance.cfgDefault;
            foreach (var c in cfg)
            {
                string[] r = c.Split('=');
                if (r[0] == "Authentication_IPAddress")
                {
                    authentication.authenticationIpaddress = r[1];
                }

                if (r[0] == "Authentication_Port")
                {
                    authentication.authenticationPort = int.Parse(r[1]);
                }

                if (r[0] == "BackgroundOst")
                {
                    backgroundOST.maxValue = 1;
                    backgroundOST.value = float.Parse(r[1]);
                    AudioManager.Instance.asOst.volume = float.Parse(r[1]);
                }

                if (r[0] == "BackgroundInterface")
                {
                    backgroundUIInterface.maxValue = 1;
                    backgroundUIInterface.value = float.Parse(r[1]);
                    AudioManager.Instance.asInterface.volume = float.Parse(r[1]);
                    GameManager.Instance.Player.asInterface.volume = 1;
                }

                if (r[0] == "WindowsMode")
                {
                    windowsMode.isOn = SystemConfig.Instance.GetBoolByInt(int.Parse(r[1]));
                    if (windowsMode.isOn)
                    {
                            
                    }
                }

                if (r[0] == "FullMode")
                {
                    fullMode.isOn = SystemConfig.Instance.GetBoolByInt(int.Parse(r[1]));
                }

                if (r[0] == "DisplayRes")
                {
                    Instance.screenRes.value = int.Parse(r[1]);
                            
                    int _wScreen = wScreen[int.Parse(r[1])];
                    int _hScreen = hScreen[int.Parse(r[1])];
                    int _rScreen = rScreen[int.Parse(r[1])];
                    //window mode activation
                    if (windowsMode.isOn)
                    {
                        Screen.SetResolution(_wScreen, _hScreen, FullScreenMode.Windowed,_rScreen);
                    }
                            
                    //full mode
                    if (fullMode.isOn)
                    {
                        Screen.SetResolution(_wScreen,_hScreen, FullScreenMode.FullScreenWindow, _rScreen);
                    }
                }

                if (r[0] == "Luminance")
                {
                    luminance.isOn = SystemConfig.Instance.GetBoolByInt(int.Parse(r[1]));
                }

                if (r[0] == "AllowTrading")
                {
                    allTrading.isOn = SystemConfig.Instance.GetBoolByInt(int.Parse(r[1]));
                }

                if (r[0] == "HideAround")
                {
                    hideAround.isOn = SystemConfig.Instance.GetBoolByInt(int.Parse(r[1]));
                }
                
            }
            SystemConfig.Instance.WriteChangeConfig();
            
            
            
        }

        public void OpenGameInterface()
        {
            
        }
        
        
        #endregion

        

        
    }
}
