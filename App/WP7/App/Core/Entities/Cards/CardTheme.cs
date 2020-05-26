using System;
using System.Globalization;
using System.Windows.Media.Imaging;
using TexasHoldemCalculator.Interfaces.Card;

namespace TexasHoldemCalculator.Core.Entities.Cards
{
	public abstract class CardThemeBase : ICardTheme
	{
		#region Variables

		private const string _defaultTheme = "Border";
		//private const string _providerFormat = "/Provider/CardImage/{0}";
        private const string _cardBack = "/Core/Provider/CardImages/Backs/CardBack.png";
        private const string _cardName = "/Core/Provider/CardImages/{0}/{1}/{2}{3}.png";
        private const string _splashImage = "/Core/Provider/CardImages/Splash/THC.RoyalFlush.Animation.jpg";
        private const string _startingHand = "/Core/Provider/CardImages/Hands/StartingHands{0}.bmp";
		private const string _startingHandA = "A";
		private const string _startingHandB = "B";
		//private static string _assemblyName;

		#endregion //Variables

		#region Properties

		/// <summary>
		/// 
		/// Returns the starting hands image for the basic addition.
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual BitmapImage StartingHandBasic
		{
			get
			{
				return this.GetImage(string.Format(CultureInfo.InvariantCulture, _startingHand, _startingHandB));
			}
		}

		/// <summary>
		/// 
		/// Returns the starting hands image for the adjusted addition.
		/// 
		/// </summary>
		/// <returns></returns>
		protected virtual BitmapImage StartingHandAdjustedBasic
		{
			get
			{
				return this.GetImage(string.Format(CultureInfo.InvariantCulture, _startingHand, _startingHandA));
			}
		}

		public virtual BitmapImage SplashImage
		{
			get
			{
				return this.GetImage(string.Format(CultureInfo.InvariantCulture, _splashImage));
			}
		}

		/// <summary>
		/// 
		/// Simply the default card back shown when cards no not viewed or when there is an error.
		/// 
		/// </summary>
		/// <returns></returns>
		public virtual BitmapImage DefaultCardBack
		{
			get
			{
				return this.GetImage(string.Format(CultureInfo.InvariantCulture, _cardBack));
			}
		}

        //protected string ResourceAssemblyName
        //{
        //    get
        //    {
        //        return _assemblyName;
        //    }
        //    set
        //    {
        //        _assemblyName = value;
        //    }
        //}

		#endregion Properties

		protected CardThemeBase()
		{
            //var name = Assembly.GetExecutingAssembly().FullName;
            //var asmName = new AssemblyName(name);

            //_assemblyName = asmName.Name;
		}

		#region Methods

		public virtual string ThemeName
		{
			get
			{
				return _defaultTheme;
			}
		}

		/// <summary>
		/// 
		/// Returns a card image based on the image name.
		/// 
		/// </summary>
		/// <param name="imageName"></param>
		/// <returns></returns>
		public virtual BitmapImage GetImage(string imageName)
		{
			var bmp = new BitmapImage(new Uri(imageName, UriKind.Relative));

			return bmp;
		}

		/// <summary>
		/// 
		/// Returns a card image based on the card name and suit.
		/// 
		/// </summary>
		/// <param name="card"></param>
		/// <returns></returns>
		public virtual BitmapImage GetCardImage(CardValue card)
		{
			return card == null ? this.DefaultCardBack : this.GetCardImage(card.Name, card.Suit);
		}

		/// <summary>
		/// 
		/// Returns a card image based on the card name and suit.
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="suit"></param>
		/// <returns></returns>
		public virtual BitmapImage GetCardImage(CardName name, Suit suit)
		{
			//Cards start at 0 for 2 and 12 for A.
			char csuit = suit.ToString().ToLower(CultureInfo.InvariantCulture)[0];
			string cardFileName = string.Format(
				CultureInfo.InvariantCulture, _cardName, _defaultTheme, suit, (int)name, csuit);

		    var bmp = new BitmapImage(new Uri(cardFileName, UriKind.Relative));

			return bmp;
		}

        //protected Stream GetAbsoluteStreamResource(string resource)
        //{
        //    return this.GetType().Assembly.GetManifestResourceStream(resource);
        //}

        //protected Stream GetRelativeStreamResource(string resource)
        //{
        //    return this.GetType().Assembly.GetManifestResourceStream(string.Format(_providerFormat, _assemblyName, resource));
        //}

		#endregion Methods
	}
}
