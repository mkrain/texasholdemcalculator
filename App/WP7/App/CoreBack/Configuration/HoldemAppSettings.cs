using System;
using System.Collections.Generic;
using System.Globalization;
using Holdem.Interfaces.Configuration;
using Holdem.Interfaces.Extensions;

namespace TexasHoldemCalculator.Core.Configuration
{
	public class HoldemAppSettings : Dictionary<ConfigKey, object>, IHoldemPhoneConfiguration
        // : HoldemConfigurationBase<ConfigKey, object>, IHoldemPhoneConfiguration
    {
        private readonly IDictionary<string, object> _dictionary;

        public HoldemAppSettings(IDictionary<string, object> dictionary)
        {
            if( dictionary == null )
                throw new ArgumentNullException("dictionary");

            _dictionary = dictionary;

            _dictionary.Keys.ForEach(
              k =>
              {
                  var key = this.ConvertKey(k);

                  if( key == ConfigKey.Invalid )
                      return;
                  var value = _dictionary[k];

                  base.Add(key, value);
              });
        }

        private ConfigKey ConvertKey(string savedKey)
        {
            if (!Enum.IsDefined(typeof(ConfigKey), savedKey))
                return ConfigKey.Invalid;

            var key = (ConfigKey)Enum.Parse(typeof(ConfigKey), savedKey, true);

            return key;
        }

        #region Overrides

        public new void Clear()
        {
            _dictionary.Clear();
            base.Clear();
        }

        public new object this[ConfigKey key]
        {
            get
            {
                return _dictionary[key.ToString()];
            }
            set
            {
                _dictionary[key.ToString()] = value;
                base[key] = value;
            }
        }

        public new void Add(ConfigKey key, object value)
        {
            if (this.ContainsKey(key))
                return;

            _dictionary.Add(key.ToString(), value);
        }

        public new bool ContainsKey(ConfigKey key)
        {
            return _dictionary.ContainsKey(key.ToString());
        }

        public object Get(ConfigKey key)
        {
            if (_dictionary.ContainsKey(key.ToString()))
                return this[key];

            return null;
        }

	    public T Get<T>(ConfigKey key) where T : IConvertible
	    {
	        return this.Cast<T>(key);
	    }

        public T Cast<T>(ConfigKey key) where T : IConvertible
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

	    public object Get(ConfigKey key, object defaultValue)
        {
            if (!_dictionary.ContainsKey(key.ToString()))
            {
                _dictionary.Add(key.ToString(), defaultValue);
                
                return defaultValue;
            }

            return this[key];
        }

        public void Set(ConfigKey key, object value)
        {
            if (!_dictionary.ContainsKey(key.ToString()))
            {
                _dictionary.Add(key.ToString(), value);
                
                return;
            }

            this[key] = value;
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