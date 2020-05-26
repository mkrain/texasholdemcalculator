using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace TexasHoldemCalculator.Core.Controls
{
    [ContentProperty("TemplateMap")]
    public class TemplateSelectorConverter : IValueConverter
    {
        private bool _loaded;

        // Using a DependencyProperty as the backing store for SourceType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceTypeProperty =
            DependencyProperty.RegisterAttached(
                "SourceType",
                typeof(string),
                typeof(TemplateSelectorConverter),
                new PropertyMetadata(null));

        public TemplateMap TemplateMap
        {
            get;
            set;
        }

        public bool LoadFromApplicationResources
        {
            get;
            set;
        }

        public TemplateSelectorConverter()
        {
            this.TemplateMap = new TemplateMap();
        }

        public static string GetSourceType(DependencyObject obj)
        {
            return (string)obj.GetValue(SourceTypeProperty);
        }

        public static void SetSourceType(DependencyObject obj, string value)
        {
            obj.SetValue(SourceTypeProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if( this.LoadFromApplicationResources && !_loaded )
            {
                this.LoadFromResources();
            }

            if( value != null )
            {
                var valueType = value.GetType();

                return ( from template in this.TemplateMap
                         where template.SourceType == valueType.FullName
                         select template.DataTemplate ).FirstOrDefault();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private void LoadFromResources()
        {
            _loaded = true;

            var dataTemplates =
                from resource in GetDataTemplates()
                let sourceType = GetSourceType(resource)
                where !string.IsNullOrEmpty(sourceType)
                select new TemplateMapEntry
                       {
                           SourceType = sourceType,
                           DataTemplate = resource,
                       };

            this.TemplateMap.AddRange(dataTemplates);
        }

        private static IEnumerable<DataTemplate> GetDataTemplates()
        {
            return Application.Current.Resources.Values.OfType<DataTemplate>();
        }
    }

    public class TemplateMap : List<TemplateMapEntry>
    {
    }

    public class TemplateMapEntry
    {
        public string SourceType
        {
            get;
            set;
        }

        public DataTemplate DataTemplate
        {
            get;
            set;
        }
    }
}