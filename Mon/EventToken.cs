using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
    /// <summary>
    /// Represents an animation created using Monolith.
    /// Used to chain animations together.
    /// </summary>
    public class EventToken
    {
        protected Timeline _Animation;
        protected Dictionary<IAnimator, TimeSpan?> _Children;
        public Storyboard TokenStoryboard { get; private set; }

        protected EventToken()
        {
            _Children = new Dictionary<IAnimator, TimeSpan?>();
        }
        
        public EventToken(Timeline animation, Storyboard storyboard)
        {
            _Children = new Dictionary<IAnimator, TimeSpan?>();
            _Animation = animation;
            _Animation.Completed += AnimationEventHandler;
            TokenStoryboard = storyboard;
        }

        // Prevent public access to the no parameter constructor to make sure
        // it's not used willy-nilly
        /// <summary>
        /// Factory method for creating an empty event token.
        /// Used in conjunction with PassOn for creating animations out of
        /// order.
        /// </summary>
        /// <returns></returns>
        public static EventToken PlaceholderToken()
        {
            return new EventToken();
        }

        /// <summary>
        /// Passes on child animations to the specified successor.
        ///
        /// The current EventToken's child animations are activated by the
        /// successor's completion events.
        ///
        /// Used in conjunction with EventToken.PlaceHolderToken for creating
        /// animations out of order.
        /// </summary>
        /// <param name="successor"></param>
        /// <returns></returns>
        public EventToken PassOn(EventToken successor)
        {
            // Unregister for the predecessor's completion events
            if (_Animation != null)
            {
                _Animation.Completed -= AnimationEventHandler;
            }

            // Hand off the children to the successor
            if (_Children != null)
            {
                foreach (IAnimator anim in _Children.Keys)
                {
                    successor.AddChildAnimator(anim, _Children[anim]);
                }

                _Children.Clear();
            }

            return successor;
        }

        protected void AnimationEventHandler(object sender, object e)
        {
            // Deregister for callbacks 
            _Animation.Completed -= AnimationEventHandler;

            // Run the animations of every child
            foreach (IAnimator child in _Children.Keys)
            {
                TimeSpan? timespan = _Children[child];

                if (timespan == null)
                {
                    child.Now();
                }
                else
                {
                    child.After(timespan.Value.Seconds, OrSo.Secs);
                }
            }
        }

        /// <summary>
        /// Adds a child animation to be activated when the current
        /// EventToken's animation completes.
        /// </summary>
        /// <param name="child"></param>
        public void AddChildAnimator(IAnimator child)
        {
            if (!_Children.ContainsKey(child))
            {
                _Children.Add(child, null);
            }
        }

        /// <summary>
        /// Adds a child animation to be activated after the specified duration
        /// after current EventToken's animation completes.
        /// </summary>
        /// <param name="child"></param>
        public void AddChildAnimator(IAnimator child, TimeSpan? timespan)
        {
            if (!_Children.ContainsKey(child))
            {
                _Children.Add(child, timespan);
            }
        }
    }
}
