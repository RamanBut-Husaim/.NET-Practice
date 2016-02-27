using System;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MemoryLeaks.DynamicAssemblyExplosion
{
    public partial class DynamicAssemblyExplosion : Form
    {
        private const int IterationCount = 1000;

        public DynamicAssemblyExplosion()
        {
            this.InitializeComponent();
        }

        private void btn_explode_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < IterationCount; ++i)
            {
                new XmlSerializer(typeof (SomeCode), new XmlRootAttribute(""));
            }
        }
    }

    public sealed class SomeCode
    {
        public int Some { get; set; }

        public int Code { get; set; }
    }
}
