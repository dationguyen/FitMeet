using FitMeet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IFitMeetRestService _fitMeetRestService;
        private ActivityData _data;

        public List<Activity> Activities
        {
            get
            {
                return _data?.Activity;
            }
        }

        public List<Goal> Goals
        {
            get
            {
                return _data?.Goal;
            }
        }

        public List<Skill> Skills
        {
            get
            {
                return _data?.Skill;
            }
        }

        private async Task<ActivityData> GetData()
        {
            var data = new ActivityData();
            var result = await _fitMeetRestService.GetActivityDataAsync();
            if (result != null)
            {
                data = result?.Output?.Response;
            }
            return data;
        }

        public async Task UpdateAsync()
        {
            if (_data == null)
            {
                _data = await GetData();
            }
        }

        public ActivityService(IFitMeetRestService fitMeetRestService)
        {
            _fitMeetRestService = fitMeetRestService;
        }
    }
}
