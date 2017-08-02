using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public interface IActivityService
    {
        List<Models.Activity> Activities { get; }
        List<Models.Goal> Goals { get; }
        List<Models.Skill> Skills { get; }

        Task UpdateAsync();

    }



}
