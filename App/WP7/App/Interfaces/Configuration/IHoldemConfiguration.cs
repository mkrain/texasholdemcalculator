namespace TexasHoldemCalculator.Interfaces.Configuration
{
    public static class ConfigKey
    {
        public static string Invalid {get { return "invalid"; }}
        public static string HandHistorySaveDirectory {get { return "hhsd"; }}

        //Holdem Application Information
        public static string HoldemLicenseInformationTrial { get { return "HoldemLicenseInformationTrial"; } }

        //Sensor
        public static string AccelerometerCalibrationX { get { return "AccelerometerCalibrationX"; } }
        public static string AccelerometerCalibrationY { get { return "AccelerometerCalibrationY"; } }

        public static class View
        {
            public static class Calc
            {
                public static string NumberOfOuts { get { return "no"; } }
                public static string PotSize { get { return "ps"; } }
                public static string BetAmount { get { return "ba"; } }
                public static string HandRank { get { return "hr"; } }
            }

            public static class Stats
            {
                public static string NumberOfPlayers { get { return "nop"; } }
                public static string NumberOfOuts { get { return "noo"; } }
                public static string SelectedKicker { get { return "sk"; } }
                public static string SelectedPocketPair { get { return "spp"; } }
                public static string SelectedPanoramoItem { get { return "spi"; } }
            }

            public static class StartingHands
            {
                public static string SelectedStartingHand { get { return "ssh"; } }
                public static string SelectionEventRegistration { get { return "shser"; } }
            }

            public static class Options
            {
                public static string NumberOfPlayers { get { return "nop"; } }
                public static string SkipScale { get { return "ss"; } }
                public static string Precision { get { return "p"; } }
                public static string UserName { get { return "un"; } }
                public static string RecursiveSearch { get { return "rs"; } }
                public static string Preview { get { return "p"; } }
                public static string WriteHandHistory { get { return "whh"; } }
                public static string SelectedReplayEngine { get { return "sre"; } }
                public static string SelectedSearchFilter { get { return "ssf"; } }
                public static string HandHistorySaved { get { return "hhs"; } }
            }
        }

        //Calculator View
        //public static string HoldemCalcViewNumberOfOuts {get { return "hcvno"; }}
        //public static string HoldemCalcViewPotSize {get { return "hcvps"; }}
        //public static string HoldemCalcViewBetAmount {get { return "hcvba"; }}
        //public static string HoldemCalcViewHandRank {get { return "hcvhr"; }}
        
        //StatsView
        //public static string HoldemStatsViewNumberOfPlayers {get { return "HoldemStatsViewNumberOfPlayers"; }}
        //public static string HoldemStatsViewNumberOfOuts {get { return "HoldemStatsViewNumberOfOuts"; }}
        //public static string HoldemStatsViewSelectedKicker {get { return "HoldemStatsViewSelectedKicker"; }}
        //public static string HoldemStatsSelectedPocketPair {get { return "HoldemStatsSelectedPocketPair"; }}
        //public static string HoldemSelectedPanoramoItem {get { return "HoldemSelectedPanoramoItem"; }}
        
        //StartingHandsView
        //public static string HoldemHandsViewSelectedStartingHand {get { return "HoldemHandsViewSelectedStartingHand"; }}
        //public static string HoldemStartingHandsSelectionEventRegistration {get { return "HoldemStartingHandsSelectionEventRegistration"; }}

        //OptionsView
        //public static string HoldemOptionsViewNumberOfPlayers {get { return "HoldemOptionsViewNumberOfPlayers"; }}
        //public static string HoldemOptoinsViewCardAnimations {get { return "HoldemOptoinsViewCardAnimations"; }}
        //public static string HoldemOptionsViewSkipScale {get { return "HoldemOptionsViewSkipScale"; }}
        //public static string HoldemOptionsViewPrecision {get { return "HoldemOptionsViewPrecision"; }}
        //public static string HoldemOptionsViewUserName {get { return "HoldemOptionsViewUserName"; }}
        //public static string HoldemOptionsViewRecursiveSearch {get { return "HoldemOptionsViewRecursiveSearch"; }}
        //public static string HoldemOptionsViewPreview {get { return "HoldemOptionsViewPreview"; }}
        //public static string HoldemOptionsViewWriteHandHistory {get { return "HoldemOptionsViewWriteHandHistory"; }}
        //public static string HoldemOptionsViewSelectedReplayEngine {get { return "HoldemOptionsViewSelectedReplayEngine"; }}
        //public static string HoldemOptionsViewSelectedSearchFilter {get { return "HoldemOptionsViewSelectedSearchFilter"; }}
        //public static string HoldemOptionsViewHandHistorySaved {get { return "HoldemOptionsViewHandHistorySaved"; }}
    }
}