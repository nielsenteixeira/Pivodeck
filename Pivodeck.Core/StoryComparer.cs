using System.Collections.Generic;
using PivotalTracker.FluentAPI.Domain;

namespace Pivodeck.Core
{
    public class StoryComparer : IEqualityComparer<Story>
    {
        public bool Equals(Story x, Story y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Story obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}