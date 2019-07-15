using System.Windows;

namespace FileVersion
{
    /// <summary>
    /// FileAsk.xaml 的交互逻辑
    /// </summary>
    public partial class FileAsk : Window
    {
        public int Fmsg { get; internal set; }
        public FileAsk(string v)
        {
            InitializeComponent();
            Filemsg.Text = v;
            revision.Focus();
        }
        private void F_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;//主版本
            Fmsg = 100;
        }
        private void S_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;//主版本
            Fmsg = 10;
        }
        private void L_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;//主版本
            Fmsg = 1;
        }
        private void C_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}