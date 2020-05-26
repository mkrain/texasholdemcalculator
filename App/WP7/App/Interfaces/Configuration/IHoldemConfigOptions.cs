using System.Collections.Generic;

namespace TexasHoldemCalculator.Interfaces.Configuration
{
    public interface IHoldemConfigOptions
    {
        int HoldemHandCalcPrecision
        {
            get;
            set;
        }

        int HandHistorySkipScale
        {
            get;
            set;
        }

        int HandHistorySkipLargeChange
        {
            get;
            set;
        }

        int HandHistorySkipSmallChange
        {
            get;
            set;
        }

        int HandHistoryPlayerCount
        {
            get;
            set;
        }

        bool HandHistoryRecursion
        {
            get;
            set;
        }

        string HandHistoryLastSearchDir
        {
            get;
            set;
        }

        string HandHistorySearchUsername
        {
            get;
            set;
        }

        string HoldemHandCardType
        {
            get;
            set;
        }

        string HoldemHandThemeDirectory
        {
            get;
            set;
        }

        string HandHistorySearchFilter
        {
            get;
            set;
        }

        IEnumerable<string> AvailableThemes
        {
            get;
            set;
        }

        string CurrentTheme
        {
            get;
            set;
        }

        bool HandHistoryPreview
        {
            get;
            set;
        }

        bool HoldemHandPreview
        {
            get;
            set;
        }

        bool GeneralSuppressPopup
        {
            get;
            set;
        }

        bool HoldemHandAutoWrite
        {
            get;
            set;
        }

        string HandHistorySaveDirectory
        {
            get;
            set;
        }

        string HandHistoryReplayEngineAsmDirectory
        {
            get;
            set;
        }

        string HandHistorySelectedReplayEngine
        {
            get;
            set;
        }
    }
}