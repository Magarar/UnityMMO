using System;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIs
{
    public class CanvasHp : MonoBehaviour
    {
        public Slider hpBar;
        public TextMeshProUGUI playerName;
        public TextMeshProUGUI playerGuild;
        
        //damage pop up
        public bool isDamagePopUp = false;
        

        public void SetHp(int max, int min)
        {
            hpBar.maxValue = max;
            hpBar.value = min;
        }

        private void DamagePopUp()
        {
            if(!isDamagePopUp)
                return;
            transform.Translate(Vector3.up * (5f * Time.deltaTime));
            Destroy(gameObject, 3f);
            transform.eulerAngles = GameManager.Instance.Player.playerCamera.transform.eulerAngles;
        }

        private void Update()
        {
            DamagePopUp();
        }
    }
}
