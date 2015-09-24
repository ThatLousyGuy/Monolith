using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
    public abstract class AbstractDoubleAnimator : AbstractAnimator
    {
        protected double _from;
        protected double _to;

        public AbstractDoubleAnimator(UIElement elem) : base(elem) { }

        /// <summary>
        /// Sets the value to animate from.
        /// If left unset, the animation will start from the current value
        /// of the property to be animated.
        /// </summary>
        /// <param name="fromValue"></param>
        /// <returns></returns>
        public virtual AbstractDoubleAnimator From(double fromValue)
        {
            _from = fromValue;
            _fromDefined = true;
            return this;
        }

        /// <summary>
        /// Sets the value to animate to.
        /// </summary>
        /// <param name="toValue"></param>
        /// <returns></returns>
        public virtual AbstractDoubleAnimator To(double toValue)
        {
            _to = toValue;
            _toDefined = true;
            return this;
        }

        protected override void ApplyAnimationParams(Timeline animation)
        {
            DoubleAnimation doubleAnim = animation as DoubleAnimation;

            // Apply Ease
            doubleAnim.EasingFunction = _ease;

            // Apply To
            doubleAnim.To = _to;

            // Apply From
            if (_fromDefined)
            {
                doubleAnim.From = _from;
            }
        }

        protected abstract override Timeline CreateAnimation();
    }
}
