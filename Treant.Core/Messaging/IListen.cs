using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Treant.Core.Messaging
{
    public interface IListen<T>
    {
        void Handle(T message);
    }
}
