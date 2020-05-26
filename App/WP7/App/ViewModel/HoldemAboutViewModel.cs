using System.Collections.Generic;
using GalaSoft.MvvmLight;
using TexasHoldemCalculator.Interfaces.Model;

namespace TexasHoldemCalculator.ViewModel
{
    public class HoldemAboutViewModel : ViewModelBase
    {
        private readonly IHoldemAboutTipModel _model;
        
        public IEnumerable<HoldemAboutTip> Tips
        {
            get { return _model.Tips; }
        }

        public HoldemAboutViewModel(IHoldemAboutTipModel model)
        {
            _model = model;
        }
    }
}