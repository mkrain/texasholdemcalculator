using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Media;

using HandGenerator.Entities.Model;
using HandGenerator.Model;

using Holdem.Interfaces.Card;
using Holdem.Interfaces.StartingHands;

using ColorNames = HandGenerator.Phone.Supported.ColorNames;

namespace HandGenerator.ViewModel
{
    public class HoldemPhoneColorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly StartingHandsGeneratorModel _model;
        private readonly IList<HoldemColor> _supportedColors;
        

        public string Title
        {
            get
            {
                return _model.StartingHands.Title;
            }
            set
            {
                _model.StartingHands.Title = value;
                NotifyPropertyChanged("Title");
            }
        }

        public IList<HoldemColor> CurrentPaletteColors
        {
            get
            {
                return ( from key in this.HandStrength.Keys
                         let color = this.HandStrength[key].Color
                         let found = _supportedColors.FirstOrDefault(x => x.ColorFromName == color)
						 where found != null
                         select new HoldemColor(found.ColorName, key) ).ToList();
            }
        }

        public IEnumerable<CardValue> AllHands
        {
            get
            {
                return _model.StartingHands.AllHands;
            }
        }

        private IStartingHand StartingHands
        {
            get
            {
                return _model.StartingHands;
            }
        }

        private IHandHighlight Highlights
        {
            get
            {
                return _model.StartingHands.HandHighlight;
            }
        }

        private IDictionary<int, IHandStrength> HandStrength
        {
            get
            {
                return this.Highlights.HandStrength;
            }
        }

        public HoldemPhoneColorViewModel() : this(new StartingHandsGeneratorModel())
        {

        }

        public HoldemPhoneColorViewModel(StartingHandsGeneratorModel model)
        {
            _model = model;

            var enums = Enum.GetValues(typeof(ColorNames)).Cast<ColorNames>().ToList();

            _supportedColors =
                (from colors
                    in enums
                 orderby colors.ToString()
                 select new HoldemColor(colors)).ToList();
        }

