using FitMeet.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FitMeet.Controls
{
    public class ActivitiesStackLayout : StackLayout
    {
        public ActivitiesStackLayout()
        {
            var item = ItemsSource;
        }

        public static BindableProperty ItemsSourceProperty = BindableProperty.Create(
            propertyName: "ItemsSource",
            returnType: typeof(List<ActivityModel>),
            declaringType: typeof(ActivitiesStackLayout),
            defaultValue: new List<ActivityModel>(),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: ItemsSourceValueChanged);

        public List<ActivityModel> ItemsSource
        {
            get { return (List<ActivityModel>)GetValue(ItemsSourceProperty); }
            set
            {
                SetValue(ItemsSourceProperty, value);
                OnPropertyChanged();
                AddItemToPannel();
            }
        }

        private void AddItemToPannel()
        {
            this.Children.Clear();
            if (ItemsSource != null)
            {
                foreach (var activity in ItemsSource)
                {
                    this.Children.Add(new Image() { Source = activity.ActivityIcon });
                }
            }
        }

        private static void ItemsSourceValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var pannel = ((ActivitiesStackLayout)bindable);
            pannel.ItemsSource = (List<ActivityModel>)newValue;
        }


    }
}
