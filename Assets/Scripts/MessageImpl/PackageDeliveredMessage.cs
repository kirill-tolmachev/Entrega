using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scripts;
using Scripts.Infrastructure.Messages;

namespace Assets.Scripts.MessageImpl
{
    internal class PackageDeliveredMessage : IMessage
    {
        public Target Target { get; }

        public Package Package { get; }

        public PackageDeliveredMessage(Package package, Target target)
        {
            Package = package;
            Target = target;
        }
    }
}
