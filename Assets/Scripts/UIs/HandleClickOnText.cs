using Manager;
using Server;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIs
{
    public class HandleClickOnText : MonoBehaviour,IPointerClickHandler
    {
        public TextMeshProUGUI myTextMeshPro;
        
        public Authentication authentication;
        public World world;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            int index = TMP_TextUtilities.FindIntersectingLink(myTextMeshPro, Input.mousePosition, null);

            if (index != -1)
            {
                TMP_LinkInfo linkInfo = myTextMeshPro.textInfo.linkInfo[index];
                string result = linkInfo.GetLinkID();
                
                //check
                string[] r = result.Split('-');
                string one = r[0];//header
                string two = r[1];//owner
                if (r[0] == "Around")
                {
                    ChatSystemManager.Instance.ClickTextAround(one, two);
                }

                if (r[0] == "World")
                {
                    ChatSystemManager.Instance.ClickTextWorld(one, two);
                }

                if (r[0] == "Whisper")
                {
                    ChatSystemManager.Instance.ClickTextWhisper(one, two);
                }
                
            }
        }
    }
}
