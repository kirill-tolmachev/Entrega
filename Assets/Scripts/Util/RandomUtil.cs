using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Util
{
    internal static class RandomUtil
    {
        public static T PickRandom<T>(this IList<T> items)
        {
            return items[Random.Range(0, items.Count)];
        }
    }
}
