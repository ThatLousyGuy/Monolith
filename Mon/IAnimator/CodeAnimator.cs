using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

namespace Lousy.Mon
{
    public delegate void CodeDelegate();

    /// <summary>
    /// A frontend for running arbitrary code after an animation
    /// </summary>
    public class CodeAnimator : IAnimator
    {
        protected UIElement _elem;
        protected EasingFunctionBase _ease;
        protected bool _fromDefined = false;
        protected bool _toDefined = false;
        protected bool _durationDefined = false;
        protected Duration _duration;
        protected Timeline _animation;
        protected Storyboard _storyboard;
        protected EventToken _eventToken;
        protected CodeDelegate _delegate;
        protected bool _reverse;

        public CodeAnimator(CodeDelegate del)
        {
            _delegate = del;
            _elem = new Rectangle();

            _reverse = false;
        }

        /// <summary>
        /// Dummy function. Does nothing.
        /// </summary>
        /// <param name="ease"></param>
        /// <returns></returns>
        public IAnimator With(EasingFunctionBase ease)
        {
            return this;
        }

        /// <summary>
        /// Dummy function. Does nothing.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="timeType"></param>
        /// <returns></returns>
        public IAnimator For(double duration, OrSo timeType)
        {
            return this;
        }

        /// <summary>
        /// Dummy function. Does nothing.
        /// </summary>
        /// <returns></returns>
        public IAnimator AndReverseIt()
        {
            _reverse = true;

            return this;
        }

        public EventToken Now()
        {
            if (_storyboard == null)
            {
                CreateStoryboard();
            }

            // Run the code delegate
            _delegate.Invoke();

            _storyboard.AutoReverse = _reverse;

            // Start the animation
            _storyboard.Begin();

            if (_eventToken == null)
            {
                _eventToken = new EventToken(_animation, _storyboard);
            }
            return _eventToken;
        }

        protected void CreateStoryboard()
        {
            _animation = CreateAnimation();
            _storyboard = new Storyboard();

            // Apply nigh-instant duration just to avoid any potential
            // problems or undefined behavior with 0 duration
            _animation.Duration = new Duration(TimeSpan.FromTicks(1));

            _storyboard.Children.Add(_animation);
        }

        protected Timeline CreateAnimation()
        {
            // Create an animation that will be applied to the transform
            DoubleAnimation animation = new DoubleAnimation();
            Storyboard.SetTarget(animation, _elem);
            Storyboard.SetTargetProperty(animation, "Opacity");
            animation.To = 1;

            return animation;
        }

        public EventToken After(double duration, OrSo timeType)
        {
            if (_storyboard == null)
            {
                CreateStoryboard();
            }

            // Apply the begin time
            switch (timeType)
            {
                case OrSo.Ticks:
                    _animation.BeginTime = new TimeSpan((long)duration);
                    break;
                case OrSo.Seconds:
                    _animation.BeginTime = new TimeSpan((long)(duration * 1000 * 10000));
                    break;
                default:
                    throw new Exception("Improper time type specified");
            }

            // Run the code delegate
            _delegate.Invoke();

            _storyboard.AutoReverse = _reverse;

            // Start the animation
            _storyboard.Begin();

            if (_eventToken == null)
            {
                _eventToken = new EventToken(_animation, _storyboard);
            }
            return _eventToken;
        }

        public EventToken After(EventToken token)
        {
            if (_storyboard == null)
            {
                CreateStoryboard();
            }

            _storyboard.AutoReverse = _reverse;

            token.AddChildAnimator(this);

            if (_eventToken == null)
            {
                _eventToken = new EventToken(_animation, _storyboard);
            }
            return _eventToken;
        }

        public EventToken After(EventToken token, double duration, OrSo timeType)
        {
            TimeSpan timespan;
            if (_storyboard == null)
            {
                CreateStoryboard();
            }

            _storyboard.AutoReverse = _reverse;

            switch (timeType)
            {
                case OrSo.Ticks:
                    timespan = TimeSpan.FromTicks((long)duration);
                    break ;
                case OrSo.Seconds:
                    timespan = TimeSpan.FromSeconds(duration);
                    break;
                default:
                    throw new Exception("Improper time type specified");
            }

            token.AddChildAnimator(this, timespan);

            if (_eventToken == null)
            {
                _eventToken = new EventToken(_animation, _storyboard);
            }
            return _eventToken;
        }
    }
}
