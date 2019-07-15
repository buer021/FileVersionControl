using System.Windows;

namespace FileVersion
{
    /// <summary>
    /// Input.xaml 的交互逻辑
    /// </summary>
    public partial class Input : Window
    {
        public string Tmsg { get; internal set; }
        public Input(string v)
        {
            InitializeComponent();
            tmsg.Text = v;
            tmsg.Focus();
        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            Tmsg = tmsg.Text;
            DialogResult = true;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}