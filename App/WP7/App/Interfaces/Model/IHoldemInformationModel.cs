
namespace TexasHoldemCalculator.Interfaces.Model
{
    public interface IHoldemInformationModel
    {
        string InformationDescriptionLabel { get; }

        string InformationWebsiteLabel { get; }

        string InformationEmailLabel { get; }

        string InformationDescriptionText { get; }

        string InformationWebsiteText { get; }

        string InformationEmailText { get; }
    }
}
