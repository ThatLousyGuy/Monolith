using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
    public abstract class AbstractDoubleAnimator : AbstractAnimator
    {
        protected double _from;
        protected double _to;

        public AbstractDoubleAnimator(UIElement elem) : base(elem) { }

        public AbstractDoubleAnimator From(double fromValue)
        {
            _from = fromValue;
            _fromDefined = true;
            return this;
        }

        public AbstractDoubleAnimator To(double toValue)
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
