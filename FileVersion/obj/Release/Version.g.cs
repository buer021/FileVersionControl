﻿#pragma checksum "..\..\Version.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0407337415641213391E5D245E15BE8F633BD868D8531CB4A04B5E6545C20812"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace FileVersion {
    
    
    /// <summary>
    /// Version
    /// </summary>
    public partial class Version : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\Version.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView FileName;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\Version.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView FileList;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Version.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridView gid;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\Version.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button about;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\Version.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button pack;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\Version.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button open;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\Version.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button del;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\Version.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button save;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\Version.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button rename;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\Version.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button refr;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\Version.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label msg;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FileVersion;component/version.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Version.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.FileName = ((System.Windows.Controls.ListView)(target));
            
            #line 16 "..\..\Version.xaml"
            this.FileName.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.FileName_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 24 "..\..\Version.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_5);
            
            #line default
            #line hidden
            return;
            case 3:
            this.FileList = ((System.Windows.Controls.ListView)(target));
            
            #line 28 "..\..\Version.xaml"
            this.FileList.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new System.Windows.RoutedEventHandler(this.Header_Click));
            
            #line default
            #line hidden
            
            #line 30 "..\..\Version.xaml"
            this.FileList.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.FileList_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 30 "..\..\Version.xaml"
            this.FileList.Drop += new System.Windows.DragEventHandler(this.FileList_Drop);
            
            #line default
            #line hidden
            return;
            case 4:
            this.gid = ((System.Windows.Controls.GridView)(target));
            return;
            case 5:
            
            #line 40 "..\..\Version.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 41 "..\..\Version.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Open_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 42 "..\..\Version.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.about = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\Version.xaml"
            this.about.Click += new System.Windows.RoutedEventHandler(this.Button_Click_4);
            
            #line default
            #line hidden
            return;
            case 9:
            this.pack = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\Version.xaml"
            this.pack.Click += new System.Windows.RoutedEventHandler(this.Button_Click_3);
            
            #line default
            #line hidden
            return;
            case 10:
            this.open = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\Version.xaml"
            this.open.Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 11:
            this.del = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\Version.xaml"
            this.del.Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.save = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\Version.xaml"
            this.save.Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 13:
            this.rename = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\Version.xaml"
            this.rename.Click += new System.Windows.RoutedEventHandler(this.Button_Click_5);
            
            #line default
            #line hidden
            return;
            case 14:
            this.refr = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\Version.xaml"
            this.refr.Click += new System.Windows.RoutedEventHandler(this.Rfs_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            this.msg = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

