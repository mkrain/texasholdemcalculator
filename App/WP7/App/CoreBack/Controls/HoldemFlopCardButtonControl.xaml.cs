using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GalaSoft.MvvmLight.Command;

namespace TexasHoldemCalculator.Core.Controls
{
    public partial class HoldemFlopCardButtonControl
    {
        #region DependencyProperty Definitions

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(
                "Orientation",
                typeof(Orientation),
                typeof(HoldemFlopCardButtonControl),
                new PropertyMetadata(default(Orientation), null));

        public static readonly DependencyProperty CardImageCommandOneProperty =
            DependencyProperty.Register(
                "CardImageCommandOne",
                typeof(RelayCommand),
                typeof(HoldemFlopCardButtonControl),
                new PropertyMetadata(default(RelayCommand), null));

        public static readonly DependencyProperty CardImageCommandTwoProperty =
            DependencyProperty.Register(
                "CardImageCommandTwo",
                typeof(RelayCommand),
                typeof(HoldemFlopCardButtonControl),
                new PropertyMetadata(default(RelayCommand), null));

        public static readonly DependencyProperty CardImageCommandThreeProperty =
            DependencyProperty.Register(
                "CardImageCommandThree",
                typeof(RelayCommand),
                typeof(HoldemFlopCardButtonControl),
                new PropertyMetadata(default(RelayCommand), null));

        public static readonly DependencyProperty CardImageCommandFourProperty =
            DependencyProperty.Register(
                "CardImageCommandFour",
                typeof(RelayCommand),
                typeof(HoldemFlopCardButtonControl),
                new PropertyMetadata(default(RelayCommand), null));

        public static readonly DependencyProperty CardImageCommandFiveProperty =
            DependencyProperty.Register(
                "CardImageCommandFive",
                typeof(RelayCommand),
                typeof(HoldemFlopCardButtonControl),
                new PropertyMetadata(default(RelayCommand), null));

        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register(
                "ItemSource",
                typeof(IEnumerable<BitmapImage>),
                typeof(HoldemFlopCardButtonControl),
                new PropertyMetadata(default(IEnumerable<BitmapImage>), null));

        public static readonly DependencyProperty IsVerticalAnimationProperty =
            DependencyProperty.Register(
                "IsVerticalAnimation",
                typeof(bool),
                typeof(HoldemFlopCardButtonControl),
                new PropertyMetadata(default(bool), null));

        #endregion

        #region DependencyProperties

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public RelayCommand CardImageCommandOne
        {
            get { return (RelayCommand)GetValue(CardImageCommandOneProperty); }
            set { SetValue(CardImageCommandOneProperty, value); }
        }

        public RelayCommand CardImageCommandTwo
        {
            get { return (RelayCommand)GetValue(CardImageCommandTwoProperty); }
            set { SetValue(CardImageCommandTwoProperty, value); }
        }

        public RelayCommand CardImageCommandThree
        {
            get { return (RelayCommand)GetValue(CardImageCommandThreeProperty); }
            set { SetValue(CardImageCommandThreeProperty, value); }
        }

        public RelayCommand CardImageCommandFour
        {
            get { return (RelayCommand)GetValue(CardImageCommandFourProperty); }
            set { SetValue(CardImageCommandFourProperty, value); }
        }

        public RelayCommand CardImageCommandFive
        {
            get { return (RelayCommand)GetValue(CardImageCommandFiveProperty); }
            set { SetValue(CardImageCommandFiveProperty, value); }
        }

        public IEnumerable<BitmapImage> ItemSource
        {
            get { return (IEnumerable<BitmapImage>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        public bool IsVerticalAnimation
        {
            get { return (bool)GetValue(IsVerticalAnimationProperty); }
            set { SetValue(IsVerticalAnimationProperty, value); }
        }

        #endregion

        public HoldemFlopCardButtonControl()
        {
            InitializeComponent();

            this.HoldemFlopCardStackPanel.DataContext = this;
        }

        #region Animation Start/Stop Method

        public void AnimateCardImageOne()
        {
            if (this.IsVerticalAnimation)
                this.AnimateCardVerticalFlopOne.Begin();
            else
                this.AnimateCardHorizontalFlopOne.Begin();
        }

        public void AnimateCardImageTwo()
        {
            if (this.IsVerticalAnimation)
                this.AnimateCardVerticalFlopTwo.Begin();
            else
                this.AnimateCardHorizontalFlopTwo.Begin();
        }

        public void AnimateCardImageThree()
        {
            if (this.IsVerticalAnimation)
                this.AnimateCardVerticalFlopThree.Begin();
            else
                this.AnimateCardHorizontalFlopThree.Begin();
        }

        public void AnimateCardImageFour()
        {
            if (this.IsVerticalAnimation)
                this.AnimateCardVerticalTurn.Begin();
            else
                this.AnimateCardHorizontalTurn.Begin();
        }

        public void AnimateCardImageFive()
        {
            if (this.IsVerticalAnimation)
                this.AnimateCardVerticalRiver.Begin();
            else
                this.AnimateCardHorizontalRiver.Begin();
        }

        #endregion
    }
}
