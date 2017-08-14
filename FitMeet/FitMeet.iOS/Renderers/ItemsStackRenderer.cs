using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Xamarin.Forms;
using FitMeet.Controls;
using FitMeet.iOS.Renderers;
using Xamarin.Forms.Platform.iOS;
using System.ComponentModel;
using System.Collections.Specialized;

[assembly: ExportRenderer(typeof(ItemsStack) , typeof(ItemsStackRenderer))]

namespace FitMeet.iOS.Renderers
{
    public class ItemsStackRenderer : VisualElementRenderer<StackLayout>
    {
        //protected virtual void ElementPropertyChanged( object sender , PropertyChangedEventArgs e )
        //{
        //    var element = (ItemsStack)Element;
        //    if ( e.PropertyName == "ItemsSource" )
        //    {
        //        if ( element.ItemsSource is INotifyCollectionChanged )
        //            (element.ItemsSource as INotifyCollectionChanged).CollectionChanged += DataCollectionChanged;

        //        context.RunOnUiThread(() => UpdateFromCollectionChange());
        //    }
        //}
    }
}