using System.Collections.Generic;
using System.Linq;
using Items;
using TMPro;
using UIs;
using UnityEngine;

namespace Manager
{
    public class InventorySystemManager : MonoBehaviour
    {
        public static InventorySystemManager Instance;
        
        public List<Slot> inventorySlots = new List<Slot>();
        public ItemPickUp ItemPickUp;
        public List<GameObject> spawnItemJunk = new List<GameObject>();

        public TextMeshProUGUI setGold;
        public TextMeshProUGUI setSilver;
        public TextMeshProUGUI setCopper;

        public TextMeshProUGUI setWeight;
        public TextMeshProUGUI setEmber;
        

        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        public void Start()
        {
            InitSlotID();
        }

        public void SetPlayerMoney(int money)
        {
            int _gold = money / 10000;
            int _sliver = (money/ 100) % 100;
            int _copper = money % 100;
            
            setGold.text = _gold.ToString();
            setSilver.text = _sliver.ToString();
            setCopper.text = _copper.ToString();
        }

        public void SetPlayerWeight(int weight, int weightMin)
        {
            setWeight.text = $"{weightMin}/{weight}";
        }

        public void SetEmber(int ember)
        {
            setEmber.text = $"Ember:{ember}";
        }

        public void DestroyAllItemJunk()
        {
            if (spawnItemJunk.Count <= 0)
                return;
            for (int i = 0; i < spawnItemJunk.Count; i++)
            {
                Destroy(spawnItemJunk[i]);
            }
            spawnItemJunk.Clear();
                
        }

        public void SetItem(string result)
        {
            string[] r = result.Split(' ');
            string[] curItem = r.Skip(2).ToArray();
            for (int i = 0; i < curItem.Count(); i++)
            {
                inventorySlots[i].itemID = int.Parse(curItem[i].Split(',')[0]);
                // Debug.Log(inventorySlots[i].itemID);
                inventorySlots[i].itemCount = int.Parse(curItem[i].Split(',')[1]);
                inventorySlots[i].enduranceMin = int.Parse(curItem[i].Split(',')[2]);
                inventorySlots[i].enduranceMax = int.Parse(curItem[i].Split(',')[3]);
                inventorySlots[i].coolDownMax = float.Parse(curItem[i].Split(',')[4]);
                inventorySlots[i].coolDownMin = float.Parse(curItem[i].Split(',')[5]);
            }
        }

        public void SpawnItem(string result)
        {
            string[] r= result.Split(' ');
            string netID = r[2];
            int itemID = int.Parse(r[3]);
            int itemCount = int.Parse(r[4]);
            float x = float.Parse(r[5]);
            float y = float.Parse(r[6]);
            float z = float.Parse(r[7]);
            ItemPickUp ipu = Instantiate(ItemPickUp, new Vector3(x, y, z), Quaternion.identity);
            spawnItemJunk.Add(ipu.gameObject);
            ipu.gameObject.SetActive(true);
            ipu.netID = netID;
            ipu.itemID = itemID;
            ipu.itemCount = itemCount;
            ipu.OpenItem(itemID);
            ipu.itemNet.InitItem();
        }

        public void SpawnItemForCreature(string result,Vector3 myDyingPosition)
        {
            string[] r= result.Split(' ');
            string netID = r[2];
            int itemID = int.Parse(r[3]);
            int itemCount = int.Parse(r[4]);
            ItemPickUp ipu = Instantiate(ItemPickUp, myDyingPosition, Quaternion.identity);
            spawnItemJunk.Add(ipu.gameObject);
            ipu.gameObject.SetActive(true);
            ipu.netID = netID;
            ipu.itemID = itemID;
            ipu.itemCount = itemCount;
            ipu.OpenItem(itemID);
            ipu.itemNet.InitItem();
        }

        public void InitSlotID()
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                inventorySlots[i].slotID = i;
                inventorySlots[i].slotType = SlotType.Inventory;
            }
        }

        public void ClearAllInventory()
        {
            foreach (var s in inventorySlots)
            {
                s.ResetSlot();
            }
        }
    }
}
