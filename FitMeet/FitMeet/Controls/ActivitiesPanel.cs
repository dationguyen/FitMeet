using FitMeet.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FitMeet.Controls
{
    public class ActivitiesPanel : StackLayout
    {
        public static BindableProperty ItemSourceProperty = BindableProperty.Create(
            propertyName: "ItemSource" ,
            returnType: typeof(List<SkillModel>) ,
            declaringType: typeof(ActivitiesPanel) ,
            defaultValue: new List<SkillModel>() ,
            defaultBindingMode: BindingMode.OneWay ,
            propertyChanged: ItemSourceValueChanged);

        public List<SkillModel> ItemSource
        {
            get { return (List<SkillModel>)GetValue(ItemSourceProperty); }
            set
            {
                SetValue(ItemSourceProperty , value);
                OnPropertyChanged();
                AddItemToPannel();
            }
        }

        private void AddItemToPannel()
        {
            this.Children.Clear();
            if ( ItemSource != null )
            {
                foreach ( var skill in ItemSource )
                {
                    var activities = skill?.Activities;
                    if ( activities != null )
                    {
                        foreach ( var activity in activities )
                        {
                            this.Children.Add(new Image() { Source = activity.ActivityIcon });
                        }
                    }
                }
            }
        }

        private static void ItemSourceValueChanged( BindableObject bindable , object oldValue , object newValue )
        {
            var pannel = ((ActivitiesPanel)bindable);
            pannel.ItemSource = (List<SkillModel>)newValue;
        }
    }
}
