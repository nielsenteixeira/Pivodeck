using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PivotalTracker.FluentAPI.Domain;
using PivotalTracker.FluentAPI.Service;

namespace Pivodeck.Core
{
    public class PivotalNotifier : IDisposable
    {

        public bool NewTask { get; set; }
        public bool DeletedTask { get; set; }
        public bool DeliveredTask { get; set; }
        public bool FinishedTask { get; set; }
        public bool StartedTask { get; set; }

        private readonly int _projectId;
        private PivotalTrackerFacade _pivotal;
        private const int SleepSeeconds = 1000;
        private DateTime _lastTimeFoundNewTask;
        private List<Story> _cachedStories = new List<Story>();
        private readonly Thread _searcher;
        private bool _strillRunning;

        public delegate void TaskStatusChanged(Story story);

        public event TaskStatusChanged OnCreatedTask;
        public event TaskStatusChanged OnStartedTask;
        public event TaskStatusChanged OnFinishedTask;
        public event TaskStatusChanged OnDeliveredTask;
        public event TaskStatusChanged OnDeletedTask;

        public PivotalNotifier()
        {
            _strillRunning = true;
            _lastTimeFoundNewTask = DateTime.Now;
        }

        public PivotalNotifier(string tokenValue, int projectId)
            : this()
        {
            _projectId = projectId;
            var token = new Token(tokenValue);
            _pivotal = new PivotalTrackerFacade(token);
            _searcher = new Thread(Observer);
            _searcher.Start();
        }

        public void SetToken(string tokenValue)
        {
            var token = new Token(tokenValue);
            _pivotal = new PivotalTrackerFacade(token);
        }

        public void CreateTask(string name, StoryTypeEnum type, string labels, int points, string description)
        {
            var stories = _pivotal.Projects().Get(_projectId).Stories();
            stories.Create().SetName(name)
                .SetType(type)
                .SetLabel(labels)
                .SetEstimate(points)
                .SetDescription(description).Save();
        }

        public void Observer()
        {
            while (_strillRunning)
            {
                try
                {
                    var stories = _pivotal.Projects().Get(_projectId).Stories().All().Item.ToList();

                    if (stories.Any(x => x.CreatedDate >= _lastTimeFoundNewTask))
                    {
                        stories.Where(x => x.CreatedDate >= _lastTimeFoundNewTask).ToList().ForEach(OnCreatedTask.Invoke);
                        _lastTimeFoundNewTask = DateTime.Now;
                    }

                    foreach (var story in from cachedStorey in _cachedStories
                                          let story = stories.FirstOrDefault(x => x.Id.Equals(cachedStorey.Id))
                                          where story != null && story.CurrentState != cachedStorey.CurrentState
                                          select story)
                    {
                        switch (story.CurrentState)
                        {
                            case StoryStateEnum.Started: OnStartedTask(story); break;
                            case StoryStateEnum.Finished: OnFinishedTask(story); break;
                            case StoryStateEnum.Delivered: OnDeliveredTask(story); break;
                            case StoryStateEnum.Accepted: throw new NotImplementedException("Modifcação de status para aceito não implementado");
                            case StoryStateEnum.Rejected: throw new NotImplementedException("Modifcação de status para rejeitado não implementado");
                            default: throw new ArgumentOutOfRangeException();
                        }
                    }

                    if (!_cachedStories.All(x => !stories.Contains(x, StoryComparer.Intance)))
                        _cachedStories.Where(x => !stories.Contains(x, StoryComparer.Intance)).ToList().ForEach(OnDeletedTask.Invoke);

                    _cachedStories = stories;
                }
                catch { continue; }
                finally { Thread.Sleep(SleepSeeconds); }
            }
        }

        public void Dispose()
        {
            _strillRunning = false;
            _searcher.Abort();
        }
    }
}
