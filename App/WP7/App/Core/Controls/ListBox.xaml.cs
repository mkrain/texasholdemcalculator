using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace TexasHoldemCalculator.Core.Controls
{
	public class ListBox : System.Windows.Controls.ListBox
	{
		protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
		{
			var listBoxItem = (ListBoxItem)element;

			// Bind Grid properties
			listBoxItem.SetBinding(Grid.RowProperty, new Binding("Row"));
			listBoxItem.SetBinding(Grid.ColumnProperty, new Binding("Column"));
			listBoxItem.SetBinding(Grid.ColumnSpanProperty, new Binding("ColumnSpan"));

			base.PrepareContainerForItemOverride(element, item);
		}
	}
}