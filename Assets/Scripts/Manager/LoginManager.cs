using Server;
using TMPro;
using UIs;
using UnityEngine;

namespace Manager
{
    public class LoginManager : MonoBehaviour
    {
        public static LoginManager Instance;
        
        public Authentication authentication;
        public World world;
        

        public TextMeshProUGUI loginRealmlistHeader;
        
        public TMP_InputField userNameInput;
        public TMP_InputField passwordInput;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            AudioManager.Instance.PlayOst(0);
        }
        
        private void Update()
        {
            PressTab();
        }
        
        public void StartGame()
        {
            authentication.TcpSendMessage("CHAT GETREALM", () =>
            {
                MessageBoxManager.Instance.OpenMessageBox("Authentication Server is down!", null);
            });
            
            
            
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        public void Register()
        {
            Application.OpenURL("https://www.127.0.0.1.com/");
        }

        public void Reload()
        {
            
        }

        public void OfficialWebsite()
        {
            
        }

        public void ReselectServer()
        {
            GameManager.Instance.realmlist_name = string.Empty;
            Header.Instance.OpenGameUIHeader(1);
            authentication.TcpSendMessage("CHAT GETREALM", () =>
            {
                MessageBoxManager.Instance.OpenMessageBox("Authentication Server is down!", null);
            });
            GameManager.Instance.gameSettings = 1;
            
        }

        public void ChangeServerWhileQueue()
        {
            GameManager.Instance.realmlist_name = string.Empty;
            Header.Instance.OpenGameUIHeader(1);
            authentication.TcpSendMessage("CHAT GETREALM", () =>
            {
                MessageBoxManager.Instance.OpenMessageBox("Authentication Server is down!", null);
            });
            world.TcpSendMessage($"QUEUE CANCEL", () =>
            {
                MessageBoxManager.Instance.OpenMessageBox("World Server is down!", null);
            });
            GameManager.Instance.gameSettings = 1;
        }

        public void Confirm()
        {
            if (userNameInput.text == string.Empty)
            {
                MessageBoxManager.Instance.OpenMessageBox("Please enter your username!", null);
                return;
            }
            if (passwordInput.text == string.Empty)
            {
                MessageBoxManager.Instance.OpenMessageBox("Please enter your password!", null);
                return;
            }
            authentication.TcpSendMessage($"CHAT LOGIN {userNameInput.text} {passwordInput.text}", () =>
            {
                MessageBoxManager.Instance.OpenMessageBox("Authentication Server is down!", null);
                return;
            });
            MessageBoxManager.Instance.OpenMessageBox("Login...!", null);
            Header.Instance.CloseGameUIHeader();
        }

        public void Back()
        {
            GameManager.Instance.realmlist_name = string.Empty;
            Header.Instance.OpenGameUIHeader(0);
        }
        
        private bool isPressingTab = false;
        //press tap to select
        private void PressTab()
        {
            if (GameManager.Instance.gameSettings == 1 && !isPressingTab)
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    isPressingTab = true;
                    passwordInput.Select();
                }
            }
            if (GameManager.Instance.gameSettings == 1 && isPressingTab)
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    isPressingTab = false;
                    userNameInput.Select();
                }
            }
        }

        
    }
}
