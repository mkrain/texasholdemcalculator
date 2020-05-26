using System.Windows;

using Microsoft.Phone.Controls;

namespace TexasHoldemCalculator
{
    public class PhoneApplicationPageWithTransition : PhoneApplicationPage
    {
        public PhoneApplicationPageWithTransition()
        {
            this.SetTransition(this);
        }

        private void SetTransition(UIElement element)
        {
            SetInTransition(element);
            SetOutTransition(element);
        }

        private static void SetInTransition(UIElement element)
        {
            var inTransition = new NavigationInTransition();

            var backwardTurnstileTransition = 
                new TurnstileTransition 
                {
                    Mode = TurnstileTransitionMode.BackwardIn
                };

            var forwardTurnstileTransition = 
                new TurnstileTransition 
                {
                    Mode = TurnstileTransitionMode.ForwardIn
                };

            inTransition.Backward = backwardTurnstileTransition;
            inTransition.Forward = forwardTurnstileTransition;

            TransitionService.SetNavigationInTransition(element, inTransition);
        }

        private static void SetOutTransition(UIElement element)
        {
            var outTransition = new NavigationOutTransition();

            var backwardTurnstileTransition = 
                new TurnstileTransition 
                {
                    Mode = TurnstileTransitionMode.BackwardOut
                };

            var forwardTurnstileTransition = 
                new TurnstileTransition 
                {
                    Mode = TurnstileTransitionMode.ForwardOut
                };

            outTransition.Backward = backwardTurnstileTransition;
            outTransition.Forward = forwardTurnstileTransition;

            TransitionService.SetNavigationOutTransition(element, outTransition);
        }
    }
}