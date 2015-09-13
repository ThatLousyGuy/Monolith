using Windows.UI.Xaml;

namespace Lousy.Mon
{
    /// <summary>
    /// Fluent animation factory for UIElements
    /// </summary>
    public static class Oli
    {
        /// <summary>
        /// Moves a UIElement along the X axis.
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public static MoveXAnimator MoveXOf(UIElement elem)
        {
            return new MoveXAnimator(elem);
        }

        /// <summary>
        /// Moves a UIElement along the Y axis.
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public static MoveYAnimator MoveYOf(UIElement elem)
        {
            return new MoveYAnimator(elem);
        }

        /// <summary>
        /// Scales a UIElement along the X axis.
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public static ScaleXAnimator ScaleXOf(UIElement elem)
        {
            return new ScaleXAnimator(elem);
        }

        /// <summary>
        /// Scales a UIElement along the Y axis.
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public static ScaleYAnimator ScaleYOf(UIElement elem)
        {
            return new ScaleYAnimator(elem);
        }

        /// <summary>
        /// Rotates a UIElement.
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public static RotateAnimator Rotate(UIElement elem)
        {
            return new RotateAnimator(elem);
        }

        /// <summary>
        /// Changes the opacity of a UIElement.
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public static FadeAnimator Fade(UIElement elem)
        {
            return new FadeAnimator(elem);
        }

        /// <summary>
        /// Changes the width of a UIElement.
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public static WidthAnimator AnimateWidthOf(UIElement elem)
        {
            return new WidthAnimator(elem);
        }

        /// <summary>
        /// Changes the height of a UIElement.
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public static HeightAnimator AnimateHeightOf(UIElement elem)
        {
            return new HeightAnimator(elem);
        }

        /// <summary>
        /// Runs arbitrary code.
        /// Use this method to run code after an animation completes.
        /// </summary>
        /// <param name="del"></param>
        /// <returns></returns>
        public static CodeAnimator Run(CodeDelegate del)
        {
            return new CodeAnimator(del);
        }

        /// <summary>
        /// Animates an arbitrary property of a UIElement.
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public static PropertyAnimator Animate(UIElement elem)
        {
            return new PropertyAnimator(elem);
        }
    }
}
