using System;
using System.Collections;
using Manager;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore;

namespace UIs
{
    public class EmojiSelection : MonoBehaviour
    {
        public int emojiID;
        public string emojiName;
        public string emojiDescription;
        public string emojiAnim;
        public TextMeshProUGUI emojiTxt;

        [HideInInspector] public GameObject emojiPanel;
        [HideInInspector]public TMP_InputField chatInput;

        public void SetEmoji(TMP_SpriteAsset curAss, int _emojiID,GameObject _emojiPanel,TMP_InputField _chatInput)
        {
            emojiPanel = _emojiPanel;
            chatInput = _chatInput;
            foreach (var allSprite in curAss.spriteGlyphTable)
            {
                allSprite.scale = 2f;
            }

            foreach (var allSprite in curAss.spriteGlyphTable)
            {
                allSprite.metrics = new GlyphMetrics(allSprite.metrics.width, allSprite.metrics.height,
                    allSprite.metrics.horizontalBearingX, allSprite.metrics.horizontalBearingY,
                    50);
            }
            emojiID = _emojiID;
            emojiName = curAss.name;

            emojiAnim = $"   <sprite=\"{curAss.name}\" anim=\"0,{curAss.spriteCharacterTable.Count-1},30\">   ";
            emojiTxt.text = emojiAnim;
            emojiTxt.ForceMeshUpdate();
            
            ChatSystemManager.Instance.emojiDict.Add($"sprite={emojiID}",emojiAnim);
            
            

            
        }

        public void SelectThisEmoji()
        {
            chatInput.text +=$" sprite={emojiID} ";
            chatInput.ForceLabelUpdate();
            emojiPanel.SetActive(false);

            chatInput.Select();
            // StartCoroutine(WaitABit(0.1f, () =>
            // {
            //     chatInput.Select();
            // }));
        }

        private IEnumerator WaitABit(float t, Action callback)
        {
            yield return new WaitForSeconds(t);
            callback?.Invoke();
        }
    }
}
