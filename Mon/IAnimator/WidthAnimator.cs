using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
    /// <summary>
    /// A frontend for animating the width of a UIElement
    /// </summary>
    public class WidthAnimator: AbstractDoubleAnimator 
    {
        public WidthAnimator(UIElement elem) : base(elem) { }

        protected override Timeline CreateAnimation()
        {
            FrameworkElement frameworkElem = _elem as FrameworkElement;
            // If the duration is zero, transform immediately
            // This is here instead of the parent because the transform is only accessible here
            // and we cannot change it via the animation because the animation might get overwritten
            // before it has a chance to play
            if (_duration == TimeSpan.Zero)
            {
                frameworkElem.Width = _to;
            }
            
            // Create an animation that will be applied to the transform
            DoubleAnimation animation = new DoubleAnimation();
            Storyboard.SetTarget(animation, frameworkElem);
            Storyboard.SetTargetProperty(animation, "(FrameworkElement.Width)");

            return animation;
        }
        
        /// <summary>
        /// Sets the width immediately, before the animation activates.
        /// </summary>
        /// <param name="fromValue"></param>
        /// <returns></returns>
        public WidthAnimator InstantlyFrom(double fromValue)
        {
            FrameworkElement frameworkElem = _elem as FrameworkElement;
            From(fromValue);
            frameworkElem.Width = fromValue;
            return this;
        }
    }
}
