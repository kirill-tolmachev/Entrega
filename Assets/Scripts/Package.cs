using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Package : MovableObject
    {
        public float Direction { get; set; }

        public bool FoundTarget { get; set; }
    }
}
