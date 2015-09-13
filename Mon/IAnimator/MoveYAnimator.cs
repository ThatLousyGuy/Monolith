using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
    /// <summary>
    /// A frontend for animating the translation of a UIElement along the Y axis
    /// </summary>
    public class MoveYAnimator : AbstractAffineDoubleAnimator
    {
        public MoveYAnimator(UIElement elem) : base(elem) { }

        protected override Timeline CreateAnimation()
        {
            // If the duration is zero, transform immediately
            // This is here instead of the parent because the transform is only accessible here
            // and we cannot change it via the animation because the animation might get overwritten
            // before it has a chance to play
            if (_duration == TimeSpan.Zero)
            {
                _transform.TranslateY = _to;
            }
            
            
            // Create an animation that will be applied to the transform
            DoubleAnimation animation = new DoubleAnimation();
            Storyboard.SetTarget(animation, _transform);
            Storyboard.SetTargetProperty(animation, "(CompositeTransform.TranslateY)");

            return animation;
        }
        
        /// <summary>
        /// Sets the Y translation immediately, before the animation activates.
        /// </summary>
        /// <param name="fromValue"></param>
        /// <returns></returns>
        public MoveYAnimator InstantlyFrom(double fromValue)
        {
            From(fromValue);
            _transform.TranslateY = fromValue;
            return this;
        }
    }
}
