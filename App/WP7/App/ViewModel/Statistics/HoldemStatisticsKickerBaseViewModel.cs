using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Common.Core.Configuration;
using GalaSoft.MvvmLight.Command;
using TexasHoldemCalculator.Core.Entities.Cards;
using TexasHoldemCalculator.Interfaces.Card;
using TexasHoldemCalculator.Interfaces.ViewModel;

namespace TexasHoldemCalculator.ViewModel.Statistics
{
    public abstract class HoldemStatisticsKickerBaseViewModel : HoldemStatisticsBaseViewModel
    {
        #region Variables

        private CardName _selectedKicker;
        private static IList<CardKicker> _kickers;

        #endregion

        #region Public Properties

        public abstract Suit CardOneSuit { get; }
        public abstract Suit CardTwoSuit { get; }
        public abstract CardName CardOneName { get; }
        public abstract CardName CardTwoName { get; }

        public IList<CardKicker> Kickers
        {
            get
            {
                return _kickers;
            }
        }

        public CardName SelectedKicker
        {
            get
            {
                return _selectedKicker;
            }
            set
            {
                _selectedKicker = value;

                this.GenerateOdds();
            }
        }

        public RelayCommand<HoldemCardKickerEventArgs> SelectedCardCommand
        {
            get;
            private set;
        }

        public BitmapImage CardImageOne
        {
            get
            {
                return base.ThemeManager.GetCardImage(this.CardOneName, this.CardOneSuit);
            }
        }

        public BitmapImage CardImageTwo
        {
            get
            {
                return base.ThemeManager.GetCardImage(this.CardTwoName, this.CardTwoSuit);
            }
        }

        #endregion

        protected HoldemStatisticsKickerBaseViewModel(
            ICardThemeManager themeManager,
            IPhoneConfiguration configuration)
            : base(themeManager, configuration)
        {
            this.SelectedCardCommand = new RelayCommand<HoldemCardKickerEventArgs>(this.KickerSelected);

            _kickers =
                new List<CardKicker>
                {
                    new CardKicker(base.ThemeManager, CardName.Two),
                    new CardKicker(base.ThemeManager, CardName.Three),
                    new CardKicker(base.ThemeManager, CardName.Four),
                    new CardKicker(base.ThemeManager, CardName.Five),
                    new CardKicker(base.ThemeManager, CardName.Six),
                    new CardKicker(base.ThemeManager, CardName.Seven),
                    new CardKicker(base.ThemeManager, CardName.Eight),
                    new CardKicker(base.ThemeManager, CardName.Nine),
                    new CardKicker(base.ThemeManager, CardName.Ten),
                    new CardKicker(base.ThemeManager, CardName.Jack),
                    new CardKicker(base.ThemeManager, CardName.Queen),
                    new CardKicker(base.ThemeManager, CardName.King),
                    new CardKicker(base.ThemeManager, CardName.Ace)
                };
        }

        #region Protected Methods

        protected new void UpdateCardImages()
        {
            base.RaisePropertyChanged("CardImageOne");
            base.RaisePropertyChanged("CardImageTwo");
            base.RaisePropertyChanged("SelectedKicker");
        }

        #endregion

        #region Private Methods

        private void KickerSelected(HoldemCardKickerEventArgs kicker)
        {
            this.SelectedKicker = kicker.Kicker.CardName;

            base.VisibilityChanged("Visibility1");
        }

        #endregion
    }
}