        public void ExportStartingHand(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None))
            {
                _model.ExportToStream(stream, this.StartingHands);
            }
        }

        public void ImportStartingHand(string fileName)
        {
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                _model.ImportFromStream(stream);
                NotifyHandUpdated();
            }
        }

        /// <summary>
        /// 
        /// Reloads the template starting hand.
        /// 
        /// </summary>
        public void ResetStartingHand()
        {
            _model.ResetHand();
            NotifyHandUpdated();
        }

        public void ResetToStartingHandsAllEnabled()
        {
            _model.ResetAllEnabledHand();
            NotifyHandUpdated();
        }

        public void ResetToStartingHandsAllDisabled()
        {
            _model.ResetAllDisabledHand();
            NotifyHandUpdated();
        }

        private void NotifyHandUpdated()
        {
            NotifyPropertyChanged("AllHands");
            NotifyPropertyChanged("Title");
            NotifyPropertyChanged("CurrentPaletteColors");
        }

        public void UpdateHandWithHighlight(CardValue newValue)
        {
            this.UpdateHandWithHighlight(newValue, false);
        }

        public Color FindHightlightFromStrength(int strength)
        {
            return _model.StartingHands.FindColorFromStrength(strength);
        }

        public bool UpdateToNewColor(HoldemColor oldColor, HoldemColor newColor)
        {
            //Simple optimization to not change one color to the same color.
            if (oldColor == newColor)
                return false;

            //If the new color is mapped elsewhere, it cannot update the old color.
            var hasOtherColorValue =
                this.HandStrength
                    .Values
                    .FirstOrDefault(x => x.Color == newColor.ColorFromName);

            if( hasOtherColorValue != null )
                return false;

            //Find the old color and update it to the new one
            var hasOldColorValue =
                this.HandStrength
                    .Values
                    .FirstOrDefault(x => x.Color == oldColor.ColorFromName);
            //This color doesn't exist but it should
            if (hasOldColorValue == null)
                return false;

            this.HandStrength[hasOldColorValue.Id].Color = newColor.ColorFromName;

            RefreshHands();

            return true;
        }

        /// <summary>
        /// 
        /// This will update all hands with the corresponding strength
        /// to use the highlight from the update cardValue.
        /// 
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="applyToLikeHightlight"></param>
        public void UpdateHandWithHighlight(CardValue newValue, bool applyToLikeHightlight)
        {
            int strength = newValue.Strength;
            var containsStrength = this.StartingHands.ContainsStrength(strength);

            //Find the hand that represents the bound copy.
            var realHand =
                this.StartingHands
                    .FindHandByRow(newValue.Parent)
                    .Cards
                    .FirstOrDefault(x => x.Name == newValue.Name);

            if( realHand == null )
                throw new ArgumentException("There is no starting hand with that value.");

            int oldStrength = realHand.Strength;

            //Is this color mapped already?
            var hasValue =
                this.HandStrength
                    .Values
                    .FirstOrDefault(x => x.Color == newValue.Highlight);

            //This is a new color/strength addition.
            if (!containsStrength)
            {
                //This is a new color add it to handhighlight.
                if (hasValue == null)
                {
                    realHand.Strength = newValue.Strength;
                    realHand.Highlight = newValue.Highlight;
                    AddNewStrengthHiglightMatch(strength, newValue.Highlight);
                    this.HandStrength[strength].Color = newValue.Highlight;

                    //hasValue =
                    //    this.HandStrength
                    //        .Values
                    //        .FirstOrDefault(x => x.Color == newValue.Highlight);
                }
                //else
                //The color specified is occupied by another value
                //Force the new value to be the saved value
                //    newValue.Strength = realHand.Strength;

                //if (applyToLikeHightlight)
                //    _model.StartingHands.UpdateVisibilityToLikeStrength(newValue.Strength, newValue.IsVisible);
                //else
                //    realHand.IsVisible = newValue.IsVisible;

                //return;
            }
            

            //if (applyToLikeHightlight)
                //_model.StartingHands.UpdateVisibilityToLikeStrength(strength, newValue.IsVisible);
            //else
                //realHand.IsVisible = newValue.IsVisible;

            //if (hasValue == null)
            //{
            //    //This is a unique color and can be used for this strength.
            //    this.HandStrength[strength].Color = newValue.Highlight;
            //    realHand.Strength = strength;
            //    RefreshHands();
            //}
            //else
            //{
                //Only the strength has changed so update it to the current
                //strength/highlight key value pair.

                //Apply to all like colors.
                if (applyToLikeHightlight)
                {
                    this.StartingHands.SetColorFromStrength(realHand);
                    _model.StartingHands.UpdateLikeStrengths(oldStrength, strength);
                    _model.StartingHands.UpdateVisibilityToLikeStrength(strength, newValue.IsVisible);
                }
                else
                {
                    var colorFromStrength = this.StartingHands.FindColorFromStrength(strength);
                    realHand.Highlight = colorFromStrength;
                    realHand.Strength = strength;
                    realHand.IsVisible = newValue.IsVisible;
                }

                NotifyPropertyChanged("AllHands");
            //}
        }

        //public void UpdateVisibilityToLikeStrength(int strength, bool isVisible)
        //{

        //}

        private void AddNewStrengthHiglightMatch(int strength, Color highlight)
        {
            if (this.StartingHands.ContainsStrength(strength))
            {
                this.HandStrength[strength].Color = highlight;
                return;
            }

            this.HandStrength.Add(
                    strength,
                    new HandStrength
                    {
                        Id = strength,
                        Color = highlight
                    });

            RefreshHands();
        }

        private void RefreshHands()
        {
            this.StartingHands.RefreshHandStrength();
            this.NotifyPropertyChanged("AllHands");
            this.NotifyPropertyChanged("CurrentPaletteColors");
        }

        private void NotifyPropertyChanged(string property)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}