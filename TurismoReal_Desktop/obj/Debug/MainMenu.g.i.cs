﻿#pragma checksum "..\..\MainMenu.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0589841B80BF23EC80328E6E78D3ED8633F62949CDDF94A17D862E476C616E56"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using FontAwesome.WPF;
using FontAwesome.WPF.Converters;
using MahApps.Metro;
using MahApps.Metro.Accessibility;
using MahApps.Metro.Actions;
using MahApps.Metro.Automation.Peers;
using MahApps.Metro.Behaviors;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Converters;
using MahApps.Metro.Markup;
using MahApps.Metro.Theming;
using MahApps.Metro.ValueBoxes;
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
using TurismoReal_Desktop;


namespace TurismoReal_Desktop {
    
    
    /// <summary>
    /// MainMenu
    /// </summary>
    public partial class MainMenu : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 35 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_cerrarSesion;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Tile btn_transporte;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Tile btn_winUsuarios;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MahApps.Metro.Controls.Tile btn_winServiciosExtra;
        
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
            System.Uri resourceLocater = new System.Uri("/TurismoReal_Desktop;component/mainmenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainMenu.xaml"
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
            this.btn_cerrarSesion = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\MainMenu.xaml"
            this.btn_cerrarSesion.Click += new System.Windows.RoutedEventHandler(this.btn_cerrarSesion_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 44 "..\..\MainMenu.xaml"
            ((MahApps.Metro.Controls.Tile)(target)).Click += new System.Windows.RoutedEventHandler(this.btn_winDpto_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btn_transporte = ((MahApps.Metro.Controls.Tile)(target));
            
            #line 50 "..\..\MainMenu.xaml"
            this.btn_transporte.Click += new System.Windows.RoutedEventHandler(this.btn_transporte_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btn_winUsuarios = ((MahApps.Metro.Controls.Tile)(target));
            
            #line 62 "..\..\MainMenu.xaml"
            this.btn_winUsuarios.Click += new System.Windows.RoutedEventHandler(this.btn_winUsuarios_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btn_winServiciosExtra = ((MahApps.Metro.Controls.Tile)(target));
            
            #line 68 "..\..\MainMenu.xaml"
            this.btn_winServiciosExtra.Click += new System.Windows.RoutedEventHandler(this.btn_winServiciosExtra_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
