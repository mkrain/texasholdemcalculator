// http://www.CodeUnplugged.wordpress.com NotifyIcon for WPF
// Copyright (c) 2010 Inderpreet Gujral
// Contact and Information: http://www.CodeUnplugged.wordpress.com
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the Code Project Open License (CPOL);
// either version 1.0 of the License, or (at your option) any later
// version.
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//
// THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HandGenerator.Controls
{
    /// <summary>
    /// Interaction logic for NumericBox.xaml
    /// </summary>
    public partial class NumericBox
    {
        public static readonly DependencyProperty _valueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(NumericBox),
                                        new PropertyMetadata(0, OnSomeValuePropertyChanged));

        public static readonly DependencyProperty _maximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(NumericBox), new UIPropertyMetadata(100));

        public static readonly DependencyProperty _minimumProperty =
            DependencyProperty.Register("Minimum", typeof(int), typeof(NumericBox), new UIPropertyMetadata(0));

        public static readonly DependencyProperty _textBoxWidthProperty =
            DependencyProperty.Register("TextBoxWidth", typeof(int), typeof(NumericBox), null);

        public static readonly DependencyProperty _textBoxHeightProperty =
            DependencyProperty.Register("TextBoxHeight", typeof(int), typeof(NumericBox), null);

        //public static readonly DependencyProperty _backgroundProperty =
        //    DependencyProperty.Register("Background", typeof(Brush), typeof(NumericBox), null);

        // Value changed
        private static readonly RoutedEvent _valueChangedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble,
                                             typeof(RoutedEventHandler), typeof(NumericBox));

        private static readonly RoutedEvent _increaseClickedEvent =
            EventManager.RegisterRoutedEvent("IncreaseClicked", RoutingStrategy.Bubble,
                                             typeof(RoutedEventHandler), typeof(NumericBox));

        private static readonly RoutedEvent _decreaseClickedEvent =
            EventManager.RegisterRoutedEvent("DecreaseClicked", RoutingStrategy.Bubble,
                                             typeof(RoutedEventHandler), typeof(NumericBox));

        private static readonly Regex _numMatch = new Regex(@"^-?\d+$");

        /// <summary>
        /// 
        /// Initializes a new instance of the NumericBox.NumericBox class.
        /// 
        /// </summary>
        public NumericBox()
        {
            this.InitializeComponent();

            this.Maximum = int.MaxValue;
            this.Minimum = 0;
            this.TextBoxValue.Text = "0";
        }

        /// <summary>
        /// 
        /// The Value property represents the TextBoxValue of the control.
        /// 
        /// </summary>
        /// <returns>The current TextBoxValue of the control</returns>      
        public int Value
        {
            get
            {
                return (int)this.GetValue(_valueProperty);
            }
            set
            {
                //this.TextBoxValue.Text = value.ToString();
                this.SetValue(_valueProperty, value);
            }
        }

        /// <summary>
        /// 
        /// Maximum value for the Numeric Up Down control
        /// 
        /// </summary>
        public int Maximum
        {
            get
            {
                return (int)this.GetValue(_maximumProperty);
            }
            set
            {
                this.SetValue(_maximumProperty, value);
            }
        }

        /// <summary>
        /// 
        /// Height of the textbox containing the numeric value.
        /// 
        /// </summary>
        public int TextBoxHeight
        {
            get
            {
                return (int)this.TextBoxValue.Height;
            }
            set
            {
                this.TextBoxValue.Height = value;
                this.TextBoxValue.InvalidateVisual();
            }
        }

        /// <summary>
        /// 
        /// Width of the textbox containing the numeric value.
        /// 
        /// </summary>
        public int TextBoxWidth
        {
            get
            {
                return (int)this.TextBoxValue.Width;
            }
            set
            {
                this.TextBoxValue.Width = value;
            }
        }

        /// <summary>
        /// Background for this control
        /// </summary>
        //[Bindable(true), Category("Appearance")]
        //public new Brush Background
        //{
        //    get
        //    {
        //        return this.TextBoxValue.Background;
        //    }
        //    set
        //    {
        //        this.TextBoxValue.Background = value;
        //        //this.SetValue(_backgroundProperty, value);
        //    }
        //}

        /// <summary>
        /// 
        /// Minimum value of the numeric up down control.
        /// 
        /// </summary>
        public int Minimum
        {
            get
            {
                return (int)this.GetValue(_minimumProperty);
            }
            set
            {
                this.SetValue(_minimumProperty, value);
            }
        }

        private void ResetText(TextBox tb)
        {
            tb.Text = 0 < this.Minimum ? this.Minimum.ToString() : "0";

            tb.SelectAll();
        }

        private void ValuePreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var tb = (TextBox)sender;
            string text = tb.Text.Insert(tb.CaretIndex, e.Text);

            e.Handled = !_numMatch.IsMatch(text);
        }

        private void ValueTextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;

            if( !_numMatch.IsMatch(tb.Text) )
                this.ResetText(tb);

            this.Value = Convert.ToInt32(tb.Text);

            if( this.Value < this.Minimum )
                this.Value = this.Minimum;
            if( this.Value > this.Maximum )
                this.Value = this.Maximum;

            this.RaiseEvent(new RoutedEventArgs(_valueChangedEvent));
        }

        private void IncreaseClick(object sender, RoutedEventArgs e)
        {
            if( this.Value < this.Maximum )
            {
                this.Value++;
                this.RaiseEvent(new RoutedEventArgs(_increaseClickedEvent));
            }
        }

        private void DecreaseClick(object sender, RoutedEventArgs e)
        {
            if( this.Value > this.Minimum )
            {
                this.Value--;
                this.RaiseEvent(new RoutedEventArgs(_decreaseClickedEvent));
            }
        }

        private static void OnSomeValuePropertyChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            var numericBox = target as NumericBox;

            if( numericBox != null )
                numericBox.TextBoxValue.Text = e.NewValue.ToString();
        }

        /// <summary>
        /// 
        /// The ValueChanged event is called when the TextBoxValue of the control changes.
        /// 
        /// </summary>
        public event RoutedEventHandler ValueChanged
        {
            add
            {
                this.AddHandler(_valueChangedEvent, value);
            }
            remove
            {
                this.RemoveHandler(_valueChangedEvent, value);
            }
        }

        //Increase button clicked

        /// <summary>The IncreaseClicked event is called when the Increase button clicked</summary>
        public event RoutedEventHandler IncreaseClicked
        {
            add
            {
                this.AddHandler(_increaseClickedEvent, value);
            }
            remove
            {
                this.RemoveHandler(_increaseClickedEvent, value);
            }
        }

        //Increase button clicked

        /// <summary>The DecreaseClicked event is called when the Decrease button clicked</summary>
        public event RoutedEventHandler DecreaseClicked
        {
            add
            {
                this.AddHandler(_decreaseClickedEvent, value);
            }
            remove
            {
                this.RemoveHandler(_decreaseClickedEvent, value);
            }
        }

        /// <summary>
        /// Checking for Up and Down events and updating the value accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValuePreviewKeyDown(object sender, KeyEventArgs e)
        {
            if( e.IsDown && e.Key == Key.Up && this.Value < this.Maximum )
            {
                this.Value++;
                this.RaiseEvent(new RoutedEventArgs(_increaseClickedEvent));
            }
            else if( e.IsDown && e.Key == Key.Down && this.Value > this.Minimum )
            {
                this.Value--;
                this.RaiseEvent(new RoutedEventArgs(_decreaseClickedEvent));
            }
        }
        
        private void BorderGotFocus(object sender, RoutedEventArgs e)
        {
            //Hack, calling focus on the textbox triggers this event handler a second time
            //TODO: Figure out how to prevent the double fire.
            if (!(e.OriginalSource is NumericBox))
                return;

            this.TextBoxValue.Focus();
            this.TextBoxValue.SelectAll();
        }
    }
}