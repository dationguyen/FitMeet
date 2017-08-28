using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitMeet.Services
{
    public interface IGoogleLocationService
    {
        Task<List<string>> AutoComplete(string location);
    }
}
