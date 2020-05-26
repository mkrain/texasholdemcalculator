using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TexasHoldemCalculator.Core.Controls
{
    public partial class RoundTextBox : UserControl
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached(
                "Text",
                typeof(string),
                typeof(RoundTextBox),
                new PropertyMetadata(TextChanged));

        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
            }
        }


        public RoundTextBox()
        {
            var keyboard = new InputScope();
            var scopeName = new InputScopeName
            {
                NameValue = InputScopeNameValue.Digits
            };
            keyboard.Names.Add(scopeName);

            // Required to initialize variables
            InitializeComponent();

            this.InnerTextBox.InputScope = keyboard;
        }

        private static void TextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var myControl = d as RoundTextBox;

            if (myControl == null)
                return;

            if (e.NewValue == null)
                return;

            var text = (string)e.NewValue;
            myControl.InnerTextBox.Text = text;
        }
    }
}
