using System;
using Xamarin.Forms;

namespace FitMeet.Controls
{
    public class CheckBox:ContentView
    {
        protected Grid ContentGrid;
        protected ContentView ContentContainer;
        public Label TextContainer;
        protected Image ImageContainer;

        public CheckBox()
        {
            var TapGesture = new TapGestureRecognizer();
            TapGesture.Tapped += TapGestureOnTapped;
            GestureRecognizers.Add(TapGesture);

            ContentGrid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            ContentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(36) });
            ContentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1,GridUnitType.Star) });
            ContentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1,GridUnitType.Auto) });

            ImageContainer = new Image
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
            };
            ImageContainer.HeightRequest = 24;
            ImageContainer.WidthRequest = 24;

            ContentGrid.Children.Add(ImageContainer);

            ContentContainer = new ContentView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            Grid.SetColumn(ContentContainer,1);

            TextContainer = new Label
            {
                TextColor = Color.FromHex("#5a5a5a"),
                FontSize = 11,
                Margin = new Thickness(0,2,0,0),
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            ContentContainer.Content = TextContainer;

            ContentGrid.Children.Add(ContentContainer);

            base.Content = ContentGrid;

            this.Image.Source = "Image_Unchecked.png";
            this.BackgroundColor = Color.Transparent;
        }

        public static BindableProperty CheckedProperty = BindableProperty.Create(
            propertyName: "Checked",
            returnType: typeof(Boolean),
            declaringType: typeof(CheckBox),
            defaultValue: false,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: CheckedValueChanged);

        public static BindableProperty TextProperty = BindableProperty.Create(
            propertyName: "Text",
            returnType: typeof(String),
            declaringType: typeof(CheckBox),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: TextValueChanged);

        public Boolean Checked
        {
            get
            {
                if(GetValue(CheckedProperty) == null)
                    return false;
                return (Boolean)GetValue(CheckedProperty);
            }
            set
            {
                SetValue(CheckedProperty,value);
                OnPropertyChanged();
                RaiseCheckedChanged();
            }
        }

        private static void CheckedValueChanged(BindableObject bindable,object oldValue,object newValue)
        {
            if(newValue != null && (Boolean)newValue == true)
                ((CheckBox)bindable).Image.Source = "Image_Checked.png";
            else
                ((CheckBox)bindable).Image.Source = "Image_Unchecked.png";
        }

        public event EventHandler CheckedChanged;
        private void RaiseCheckedChanged()
        {
            CheckedChanged?.Invoke(this,EventArgs.Empty);
        }

        private Boolean _IsEnabled = true;
        public new Boolean IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                _IsEnabled = value;
                OnPropertyChanged();
                this.Opacity = value ? 1 : .5;
                base.IsEnabled = value;
            }
        }

        public event EventHandler Clicked;
        private void TapGestureOnTapped(object sender,EventArgs eventArgs)
        {
            if(IsEnabled)
            {
                Checked = !Checked;
                Clicked?.Invoke(this,new EventArgs());
            }
        }

        private static void TextValueChanged(BindableObject bindable,object oldValue,object newValue)
        {
            ((CheckBox)bindable).TextContainer.Text = (String)newValue;
        }

        public event EventHandler TextChanged;
        private void RaiseTextChanged()
        {
            TextChanged?.Invoke(this,EventArgs.Empty);
        }

        public Image Image
        {
            get { return ImageContainer; }
            set { ImageContainer = value; }
        }

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty,value);
                OnPropertyChanged();
                RaiseTextChanged();
            }
        }

    }
}

