using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Server;
using TMPro;
using UIs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Manager
{
    public class ChatSystemManager : MonoBehaviour
    {
        public static ChatSystemManager Instance;
        
        public Authentication authentication;
        public World world;
        
        public List<Button> swapBtn = new List<Button>();
        public List<TextMeshProUGUI> swapBtnTxt = new List<TextMeshProUGUI>();
        public List<TextMeshProUGUI> channelTxt = new List<TextMeshProUGUI>();
        public TMP_InputField chatInput;
        public ScrollRect chatScrollRect;
        [Header("Emoji")]
        public List<TMP_SpriteAsset> emojiList = new ();
        public Transform emojiSpawnParent;
        public GameObject emojiPanel;
        public EmojiSelection emojiBtn;
        [Header("Custom chat")]
        public GameObject customChatChannels;
        public TextMeshProUGUI customChatChannelTxt;
        
        public Dictionary<string,string> emojiDict = new Dictionary<string, string>();
        
        
        private bool pressEnter = false;

        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
            
            ChangeSwapBtnChannelTxtColor(0);
            ChangeChanelTxt(0);
        }

        private void Start()
        {
            SpawnEmojiList();
        }

        private void Update()
        {
            // if(!GameManager.Instance.Is_Start_Game)
            //     return;
            UpdateOnPressEnter();
            UpdateOnWhisperChat();
            UpdateOnPressFontSlash();
        }

        private void UpdateOnPressEnter()
        {
            if (!pressEnter && Input.GetKeyDown(KeyCode.Return))
            {
                if(GameManager.Instance.Is_Lock_Character)
                    return;
                
                EventSystem.current.SetSelectedGameObject(null);

                if(GameManager.Instance.curUseChannel==0 || 
                   GameManager.Instance.curUseChannel==1 ||
                   GameManager.Instance.curUseChannel==3 ||
                   GameManager.Instance.curUseChannel==4 || 
                   GameManager.Instance.curUseChannel==6)
                    GameManager.Instance.curUseChannel = 0;
                
                GameManager.Instance.curUseChannel = 0;
                chatInput.gameObject.SetActive(true);
                GameManager.Instance.IsChatting = true;
                chatInput.Select();
              
                
                
                
                pressEnter = true;
                return;
            }

            if (pressEnter && Input.GetKeyDown(KeyCode.Return))
            {
                SendChat();
                return;
            }
        }

        private void UpdateOnWhisperChat()
        {
            if(GameManager.Instance.currentWhoWhisper==string.Empty)
                return;
            if (!chatInput.text.StartsWith($"$\"<color=purple>{GameManager.Instance.currentWhoWhisper}</color>"))
            {
                GameManager.Instance.currentWhoWhisper = string.Empty;
                GameManager.Instance.curUseChannel = 0;
                chatInput.text = $"/{GameManager.Instance.currentWhoWhisper}";
            }
        }

        public void UpdateOnPressFontSlash()
        {
            if (!pressEnter && Input.GetKeyDown(KeyCode.Slash))
            {
                if(GameManager.Instance.Is_Lock_Character)
                    return;
                chatInput.text = "/";
                
                EventSystem.current.SetSelectedGameObject(null);
                
                if(GameManager.Instance.curUseChannel==0 || 
                   GameManager.Instance.curUseChannel==1 ||
                   GameManager.Instance.curUseChannel==3 ||
                   GameManager.Instance.curUseChannel==4 || 
                   GameManager.Instance.curUseChannel==6)
                    GameManager.Instance.curUseChannel = 0;
                
                GameManager.Instance.curUseChannel = 0;
                chatInput.gameObject.SetActive(true);
                pressEnter = true;
                GameManager.Instance.IsChatting = true;

                StartCoroutine(WaitForSecond(0.1f, () =>
                {
                    chatInput.Select();
                    chatInput.caretPosition = chatInput.text.Length;
                }));
            }
        }

        public void SendChat()
        {
            if(!pressEnter)
                return;
            
            if(chatInput.text == "")
                return;

            string[] r;
            
            //chatting
            switch (GameManager.Instance.curUseChannel)
            {
                case 0://all
                    world.TcpSendMessage($"CHATTING ALL {GameManager.Instance.characters_name} {chatInput.text}",null);
                    break;
                case 1://private
                    world.TcpSendMessage($"CHATTING PRIVATE {GameManager.Instance.characters_name} {chatInput.text}",null);
                    break;
                case 2://system
                    world.TcpSendMessage($"CHATTING SYSTEM {GameManager.Instance.characters_name} {chatInput.text}",null);
                    break;
                case 3://school
                    world.TcpSendMessage($"CHATTING SCHOOL {GameManager.Instance.characters_name} {chatInput.text}",null);
                    break;
                case 4://guild
                    world.TcpSendMessage($"CHATTING GUILD {GameManager.Instance.characters_name} {chatInput.text}",null);
                    break;
                case 5://world
                    world.TcpSendMessage($"CHATTING WORLD {GameManager.Instance.characters_name} {chatInput.text}",null);  
                    break;
                case 6://whisper
                    r = chatInput.text.Split(' ');
                    if (r.Length > 1)
                    {
                        Debug.Log("whisper");
                        if (r[0].StartsWith("<color") && r[0].EndsWith("</color>"))
                            r[0] = GameManager.Instance.currentWhoWhisper;
                        string reChat = string.Join(" ", r);
                        world.TcpSendMessage($"CHATTING WHISPER {GameManager.Instance.characters_name} {reChat}",null);
                    }
                    break;
            }
            
            
            chatInput.gameObject.SetActive(false);
            chatInput.text = "";
            EventSystem.current.SetSelectedGameObject(null);
            GameManager.Instance.IsChatting = false;
            pressEnter = false;
            GameManager.Instance.currentWhoWhisper = string.Empty;
            emojiPanel.SetActive(false);
            customChatChannels.SetActive(false);
        }

        public void ResetChat()
        {
            chatInput.gameObject.SetActive(false);
            chatInput.text = "";
            EventSystem.current.SetSelectedGameObject(null);
            GameManager.Instance.IsChatting = false;
            pressEnter = false;
            emojiPanel.SetActive(false);
            customChatChannels.SetActive(false);
        }

        //channels
        

        public void OnChangeValueChatInput(string txt)
        {
            if (txt.Length > 0)
            {
                string[] r = txt.Split(' ');
                if (r.Length > 1)
                {
                    string curPlayer = r[0].Replace("/","");
                    if (CharactersManager.Instance.spawnPlayerDict.ContainsKey(curPlayer))
                    {
                        customChatChannelTxt.text = "S";
                        customChatChannelTxt.color = Color.white;
                        
                        GameManager.Instance.currentWhoWhisper = curPlayer;
                        chatInput.text = $"<color=purple>{curPlayer}</color>";
                        GameManager.Instance.curUseChannel = 6;
                        StartCoroutine(WaitForSecond(0.5f, () => { 
                            EventSystem.current.SetSelectedGameObject(null);
                            chatInput.Select();
                            chatInput.caretPosition = chatInput.text.Length; }));
                        

                    }
                }
                
               
            }

            
            
        }

        private IEnumerator WaitForSecond(float t, Action callback)
        {
            yield return new WaitForSeconds(t);
            callback?.Invoke();
        }
        
        public void SetAllChannelTxt(string result)
        {
            //calculate distance
            
            string[] re = result.Split(' ');
            string[] ro = re.Skip(3).ToArray();
            string reResult = $"<link=Around-{re[2]}><color=grey>[Around]</color> {re[2]}</link>: ";
            for (int i = 0; i < ro.Length; i++)
            {
                if (emojiDict.ContainsKey(ro[i]))
                {
                    ro[i] = emojiDict[ro[i]];
                }
            }
            reResult += string.Join(" ", ro);
            
            if (re[2] == GameManager.Instance.characters_name)
            {
                channelTxt[0].text += "\n"+reResult+"\n ";
                channelTxt[1].ForceMeshUpdate();
            }
            else
            {
                float distance = Vector3.Distance(CharactersManager.Instance.spawnPlayerDict[re[2]].transform.position, GameManager.Instance.Player.transform.position);
                if (distance < 50)
                {
                       
                    channelTxt[0].text +="\n"+reResult+"\n ";
                    channelTxt[1].ForceMeshUpdate();
                }
            }
            
        }

        public void SetPrivateChannelTxt(string result)
        {
            
        }

        public void SetSystemChannelTxt(string result)
        {
            
        }

        public void SetSchoolChannelTxt(string result)
        {
            
        }

        public void SetGuildChannelTxt(string result)
        {
            
        }

        public void SetWorldChannelTxt(string result)
        {
            string[] re = result.Split(' ');
            string[] ro = re.Skip(3).ToArray();
            string reResult = $"<link=World-{re[2]}><color=green>[World]</color> {re[2]}</link>: ";
            for (int i = 0; i < ro.Length; i++)
            {
                if (emojiDict.ContainsKey(ro[i]))
                {
                    ro[i] = emojiDict[ro[i]];
                }
                reResult += ro[i];
            }
            channelTxt[0].text += "\n"+reResult+"\n ";
            channelTxt[1].ForceMeshUpdate();
        }

        public void SetWhisperTxt(string result)
        {
            string[] re = result.Split(' ');
            string[] ro = re.Skip(3).ToArray();
            string reResult = $"<link=Whisper-{re[2]}><color=purple>[Whisper]</color> {re[2]}</link>: ";
            for (int i = 0; i < ro.Length; i++)
            {
                if (emojiDict.ContainsKey(ro[i]))
                {
                    ro[i] = emojiDict[ro[i]];
                }
                reResult += ro[i];
            }
            channelTxt[0].text += "\n"+reResult+"\n ";
            channelTxt[1].ForceMeshUpdate();
        }

        public void SwapChannel(int channelIndex)
        {
            ChangeSwapBtnChannelTxtColor(channelIndex);
            ChangeChanelTxt(channelIndex);
            GameManager.Instance.curUseChannel = channelIndex;
        }

        public void OpenEmojiList()
        {
            emojiPanel.SetActive(!emojiPanel.activeSelf);
        }

        public void OpenEmotionalList()
        {
            
        }
        
        public void SendChatMessage()
        {
            
        }

        public void OpenSwapChannelCustom()
        {
            customChatChannels.SetActive(!customChatChannels.activeSelf);
        }

        private void ChangeSwapBtnChannelTxtColor(int channelIndex)
        {
            foreach (var r_swapBtnTxt in swapBtnTxt)
            {
                r_swapBtnTxt.color = Color.grey;
            }
            swapBtnTxt[channelIndex].color = Color.green;
        }

        private void ChangeChanelTxt(int channelIndex)
        {
            foreach (var r_channelTxt in channelTxt)
                r_channelTxt.gameObject.SetActive(false);
            channelTxt[channelIndex].gameObject.SetActive(true);
            chatScrollRect.content = channelTxt[channelIndex].GetComponent<RectTransform>();
        }

        public void SpawnEmojiList()
        {
            for (var i = 0; i < emojiList.Count; i++)
            {
                var r_emoji = emojiList[i];
                var emoji = Instantiate(emojiBtn, emojiSpawnParent, false);
                emoji.transform.localScale = Vector3.one;
                emoji.gameObject.name = r_emoji.name;
                emoji.SetEmoji(r_emoji, i,emojiPanel,chatInput);
            }
        }

        public void ChooseCustomChatChannel(int channelIndex)
        {
            customChatChannels.SetActive(false);
            switch (channelIndex)
            {
                case 0://around player
                    customChatChannelTxt.text = "S";
                    customChatChannelTxt.color = Color.white;
                    GameManager.Instance.curUseChannel = 0;
                    chatScrollRect.content = channelTxt[0].GetComponent<RectTransform>();
                    break;
                case 5://world channel
                    customChatChannelTxt.text = "GM";
                    customChatChannelTxt.color = Color.red;
                    GameManager.Instance.curUseChannel = 5;
                    chatScrollRect.content = channelTxt[5].GetComponent<RectTransform>();
                    break;
                case 2://system channel
                    customChatChannelTxt.text = "W";
                    customChatChannelTxt.color = Color.red;
                    GameManager.Instance.curUseChannel = 2;
                    chatScrollRect.content = channelTxt[2].GetComponent<RectTransform>();
                    break;
            }
            chatInput.Select();
        }
        
        //click on text activation
        public void ClickTextAround(string one, string two)
        {
  
            if (chatInput.gameObject.activeInHierarchy)
            {
                chatInput.text = "/"+two;
                StartCoroutine(WaitForSecond(0.1f, () =>
                {
                    chatInput.text += " ";
                    
                }));
                pressEnter = true;
                return;
            }
            
            if (!chatInput.gameObject.activeInHierarchy)//if chat input does open
            {
                if(GameManager.Instance.Is_Lock_Character)
                    return;
                
                EventSystem.current.SetSelectedGameObject(null);
                
                GameManager.Instance.curUseChannel = 6;
                chatInput.gameObject.SetActive(true);
                GameManager.Instance.IsChatting = true;

                chatInput.text = "/"+two;
                StartCoroutine(WaitForSecond(0.1f, () =>
                {
                    chatInput.text += " ";
                }));
                pressEnter = true;
                return;
            }

            
        }
        
        public void ClickTextWorld(string one, string two)
        {

            if (chatInput.gameObject.activeInHierarchy)
            {
                chatInput.text = "/"+two;
                StartCoroutine(WaitForSecond(0.1f, () =>
                {
                    chatInput.text += " ";
                    
                }));
                pressEnter = true;
                return;
            }
            
            if (!chatInput.gameObject.activeInHierarchy)//if chat input does open
            {
                if(GameManager.Instance.Is_Lock_Character)
                    return;
                
                EventSystem.current.SetSelectedGameObject(null);
                
                GameManager.Instance.curUseChannel = 6;
                chatInput.gameObject.SetActive(true);
                GameManager.Instance.IsChatting = true;

                chatInput.text = "/"+two;
                StartCoroutine(WaitForSecond(0.1f, () =>
                {
                    chatInput.text += " ";
                }));
                pressEnter = true;
                return;
            }
        }
        
        public void ClickTextWhisper(string one, string two)
        {

            if (chatInput.gameObject.activeInHierarchy)
            {
                chatInput.text = "/"+two;
                StartCoroutine(WaitForSecond(0.1f, () =>
                {
                    chatInput.text += " ";
                    
                }));
                pressEnter = true;
                return;
            }
            
            if (!chatInput.gameObject.activeInHierarchy)//if chat input does open
            {
                if(GameManager.Instance.Is_Lock_Character)
                    return;
                
                EventSystem.current.SetSelectedGameObject(null);
                
                GameManager.Instance.curUseChannel = 6;
                chatInput.gameObject.SetActive(true);
                GameManager.Instance.IsChatting = true;

                chatInput.text = "/"+two;
                StartCoroutine(WaitForSecond(0.1f, () =>
                {
                    chatInput.text += " ";
                }));
                pressEnter = true;
                return;
            }
        }
        
    }
}
