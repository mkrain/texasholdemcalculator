using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TexasHoldemCalculator.Core.Controls
{
    public partial class HoldemHoleCardsControl : UserControl
    {
        #region DependencyProperty Definitions

        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(
                "Orientation",
                typeof(Orientation),
                typeof(HoldemHoleCardsControl),
                new PropertyMetadata(default(Orientation), null));

        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register(
                "ItemSource",
                typeof(IEnumerable<BitmapImage>),
                typeof(HoldemHoleCardsControl),
                new PropertyMetadata(default(IEnumerable<BitmapImage>), null));

        #endregion

        #region DependencyProperties

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public IEnumerable<BitmapImage> ItemSource
        {
            get { return (IEnumerable<BitmapImage>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        #endregion

        public HoldemHoleCardsControl()
        {
            InitializeComponent();

            this.HoldemHoleStackPanel.DataContext = this;
        }

        #region Animation Start/Stop Method

        public void AnimateCardImageOne()
        {
            this.AnimateHoleCardOne.Begin();
        }

        public void AnimateCardImageTwo()
        {
            this.AnimateHoleCardTwo.Begin();
        }

        #endregion
    }
}
