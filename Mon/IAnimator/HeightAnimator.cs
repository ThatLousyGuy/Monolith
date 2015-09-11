using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
    public class HeightAnimator: AbstractDoubleAnimator 
    {
        public HeightAnimator(UIElement elem) : base(elem) { }

        protected override Timeline CreateAnimation()
        {
            FrameworkElement frameworkElem = _elem as FrameworkElement;
            // If the duration is zero, transform immediately
            // This is here instead of the parent because the transform is only accessible here
            // and we cannot change it via the animation because the animation might get overwritten
            // before it has a chance to play
            if (_duration == TimeSpan.Zero)
            {
                frameworkElem.Height = _to;
            }
            
            // Create an animation that will be applied to the transform
            DoubleAnimation animation = new DoubleAnimation();
            Storyboard.SetTarget(animation, frameworkElem);
            Storyboard.SetTargetProperty(animation, "(FrameworkElement.Height)");

            return animation;
        }

        //
        // Transform immediately before the animation activates
        //
        public HeightAnimator InstantlyFrom(double fromValue)
        {
            FrameworkElement frameworkElem = _elem as FrameworkElement;
            From(fromValue);
            frameworkElem.Height = fromValue;
            return this;
        }
    }
}
