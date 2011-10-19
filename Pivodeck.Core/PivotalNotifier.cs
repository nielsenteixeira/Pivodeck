using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PivotalTracker.FluentAPI.Domain;
using PivotalTracker.FluentAPI.Service;

namespace Pivodeck.Core
{
    public class PivotalNotifier
    {
        private PivotalTrackerFacade Pivotal;

        public PivotalNotifier(string tokenValue)
        {
            var token = new Token(tokenValue); 
            Pivotal = new PivotalTrackerFacade(token);
        }

        public void CreateTask(int projectId, string name, StoryTypeEnum type, string labels, int points, string description)
        {
            Pivotal.Projects().Get(projectId).Stories()
            .Create()
                  .SetName(name)
                  .SetType(type)
                  .SetLabel(labels)
                  .SetEstimate(points)
                  .SetDescription(description)
            .Save();
        }
    }
}
