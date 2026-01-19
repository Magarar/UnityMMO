using Server;
using TMPro;
using UIs;
using UnityEngine;

namespace Manager
{
    public class SplitManager : MonoBehaviour
    {
        public static SplitManager Instance;
        
        public Authentication authentication;
        public World world;

        public GameObject splitItemPanel;
        public TMP_InputField valueText;
        public Slot splitArrow;
        public Slot dragDropSlot;
        public Slot curDropItem;
        public Slot curDragItem;
        public int splitItemCount;

        public float curX;
        public float curY;
        public float curZ;

        public bool isDragItem;
        public bool isArrow;
        
        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }
        
        private void Update()
        {
            DragItemUpdate();
            UpdateSplitArrow();
        }

        public void OnValueChangeTMProText(string txt)
        {
            if (int.TryParse(txt, out int value))
            {
                Debug.Log(curDragItem==null);
                if (curDragItem!=null && splitItemCount > curDragItem.itemCount)
                {
                    splitItemCount = curDragItem.itemCount;
                    valueText.text = splitItemCount.ToString();
                    return;
                }

                if (splitItemCount <= 0)
                {
                    splitItemCount = 1;
                    valueText.text = splitItemCount.ToString();
                    return;
                }
            }
            else
            {
                valueText.text = splitItemCount.ToString();
                return;
            }
        }

        public void Increment(int value)
        {
            splitItemCount = int.Parse(valueText.text);
            splitItemCount += value;
            valueText.text = splitItemCount.ToString();
            
        }

        public void Decrement(int value)
        {
            splitItemCount = int.Parse(valueText.text);
            splitItemCount -= value;
            valueText.text = splitItemCount.ToString();
        }

        public void Confirm()
        {
            splitItemPanel.SetActive(false);
            isDragItem = true;
            dragDropSlot.itemID = curDragItem.itemID;
            dragDropSlot.itemCount = int.Parse(valueText.text);
        }

        public void Cancel()
        {
            splitItemPanel.SetActive(false);
        }

        public void SplitItemReset()
        {
            valueText.text = "1";
            splitItemCount = 1;
        }

        private void DragItemUpdate()
        {
            if(!isDragItem)
                return;
            dragDropSlot.gameObject.SetActive(true);
            dragDropSlot.itemID = curDragItem.itemID;
            dragDropSlot.itemCount = splitItemCount;
            
            Vector3 curMousePotion = Input.mousePosition;
            dragDropSlot.transform.position = new Vector3(curMousePotion.x+curX,curMousePotion.y+curY,curZ);
        }

        public void DragReset()
        {
            isDragItem = false; 
            dragDropSlot.gameObject.SetActive(false);
            
            
            //send to world server for calculation
            world.TcpSendMessage($"INVENTORY SPLIT {curDragItem.slotID} {valueText.text} {curDropItem.slotID}",null); 
            curDragItem = null;
            curDropItem = null;
        }

        public void UpdateSplitArrow()
        {
            
        }

        public void ClickSplitArrow()
        {
            
        }
    }
}
