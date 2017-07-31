using Prism.Behaviors;
using System.Windows.Input;
using Xamarin.Forms;

namespace FitMeet.Behaviors
{
    public class ItemAppearingBehavior : BehaviorBase<ListView>
    {
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create("Command", typeof(ICommand), typeof(ItemAppearingBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }



        /// <inheritDoc />
        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject.ItemAppearing += OnItemAppearing;
        }

        private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (Command == null || e.Item == null) return;

            if (Command.CanExecute(e.Item))
                Command.Execute(e.Item);
        }

        /// <inheritDoc />
        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            AssociatedObject.ItemAppearing -= OnItemAppearing;
        }

    }
}
