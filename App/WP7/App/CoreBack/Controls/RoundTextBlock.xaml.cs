using System.Windows;
using System.Windows.Media;

namespace TexasHoldemCalculator.Core.Controls
{
    public partial class RoundTextBlock
    {
        public static readonly DependencyProperty BorderWidthProperty =
            DependencyProperty.RegisterAttached(
                "BorderWidth", //Name of the property
                typeof( Thickness ), //Type of the property
                typeof( RoundTextBlock ), //Type of the provider of the registered attached property
                null);

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                "CornerRadius", //Name of the property
                typeof( CornerRadius ), //Type of the property
                typeof( RoundTextBlock ), //Type of the provider of the registered attached property
                null);

        public new static readonly DependencyProperty BackgroundProperty =
            DependencyProperty.RegisterAttached(
                "Background", //Name of the property
                typeof( Brush ), //Type of the property
                typeof( RoundTextBlock ), //Type of the provider of the registered attached property
                null);

        public new static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.RegisterAttached(
                "FontFamily", //Name of the property
                typeof( FontFamily ), //Type of the property
                typeof( RoundTextBlock ), //Type of the provider of the registered attached property
                null);

        public new static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.RegisterAttached(
                "FontSize", //Name of the property
                typeof( double ), //Type of the property
                typeof( RoundTextBlock ), //Type of the provider of the registered attached property
                null);

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached(
                "Text", //Name of the property
                typeof( string ), //Type of the property
                typeof( RoundTextBlock ), //Type of the provider of the registered attached property
                null);

        public string Text
        {
            get { return this.RoundText.Text; }
            set { this.RoundText.Text = value; }
        }

        public new Brush Background
        {
            get { return this.RoundOuterBorder.Background; }
            set { this.RoundOuterBorder.Background = value; }
        }

        public new FontFamily FontFamily
        {
            get { return this.RoundText.FontFamily; }
            set { this.RoundText.FontFamily = value; }
        }

        public new double FontSize
        {
            get { return this.RoundText.FontSize; }
            set { this.RoundText.FontSize = value; }
        }

        public CornerRadius CornerRadius
        {
            get { return this.RoundOuterBorder.CornerRadius; }
            set { this.RoundOuterBorder.CornerRadius = value; }
        }

        public Thickness BorderWidth
        {
            get { return this.RoundOuterBorder.BorderThickness; }
            set { this.RoundOuterBorder.BorderThickness = value; }
        }

        public RoundTextBlock()
        {
            // Required to initialize variables
            InitializeComponent();
        }
    }
}