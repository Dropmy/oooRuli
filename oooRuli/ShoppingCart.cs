using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oooRuli
{
    public static class ShoppingCart
    {
        public static Dictionary<int, int> Content { get; set; } = new Dictionary<int, int>();

        public static event Action Update = delegate { };

        public static void AddOne(int id)
        {
            if(Content.ContainsKey(id))
            {
                Content[id]++;
            }
            else
            {
                Content.Add(id, 1);
            }

            Update.Invoke();
        }

        public static void Clear()
        {
            Content.Clear();
            Update.Invoke();
        }
    }
}
