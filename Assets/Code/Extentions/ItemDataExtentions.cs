using System.Collections.Generic;

namespace SerjBal
{
    public static class ItemDataExtentions
    {
        public static bool ContainsKey(this List<ItemData> content, string key)
        {
            foreach (ItemData item in content)
            {
                if (item.Key==key) return true;
            }
            return false;
        }

        public static ItemData Get(this List<ItemData> content, string key)
        {
            foreach (ItemData item in content)
            {
                if (item.Key==key) return item;
            }
            return null;
        }
        
        public static void Override(this List<ItemData> content, string key, ItemData data)
        {
            bool keyExists = false;
            for (int i = 0; i < content.Count; i++)
            {
                if (content[i].Key == key)
                {
                    content[i] = data;
                    keyExists = true;
                }
            }
            if (!keyExists) content.Add(data);
        }
        
        public static void RemoveKey(this List<ItemData> content, string key)
        {
            for (int i = 0; i < content.Count; i++)
            {
                if (content[i].Key==key) content.RemoveAt(i);
            }
        }
    }
}