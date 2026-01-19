using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Items
{
    public class ItemsManager : MonoBehaviour
    {
        public static ItemsManager Instance;

        public List<string> spriteFolder = new List<string>();
        
        public Dictionary<int,Item> item = new Dictionary<int, Item>();
        
        
        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this.gameObject);
        }

        public void InitItemDatabase()
        {
            for (int i = 0; i < spriteFolder.Count; i++)
            {
                List<Sprite> f = Resources.LoadAll<Sprite>($"Item/{spriteFolder[i]}").ToList();
                for (int j = 0; j < f.Count; j++)
                {
                    string[] r = f[j].name.Split(',');
                    item.Add(int.Parse(r[0]), new Item()
                    {
                        itemId = int.Parse(r[0]),
                        stack = int.Parse(r[1]),
                        sprite = f[j]
                    });
                }
            }
            Debug.Log($"{item.Count} items loaded");
        }
        
        public void Start()
        {
            InitItemDatabase();
        }
    }
}
