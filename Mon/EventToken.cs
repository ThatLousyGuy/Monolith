using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Media.Animation;

namespace Lousy.Mon
{
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
        public static EventToken PlaceholderToken()
        {
            return new EventToken();
        }

        // Pass on children to the successor
        // The children should be activated by the successor's completion events
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

        public void AddChildAnimator(IAnimator child)
        {
            if (!_Children.ContainsKey(child))
            {
                _Children.Add(child, null);
            }
        }

        public void AddChildAnimator(IAnimator child, TimeSpan? timespan)
        {
            if (!_Children.ContainsKey(child))
            {
                _Children.Add(child, timespan);
            }
        }
    }
}
