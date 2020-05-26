using System;
using System.Windows.Controls;
using Microsoft.Phone.Controls.Primitives;

namespace TexasHoldemCalculator.Core.Entities.StartingHands
{
	public class DigitDataSource : ILoopingSelectorDataSource
	{
		public DigitDataSource(int minValue, int maxValue, int step, int defaultValue, string stringFormat)
		{
			this.MinValue = minValue;
			this.MaxValue = maxValue;
			this.Step = step;
			this.SelectedItem = defaultValue;
			this.StringFormat = stringFormat;
		}

		public int MinValue
		{
			get;
			set;
		}

		public int MaxValue
		{
			get;
			set;
		}

		public int Step
		{
			get;
			set;
		}

		private string ApplyFormat(int digit)
		{
			return digit.ToString(this.StringFormat);
		}

		#region ILoopingSelectorDataSource Members

		public object GetNext(object relativeTo)
		{
			return Convert.ToInt32(relativeTo) + this.Step > this.MaxValue ? this.ApplyFormat(this.MinValue) : this.ApplyFormat(Convert.ToInt32(relativeTo) + this.Step);
		}

		public object GetPrevious(object relativeTo)
		{
			return Convert.ToInt32(relativeTo) - this.Step < this.MinValue ? this.ApplyFormat(this.MaxValue) : this.ApplyFormat(Convert.ToInt32(relativeTo) - this.Step);
		}

		public string StringFormat
		{
			get;
			private set;
		}

		private int _selectedItem;

		public object SelectedItem
		{
			get
			{
				return this.ApplyFormat(_selectedItem);
			}
			set
			{
				int newValue = Convert.ToInt32(value);

				if (_selectedItem != newValue)
				{
						int previousSelectedItem = _selectedItem;
						_selectedItem = newValue;

						if (this.SelectionChanged != null)
                            this.SelectionChanged(
                                this, 
                                new SelectionChangedEventArgs(
                                    new object[] { this.ApplyFormat(previousSelectedItem) }, 
                                    new object[] { this.ApplyFormat(_selectedItem) }));
				}
			}
		}

		public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

		#endregion
	}
}
