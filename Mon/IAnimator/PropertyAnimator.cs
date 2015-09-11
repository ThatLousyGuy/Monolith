using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
    public class PropertyAnimator : AbstractDoubleAnimator
    {
        protected string _propPath = null;

        public PropertyAnimator(UIElement elem) : base(elem) { }

        public PropertyAnimator Property(string propPath)
        {
            _propPath = propPath;
            return this;
        }

        protected override Timeline CreateAnimation()
        {
            // BUG: Cannot set the time to zero to set value immediately
            //if (_duration == TimeSpan.Zero)
            //{
            //    // Need a way to set the value of the property using the PropertyPath
            //}


            // Create an animation that will be applied to the transform
            DoubleAnimation animation = new DoubleAnimation();

            // Used to enable non-standard animations
            animation.EnableDependentAnimation = true;

            Storyboard.SetTarget(animation, _elem);
            Storyboard.SetTargetProperty(animation, _propPath);
            Storyboard.SetTargetName(animation, _propPath);

            return animation;
        }
    }
}
