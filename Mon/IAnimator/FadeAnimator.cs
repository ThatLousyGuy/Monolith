﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
    public class FadeAnimator : AbstractDoubleAnimator 
    {
        public FadeAnimator(UIElement elem) : base(elem) { }

        protected override Timeline CreateAnimation()
        {
            // If the duration is zero, transform immediately
            // This is here instead of the parent because the transform is only accessible here
            // and we cannot change it via the animation because the animation might get overwritten
            // before it has a chance to play
            if (_duration == TimeSpan.Zero)
            {
                _elem.Opacity = _to;
            }
            
            // Create an animation that will be applied to the transform
            DoubleAnimation animation = new DoubleAnimation();
            Storyboard.SetTarget(animation, _elem);
            Storyboard.SetTargetProperty(animation, "Opacity");

            return animation;
        }

        /// <summary>
        /// Sets the opacity to 0 immediately, before the animation activates.
        /// </summary>
        /// <param name="fromValue"></param>
        /// <returns></returns>
        public FadeAnimator InstantlyFrom(double fromValue)
        {
            From(fromValue);
            _elem.Opacity = fromValue;
            return this;
        }
    }
}
