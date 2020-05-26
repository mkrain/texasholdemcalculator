namespace TexasHoldemCalculator.Interfaces
{
	public static class ReplayEngineProviderMapping
	{
		public const string FILE_SYSTEM = "FileSystem";

		//TODO Create This Type
		public const string WEB = "FileSystem";

		public const string HAND_HISTORY_WRITER = "THC Hand History";
		public const string POKER_STARS_ENGINE = "Poker Stars";

		public const string BINDING_PARAM_TRANSIENT = "transientConfiguration";
		public const string BINDING_PARAM_PERSISTENT = "persistentConfiguration";
	    public const string BINDING_NAME_DEFAULT = "Default";
	}
}