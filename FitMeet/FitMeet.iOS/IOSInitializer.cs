using Microsoft.Practices.Unity;
using Plugin.Badge.iOS;
using Prism.Unity;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(TabbedPage),typeof(BadgedTabbedPageRenderer))]
namespace FitMeet.iOS
{
    public class IOSInitializer:IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {

        }
    }

}
