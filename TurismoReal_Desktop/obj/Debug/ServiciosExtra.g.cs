﻿#pragma checksum "..\..\ServiciosExtra.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "14A735D9766C63D46E856E5EA28BFE13FD5CABE5718858F3CAB7B0A9243769EA"
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
    /// ServiciosExtra
    /// </summary>
    public partial class ServiciosExtra : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 37 "..\..\ServiciosExtra.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_retroceder;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\ServiciosExtra.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dg_servicios;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\ServiciosExtra.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_nombre;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\ServiciosExtra.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_costo;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\ServiciosExtra.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_actualizarServicio;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\ServiciosExtra.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_guardarServicio;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\ServiciosExtra.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_relacionarServicio;
        
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
            System.Uri resourceLocater = new System.Uri("/TurismoReal_Desktop;component/serviciosextra.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ServiciosExtra.xaml"
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
            this.btn_retroceder = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\ServiciosExtra.xaml"
            this.btn_retroceder.Click += new System.Windows.RoutedEventHandler(this.btn_retroceder_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.dg_servicios = ((System.Windows.Controls.DataGrid)(target));
            
            #line 44 "..\..\ServiciosExtra.xaml"
            this.dg_servicios.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.dg_servicios_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tb_nombre = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tb_costo = ((System.Windows.Controls.TextBox)(target));
            
            #line 55 "..\..\ServiciosExtra.xaml"
            this.tb_costo.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tb_costo_TextChanged);
            
            #line default
            #line hidden
            
            #line 55 "..\..\ServiciosExtra.xaml"
            this.tb_costo.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.tb_costo_PreviewTextInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btn_actualizarServicio = ((System.Windows.Controls.Button)(target));
            
            #line 58 "..\..\ServiciosExtra.xaml"
            this.btn_actualizarServicio.Click += new System.Windows.RoutedEventHandler(this.btn_actualizarServicio_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btn_guardarServicio = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\ServiciosExtra.xaml"
            this.btn_guardarServicio.Click += new System.Windows.RoutedEventHandler(this.btn_guardarServicio_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btn_relacionarServicio = ((System.Windows.Controls.Button)(target));
            
            #line 60 "..\..\ServiciosExtra.xaml"
            this.btn_relacionarServicio.Click += new System.Windows.RoutedEventHandler(this.btn_relacionarServicio_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

