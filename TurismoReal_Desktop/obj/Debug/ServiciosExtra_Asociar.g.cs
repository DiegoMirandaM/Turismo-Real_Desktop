﻿#pragma checksum "..\..\ServiciosExtra_Asociar.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "040A65C99F3CB9F94FDE56D5E5DAFDA8C5AB83C47ADE1C47AC225D6DBE7C04EF"
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
    /// ServiciosExtra_Asociar
    /// </summary>
    public partial class ServiciosExtra_Asociar : MahApps.Metro.Controls.MetroWindow, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\ServiciosExtra_Asociar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RowDefinition rd_datagrid;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\ServiciosExtra_Asociar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_retroceder;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\ServiciosExtra_Asociar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lb_selectedServicio;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\ServiciosExtra_Asociar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dg_relacionDptos;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\ServiciosExtra_Asociar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_cancelar;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\ServiciosExtra_Asociar.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_guardarCambios;
        
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
            System.Uri resourceLocater = new System.Uri("/TurismoReal_Desktop;component/serviciosextra_asociar.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ServiciosExtra_Asociar.xaml"
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
            this.rd_datagrid = ((System.Windows.Controls.RowDefinition)(target));
            return;
            case 2:
            this.btn_retroceder = ((System.Windows.Controls.Button)(target));
            
            #line 37 "..\..\ServiciosExtra_Asociar.xaml"
            this.btn_retroceder.Click += new System.Windows.RoutedEventHandler(this.btn_retroceder_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lb_selectedServicio = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.dg_relacionDptos = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            this.btn_cancelar = ((System.Windows.Controls.Button)(target));
            
            #line 65 "..\..\ServiciosExtra_Asociar.xaml"
            this.btn_cancelar.Click += new System.Windows.RoutedEventHandler(this.btn_retroceder_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btn_guardarCambios = ((System.Windows.Controls.Button)(target));
            
            #line 69 "..\..\ServiciosExtra_Asociar.xaml"
            this.btn_guardarCambios.Click += new System.Windows.RoutedEventHandler(this.Btn_guardarCambios_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

