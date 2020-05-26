using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Ninject.Modules;
using TexasHoldemCalculator.Interfaces.ReplayEngine;
using TexasHoldemCalculator.ReplayEngine;

namespace TexasHoldemCalculator.Service.Modules
{
    internal sealed class HoldemServiceFactoryInjector : NinjectModule
    {
        /// <summary>
        /// 
        /// Dictionary that contains the types that implement the IReplayEngine interface.
        /// 
        /// </summary>
        private readonly Dictionary<string, Type> _engines;


        public HoldemServiceFactoryInjector()
        {
            this._engines = new Dictionary<string, Type>();
        }

        /// <summary>
        /// 
        /// List of available engines, i.e. types that implement the IReplayEngine interface.
        /// 
        /// </summary>
        public IList<string> AvailableReplayEngines
        {
            get
            {
                //Try to load engines if the ReplayEngineDirectory was recently changed
                if (this._engines.Count == 0)
                    this.AddInitialBindings();

                var coll = this._engines.Keys.ToArray();

                return new Collection<string>(coll);
            }
        }

        /// <summary>
        /// 
        /// The type bindings for injection are defined here.  These are used by the
        /// IHoldemService.
        /// 
        /// </summary>
        public override void Load()
        {
            this.AddInitialBindings();
        }

        public Type GetTypeFromFriendlyName(string friendlyName)
        {
            return this._engines.FirstOrDefault(x => x.Key.Equals(friendlyName)).Value;
        }

        /// <summary>
        /// 
        /// This adds the dynamic bindings for IReplayEngine.
        /// 
        /// </summary>
        private void AddInitialBindings()
        {
            if (this._engines.Count != 0)
                return;

            this.LoadAvailableEngines();

            //Load a binding for every type found in the assemblies.
            foreach (var pair in this._engines)
            {
                //Can be found by the type name
                this.Bind<IReplayEngine>().To(pair.Value).Named(pair.Key);
            }
        }

        /// <summary>
        /// 
        /// Searches for assemblies in the ReplayEngineDirectory.  Those containing
        /// types that implement the IReplayEngine interface are saved.
        /// 
        /// </summary>
        private void LoadAvailableEngines()
        {
            if (this._engines.Count == 0)
                this.LoadAssemblies();
        }

        /// <summary>
        /// 
        /// Load all the assemblies from the replay engine directory.
        /// 
        /// </summary>
        private void LoadAssemblies()
        {
            //_engines.Add("Poker Stars", typeof(ReplayEngine.PokerStars.PokerStarsReplayEngine));
            this._engines.Add("THC Hand History", typeof(HoldemHandHistoryWriter));
        }
    }
}