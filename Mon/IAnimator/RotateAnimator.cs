using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
    /// <summary>
    /// A frontend for animating the rotation (in degrees) of a UIElement
    /// </summary>
    public class RotateAnimator : AbstractAffineDoubleAnimator
    {
        public RotateAnimator(UIElement elem) : base(elem) { }

        protected override Timeline CreateAnimation()
        {
            // If the duration is zero, transform immediately
            // This is here instead of the parent because the transform is only accessible here
            // and we cannot change it via the animation because the animation might get overwritten
            // before it has a chance to play
            if (_duration == TimeSpan.Zero)
            {
                _transform.Rotation = _to;
            }
            
            // Create an animation that will be applied to the transform
            DoubleAnimation animation = new DoubleAnimation();
            Storyboard.SetTarget(animation, _transform);
            Storyboard.SetTargetProperty(animation, "(CompositeTransform.Rotation)");

            return animation;
        }
        
        /// <summary>
        /// Sets the rotation immediately, before the animation activates.
        /// </summary>
        /// <param name="fromValue"></param>
        /// <returns></returns>
        public RotateAnimator InstantlyFrom(double fromValue)
        {
            From(fromValue);
            _transform.Rotation = fromValue;
            return this;
        }
    }
}
