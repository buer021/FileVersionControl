using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;


namespace FileVersion
{
    /// <summary>
    /// Version.xaml 的交互逻辑
    /// </summary>
    public partial class Version : Window
    {
        public static Version SingleForm = null;
        public static Version GetSingle()
        {
            if (SingleForm == null || !SingleForm.IsVisible)
            {
                SingleForm = new Version();
            }
            return SingleForm;
        }
        public Version()
        {
            InitializeComponent();
            Rfs_Click(null,null);
        }
        private class NameClass
        {
            public string fileName { set; get; }
        }
        private class ListClass
        {
            public string fileFullName { set; get; }
            public string fileEx { set; get; }
            public string fileDate { set; get; }
            public long fileSize { set; get; }
            public int fileVersion { set; get; }
        }
        private List<NameClass> Read(string path)//文件列表读取
        {
            DirectoryInfo di = new DirectoryInfo(path);
            List<NameClass> tds = new List<NameClass>();
            if (di.Exists)//检查文件夹是否存在
            {
                FileSystemInfo[] syslist = di.GetFileSystemInfos();//获取文件夹系统信息
                foreach (FileSystemInfo items in syslist)//对于每个子项
                {
                    if (items is FileInfo)
                    {
                        if (items.Name.Contains(App.r.Match(items.Name).ToString()) && App.r.Match(items.Name).ToString() != "")//如果包含成对的{}，则
                        {
                            var xname = new NameClass()
                            {
                                fileName = items.Name.Replace(App.r.Match(items.Name).ToString(), "")
                            };
                            tds.Add(xname);
                        }
                        if (App.r.Match(items.Name).ToString() == "")
                        {
                            FileInfo fileInfo = new FileInfo(items.FullName);
                            fileInfo.MoveTo(App.workPath + "\\" + App.sex + "100" + App.eex + items.Name);
                        }
                    }
                }
            }
            //tds.Sort();
            var list_distinct = tds.GroupBy(c => c.fileName).Select(c => c.First()).ToList();//去重
            return list_distinct;
        }
        private List<ListClass> ReRead(string path, string name)//版本列表读取
        {
            DirectoryInfo di = new DirectoryInfo(path);
            List<ListClass> tds = new List<ListClass>();
            if (di.Exists)//检查文件夹是否存在
            {
                FileSystemInfo[] syslist = di.GetFileSystemInfos();//获取文件夹系统信息
                foreach (FileInfo items in syslist)//对于每个子项
                {
                    if (items is FileInfo)//如果是文件
                    {
                        string s = App.r.Match(items.Name).ToString();
                        if (name == (s == "" ? items.Name : items.Name.Replace(s, "")))//如果该文件名去掉版本号后
                        {
                            var xname = new ListClass()
                            {
                                fileFullName = items.FullName,
                                fileEx=items.Extension,
                                fileDate = items.LastWriteTime.ToString(),
                                fileSize = items.Length / 1024,
                                fileVersion = s == "" ? 100 : int.Parse(s.Replace(App.sex, "").Replace(App.eex, ""))//3
                            };
                            tds.Add(xname);
                        }
                    }
                }
            }
            return tds;
        }
        private void FileName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NameClass nameClass = FileName.SelectedItem as NameClass;
            if (nameClass == null)
            {
                FileList.ItemsSource = null;
                return;
            }
            List<ListClass> x = ReRead(App.workPath, nameClass.fileName.ToString());
            ICollectionView dataView = CollectionViewSource.GetDefaultView(x);
            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription("fileVersion", ListSortDirection.Descending);
            dataView.SortDescriptions.Add(sd);
            FileList.ItemsSource = dataView;
            FileList.SelectedIndex = 0;
            msg.Content = "该文件共有“" + x.Count + "”个版本。";
            FileList.Focus();
        }
        public void Rfs_Click(object sender, RoutedEventArgs e)
        {
            int i = FileName.SelectedIndex;
            FileName.ItemsSource = Read(App.workPath);
            FileName.SelectedIndex = i==-1?0:i;
            FileList.Focus();
        }
        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView dataView = CollectionViewSource.GetDefaultView(FileList.ItemsSource);
            dataView.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
        GridViewColumnHeader _lastHeaderClicked = null;
        ListSortDirection _lastDirection = ListSortDirection.Ascending;
        private void Header_Click(object sender, RoutedEventArgs e)
        {
            ListSortDirection direction;
            if (e.OriginalSource is GridViewColumnHeader headerClicked && FileList.ItemsSource != null)
            {
                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != _lastHeaderClicked)
                    {
                        direction = ListSortDirection.Descending;
                    }
                    else
                    {
                        if (_lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }
                    var columnBinding = headerClicked.Column.DisplayMemberBinding as System.Windows.Data.Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;
                    Sort(sortBy, direction);
                    if (direction == ListSortDirection.Ascending)
                    {
                        headerClicked.Column.HeaderTemplate =
                            Resources["ListViewItemStyle"] as DataTemplate;
                    }
                    else
                    {
                        headerClicked.Column.HeaderTemplate =
                            Resources["ListViewItemStyle"] as DataTemplate;
                    }
                    if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                    {
                        _lastHeaderClicked.Column.HeaderTemplate = null;
                    }
                    _lastHeaderClicked = headerClicked;
                    _lastDirection = direction;
                }
            }
        }
        private void FileDel(string filename, string tname, int fileVersion)//文件删除
        {
            FileInfo fileInfo = new FileInfo(filename);
            MessageBoxResult result = System.Windows.MessageBox.Show("你确认删除" + "\n" + "->文" +
                "件：" + tname + "，" + "\n" + "->版本：" + fileVersion + "。\n" + "吗？", "文件删除" +
                "确认框", MessageBoxButton.OKCancel, MessageBoxImage.Asterisk);
            if (result == MessageBoxResult.OK)
            {
                if (fileInfo.Exists)
                {
                    File.Delete(filename.ToString());
                    Rfs_Click(null,null);
                }
                else
                {
                    System.Windows.MessageBox.Show("文件不存在", "删除失败。", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)//文件删除
        {
            NameClass nameClass = FileName.SelectedItem as NameClass;
            ListClass listClass = FileList.SelectedItem as ListClass;
            if (nameClass == null)
            {
                return;
            }
            if (listClass == null)
            {
                System.Windows.MessageBox.Show("未选中版本。", "删除错误。", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            FileDel(listClass.fileFullName, nameClass.fileName, listClass.fileVersion);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)//文件保存
        {
            NameClass nameClass = FileName.SelectedItem as NameClass;
            ListClass listClass = FileList.SelectedItem as ListClass;
            if (listClass == null)
            {
                System.Windows.MessageBox.Show("未选中版本。", "错误");
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "默认保存到系统桌面";
            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            sfd.FileName = nameClass.fileName;
            string ex = sfd.FileName.Substring(sfd.FileName.LastIndexOf("."));
            sfd.Filter = "文件| *" + ex;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    File.Copy(listClass.fileFullName, sfd.FileName);
                }
                catch
                {
                    System.Windows.MessageBox.Show("文件下载失败，该文件夹下已存在该文件！");
                }
            }
        }
        private void FileList_MouseDoubleClick(object sender, MouseButtonEventArgs e)//打开文件
        {
            if (FileList.ItemsSource != null)
            {
                ListClass listClass = FileList.SelectedItem as ListClass;
                try
                {
                    System.Diagnostics.Process.Start(listClass.fileFullName);
                }
                catch
                {
                    System.Windows.MessageBox.Show("不支持的文件类型，请先设置默认打开方式！");
                }
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", App.workPath);
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)//打包
        {
            System.Windows.MessageBox.Show("开发中！欢迎反馈。");
        }
        private void CopyToClipboard(string[] filePath)
        {
            System.Windows.DataObject dataObject = new System.Windows.DataObject();
            dataObject.SetData(System.Windows.DataFormats.FileDrop, filePath);
            System.Windows.Clipboard.SetDataObject(dataObject, true);
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)//剪贴板
        {
            System.Collections.IList listClass = FileList.SelectedItems;
            string[] filePath = new string[listClass.Count];
            int i = 0;
            while (i < listClass.Count)
            {
                filePath[i] = ((ListClass)listClass[i]).fileFullName;
                i += 1;
            }
            CopyToClipboard(filePath);
        }
        private void FileList_Drop(object sender, System.Windows.DragEventArgs e)//添加版本
        {
            string fileNam = ((Array)e.Data.GetData(System.Windows.DataFormats.FileDrop)).GetValue(0).ToString();
            try
            {
                NameClass nameClass = FileName.SelectedItem as NameClass;
                if (nameClass == null)
                {
                    MainWindow.ReName(fileNam, "", true);
                    Rfs_Click(null, null);
                }
                else
                {
                    MessageBoxResult result = System.Windows.MessageBox.Show("你确认将" + "\n" + "->文件：" + fileNam + "，" + "\n" + "->添" +
                    "加到：" + nameClass.fileName + "\n" + "的新版本吗？", "添加文件版本" +
                        "确认框", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        FileAsk fileAsk = new FileAsk(fileNam);
                        if (fileAsk.ShowDialog() == true)
                        {
                            if (fileAsk.Fmsg == 100)
                            {
                                MainWindow.ReName(fileNam, nameClass.fileName, false, 100);
                                Rfs_Click(null, null);
                            }
                            if (fileAsk.Fmsg == 10)
                            {
                                MainWindow.ReName(fileNam, nameClass.fileName, false, 10);
                                Rfs_Click(null, null);
                            }
                            if (fileAsk.Fmsg == 1)
                            {
                                MainWindow.ReName(fileNam, nameClass.fileName, false, 1);
                                Rfs_Click(null, null);
                            }
                        }

                    }
                }
            }
            catch (Exception r)
            {
                System.Windows.MessageBox.Show("文件复制失败，请尝试重命名文件或重启软件。" + r.ToString(), "来自：文件版本管理");
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)//帮助关于
        {
            System.Windows.MessageBox.Show("1，本窗口关闭不影响后台；" + "\n" +
                "2，双击Potato Inside图标可快速打开本窗口；" + "\n" +
                "3，文件可用操作：" + "\n" +
                "->3.1 打开文件（双击或右键）；" +"\n"+
                "->3.2 复制到剪贴板；" + "\n" +
                "->3.3 拖入到右侧添加新版本；" + "\n" +
                "->3.4 删除/重命名；" + "\n" +
                "->3.5 标题排序。" + "\n" + "\n" +
                "Designed by lkjhgfdsa", "来自：文件版本管理");
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)//重命名
        {
            try
            {
                NameClass nameClass = FileName.SelectedItem as NameClass;
                Input input = new Input(nameClass.fileName);
                if (input.ShowDialog()==true)
                {
                    string order = input.Tmsg;
                    if (nameClass.fileName == order) return;
                    if (order.Contains("{第") || order.Contains("版}"))
                    {
                        System.Windows.MessageBox.Show("与已知文件版本识别符冲突，请重新命名！");
                        return;
                    };
                    List<ListClass> x = ReRead(App.workPath, nameClass.fileName.ToString()) as List<ListClass>;
                    foreach (ListClass listClass in x)
                    {
                        string s = (App.r.Match(listClass.fileFullName.Substring(listClass.fileFullName.LastIndexOf("\\") + 1)).ToString());//版本号
                        string oldpath = listClass.fileFullName;
                        string newpath = listClass.fileFullName.Replace(s, "").Replace(nameClass.fileName,order);//截取原来的名字
                        string ex = order.Substring(order.LastIndexOf("."));
                        string newName = newpath.Replace(ex, "") +s + ex;//新文件名
                        File.Move(oldpath, newName);
                    }
                    Rfs_Click(null,null);
                }
            }
            catch (Exception r)
            {
                System.Windows.MessageBox.Show("文件重命名失败" + "\n" +
                    "请尝试重启软件。" + r.ToString(), "文件重命名");
            }
        }
        private void Open_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            FileList_MouseDoubleClick(null,null);
        }
    }
}