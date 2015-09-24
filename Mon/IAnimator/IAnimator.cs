using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
    /// <summary>
    /// Determines the units of the time given.
    /// The name was chosen to maintain a grammatically correct syntax.
    /// </summary>
    public enum OrSo
    {
        Ticks,
        Tick = Ticks,
        Tic = Ticks,
        Tics = Ticks,
        Seconds,
        Second = Seconds,
        Sec = Seconds,
        Secs = Seconds,
    }

    public interface IAnimator
    {
        /// <summary>
        /// Sets the easing function to use for the animation.
        /// Easing function objects can be reused between animations.
        /// </summary>
        /// <param name="ease"></param>
        /// <returns></returns>
        IAnimator With(EasingFunctionBase ease);

        /// <summary>
        /// Sets the duration of the animation.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="timeType"></param>
        /// <returns></returns>
        IAnimator For(double duration, OrSo timeType);

        /// <summary>
        /// Sets whether to reverse the animation.
        /// </summary>
        /// <returns></returns>
        IAnimator AndReverseIt();

        /// <summary>
        /// Executes the animation immediately.
        /// </summary>
        /// <returns>A token representing the animation</returns>
        EventToken Now();

        /// <summary>
        /// Executes the animation after a specified duration.
        /// </summary>
        /// <param name="duration"></param>
        /// <param name="timeType"></param>
        /// <returns>A token representing the animation</returns>
        EventToken After(double duration, OrSo timeType);

        /// <summary>
        /// Executes the animation after the animation represented by the specified token.
        /// </summary>
        /// <param name="token"></param>
        /// <returns>A token representing the animation</returns>
        EventToken After(EventToken token);

        /// <summary>
        /// Executes the animation after a specified duration after the animation represented by the specified token.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="duration"></param>
        /// <param name="timeType"></param>
        /// <returns>A token representing the animation</returns>
        EventToken After(EventToken token, double duration, OrSo timeType);
    }
}
