using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.Provider;
using TexasHoldemCalculator.Interfaces.StartingHands;

namespace TexasHoldemCalculator.ViewModel.Modules
{
    public class HoldemCardThemeInjector : NinjectModule
    {
        public override void Load()
        {
            //Load the card generator
            this.Bind<ICardThemeManager>().To<CardThemeManager>().InSingletonScope();

            //Load the card generator
            this.Bind<IStartingHandsManager>().To<StartingHandsManager>().InSingletonScope();

            //Icon Provider
            this.Bind<IIconProvider>().To<IconProvider>().InSingletonScope();
        }
    }
}