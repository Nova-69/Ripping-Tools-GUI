﻿#pragma checksum "..\..\DDSConvPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "ECFFB8F55CF9A919A1619ABA4F4E3C2C62158505C3E8E0CE123DD60129FA22A3"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
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
using TestWFP;


namespace TestWFP {
    
    
    /// <summary>
    /// DDSConvPage
    /// </summary>
    public partial class DDSConvPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\DDSConvPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSelectDDS;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\DDSConvPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFixForcesDDS;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\DDSConvPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnConvDDS;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\DDSConvPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox listBox;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\DDSConvPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFixStreaming;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\DDSConvPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSelectStreamingPath;
        
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
            System.Uri resourceLocater = new System.Uri("/TestWFP;component/ddsconvpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DDSConvPage.xaml"
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
            this.btnSelectDDS = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\DDSConvPage.xaml"
            this.btnSelectDDS.Click += new System.Windows.RoutedEventHandler(this.btnSelectDDS_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnFixForcesDDS = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\DDSConvPage.xaml"
            this.btnFixForcesDDS.Click += new System.Windows.RoutedEventHandler(this.btnFixForcesDDS_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnConvDDS = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\DDSConvPage.xaml"
            this.btnConvDDS.Click += new System.Windows.RoutedEventHandler(this.btnConvDDS_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.listBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.btnFixStreaming = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\DDSConvPage.xaml"
            this.btnFixStreaming.Click += new System.Windows.RoutedEventHandler(this.btnFixStreaming_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnSelectStreamingPath = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\DDSConvPage.xaml"
            this.btnSelectStreamingPath.Click += new System.Windows.RoutedEventHandler(this.btnSelectStreamingPath_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

