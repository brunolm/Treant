using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Treant.Core.Extenders
{
    public static class INotifyPropertyChangedExtender
    {
        /// <summary>
        /// Raises a property changed event.
        /// </summary>
        /// <param name="bindableObject">The bindable object.</param>
        /// <param name="propertyExpression">The property expression.</param>
        public static void RaisePropertyChanged(this INotifyPropertyChanged bindableObject, [CallerMemberName] string caller = null)
        {
            if (caller != null)
                RaiseInternalPropertyChangedEvent(bindableObject, new PropertyChangedEventArgs(caller));
        }

        /// <summary>
        /// Raises the internal property changed event.
        /// </summary>
        /// <param name="bindableObject">The bindable object.</param>
        /// <param name="eventArgs">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void RaiseInternalPropertyChangedEvent(INotifyPropertyChanged bindableObject, PropertyChangedEventArgs eventArgs)
        {
            // get the internal eventDelegate
            var bindableObjectType = bindableObject.GetType();

            // search the base type, which contains the PropertyChanged event field.
            FieldInfo propChangedFieldInfo = null;
            while (bindableObjectType != null)
            {
                propChangedFieldInfo = bindableObjectType.GetField("PropertyChanged", BindingFlags.Instance | BindingFlags.NonPublic);
                if (propChangedFieldInfo != null)
                    break;

                bindableObjectType = bindableObjectType.BaseType;
            }

            if (propChangedFieldInfo == null)
                return;

            // get prop changed event field value
            var fieldValue = propChangedFieldInfo.GetValue(bindableObject);
            if (fieldValue == null)
                return;

            MulticastDelegate eventDelegate = fieldValue as MulticastDelegate;
            if (eventDelegate == null)
                return;

            // get invocation list
            Delegate[] delegates = eventDelegate.GetInvocationList();

            // invoke each delegate
            foreach (Delegate propertyChangedDelegate in delegates)
                propertyChangedDelegate.Method.Invoke(propertyChangedDelegate.Target, new object[] { bindableObject, eventArgs });
        }
    }

}
