using System;
using System.Windows.Forms;

namespace MemoryLeaks.Example
{
    public partial class EventHandlersLeak : Form
    {
        private const int IterationCount = 1000;

        // as this object is declared as an instance field of form object
        // it's lifetime is directly connected with the life of the form.
        // Due to the fact that this object contains the event inside
        // it will keep references for all subscribed objects.
        private readonly ReferenceKeeper _referenceKeeper;

        public EventHandlersLeak()
        {
            this.InitializeComponent();

            _referenceKeeper = new ReferenceKeeper();
        }

        private void btn_allocate_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < IterationCount; ++i)
            {
                // 'subscription' objects are created just in place and
                // ideally will be gc'd. But they subscribe to the event
                // with no further unsubscription so their lifetime
                // increases and depends on 'referencekeeper'.
                _referenceKeeper.Notifier += new Subscription().PerformOperation;
            }

            _referenceKeeper.Notify();
        }
    }
}
