using System;
using System.Windows.Controls;
using Microsoft.Phone.Controls.Primitives;

namespace TexasHoldemCalculator.Core.Entities.StartingHands
{
    public class IntRangeDataSource : ILoopingSelectorDataSource
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public int IncrementWith { get; set; }

        private int _selectedItem;

        public IntRangeDataSource(int minimum = 1, int maximum = 100, int incrementWith = 1, int selectedItem = 1)
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
            this.IncrementWith = incrementWith;
            this.SelectedItem = selectedItem;
        }

        public object GetNext(object relativeTo)
        {
            int value = (int)relativeTo;

            if (value >= this.Maximum)
                return this.Minimum;

            return value + this.IncrementWith;
        }

        public object GetPrevious(object relativeTo)
        {
            int value = (int)relativeTo;
            if (value <= this.Minimum)
                return this.Maximum;

            return value - this.IncrementWith;
        }
        
        public object SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                // this will use the Equals method if it is overridden for the data source item class
                if (!Equals(_selectedItem, value))
                {
                    // save the previously selected item so that we can use it 
                    // to construct the event arguments for the SelectionChanged event
                    object previousSelectedItem = _selectedItem;
                    _selectedItem = (int)value;
                    // fire the SelectionChanged event
                    this.OnSelectionChanged(previousSelectedItem, _selectedItem);
                }
            }
        }

        public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

        protected virtual void OnSelectionChanged(object oldSelectedItem, object newSelectedItem)
        {
            if( this.SelectionChanged != null )
            {
                this.SelectionChanged(this, 
                    new SelectionChangedEventArgs(
                        new [] { oldSelectedItem }, 
                        new [] { newSelectedItem }));
            }
        }
    }
}
