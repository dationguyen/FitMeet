using FitMeet.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IFitMeetRestService _fitMeetRestService;
        private ActivityData _activityData;
        public List<Place> TrainingLocationData { get; private set; }

        public List<Activity> Activities
        {
            get
            {
                return _activityData?.Activity;
            }
        }

        public List<Goal> Goals
        {
            get
            {
                return _activityData?.Goal;
            }
        }

        public List<Level> Levels
        {
            get
            {
                return _activityData?.Skill;
            }
        }

        private async Task<ActivityData> GetData()
        {
            var data = new ActivityData();
            var result = await _fitMeetRestService.GetActivityDataAsync();
            if ( result != null )
            {
                data = result?.Output?.Response;
            }
            return data;
        }

        private async Task<List<Place>> GetTrainPlaces()
        {
            var data = new List<Place>();
            var result = await _fitMeetRestService.GetTrainingLocationAsync();
            if ( result != null )
            {
                data = result?.Output?.Response;
            }
            return data;
        }


        public async Task UpdateAsync()
        {
            if ( _activityData == null )
            {
                _activityData = await GetData();
            }
            if ( TrainingLocationData == null )
            {
                TrainingLocationData = await GetTrainPlaces();
            }
        }

        public Level GetLevel( string id )
        {
            var skill = Levels?.First(a => a.Id == id);
            return skill;
        }
        public Activity GetActivity( string id )
        {
            var activity = Activities?.First(a => a.Id == id);
            return activity;
        }

        public Place GetTrainingLocation( string id )
        {
            var place = TrainingLocationData?.First(a => a.LocationId == id);
            return place;
        }

        public StaticDataService( IFitMeetRestService fitMeetRestService )
        {
            _fitMeetRestService = fitMeetRestService;
        }
    }
}
