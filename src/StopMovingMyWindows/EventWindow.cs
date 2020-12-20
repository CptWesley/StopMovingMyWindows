using System.Drawing;
using System.Windows.Forms;

namespace StopMovingMyWindows
{
    /// <summary>
    /// Window that catches all events.
    /// </summary>
    /// <seealso cref="Form" />
    internal sealed class EventWindow : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EventWindow"/> class.
        /// </summary>
        public EventWindow()
        {
            DeviceEvents.RegisterDeviceNotification(this.Handle);
            Visible = false;
            InitializeComponent();
        }

        /// <inheritdoc/>
        protected override void SetVisibleCore(bool value)
            => base.SetVisibleCore(Visible);

        /// <inheritdoc/>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == DeviceEvents.WmDevicechange)
            {
                switch ((int)m.WParam)
                {
                    case DeviceEvents.DbtDeviceArrival:
                        WindowRestorer.RestoreWindowStates();
                        break;
                    case DeviceEvents.DbtDeviceRemoveComplete:
                        WindowRestorer.SaveWindowStates();
                        break;
                }
            }
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            ClientSize = new Size(1, 1);
            Name = "StopMovingMyWindows";
            Text = Name;
            ResumeLayout(false);
        }
    }
}
