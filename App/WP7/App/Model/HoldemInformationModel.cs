using System;
using TexasHoldemCalculator.Interfaces.Model;
using TexasHoldemCalculator.Interfaces.Resource;

namespace TexasHoldemCalculator.Model
{
    public class HoldemInformationModel : IHoldemInformationModel
    {
    	private readonly IHoldemResource _resource;

        public string InformationDescriptionLabel
        {
            get
            {
				return _resource.GetString("THC_Info_Description_Label");
            }
        }

        public string InformationWebsiteLabel
        {
            get
            {
				return _resource.GetString("THC_Info_Website_Label");
            }
        }

        public string InformationEmailLabel
        {
            get
            {
				return _resource.GetString("THC_Info_Email_Label");
            }
        }

        public string InformationDescriptionText
        {
            get
            {
				return _resource.GetString("THC_Info_Description");
            }
        }

        public string InformationWebsiteText
        {
            get
            {
				return _resource.GetString("THC_Info_Website");
            }
        }

        public string InformationEmailText
        {
            get
            {
				return _resource.GetString("THC_Info_Email");
            }
        }


		public HoldemInformationModel(IHoldemResource resource)
        {
			if (resource == null)
				throw new ArgumentNullException("resource");

			_resource = resource;
        }
    }
}