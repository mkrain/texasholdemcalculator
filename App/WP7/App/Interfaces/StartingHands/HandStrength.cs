using System;
using System.Windows.Media;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace TexasHoldemCalculator.Interfaces.StartingHands
{
	[XmlRoot("HandStrength")]
	public class HandStrength : IHandStrength
	{
		[XmlAttribute("Id")]
		public int Id
		{
			get;
			set;
		}

		[XmlAttribute("Color")]
		public Color Color
		{
			get;
			set;
		}

		[XmlAttribute("Description")]
		public string Description
		{
			get;
			set;
		}

		#region IXmlSerializable Members

		public XmlSchema GetSchema()
		{
			return null;
		}

		public void ReadXml(System.Xml.XmlReader reader)
		{
			if( reader.HasAttributes && reader.MoveToAttribute("Id") )
				this.Id = int.Parse(reader.Value);
			if( reader.HasAttributes && reader.MoveToAttribute("Color") )
				this.Color = this.ConvertColor(reader.Value);
			if( reader.HasAttributes && reader.MoveToAttribute("Description") )
				this.Description = reader.Value;
		}

		public void WriteXml(System.Xml.XmlWriter writer)
		{
			writer.WriteStartElement("Strength");
			writer.WriteAttributeString("Id", this.Id.ToString());
			writer.WriteAttributeString("Color", this.ToRgb(this.Color));
			writer.WriteAttributeString("Description", this.Description);
			writer.WriteEndElement();
		}

		private Color ConvertColor(string colorValue)
		{
			if( !string.IsNullOrEmpty(colorValue) && colorValue.Contains(".") )
				return this.FromRgb(colorValue);
			return this.FromName(colorValue);
		}

		private Color FromRgb(string color)
		{
			var rgb = color.Split('.');

			if( rgb.Length < 3 )
				throw new ArgumentException("Color must be in R.G.B format, or A.R.G.B.");

			var clr = new Color();
			const int index = 0;

			try
			{
				//The xml has an alpha specified
				if( rgb.Length == 4 )
					clr.A = byte.Parse(rgb[index]);

				clr.R = byte.Parse(rgb[index + 1]);
				clr.G = byte.Parse(rgb[index + 2]);
				clr.B = byte.Parse(rgb[index + 3]);

				return clr;
			}
			catch( Exception e )
			{
				throw new ArgumentException("Color must be in R.G.B format, or A.R.G.B.", e);
			}
		}

		private Color FromName(string colorName)
		{
			ColorNames converted;

			if( Enum.IsDefined(typeof(ColorNames), colorName) )
				converted = (ColorNames)Enum.Parse(typeof(ColorNames), colorName, true);
			else
				converted = ColorNames.Beige;

			return converted.FromName();
		}

		private string ToRgb(Color rgb)
		{
			return string.Format("{0}.{1}.{2}.{3}", rgb.A, rgb.R, rgb.G, rgb.B);
		}

		#endregion
	}
}
