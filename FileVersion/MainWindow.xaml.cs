using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace FileVersion
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {   
            if (!Directory.Exists(App.workPath))
            {
                MessageBox.Show("" + "\n" +
                    "本软件所备份的文件均存储在：本软件所在目录的子文件夹\".FileVerData\"下。" + "\n" +
                    "使用说明：" + "\n" +
                    "1，拖入文件到桌面图标自动备份；" + "\n" +
                    "2，尝试鼠标左、右键点击任一“Potato Inside”图标交互；" + "\n" +
                    "3，按住Alt键，拖动本软件可创建快捷方式。" +
                    "4，本软件小窗口拖入：自动按时间标记版本号" +
                    "5，管理器窗口拖入：手动标记版本号", "感谢使用文件多版本管理器！本软件遵循GPL协议。Potato Inside",MessageBoxButton.OK,MessageBoxImage.Information);
                try
                {
                    Directory.CreateDirectory(App.workPath);
                }
                catch
                {
                    MessageBox.Show("软件对该目录无操作权限！解决办法：1，将该本程序移至其他非系统盘任意目录；2，提交BUG。");
                    Close();
                }
            }
            InitializeComponent();
            ((ContextMenu)Resources["ContextMenu"]).ItemsSource = App.menu.MenuItems as System.Collections.IEnumerable;
            Left = 1100;
            Top = 100;
        }
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ClickCount == 1)
                {
                    DragMove();
                }
                if (e.ClickCount >= 2)
                {
                    Version version = Version.GetSingle();
                    if (!version.IsVisible)
                    {
                        version.Show();
                    }
                    else
                    {
                        version.Rfs_Click(null, null);
                    }
                }
            }
            catch
            {
                MessageBox.Show("文件夹内存在文件名称与已知版本识别符冲突的文件，" +"\n"+
                    "本软件通过识别名称中的”{第“和”版}“之间的数字来识别版本。因此：" + "\n" + "\n" +
                    "->请开存储的文件夹，查看单个文件名称中除了版本号两侧的”{第“和”版}“" +
                    "是否存在其它的”{第“和”版}“，如有请删除再打开软件即可。" ,"严重错误！",MessageBoxButton.OKCancel,MessageBoxImage.Error);
            }
        }
        private static int ReadVersion(string name)//读取输入文件的最新版本，返回版本号
        {
            DirectoryInfo di = new DirectoryInfo(App.workPath);
            List<int> version = new List<int>();
            if (di.Exists)//检查文件夹是否存在
            {
                FileSystemInfo[] syslist = di.GetFileSystemInfos();//获取文件夹系统信息
                foreach (FileSystemInfo items in syslist)//对于每个子项
                {
                    if (items is FileInfo)
                    {
                        string s = App.r.Match(items.Name).ToString();
                        if (name == items.Name.Replace(s,""))//如果该文件名去掉版本号后
                        {
                            version.Add(Convert.ToInt32(s == "" ? "100" : s.Replace(App.sex, "").Replace(App.eex,"")));//2
                        }
                    }
                }
            }
            version.Sort((x, y) => -x.CompareTo(y));
            return version.Count == 0 ? 99: version[0];
        }
        public static void ReName(string xpath,string New_name,bool re)//给定文件名，传入新版本
        {
            FileInfo di = new FileInfo(xpath);
            int index = xpath.LastIndexOf('\\');
            string fname = xpath.Substring(index + 1);//获取输入的完整文件名

            int newVersion = ReadVersion(re ? fname : New_name);//如果是首页则输入则查输入名字，反则查选中名字
            if (newVersion == 99)
            {
                di.CopyTo(App.workPath + "\\" + fname.Substring(0, fname.LastIndexOf(".")) +
                App.sex + "100" + App.eex + fname.Substring(fname.LastIndexOf(".")));
            }
            else
            {
                DateTime newTime = new FileInfo(xpath).LastWriteTime;
                string oldxpath = App.workPath + "\\"+ fname.Substring(0, fname.LastIndexOf(".")) +
                    App.sex + newVersion + App.eex + fname.Substring(fname.LastIndexOf("."));
                DateTime oldTime = new FileInfo(oldxpath).LastWriteTime;
                TimeSpan diff = newTime - oldTime;
                if (diff.TotalHours <= 24)
                {
                    di.CopyTo(re ? App.workPath + "\\" + fname.Substring(0, fname.LastIndexOf(".")) +
                        App.sex + (newVersion+1).ToString() + App.eex + fname.Substring(fname.LastIndexOf(".")) :
                        App.workPath + "\\" + New_name.Substring(0, New_name.LastIndexOf(".")) +
                        App.sex + (newVersion + 1).ToString() + App.eex + New_name.Substring(New_name.LastIndexOf(".")));//重命名
                }
                if (diff.TotalHours > 24 && diff.TotalHours <= 48)
                {
                    di.CopyTo(re ? App.workPath + "\\" + fname.Substring(0, fname.LastIndexOf(".")) +
                         App.sex + (newVersion + 10).ToString() + App.eex + fname.Substring(fname.LastIndexOf(".")) :
                         App.workPath + "\\" + New_name.Substring(0, New_name.LastIndexOf(".")) +
                         App.sex + (newVersion + 10).ToString() + App.eex + New_name.Substring(New_name.LastIndexOf(".")));//重命名
                }
                if (48 < diff.TotalHours&&diff.TotalHours<72)
                {
                    di.CopyTo(re ? App.workPath + "\\" + fname.Substring(0, fname.LastIndexOf(".")) +
                         App.sex + (newVersion + 100).ToString() + App.eex + fname.Substring(fname.LastIndexOf(".")) :
                         App.workPath + "\\" + New_name.Substring(0, New_name.LastIndexOf(".")) +
                         App.sex + (newVersion + 100).ToString() + App.eex + New_name.Substring(New_name.LastIndexOf(".")));//重命名
                }
                if (72 < diff.TotalHours)
                {
                    di.CopyTo(re ? App.workPath + "\\" + fname.Substring(0, fname.LastIndexOf(".")) +
                         App.sex + (newVersion + 1000).ToString() + App.eex + fname.Substring(fname.LastIndexOf(".")) :
                         App.workPath + "\\" + New_name.Substring(0, New_name.LastIndexOf(".")) +
                         App.sex + (newVersion + 1000).ToString() + App.eex + New_name.Substring(New_name.LastIndexOf(".")));//重命名
                }
            }
        }
        public static void OP_ReName(string xpath, string New_name, bool re, int i = 1)//给定文件名，传入新版本
        {
            FileInfo di = new FileInfo(xpath);
            int index = xpath.LastIndexOf('\\');
            string fname = xpath.Substring(index + 1);//获取输入的完整文件名
            int newVersion = ReadVersion(re ? fname : New_name) + i;//子项版本号已经自增，如果是首页则输入则查输入名字，反则查选中名字
            di.CopyTo(re ? App.workPath + "\\" + fname.Substring(0, fname.LastIndexOf(".")) + App.sex + newVersion + App.eex + fname.Substring(fname.LastIndexOf(".")) : App.workPath + "\\" + New_name.Substring(0, New_name.LastIndexOf(".")) + App.sex + newVersion + App.eex + New_name.Substring(New_name.LastIndexOf(".")));//重命名
        }
        public void Window_Drop(object sender, DragEventArgs e)
        {
            try
            {
                string fileNam = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                ReName(fileNam,"",true);
            }
            catch(Exception r)
            {
                MessageBox.Show("文件拷贝失败，请尝试重命名文件或重启软件。"+r.ToString(), "来自：文件版本管理");
            }
        }
        private void Window_DragEnter(object sender, DragEventArgs e)
        {
            string filePath = ((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            FileInfo fInfor = new FileInfo(filePath);
            if (fInfor.Attributes == FileAttributes.Directory || fInfor.Name.Split('.')[0].Contains(App.sex) || fInfor.Name.Split('.')[0].Contains(App.eex))//文件夹
            {
                img.Visibility = Visibility.Hidden;
                xex.Visibility = Visibility.Visible;
                return;
            }
            else//文件
            {
                img.Visibility = Visibility.Hidden;
                mex.Visibility = Visibility.Visible;
            }
        }
        private void Window_DragLeave(object sender, DragEventArgs e)
        {
            img.Visibility = Visibility.Visible;
            mex.Visibility = Visibility.Hidden;
            xex.Visibility = Visibility.Hidden;
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            int a = ((System.Windows.Forms.MenuItem)((MenuItem)sender).Header).Index;
            switch (a)
            {
                case 0:
                    App.menu.MenuItems[0].PerformClick();//管理器
                    break;
                case 1:
                    App.menu.MenuItems[1].PerformClick();//置顶
                    break;
                case 2:
                    App.menu.MenuItems[2].PerformClick();//日志
                    break;
                case 3:
                    App.menu.MenuItems[3].PerformClick();//最小化
                    break;
                case 4:
                    App.menu.MenuItems[4].PerformClick();//退出
                    break;
            }
            ((ContextMenu)Resources["ContextMenu"]).ItemsSource = null;
            ((ContextMenu)Resources["ContextMenu"]).ItemsSource = App.menu.MenuItems as System.Collections.IEnumerable;
        }
    }
}