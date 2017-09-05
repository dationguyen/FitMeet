using Android.Content.Res;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using FitMeet.Droid.Renderers;
using Plugin.Badge.Droid;
using System.ComponentModel;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(TabbedPage),typeof(MainTabbedPageRenderer))]
namespace FitMeet.Droid.Renderers
{
    internal class MainTabbedPageRenderer:BadgedTabbedPageRenderer
    {
        bool setup;
        ViewPager pager;
        TabLayout layout;

        protected override void OnElementPropertyChanged(object sender,PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender,e);

            if(setup)
                return;

            if(e.PropertyName == "Renderer")
            {
                pager = (ViewPager)ViewGroup.GetChildAt(0);
                layout = (TabLayout)ViewGroup.GetChildAt(1);
                setup = true;

                ColorStateList colors = null;
                if((int)Build.VERSION.SdkInt >= 23)
                {
                    colors = Resources.GetColorStateList(Resource.Color.icon_tab,Forms.Context.Theme);
                }
                else
                {
                    colors = Resources.GetColorStateList(Resource.Color.icon_tab,null);
                }

                for(int i = 0 ; i < layout.TabCount ; i++)
                {
                    var tab = layout.GetTabAt(i);
                    var icon = tab.Icon;
                    if(icon != null)
                    {
                        icon = Android.Support.V4.Graphics.Drawable.DrawableCompat.Wrap(icon);
                        Android.Support.V4.Graphics.Drawable.DrawableCompat.SetTintList(icon,colors);
                    }
                }

            }
        }

        protected override void OnLayout(bool changed,int l,int t,int r,int b)
        {
            InvertLayoutThroughScale();

            base.OnLayout(changed,l,t,r,b);
        }

        private void InvertLayoutThroughScale()
        {
            ViewGroup.ScaleY = -1;

            TabLayout tabLayout = null;
            ViewPager viewPager = null;

            for(int i = 0 ; i < ChildCount ; ++i)
            {
                Android.Views.View view = (Android.Views.View)GetChildAt(i);
                if(view is TabLayout)
                    tabLayout = (TabLayout)view;
                else if(view is ViewPager)
                    viewPager = (ViewPager)view;
            }

            tabLayout.ScaleY = viewPager.ScaleY = -1;
            viewPager.SetPadding(0,-tabLayout.MeasuredHeight,0,0);
        }
    }
}