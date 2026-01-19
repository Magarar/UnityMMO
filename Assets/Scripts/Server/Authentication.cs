using System;
using System.Net.Sockets;
using System.Text;
using Manager;
using UIs;
using UnityEngine;

namespace Server
{
    public class Authentication : MonoBehaviour
    {
        //require read dir from computer and then (Set = ipaddress) - (Set = port)  -.config
        [Tooltip("认证服务器IP地址")] 
        [Header("Server Configuration")]
        [HideInInspector]public string authenticationIpaddress;
        [Tooltip("认证服务器端口号")]
        public int authenticationPort;
        
        [Header("Components")]
        public Realmlist realmlist;
        public World world;
        
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
                    Debug.Log("Authentication Server Disconnected");
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
                //Packet Execution
                string[] r = result.Split(' ');
                if (r[0] == "PREPARE")
                {
                    if (r[1] == "REALMLIST")
                    {
                        HLManager.Instance.ClearRealmList();
                        Header.Instance.CloseGameUIHeader();
                        MessageBoxManager.Instance.OpenMessageBox("Retrieve Realmlist",null);
                    }
                }
                if (r[0] == "SET")
                {
                    if (r[1] == "REALMLIST")
                    {
                        realmlist.SpawnRealmlist(result);
                        
                    }
                }

                if (r[0] == "COMPLETE")
                {
                    if (r[1] == "REALMLIST")
                    {
                        Header.Instance.OpenGameUIHeader(1);
                        MessageBoxManager.Instance.Okay();
                        realmlist.backButton.interactable = true;
                    }
                }

                if (r[0] == "FAIL")
                {
                    if (r[1] == "LOGIN")
                    {
                        MessageBoxManager.Instance.OpenMessageBox("Login Failed");
                    }
                }
                
                if (r[0] == "DONE")
                {
                    if (r[1] == "LOGIN")
                    {
                        MessageBoxManager.Instance.OpenMessageBox("Retrieving character list from server");
                        GameManager.Instance.account_guid = r[2];
                        GameManager.Instance.account_username = r[3];
                        GameManager.Instance.account_password = r[4];
                        GameManager.Instance.account_email = r[5];
                        GameManager.Instance.account_ipaddress = r[6];
                        GameManager.Instance.account_oldIpaddress = r[7];
                        GameManager.Instance.account_newIpaddress = r[8];
                        GameManager.Instance.account_security = r[9];
                        GameManager.Instance.account_emberssion = r[10];
                        GameManager.Instance.account_expansion = r[11];
                        
                        world.TcpSendMessage($"CHAT RETRIEVE_CHARACTER {GameManager.Instance.account_guid} {GameManager.Instance.realmlist_guid}",(() =>
                        {
                            MessageBoxManager.Instance.OpenMessageBox("World Server is down",null);
                        }));
                        
                        //exception or delete
                        
                        
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
                        client.Connect(authenticationIpaddress, authenticationPort);
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
                        client.Connect(authenticationIpaddress, authenticationPort);
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
            Debug.Log(authenticationIpaddress);
            byte[] data = Encoding.ASCII.GetBytes(message);
            client.Send(data);
            IsServerAlive = true;
        }

        ~Authentication()
        {
            client?.Close();
        }
        
    }
}
