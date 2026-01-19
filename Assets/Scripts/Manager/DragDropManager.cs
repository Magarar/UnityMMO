using System;
using Server;
using UIs;
using UnityEngine;

namespace Manager
{
    public class DragDropManager : MonoBehaviour
    {
        public static DragDropManager Instance;
        
        public Authentication authentication;
        public World world;
        
        public Slot dragDropSlot;
        public Slot curDropItem;
        public Slot curDragItem;

        public float curX;
        public float curY;
        public float curZ;

        public bool isDragItem;

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
        }

        private void DragItemUpdate()
        {
            if(!isDragItem)
                return;
            dragDropSlot.gameObject.SetActive(true);
            dragDropSlot.itemID = curDragItem.itemID;
            dragDropSlot.itemCount = curDragItem.itemCount;
            
            Vector3 curMousePotion = Input.mousePosition;
            dragDropSlot.transform.position = new Vector3(curMousePotion.x+curX,curMousePotion.y+curY,curZ);
        }

        public void DragReset()
        {
            isDragItem = false; 
            dragDropSlot.gameObject.SetActive(false);
            
            
            //send to world server for calculation
            if(curDragItem!=null && curDropItem!=null)
                world.TcpSendMessage($"INVENTORY DRAG {curDragItem.slotID} {curDropItem.slotID}",null); 
            curDragItem = null;
            curDropItem = null;
        }
        
        
    }
}
