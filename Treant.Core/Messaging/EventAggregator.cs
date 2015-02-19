using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Treant.Core.Messaging
{
    [Export]
    public class EventAggregator
    {
        public void Publish<T>(T message)
        {
            foreach (var item in MefBootstrap.Container.GetExportedValues<IListen<T>>())
            {
                item.Handle(message);
            }
        }
    }
}
