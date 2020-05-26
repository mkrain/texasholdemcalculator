using System;
using System.Collections.Generic;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.Provider;

namespace TexasHoldemCalculator.Model
{
    public class HoldemAboutTipModel : IHoldemAboutTipModel
    {
        private readonly List<HoldemAboutTip> _tips = new List<HoldemAboutTip>();
        private readonly IIconProvider _iconProvider;

        public IEnumerable<HoldemAboutTip> Tips
        {
            get
            {
                if (_tips == null || _tips.Count == 0)
                    InitializeHoldemTips();
                return _tips;
            }
        }

        public HoldemAboutTipModel(IIconProvider iconProvider)
        {
            if( iconProvider == null )
                throw new ArgumentNullException("iconProvider");

            _iconProvider = iconProvider;

            InitializeHoldemTips();
        }

        /// <summary>
        /// 
        /// TODO: Externalize these tips.
        /// 
        /// </summary>
        private void InitializeHoldemTips()
        {
            var tip1 =
                new HoldemAboutTip(_iconProvider)
                {
                    HeaderText = "Hide/Unhide Shared Cards",
                    SummaryText = "You can hide/unhide the flop/turn/river cards by clicking the corresponding card on the main page",
                    Tips = 
                        new List<string>
                        { 
                            "NOTE: You can have cards shown at all times by setting 'Hand Preview' to on in the settings page" 
                        }
                };

            var tip2 =
                new HoldemAboutTip(_iconProvider)
                {
                    HeaderText = "Replay Built-in Hand History",
                    SummaryText = "There is a built-in hand history reader/writer, to review generated hands",
                    Tips =
                        new List<string>
                        { 
                            "Make sure 'Write Hand History' is set to on in the settings page",
                            "Generate hands by clicking 'New Hand' on the main page",
                            "Click 'Load' on the history page",
                            "NOTE: You can change how many hands are skipped using the left/right arrow by changing 'Hand Skip Amount' in the settings menu"
                        }
                };

            var tip6 =
                new HoldemAboutTip(_iconProvider)
                {
                    HeaderText = "Add Hands from SkyDrive",
                    SummaryText = "You can add starting hands you have created via skydrive",
                    Tips =
                        new List<string>
                        { 
                            "Navigate to the 'Starting Hands' page",
                            "Select '<< Custom >>' from the bottom of the list",
                            "Follow the prompts to login to skydrive",
                            "Navigate through the skydrive browser and pick a file (xml currently)",
                            "Accept the prompts to download the file",
                            "Navigate back to 'Starting Hands', the title is appended with *C",
                            "An example can be found here: https://skydrive.live.com/redir.aspx?cid=66e5fcb98f941ac7&resid=66E5FCB98F941AC7!880&parid=66E5FCB98F941AC7!145"
                        }
                };

            var tip3 =
                new HoldemAboutTip(_iconProvider)
                {
                    HeaderText = "Calculation Precision",
                    SummaryText = "You can change the number of digits used when calculating odds, etc by changing 'Precision' in the settings page",
                    Tips = new List<string> { }
                };

            var tip4 =
                new HoldemAboutTip(_iconProvider)
                {
                    HeaderText = "Show Best Possible Hand",
                    SummaryText = "If you want to see what the best hand is given your hole cards, the flop, turn and river cards, click the '[ Show Best Hand ]' button on the main page",
                    Tips = new List<string> { }
                };

            var tip5 =
                new HoldemAboutTip(_iconProvider)
                {
                    HeaderText = "Card Animations",
                    SummaryText = "Card animations, currently for the main page only, are turned off by default.  You can enable them by setting the 'Card Animations' slider to on in the settings menu",
                    Tips = new List<string> { }
                };

            _tips.Add(tip1);
            _tips.Add(tip2);
            _tips.Add(tip6);
            _tips.Add(tip3);
            _tips.Add(tip4);
            _tips.Add(tip5);
        }
    }
}