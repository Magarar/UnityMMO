using System;
using System.Collections.Generic;
using System.IO;
using Manager;
using Server;
using UnityEngine;

namespace Config
{
    public class SystemConfig : MonoBehaviour
    {
        public static SystemConfig Instance;
        
        public World world;
        public Authentication authentication;
        
        public string dir;
        public string fileName = "SystemConfig.cfg";
        public List<string> cfgDefault = new List<string>()
        {
            "Authentication_IPAddress=127.0.0.1",
            "Authentication_Port=5545",
            "BackgroundOst=1",
            "BackgroundInterface=1",
            "WindowsMode=1",
            "FullMode=0",
            "DisplayRes=0",
            "Luminance=1",
            "AllowTrading=1",
            "HideAround=0"
        };

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
            {
                Destroy(this.gameObject);
            }
            
        }

        private void Start()
        {
            StartDir();
        }

        public bool GetBoolByInt(int value) =>value != 0;
        
        public int GetIntByBool(bool value) => value ? 1 : 0;

        private void StartWrite()
        {
            using (var sw = new StreamWriter(File.Create($@"{dir}\{fileName}")))
            {
                for (int i = 0; i < cfgDefault.Count; i++)
                {
                    sw.WriteLine(cfgDefault[i]);
                }
            }
        }
        
        private void StartRead()
        {
            using (var sr = new StreamReader(File.OpenRead($@"{dir}\{fileName}")))
            {
                string peek = string.Empty;
                while ((peek = sr.ReadLine())!=null)
                {
                    string[] r = peek.Split('=');
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
                        InGameManager.Instance.backgroundOST.maxValue = 1;
                        InGameManager.Instance.backgroundOST.value = float.Parse(r[1]);
                        AudioManager.Instance.asOst.volume = float.Parse(r[1]);
                    }

                    if (r[0] == "BackgroundInterface")
                    {
                        InGameManager.Instance.backgroundUIInterface.maxValue = 1;
                        InGameManager.Instance.backgroundUIInterface.value = float.Parse(r[1]);
                        AudioManager.Instance.asInterface.volume = float.Parse(r[1]);
                    }

                    if (r[0] == "WindowsMode")
                    {
                        InGameManager.Instance.windowsMode.isOn = GetBoolByInt(int.Parse(r[1]));
                        if (InGameManager.Instance.windowsMode.isOn)
                        {
                            
                        }
                    }

                    if (r[0] == "FullMode")
                    {
                        InGameManager.Instance.fullMode.isOn = GetBoolByInt(int.Parse(r[1]));
                    }

                    if (r[0] == "DisplayRes")
                    {
                        InGameManager.Instance.screenRes.value = int.Parse(r[1]);
                            
                        int wScreen = InGameManager.Instance.wScreen[int.Parse(r[1])];
                        int hScreen = InGameManager.Instance.hScreen[int.Parse(r[1])];
                        int rScreen = InGameManager.Instance.rScreen[int.Parse(r[1])];
                        //window mode activation
                        if (InGameManager.Instance.windowsMode.isOn)
                        {
                            Screen.SetResolution(wScreen, hScreen, FullScreenMode.Windowed,rScreen);
                        }
                            
                        //full mode
                        if (InGameManager.Instance.fullMode.isOn)
                        {
                            Screen.SetResolution(wScreen, hScreen, FullScreenMode.FullScreenWindow, rScreen);
                        }
                    }

                    if (r[0] == "Luminance")
                    {
                        InGameManager.Instance.luminance.isOn = GetBoolByInt(int.Parse(r[1]));
                    }

                    if (r[0] == "AllowTrading")
                    {
                        InGameManager.Instance.allTrading.isOn = GetBoolByInt(int.Parse(r[1]));
                    }

                    if (r[0] == "HideAround")
                    {
                        InGameManager.Instance.hideAround.isOn = GetBoolByInt(int.Parse(r[1]));
                    }

                        
                        
                }
            }
        }
        

        private void StartDir()
        {
            dir = Directory.GetCurrentDirectory();
            
            if (File.Exists($@"{dir}\{fileName}"))
            {
                //start read
                StartRead();
            }
            else
            {
                //start create cfg
                StartWrite();
                //start read
                StartRead();
            }
        }

        public void WriteChangeConfig()
        {
            using (var sw = new StreamWriter($@"{dir}\{fileName}",false))
            {
                sw.WriteLine($"Authentication_IPAddress={authentication.authenticationIpaddress}");
                sw.WriteLine($"Authentication_Port={authentication.authenticationPort}");
                sw.WriteLine($"BackgroundOst={AudioManager.Instance.asOst.volume}");
                sw.WriteLine($"BackgroundInterface={AudioManager.Instance.asInterface.volume}");
                sw.WriteLine($"WindowsMode={GetIntByBool(InGameManager.Instance.windowsMode.isOn)}");
                sw.WriteLine($"FullMode={GetIntByBool(InGameManager.Instance.fullMode.isOn)}");
                sw.WriteLine($"DisplayRes={InGameManager.Instance.screenRes.value}");
                sw.WriteLine($"Luminance={GetIntByBool(InGameManager.Instance.luminance.isOn)}");
                sw.WriteLine($"AllowTrading={GetIntByBool(InGameManager.Instance.allTrading.isOn)}");
                sw.WriteLine($"HideAround={GetIntByBool(InGameManager.Instance.hideAround.isOn)}");
                
            }
        }

        
    }
}
