using System;
using System.Collections.Generic;
using System.Globalization;
using Holdem.Interfaces.ReplayEngine;

namespace TexasHoldemCalculator.Core.Configuration
{
    public class ReplayEngineConfiguration : Dictionary<string, object>, IReplayEngineConfiguration
    {
        private readonly IDictionary<string, object> _dictionary;

        public ReplayEngineConfiguration() : this(new Dictionary<string, object>())
        {
        }

        public ReplayEngineConfiguration(IDictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        #region Overrides

        public new void Clear()
        {
            _dictionary.Clear();
        }

        public new object this[string key]
        {
            get
            {
                return _dictionary[key];
            }
            set
            {
                _dictionary[key] = value;
                base[key] = value;
            }
        }

        public new void Add(string key, object value)
        {
            if( this.ContainsKey(key) )
                return;

            _dictionary.Add(key, value);
        }

        public new bool ContainsKey(string key)
        {
            return _dictionary.ContainsKey(key);
        }

        public object Get(string key)
        {
            if( _dictionary.ContainsKey(key) )
                return this[key];

            return null;
        }

        public T Get<T>(string key) where T : IConvertible
        {
            return this.Cast<T>(key);
        }

        public object Get(string key, object defaultValue)
        {
            if( !_dictionary.ContainsKey(key) )
            {
                _dictionary.Add(key, defaultValue);
                return defaultValue;
            }

            return this[key];
        }

        public void Set(string key, object value)
        {
            if( !_dictionary.ContainsKey(key) )
            {
                _dictionary.Add(key, value);
                return;
            }

            this[key] = value;
        }

        public T Cast<T>(string key) where T : IConvertible
        {
            if (!this.ContainsKey(key))
                return default(T);

            var value = this[key];

            try
            {
                return (T)Convert.ChangeType(
                            value,
                            typeof(T),
                            CultureInfo.InvariantCulture);
            }
            catch
            {
                return default(T);
            }
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}