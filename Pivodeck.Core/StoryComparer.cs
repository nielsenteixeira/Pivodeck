using System.Collections.Generic;
using PivotalTracker.FluentAPI.Domain;

namespace Pivodeck.Core
{
    public class StoryComparer : IEqualityComparer<Story>
    {
        private static StoryComparer _storyComparer;

        protected StoryComparer() { }

        public bool Equals(Story x, Story y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Story obj)
        {
            return obj.Id.GetHashCode();
        }

        public static IEqualityComparer<Story> Intance
        {
            get { return _storyComparer ?? (_storyComparer = new StoryComparer()); }
        }
    }
}