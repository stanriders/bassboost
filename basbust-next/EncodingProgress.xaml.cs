using System.Windows.Controls;

namespace basbust_next
{
    /// <summary>
    /// Interaction logic for EncodingProgress.xaml
    /// </summary>
    public partial class EncodingProgress : UserControl
    {
        public EncodingProgress()
        {
            InitializeComponent();
        }

        public void UpdateProgress(double progress)
        {
            progressBar.Value = progress;
        }
    }
}
