
using System.Windows;
using System.Windows.Media;

namespace HandGenerator.Controls
{
    /// <summary>
    /// Interaction logic for RoundTextBlock.xaml
    /// </summary>
    public partial class RoundTextBlock
    {
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached(
                        "CornerRadius",             //Name of the property
                        typeof(CornerRadius),       //Type of the property
                        typeof(RoundTextBlock),		//Type of the provider of the registered attached property
                        null);

        public static readonly DependencyProperty OuterBorderBackgroundProperty =
            DependencyProperty.RegisterAttached(
                        "OuterBorderBackground",    //Name of the property
                        typeof(Brush),				//Type of the property
                        typeof(RoundTextBlock),		//Type of the provider of the registered attached property
                        null);

        public static readonly DependencyProperty InnerBorderBackgroundProperty =
            DependencyProperty.RegisterAttached(
                        "InnerBorderBackground",    //Name of the property
                        typeof(Brush),				//Type of the property
                        typeof(RoundTextBlock),		//Type of the provider of the registered attached property
                        null);

        public new static readonly DependencyProperty FontFamilyProperty =
            DependencyProperty.RegisterAttached(
                        "FontFamily",               //Name of the property
                        typeof(FontFamily),		    //Type of the property
                        typeof(RoundTextBlock),		//Type of the provider of the registered attached property
                        null);

        public new static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.RegisterAttached(
                        "FontSize",                 //Name of the property
                        typeof(double),		        //Type of the property
                        typeof(RoundTextBlock),		//Type of the provider of the registered attached property
                        null);

        public static readonly DependencyProperty BorderWidthProperty =
            DependencyProperty.RegisterAttached(
                        "BorderWidth",              //Name of the property
                        typeof(double),				//Type of the property
                        typeof(RoundTextBlock),		//Type of the provider of the registered attached property
                        null);

        public static readonly DependencyProperty TextProperty =
           DependencyProperty.RegisterAttached(
                       "Text",						//Name of the property
                       typeof(string),				//Type of the property
                       typeof(RoundTextBlock),		//Type of the provider of the registered attached property
                       null);


        public string Text
        {
            get
            {
                return this.RoundText.Text;
            }
            set
            {
                this.RoundText.Text = value;
            }
        }

        public Brush OuterBorderBackground
        {
            get
            {
                return this.RoundOuterBorder.Background;
            }
            set
            {
                this.RoundOuterBorder.Background = value;
            }
        }

        public Brush InnerBorderBackground
        {
            get
            {
                return this.RoundInnerBorder.Background;
            }
            set
            {
                this.RoundInnerBorder.Background = value;
            }
        }

        public new FontFamily FontFamily
        {
            get
            {
                return this.RoundText.FontFamily;
            }
            set
            {
                this.RoundText.FontFamily = value;
            }
        }

        public new double FontSize
        {
            get
            {
                return this.RoundText.FontSize;
            }
            set
            {
                this.RoundText.FontSize = value;
            }
        }

        public CornerRadius CornerRadius
        {
            get
            {
                return RoundOuterBorder.CornerRadius;
            }
            set
            {
                RoundOuterBorder.CornerRadius = value;
            }
        }

        public double BorderWidth
        {
            get
            {
                return this.RoundInnerBorder.Margin.Left;
            }
            set
            {
                this.RoundInnerBorder.Margin = new Thickness(value, value, value, value);
            }
        }


        public RoundTextBlock()
        {
            // Required to initialize variables
            InitializeComponent();
        }

        public static CornerRadius GetCornerRadius(DependencyObject obj)
        {
            return (CornerRadius)obj.GetValue(CornerRadiusProperty);
        }

        public static void SetCornerRadius(DependencyObject obj, CornerRadius radius)
        {
            obj.SetValue(CornerRadiusProperty, radius);
        }

        public static Brush GetOuterBorderBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(OuterBorderBackgroundProperty);
        }

        public static void SetOuterBorderBackground(DependencyObject obj, Brush brush)
        {
            obj.SetValue(OuterBorderBackgroundProperty, brush);
        }

        public static Brush GetInnerBorderBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(InnerBorderBackgroundProperty);
        }

        public static void SetInnerBorderBackground(DependencyObject obj, Brush brush)
        {
            obj.SetValue(InnerBorderBackgroundProperty, brush);
        }

        public static FontFamily GetFontFamily(DependencyObject obj)
        {
            return (FontFamily)obj.GetValue(FontFamilyProperty);
        }

        public static void SetFontFamily(DependencyObject obj, FontFamily family)
        {
            obj.SetValue(FontFamilyProperty, family);
        }

        public static double GetBorderWidth(DependencyObject obj)
        {
            return (double)obj.GetValue(BorderWidthProperty);
        }

        public static void SetBorderWidth(DependencyObject obj, double width)
        {
            obj.SetValue(BorderWidthProperty, width);
        }

        public static double GetFontSize(DependencyObject obj)
        {
            return (double)obj.GetValue(FontSizeProperty);
        }

        public static void SetFontSize(DependencyObject obj, double size)
        {
            obj.SetValue(FontSizeProperty, size);
        }

        public static string GetText(DependencyObject obj)
        {
            return (string)obj.GetValue(TextProperty);
        }

        public static void SetText(DependencyObject obj, string text)
        {
            obj.SetValue(TextProperty, text);
        }
    }
}
