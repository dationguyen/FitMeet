using FitMeet.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public interface IActivityService
    {
        List<Activity> Activities { get; }
        List<Goal> Goals { get; }
        List<Level> Levels { get; }
        Level GetLevel( string id );
        Activity GetActivity( string id );

        Task UpdateAsync();

    }



}
