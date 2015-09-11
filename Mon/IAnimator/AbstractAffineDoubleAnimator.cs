using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
    public abstract class AbstractAffineDoubleAnimator : AbstractDoubleAnimator
    {
        protected CompositeTransform _transform;
        
        public AbstractAffineDoubleAnimator(UIElement elem) : base(elem)
        {
            // Check for a transform, if it doesn't exist, make one
            if (_elem.RenderTransform == null || _elem.RenderTransform.GetType() != typeof(CompositeTransform))
            {
                _elem.RenderTransform = new CompositeTransform();
            }
            _transform = _elem.RenderTransform as CompositeTransform;
        }

        protected abstract override Timeline CreateAnimation();
    }
}
