using System.Windows.Media.Imaging;

namespace TexasHoldemCalculator.Interfaces.Card
{
	public interface ICardTheme
	{
		BitmapImage SplashImage
		{
			get;
		}

		/// <summary>
		/// 
		/// Simply the default card back shown when cards no not viewed or when there is an error.
		/// 
		/// </summary>
		/// <returns></returns>
		BitmapImage DefaultCardBack
		{
			get;
		}

		string ThemeName
		{
			get;
		}

		/// <summary>
		/// 
		/// Returns a card image based on the image name.
		/// 
		/// </summary>
		/// <param name="imageName"></param>
		/// <returns></returns>
		BitmapImage GetImage(string imageName);

		/// <summary>
		/// 
		/// Returns a card image based on the card name and suit.
		/// 
		/// </summary>
		/// <param name="card"></param>
		/// <returns></returns>
		BitmapImage GetCardImage(CardValue card);

		/// <summary>
		/// 
		/// Returns a card image based on the card name and suit.
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <param name="suit"></param>
		/// <returns></returns>
		BitmapImage GetCardImage(CardName name, Suit suit);
	}
}