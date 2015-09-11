using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
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
        IAnimator With(EasingFunctionBase ease);
        IAnimator For(double duration, OrSo timeType);
        IAnimator AndReverseIt();
        EventToken Now();
        EventToken After(double duration, OrSo timeType);

        #region Experimental
        EventToken After(EventToken token);
        EventToken After(EventToken token, double duration, OrSo timeType);
        #endregion
    }
}
