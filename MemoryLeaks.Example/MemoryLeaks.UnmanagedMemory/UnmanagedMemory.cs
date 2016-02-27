using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MemoryLeaks.UnmanagedMemory
{
    public partial class UnmanagedMemory : Form
    {
        private const int IterationCount = 1000;

        private readonly IList<Bitmap> _images;

        public UnmanagedMemory()
        {
            this.InitializeComponent();

            _images = new List<Bitmap>();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < IterationCount; ++i)
            {
                _images.Add(new Bitmap("lena_bw.bmp"));
            }
        }
    }
}
