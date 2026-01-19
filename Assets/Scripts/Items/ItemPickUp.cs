using System;
using System.Collections.Generic;
using Manager;
using TMPro;
using UIs;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Items
{
    [Serializable]
    public struct ItemS
    {
        public int itemID;
        public GameObject itemObject;
    }
    public class ItemPickUp : MonoBehaviour
    {
        public ItemNet itemNet;

        public string netID;
        [FormerlySerializedAs("setItemID")] public int itemID;
        public int itemCount;
        public bool setRotate;
        public float rotateSpeed;
        public CanvasHp canvasHp;
        public Slider hpSlider;
        public TextMeshProUGUI setName;
        public TextMeshProUGUI setGuild;
        
        [FormerlySerializedAs("setItem")] public List<ItemS> registerationItem = new List<ItemS>();
        public Dictionary<int,GameObject> getItem = new Dictionary<int, GameObject>();

        private void Awake()
        {
            InitItem();   
        }

        private void Update()
        {
            UpdateItemCanvas();
            UpdateItemRotate();
            UpdateItemInfo();
        }

        public void OpenItem(int _itemID)
        {
            Debug.Log(getItem.Count);
            foreach (var key in getItem.Keys)
            {
                getItem[key].SetActive(false);
            }
            getItem[_itemID].SetActive(true);
            if (ItemsManager.Instance.item.TryGetValue(_itemID, out var item))
            {
                // setName.text = item.itemName.Replace("_"," ");
                
            }
        }

        private void InitItem()
        {
            if(itemID<=0)
                return;
            foreach (var item in registerationItem)
            {
                getItem.Add(item.itemID,item.itemObject);
            }
        }

        private void UpdateItemCanvas()
        {
            if(!GameManager.Instance.Player)
                return;
            canvasHp.transform.eulerAngles = GameManager.Instance.Player.transform.eulerAngles;
            
        }

        private void UpdateItemRotate()
        {
            if(!setRotate)
                return;
            transform.Rotate(Vector3.up * (rotateSpeed * Time.deltaTime));
        }
        
        public void UpdateItemInfo()
        {
            if(!GameManager.Instance.Is_Start_Game)
                return;
            gameObject.name = ItemsManager.Instance.item[itemID].itemName;
            canvasHp.playerName.text = ItemsManager.Instance.item[itemID].itemName;
        }
        
        
        
    }
}
