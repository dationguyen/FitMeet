using FitMeet.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FitMeet.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage:ContentPage
    {
        public static BindableProperty LastItemProperty = BindableProperty.Create(
            propertyName: "LastItem",
            returnType: typeof(MessageModel),
            declaringType: typeof(ChatPage),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: SelectedIndexChanged);


        public MessageModel LastItem
        {
            get { return (MessageModel)GetValue(LastItemProperty); }
            set
            {
                SetValue(LastItemProperty,value);
                if(value != null)
                {
                    MessageListView.ScrollTo(value,ScrollToPosition.Start,true);
                }

            }
        }

        private static void SelectedIndexChanged(BindableObject bindable,object oldValue,object newValue)
        {
            ((ChatPage)bindable).LastItem = (MessageModel)newValue;
        }

        public ChatPage()
        {
            InitializeComponent();
        }



        private void TapGestureRecognizer_OnTapped(object sender,EventArgs e)
        {
            Input.Unfocus();
        }

        private void Input_OnCompleted(object sender,EventArgs e)
        {
            if(SendButton.Command.CanExecute(null))
                SendButton.Command.Execute(null);
        }
    }
}