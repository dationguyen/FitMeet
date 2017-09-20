using System.IO;
using System.Threading.Tasks;

namespace FitMeet.Services.DependencyServices
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}
