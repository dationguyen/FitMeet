using FitMeet.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ListView), typeof(CustomListViewRenderer))]

namespace FitMeet.iOS.Renderers
{
    public class CustomListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (this.Control == null) return;

            this.Control.TableFooterView = new UIView();
            this.Control.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.OnDrag;
        }
    }
}