using FitMeet.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FitMeet.Controls
{
    public class ActivitiesGridView : StackLayout
    {
        public static BindableProperty ItemSourceProperty = BindableProperty.Create(
            propertyName: "ItemSource",
            returnType: typeof(List<Activity>),
            declaringType: typeof(ActivitiesGridView),
            defaultValue: new List<Activity>(),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: ItemSourceValueChanged);
        public static BindableProperty SelectedItemsProperty = BindableProperty.Create(
            propertyName: "SelectedItems",
            returnType: typeof(ObservableCollection<Activity>),
            declaringType: typeof(ActivitiesGridView),
            defaultValue: new ObservableCollection<Activity>(),
            defaultBindingMode: BindingMode.OneWay,
            propertyChanged: SelectedItemsChanged);

        private static void SelectedItemsChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var pannel = ((ActivitiesGridView)bindable);
            pannel.SelectedItems = (ObservableCollection<Activity>)newValue;
        }
        private static void ItemSourceValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var pannel = ((ActivitiesGridView)bindable);
            pannel.ItemSource = (List<Activity>)newValue;
        }

        public List<Activity> ItemSource
        {
            get { return (List<Activity>)GetValue(ItemSourceProperty); }
            set
            {
                SetValue(ItemSourceProperty, value);
                OnPropertyChanged();
                AddItemToPannel();
            }
        }

        public ObservableCollection<Activity> SelectedItems
        {
            get { return (ObservableCollection<Activity>)GetValue(SelectedItemsProperty); }
            set
            {
                SetValue(SelectedItemsProperty, value);
                TriggerSelectedItems();
            }
        }

        private void TriggerSelectedItems()
        {
            foreach (StackLayout panel in this.Children)
            {
                foreach (ToggleButton item in panel.Children)
                {
                    if (SelectedItems.Contains((Activity)item.CommandParameter))
                    {
                        item.Checked = true;
                    }
                    else
                    {
                        item.Checked = false;
                    }
                }
            }
        }

        private void AddItemToPannel()
        {
            this.Children.Clear();
            if (ItemSource != null)
            {
                int row = -1;
                List<StackLayout> listStackLayouts = new List<StackLayout>();
                for (int i = 0; i < ItemSource.Count; i++)
                {
                    if (i % 3 == 0)
                    {
                        listStackLayouts.Add(new StackLayout()
                        { Orientation = StackOrientation.Horizontal, Spacing = 20 });
                        row++;
                    }
                    var button = new ToggleButton()
                    {
                        Image = ItemSource[i].Icon,
                        BorderColor = Color.FromHex("#5a5a5a"),
                        BorderWidth = 0.5,
                        HeightRequest = 48,
                        WidthRequest = 48,
                        Checked = false,
                        CommandParameter = ItemSource[i]
                    };
                    button.Clicked += (sender, args) =>
                    {
                        var b = (ToggleButton)sender;
                        var activity = (Activity)b.CommandParameter;
                        b.Checked = !b.Checked;
                        if (b.Checked == true)
                        {
                            SelectedItems.Add(activity);
                        }
                        else
                        {
                            SelectedItems.Remove(activity);
                        }
                    };
                    listStackLayouts[row].Children.Add(button);


                }
                foreach (var panel in listStackLayouts)
                {
                    this.Children.Add(panel);
                }

            }
        }




    }
}
