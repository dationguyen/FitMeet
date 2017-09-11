using Prism.Navigation;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbedPage:TabbedPage, INavigatingAware, INavigatedAware
    {
        public static BindableProperty SelectedIndexProperty = BindableProperty.Create(
            propertyName: "SelectedIndex",
            returnType: typeof(int),
            declaringType: typeof(MainTabbedPage),
            defaultValue: -1,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: SelectedIndexChanged);


        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set
            {
                if(Children.Count > 0 && value >= 0)
                {
                    CurrentPage = Children[value];
                    SetValue(SelectedIndexProperty,value);
                }
            }
        }

        private static void SelectedIndexChanged(BindableObject bindable,object oldValue,object newValue)
        {
            ((MainTabbedPage)bindable).SelectedIndex = (int)newValue;
        }

        public MainTabbedPage()
        {
            InitializeComponent();
            this.CurrentPageChanged += CurrentPageHasChanged;
        }

        private void CurrentPageHasChanged(object sender,EventArgs e)
        {
            var title = this.CurrentPage.Title;
            if(title == "Search")
                title = "Members";
            this.Title = title;
            if(CurrentPage != null && Children.Count > 0)
            {
                SelectedIndex = Children.IndexOf(CurrentPage);
            }
        }
        public void OnNavigatingTo(NavigationParameters parameters)
        {
            foreach(var child in Children)
            {
                (child as INavigatingAware)?.OnNavigatingTo(parameters);
                (child?.BindingContext as INavigatingAware)?.OnNavigatingTo(parameters);
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            foreach(var child in Children)
            {
                (child as INavigatedAware)?.OnNavigatedFrom(parameters);
                (child?.BindingContext as INavigatedAware)?.OnNavigatedFrom(parameters);
            }
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            foreach(var child in Children)
            {
                (child as INavigatedAware)?.OnNavigatedTo(parameters);
                (child?.BindingContext as INavigatedAware)?.OnNavigatedTo(parameters);
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (SelectedIndex > 0)
            {
                SelectedIndex = 0;
                return true;
            }
            return false;
        }
    }
}