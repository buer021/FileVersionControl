using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms; // NotifyIcon control

namespace FileVersion
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static NotifyIcon trayIcon;
        public readonly static ContextMenu menu = new ContextMenu();
        public readonly static string workPath = Environment.CurrentDirectory + "\\.FileVer_Data";
        public readonly static string sex = "{第";
        public readonly static string eex = "版}";
        public readonly static Regex r = new Regex(sex + @"(?<value>[\s|\S]*)" + eex);//2
        private  void ApplicationStartup(object sender, StartupEventArgs e)
        {
            RemoveTrayIcon();
            AddTrayIcon();
        }
    private void AddTrayIcon()
        {
            if (trayIcon != null)
            {
                return;
            }
            System.Drawing.Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            trayIcon = new NotifyIcon
            {
                Icon = icon,
                Text = "文件版本控制正在后台运行..."
            };
            trayIcon.Visible = true;

            MenuItem showFileVer = new MenuItem();
            MenuItem showTop = new MenuItem();
            MenuItem log = new MenuItem();
            MenuItem miniItem = new MenuItem();
            MenuItem closeItem = new MenuItem();

            showFileVer.Text = "管理器";
            showFileVer.Click += new EventHandler(delegate {
                Version version = Version.GetSingle();
                if (!version.IsVisible)
                {
                    version.Show();
                }
                else
                {
                    version.Rfs_Click(null,null);
                }
            });
            showTop.Text = "√ 置顶";
            showTop.Click += new EventHandler(delegate {
                if (MainWindow.Topmost == true)
                {
                    MainWindow.Topmost = false;
                    showTop.Text = "置顶";
                }
                else
                {
                    MainWindow.Topmost = true;
                    showTop.Text = "√ 置顶";
                }
                ((System.Windows.Controls.ContextMenu)MainWindow.Resources["ContextMenu"]).ItemsSource = null;
                ((System.Windows.Controls.ContextMenu)MainWindow.Resources["ContextMenu"]).ItemsSource = menu.MenuItems;
            });
            log.Text = "日志";
            log.Click += new EventHandler(delegate { System.Windows.MessageBox.Show("开发中！欢迎反馈。"); });
            miniItem.Text = "最小化";
            miniItem.Click += new EventHandler(delegate {
                if (MainWindow.IsVisible == true)
                {
                    MainWindow.Hide();
                    miniItem.Text = "最大化";
                }
                else
                {
                    MainWindow.Show();
                    miniItem.Text = "最小化";
                }
                ((System.Windows.Controls.ContextMenu)MainWindow.Resources["ContextMenu"]).ItemsSource = null;
                ((System.Windows.Controls.ContextMenu)MainWindow.Resources["ContextMenu"]).ItemsSource = menu.MenuItems;
            });
            closeItem.Text = "退出";
            closeItem.Click += new EventHandler(delegate { Shutdown();});

            menu.MenuItems.Add(showFileVer);
            menu.MenuItems.Add(showTop);
            menu.MenuItems.Add(log);
            menu.MenuItems.Add(miniItem);
            menu.MenuItems.Add(closeItem);
            trayIcon.ContextMenu = menu;//设置NotifyIcon的右键弹出菜单
            trayIcon.DoubleClick += new EventHandler(delegate {menu.MenuItems[3].PerformClick();});
        }
        private void TrayIcon_Click(object sender, EventArgs e)
        {
        }
        private void RemoveTrayIcon()
        {
            if (trayIcon != null)
            {
                trayIcon.Visible = false;
                trayIcon.Dispose();
                trayIcon = null;
            }
        }
        private void ApplicationExit(object sender, ExitEventArgs e)
        {
            RemoveTrayIcon();
        }
    }
}