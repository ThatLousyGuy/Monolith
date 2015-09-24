using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;

namespace Lousy.Mon
{
    public delegate void CodeDelegate();

    /// <summary>
    /// <para>Frontend for running arbitrary code after an animation.</para>
    /// <para>Note: Specifying a duration using For() creates a dummy animation 
    /// that lasts for the specified duration.</para>
    /// </summary>
    public class CodeAnimator : AbstractAnimator
    {
        protected CodeDelegate _delegate;

        private CodeAnimator(UIElement elem) : base(elem)
        {

        }

        public CodeAnimator(CodeDelegate del) : base(new Rectangle())
        {
            _delegate = del;
            _elem = new Rectangle();
            
            _duration = new Duration(GetTimeSpan(1, OrSo.Tick));

            _reverse = false;

        }

        protected override void ApplyAnimationParams(Timeline animation)
        {
            DoubleAnimation doubleAnim = animation as DoubleAnimation;

            // Apply To
            doubleAnim.To = 0.0;

            // Apply From
            if (_fromDefined)
            {
                doubleAnim.From = 1.0;
            }
        }

        /// <summary>
        /// Dummy method. Does nothing.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="timeType"></param>
        /// <returns></returns>
        public override IAnimator For(double duration, OrSo timeType)
        {
            return this;
        }

        /// <summary>
        /// Runs the user code immediately
        /// </summary>
        /// <returns></returns>
        public override EventToken Now()
        {
            if (_storyboard == null)
            {
                CreateStoryboard();
            }

            // Run the code delegate
            _delegate.Invoke();
            
            return base.Now();
        }

        /// <summary>
        /// <para>Executes the user code after a specified duration.</para>
        /// <para>Note: This method produces a working EventToken</para>
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="timeType"></param>
        /// <returns>A token representing the animation</returns>
        public override EventToken After(double duration, OrSo timeType)
        {
            DispatcherTimer timer = new DispatcherTimer();
            EventToken placeholder = EventToken.PlaceholderToken();

            timer.Interval = GetTimeSpan(duration, timeType);
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                placeholder.PassOn(Now());
            };
            timer.Start();

            return placeholder;
        }

        /// <summary>
        /// <para>Executes the user code after the animation
        /// animation represented by the specified token.</para>
        /// <para>Note: This method produces a working EventToken</para>
        /// </summary>
        /// <param name="token"></param>
        /// <returns>A token representing the animation</returns>
        public override EventToken After(EventToken token)
        {
            return base.After(token);
        }

        /// <summary>
        /// <para>Executes the user code a specified duration after 
        /// the animation represented by the specified token.</para>
        /// <para>Note: This method produces a working EventToken</para>
        /// </summary>
        /// <param name="token"></param>
        /// <param name="duration"></param>
        /// <param name="timeType"></param>
        /// <returns>A token representing the animation</returns>
        public override EventToken After(EventToken token, double duration, OrSo timeType)
        {
            EventToken placeholder = EventToken.PlaceholderToken();

            // Separate the delay into stages
            // Use another CodeAnimator to trigger the timer after 
            // the original animation
            var afterTokenAnimator = new CodeAnimator(() =>
            {
                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = GetTimeSpan(duration, timeType);
                timer.Tick += (s, e) =>
                {
                    timer.Stop();
                    placeholder.PassOn(Now());
                };
                timer.Start();
            });

            afterTokenAnimator.After(token);

            return placeholder;
        }

        protected override Timeline CreateAnimation()
        {
            // Create an animation that will be applied to the transform
            DoubleAnimation animation = new DoubleAnimation();
            Storyboard.SetTarget(animation, _elem);
            Storyboard.SetTargetProperty(animation, "Opacity");

            return animation;
        }
    }
}
