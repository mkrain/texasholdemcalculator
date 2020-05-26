using System.Windows.Media;

using HandGenerator.Phone.Supported;
using HandGenerator.Phone.Supported.Extensions;

namespace HandGenerator.Entities.Model
{
    public class HoldemColor
    {
        public int Id
        {
            get;
            set;
        }

        public string Color
        {
            get;
            set;
        }

        public ColorNames ColorName
        {
            get;
            set;
        }

        public Color ColorFromName
        {
            get
            {
                return this.ColorName.FromName();
            }
        }

        public Brush ColorBrush
        {
            get
            {
                return new SolidColorBrush(this.ColorFromName);
            }
        }

        public HoldemColor(ColorNames color, int id = 0)
        {
            this.ColorName = color;
            this.Id = id;
        }
    }
}
