﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
    public class ScaleXAnimator : AbstractAffineDoubleAnimator
    {
        public ScaleXAnimator(UIElement elem) : base(elem) { }

        protected override Timeline CreateAnimation()
        {
            // If the duration is zero, transform immediately
            // This is here instead of the parent because the transform is only accessible here
            // and we cannot change it via the animation because the animation might get overwritten
            // before it has a chance to play
            if (_duration == TimeSpan.Zero)
            {
                _transform.ScaleX = _to;
            }
            
            // Create an animation that will be applied to the transform
            DoubleAnimation animation = new DoubleAnimation();
            Storyboard.SetTarget(animation, _transform);
            Storyboard.SetTargetProperty(animation, "(CompositeTransform.ScaleX)");

            return animation;
        }

        //
        // Transform immediately before the animation activates
        //
        public ScaleXAnimator InstantlyFrom(double fromValue)
        {
            From(fromValue);
            _transform.ScaleX = fromValue;
            return this;
        }
    }
}
