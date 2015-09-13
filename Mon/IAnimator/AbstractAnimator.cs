using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
    public abstract class AbstractAnimator : IAnimator
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
        protected bool _reverse;

        public AbstractAnimator(UIElement elem)
        {
            _elem = elem;
            _ease = null;
            _reverse = false;
        }

        /// <summary>
        /// Sets the easing function to use for the animation.
        /// Easing function objects can be reused between animations.
        /// </summary>
        /// <param name="ease"></param>
        /// <returns></returns>
        public IAnimator With(EasingFunctionBase ease)
        {
            _ease = ease;
            return this;
        }

        /// <summary>
        /// Sets the duration of the animation.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="timeType"></param>
        /// <returns></returns>
        public IAnimator For(double duration, OrSo timeType)
        {
            _duration = new Duration(GetTimeSpan(duration, timeType));
            _durationDefined = true;

            return this;
        }

        /// <summary>
        /// Sets whether to reverse the animation.
        /// </summary>
        /// <returns></returns>
        public IAnimator AndReverseIt()
        {
            _reverse = true;

            return this;
        }

        /// <summary>
        /// Executes the animation immediately.
        /// </summary>
        /// <returns>A token representing the animation</returns>
        public EventToken Now()
        {
            if (_storyboard == null)
            {
                CreateStoryboard();
            }

            _storyboard.AutoReverse = _reverse;

            _storyboard.Begin();

            if (_eventToken == null)
            {
                _eventToken = new EventToken(_animation, _storyboard);
            }
            return _eventToken;
        }

        /// <summary>
        /// Executes the animation after a specified duration.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="timeType"></param>
        /// <returns>A token representing the animation</returns>
        public EventToken After(double duration, OrSo timeType)
        {
            if (_storyboard == null)
            {
                CreateStoryboard();
            }


            _animation.BeginTime = GetTimeSpan(duration, timeType);

            _storyboard.AutoReverse = _reverse;

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
            ApplyAnimationParams(_animation);

            // Apply Duration
            _animation.Duration = _duration;

            _storyboard.Children.Add(_animation);
        }

        /// <summary>
        /// Executes the animation after the animation represented by the specified token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>A token representing the animation</returns>
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

        /// <summary>
        /// Executes the animation after a specified duration after the animation represented by the specified token.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="duration"></param>
        /// <param name="timeType"></param>
        /// <returns>A token representing the animation</returns>
        public EventToken After(EventToken token, double duration, OrSo timeType)
        {
            if (_storyboard == null)
            {
                CreateStoryboard();
            }

            _storyboard.AutoReverse = _reverse;

            token.AddChildAnimator(this, GetTimeSpan(duration, timeType));

            if (_eventToken == null)
            {
                _eventToken = new EventToken(_animation, _storyboard);
            }
            return _eventToken;
        }

        #region Helpers

        protected TimeSpan GetTimeSpan(double duration, OrSo timeType)
        {
            switch (timeType)
            {
                case OrSo.Ticks:
                    return TimeSpan.FromTicks((long)duration);
                case OrSo.Seconds:
                    return TimeSpan.FromSeconds(duration);
                default:
                    throw new Exception("Improper time type specified");
            }
        }

        #endregion

        // Subclass must create animation and apply it to a transform
        protected abstract Timeline CreateAnimation();

        // Subclass must apply required settings
        protected abstract void ApplyAnimationParams(Timeline animation);
    }
}
