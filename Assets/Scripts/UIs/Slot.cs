using System;
using Items;
using Manager;
using Server;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

namespace UIs
{
    public enum SlotType
    {
        None,
        Inventory,
        Spell,
        Book,
        Trade,
        Shop,
        Mechan,
        DragDrop,
        Split
    }
    
    public class Slot : MonoBehaviour,IPointerClickHandler
    {
        private Authentication authentication;
        private World world;
        
        public string result {get; set;}  = string.Empty;
        
        public UnityEvent leftClick;
        public UnityEvent middleClick;
        public UnityEvent rightClick;

        public int slotID;
        public SlotType slotType;
        
        public int itemID;
        public int itemCount;
        public int itemStack;
        public int itemMacro;
        //server set
        public int enduranceMax = 0;
        public int enduranceMin = 0;
        public float coolDownMax = 0;
        public float coolDownMin = 0;
        
        public TextMeshProUGUI itemCountText;
        public TextMeshProUGUI itemMacroText;
        public TextMeshProUGUI itemCooldownText;
        
        public Image setItemSprite;
        public Image itemCooldown;
        public Sprite defaultItemSprite;

        private void Awake()
        {
            authentication = FindObjectOfType<Authentication>();
            world = FindObjectOfType<World>();
            
            leftClick.AddListener(new UnityAction(ButtonLeftClick));
            middleClick.AddListener(new UnityAction(ButtonMiddleClick));
            rightClick.AddListener(new UnityAction(ButtonRightClick));
        }

        private void Update()
        {
            UpdateCurSlot();
            UpdateItemCooldown();
        }

        public void MouseEnter()
        {
            if(itemID==0)
                return;
            ItemInfoManager.Instance.ShowItemInfo(this);   
        }

        public void MouseExit()
        {
            ItemInfoManager.Instance.CloseItemInfo();
        }

        public void UseItem(int key)
        {
            if (itemID == 0)
            {
                //drop
                if (DragDropManager.Instance.isDragItem)
                {
                    Debug.Log("Drop");
                    DragDropManager.Instance.curDropItem = this;
                    DragDropManager.Instance.DragReset();
                    return;
                }
                if (SplitManager.Instance.isDragItem)
                {
                    
                    SplitManager.Instance.curDropItem = this;
                    SplitManager.Instance.DragReset();
                    return;
                }
                return;
            }

            if (key == 0 && Input.GetKey(KeyCode.LeftShift))
            {
                //arrow
                if(SplitManager.Instance.isArrow)
                    return;
                //drag
                if (!SplitManager.Instance.isDragItem)
                {
                    if(itemCount==1)
                        return;
                    
                    SplitManager.Instance.curDragItem = this;
                    SplitManager.Instance.SplitItemReset();
                    SplitManager.Instance.splitItemPanel.SetActive(true);
                    return;
                }
            }
            
            else if (key==0)
            {
                //arrow
                if (SplitManager.Instance.isArrow)
                {
                    //disable
                    SplitManager.Instance.isArrow = false;
                    SplitManager.Instance.splitArrow.gameObject.SetActive(false);
                    
                    SplitManager.Instance.curDragItem = this;
                    SplitManager.Instance.SplitItemReset();
                    SplitManager.Instance.splitItemPanel.SetActive(true);
                    return;
                }
                    
                //drag
                if (!DragDropManager.Instance.isDragItem && !SplitManager.Instance.isDragItem)
                {
                    DragDropManager.Instance.curDragItem = this;
                    DragDropManager.Instance.isDragItem = true;
                    return;
                }
                //drop
                if (DragDropManager.Instance.isDragItem && !SplitManager.Instance.isDragItem)
                {
                    DragDropManager.Instance.curDropItem = this;
                    DragDropManager.Instance.DragReset();
                    return;
                }
                
                //drop split
                if (SplitManager.Instance.isDragItem)
                {
                    SplitManager.Instance.curDropItem = this;
                    SplitManager.Instance.DragReset();
                    return;
                }
            }

            else if (key==1)
            {
                if(DragDropManager.Instance.isDragItem || SplitManager.Instance.isDragItem)
                    return;
                if (coolDownMin > 0)
                {
                    Debug.Log("CoolDown");
                    return;

                }
                world.TcpSendMessage($"INVENTORY REMOVE {itemID} {slotID}",null);
                return;
            }
        }

        private void UpdateCurSlot()
        {
            itemCountText.text = itemCount.ToString();
            itemMacroText.text = itemMacro.ToString();
            if (itemCount > 0)
            {
                setItemSprite.sprite = ItemsManager.Instance.item[itemID].sprite;
                itemCountText.gameObject.SetActive(true);
                
            }
            else
            {
                itemCountText.gameObject.SetActive(false);
                
                itemID = 0;
                itemCount = 0;
                itemStack = 0;
                if(slotType==SlotType.None)
                    return;
                setItemSprite.sprite = defaultItemSprite;

            }

            if (itemMacro > 0)
            {
                itemMacroText.gameObject.SetActive(true);
            }
            else
            {
                itemMacroText.gameObject.SetActive(false);
            }
                
        }

        private void UpdateItemCooldown()
        {
            if(slotType!=SlotType.Inventory)
                return;
            if (coolDownMin > 0)
            {
                coolDownMin -= Time.deltaTime;
                itemCooldown.fillAmount = Mathf.Clamp01(coolDownMin / coolDownMax);
                float _min = coolDownMin/60;
                float _sec = coolDownMin%60;
                itemCooldownText.text = $"{_min:00}{_sec:00}";
                itemCooldown.gameObject.SetActive(true);
            }
            else
            {
                itemCooldown.gameObject.SetActive(false);
            }
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                leftClick.Invoke();
            else if (eventData.button == PointerEventData.InputButton.Middle)
                middleClick.Invoke();
            else if (eventData.button == PointerEventData.InputButton.Right)
                rightClick.Invoke();
  
        }
        
        private void ButtonLeftClick()
        {
            UseItem(0);
        }

        private void ButtonMiddleClick()
        {
            Debug.Log("Button Middle Click");
        }

        private void ButtonRightClick()
        {
            UseItem(1);
        }

        public void ResetSlot()
        {
            itemID = 0;
            itemCount = 0;
            enduranceMax = 0;
            enduranceMin = 0;
            coolDownMax = 0;
            coolDownMin = 0;
            
        }
        
    }
}
