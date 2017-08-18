using Prism.Events;

namespace FitMeet.EventAggregator
{
    public class UpdateFilterEvent:PubSubEvent<UpdateFilterEventArgs>
    {
    }

    public class ChangeTabbedEvent:PubSubEvent<int>
    {
    }
}
