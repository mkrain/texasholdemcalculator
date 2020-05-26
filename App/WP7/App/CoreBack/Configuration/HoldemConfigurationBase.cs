using System;
using System.Collections.Generic;
using System.Globalization;
using Holdem.Interfaces.Configuration;

namespace TexasHoldemCalculator.Core.Configuration
{
    public abstract class HoldemConfigurationBase<TKey, TValue> : Dictionary<TKey, TValue>, IHoldemConfiguration<TKey, TValue>
    {
        protected HoldemConfigurationBase() : this(new Dictionary<TKey, TValue>())
        {
            
        }

        protected HoldemConfigurationBase(Dictionary<TKey, TValue> dict) : base(dict)
        {

        }

        //public new virtual TValue this[TKey key]
        //{
        //    get
        //    {
        //        return base[key];
        //    }
        //    set
        //    {
        //        base[key] = value;
        //    }
        //}

        public new virtual void Add(TKey key, TValue value)
        {
            if (!this.ContainsKey(key))
                base.Add(key, value);
        }

        //public new virtual void Clear()
        //{
        //    base.Clear();
        //}

        //public new virtual bool ContainsKey(TKey key)
        //{
        //    return base.ContainsKey(key);
        //}

        public virtual TValue Get(TKey key, TValue defaultValue)
        {
            if (!this.ContainsKey(key))
                this.Add(key, defaultValue);
            return this[key];
        }

        public virtual T Get<T>(TKey key) where T : IConvertible
        {
            return this.Cast<T>(key);
        }

        public virtual TValue Get(TKey key)
        {
            if (this.ContainsKey(key))
                return this[key];
            return default(TValue);
        }

        public virtual void Set(TKey key, TValue value)
        {
            if (!this.ContainsKey(key))
                this.Add(key, value);
            this[key] = value;
        }

        public virtual void Load()
        {

        }

        public virtual void Save()
        {

        }

        /// <summary>
        /// 
        /// Converts the specified string into the given T value.
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Cast<T>(TKey key) where T : IConvertible
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

        //protected static ConfigKey ConvertKey(string key)
        //{
        //    return (ConfigKey)Enum.Parse(typeof(ConfigKey), key, true);
        //}
    }
}
