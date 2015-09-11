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

        public IAnimator With(EasingFunctionBase ease)
        {
            _ease = ease;
            return this;
        }

        public IAnimator For(double duration, OrSo timeType)
        {
            _duration = new Duration(GetTimeSpan(duration, timeType));
            _durationDefined = true;

            return this;
        }

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

            _storyboard.AutoReverse = _reverse;

            _storyboard.Begin();

            if (_eventToken == null)
            {
                _eventToken = new EventToken(_animation, _storyboard);
            }
            return _eventToken;
        }

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
