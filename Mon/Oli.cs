using Windows.UI.Xaml;

namespace Lousy.Mon
{
    public static class Oli
    {
        public static MoveXAnimator MoveXOf(UIElement elem)
        {
            return new MoveXAnimator(elem);
        }

        public static MoveYAnimator MoveYOf(UIElement elem)
        {
            return new MoveYAnimator(elem);
        }

        public static ScaleXAnimator ScaleXOf(UIElement elem)
        {
            return new ScaleXAnimator(elem);
        }

        public static ScaleYAnimator ScaleYOf(UIElement elem)
        {
            return new ScaleYAnimator(elem);
        }

        public static RotateAnimator Rotate(UIElement elem)
        {
            return new RotateAnimator(elem);
        }

        public static FadeAnimator Fade(UIElement elem)
        {
            return new FadeAnimator(elem);
        }

        public static WidthAnimator AnimateWidthOf(UIElement elem)
        {
            return new WidthAnimator(elem);
        }

        public static HeightAnimator AnimateHeightOf(UIElement elem)
        {
            return new HeightAnimator(elem);
        }

        public static CodeAnimator Run(CodeDelegate del)
        {
            return new CodeAnimator(del);
        }

        public static PropertyAnimator Animate(UIElement elem)
        {
            return new PropertyAnimator(elem);
        }
    }
}
