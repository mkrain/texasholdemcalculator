using GalaSoft.MvvmLight;
using TexasHoldemCalculator.Interfaces.Model;

namespace TexasHoldemCalculator.ViewModel
{
    public class HoldemInformationViewModel : ViewModelBase
    {
        private readonly IHoldemInformationModel _model;

        public string InformationDescriptionLabel
        {
            get
            {
                return _model.InformationDescriptionLabel;
            }
        }

        public string InformationWebsiteLabel
        {
            get
            {
                return _model.InformationWebsiteLabel;
            }
        }

        public string InformationEmailLabel
        {
            get
            {
                return _model.InformationEmailLabel;
            }
        }

        public string InformationDescriptionText
        {
            get
            {
                return _model.InformationDescriptionText;
            }
        }

        public string InformationWebsiteText
        {
            get
            {
                return _model.InformationWebsiteText;
            }
        }

        public string InformationEmailText
        {
            get
            {
                return _model.InformationEmailText;
            }
        }

        public HoldemInformationViewModel(IHoldemInformationModel model)
        {
            _model = model;
        }
    }
}