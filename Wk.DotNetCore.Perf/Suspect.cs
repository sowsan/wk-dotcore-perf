using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Wk.DotNetCore.Perf
{
    public class Suspect
    {
        private readonly Dictionary<string, object> _store;

        public Suspect()
            : this(new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase))
        {
        }

        public Suspect(Dictionary<string, object> store)
        {
            _store = new Dictionary<string, object>(store, StringComparer.OrdinalIgnoreCase);
        }
        
        public T GetOrDefault<T>(string propertyName)
        {
            if (!_store.ContainsKey(propertyName))
            {
                return default(T);
            }

            return (T)_store[propertyName];
        }
               
        [Obsolete("Will be removed in next version, use GetOrDefault<T> instead")]
        public T GetOrDefaultSlow<T>(string propertyName)
        {
            if (!_store.ContainsKey(propertyName))
            {
                return default(T);
            }

            var propertyValue = _store[propertyName];
            if (propertyValue == null)
            {
                return (T)propertyValue;
            }

            var propertyValueType = propertyValue.GetType();
            var targetType = typeof(T);
            if (propertyValueType == targetType)
            {
                return (T)_store[propertyName];
            }

            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                targetType = Nullable.GetUnderlyingType(targetType);
            }

            return (T)Convert.ChangeType(propertyValue, targetType);
        }

        public void Set<T>(string propertyName, T value)
        {
            if (_store.ContainsKey(propertyName))
            {
                _store[propertyName] = value;
            }
            else
            {
                _store.Add(propertyName, value);
            }
        }
    }
}