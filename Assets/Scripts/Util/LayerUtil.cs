using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Util
{
    internal static class LayerUtil
    {
        public static bool HasLayer(this LayerMask mask, int layer) => mask == (mask | (1 << layer));
    }
}
