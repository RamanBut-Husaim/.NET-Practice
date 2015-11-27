using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Async.Example.ViewModel
{
    public class ObservableObject<T> : INotifyPropertyChanged where T : class
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChangedEvent<TValue>(Expression<Func<T, TValue>> propertySelector)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(this.GetPropertyName(propertySelector)));
        }

        private string GetPropertyName<TValue>(Expression<Func<T, TValue>> propertySelector)
        {
            var memberExpression = propertySelector.Body as MemberExpression;
            if (memberExpression != null)
            {
                return memberExpression.Member.Name;
            }

            throw new ArgumentException("The property in invalid.");
        }
    }
}
