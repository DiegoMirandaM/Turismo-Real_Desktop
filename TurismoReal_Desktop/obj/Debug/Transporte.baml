   M S B A M L   `   `   `  ����N  JTurismoReal_Desktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:TurismoReal_DesktopTurismoReal_Desktop  5   	   $    TurismoReal_Desktop.Transporte   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f:Control,http://metro.mahapps.com/winfx/xaml/controls  5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:TurismoReal_Desktop     5   	   6mah,http://metro.mahapps.com/winfx/xaml/controls  5   	   Z VFontAwesome.WPF, Version=4.7.0.37774, Culture=neutral, PublicKeyToken=0758b07a11a4f466-fa$http://schemas.fontawesome.io/icons/  5	   	     � WindowStartupLocation$  CenterScreen=�5   	    � 
ResizeMode$ CanMinimize=�6.    � Title$  Transportes - Turismo Real��5   	   
���   �$	��700��6@   $	��800��6M   (  "MahApps.Metro.Controls.MetroWindow   TitleCharacterCasing$ Normal=�6Y      WindowTitleBrush �#fe7a4a.��5      �  � ShowGridLines .   � ColumnDefinitions 5   
   �� 5      $��0.18* �6   �� 5      $��* �6   �� 5      $
��0.1* �6   �� 5      $��* �6   �� 5      $
��0.1* �6    � RowDefinitions 5   
   �� 5      $��0.14* �6   �� 5      $
��0.1* �6   �� 5      $
��0.4* �6   �� 5      $��0.05* �6   �� 5      $
��0.1* �6   �� 5      $
��0.7* �6   �� 5      $��0.03* �6   �� 5      $��0.15* �6   �� 5       $��0.05* �6   .+�5$   
   �� -   5        $��btn_retroceder��5$   v       MahApps.Styles.Button.Circle#���   6   $��0��6H   $��0��6X   $��2��6e   $��55��6�   $��55��6�   $��Center=�6�   $
��Left=�6�   .��5%      "  FontAwesome.WPF.ImageAwesome     Icon$ SignOut=�6   $��40��6-   	   Rotation$		 180Q�68   
   
Foreground
 �#fe7a4a6G   5&      �� 5)   
   $��1��6   $��0��6    $��3��6-   $	��Top=�6A   $
��Left=�6Y   $!��Planificación de traslados��6u   $��35(�6�   ���#fe7a4a$
��Bold"�6�   �� 5*   
   $��1��6   $��1��6    $��2��6-   $��Center=�6A   $
��Left=�6\   $6��0Por favor, seleccione solicitud de transporte...��6w   $��DemiBold"�6�   $��18(�6�   ���#04aba36�   &   System.Windows.Controls.DataGrid  5,   
   -   5        $��dg_listaSolicitudes��5,      $��2��6.   $��1��6;   $��3��6K   $
��Left=�6_      GridLinesVisibility$	 All=�6z      CanUserAddRows .     CanUserDeleteRows .     AutoGenerateColumns .     CanUserResizeRows .  ���	#FFD4E8E6   
IsReadOnly .    Columns 5-      0  *System.Windows.Controls.DataGridTextColumn  5.      ,  &System.Windows.Controls.DataGridColumn   Header$ Cliente��6D      Width5  /System.Windows.Controls.DataGridLengthConverter$ 0.75* 6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *
nomCliente+6U     5/      $ Departamento��6B   $
 0.9* 6X    6%   �� *
nomDepto+6X     50      $ 	Pasajeros��6C   $ 0.65* 6V    6%   �� *	PASAJEROS+6V     51      $ Inicio arriendo��6j   $
 0.9* 6�    6%   �� *FECHA_INICIO_ARRIENDO+ �� StringFormat$ 
dd/MM/yyyy��6�     52      $ Costo��6V   $
 0.7* 6e    6%   �� *COSTO+$ {0:c0}��6e   4  .System.Windows.Controls.DataGridCheckBoxColumn  53      $ Aceptada��6K   $ 0.62* 6]    6)   �� *bool_aceptada+6]     54      $ 	Rechazada��6L   $ 0.69* 6_    6)   �� *bool_cancelada+6_   56      �� 58   
   $��1��6   $��4��6!   $��2��6.   
������ ���#04aba3$��2q�6n    �� CornerRadius$ 5��6�   ._�59      ��  �� Orientation$ Vertical=�6   $��20,40,20,10q�60   .+�5;      �� $ 
Horizontal=�6   $��0,0,0,7q�66   .+�5<      �� $��Dirección de origen:��6   �� 5=      -   5        $��tb_dirOrigen_ida��5=      
�������$	��150��6M    �� 
IsReadOnly . $��5,0,0,0q�6k   |�.  6|   5>      �� 5@      $ 
Horizontal=�6   $��0,0,0,7q�66   .+�5A      �� $��Dirección de destino:��6   �� 5B      -   5        $��tb_dirDest_ida��5B      
�������$	��150��6K    . |�.  6i   5C      �� 5E      $ 
Horizontal=�6   $��0,0,0,7q�66   .+�5F      �� $��Fecha de ida:��6   �� 5G      -   5        $��	tb_fecIda��5G      
�������$	��150��6F    . $��40,0,0,0q�6d   |�.  6v   5H      �� 5J      $ 
Horizontal=�6   $��0,0,0,7q�66   .+�5K      �� $��Total pasajeros:��6   �� 5L      -   5        $��tb_pasajIda��5L      
�������$	��150��6H    . $��30,0,0,0q�6f   |�.  6x   5M      �� 5O      $ 
Horizontal=�6   $��0,0,0,7q�66   .+�5P      �� $��Descripción vehículo:��6   �� 5Q      -   5        $��tb_descVehIda��5Q      *	  $MahApps.Metro.Controls.TextBoxHelper 	  	Watermark$ Chery Tiggo rojo��63    �� 	MaxLength$	 130��6b   $	��150��6r   5R      �� 5T      $ 
Horizontal=�6   $��70,0,0,7q�66   .+�5U      �� $��Patente:��6   �� 5V      -   5        $��tb_patenteIda��5V      $ AEIOU27��63   $ 7��6Y    �� CharacterCasing$ Upper=�6g   $	��150��6   5W      �� 5Y      $ 
Horizontal=�6   $��0,0,0,7q�66   .+�5Z      �� $��Conductor designado:��6   �� 5[      -	   5        $��cb_conductorIda��5[      $	��150��66    �� DisplayMemberPath$ 	FULL_NAME��6B   $ Nombre conductor��6`    �� SelectedValuePath$ ID_CONDUCTOR��6�   5\      �� 5^      $ 
Horizontal=�6   $��0,0,0,7q�66   .+�5_      �� $��Precio del traslado:��6   �� 5`      -
   5        $��tb_precioIda��5`      
�������$	��150��6I    . $��15,0,0,0q�6g   |�.  6y   5a      5b      5d      �� 5f   
   $��1��6   $��4��6!   ���#04aba3$ 5��6C   �� 5g   
   $��1��6   $��4��6    ���#d5e8e6$��Viaje de ida��6B   $
��Left=�6Y   $��20(�6t   �� 5i   
   $��4��6   $��3��6   $��2��6.   
������ ���#04aba3$��2q�6n   $ 5��6�   ._�5k      �� $ Vertical=�6   $��20,40,20,10q�60   .+�5m      �� $ 
Horizontal=�6   $��0,0,0,7q�66   .+�5n      �� $��Dirección de origen:��6   �� 5o      -   5        $��tb_dirOrigen_vuelta��5o      
�������$	��150��6P    . $��5,0,0,0q�6n   |�.  6   5p      �� 5r      $ 
Horizontal=�6   $��0,0,0,7q�66   .+�5s      �� $��Dirección de destino:��6   �� 5t      -   5        $��tb_dirDest_vuelta��5t      
�������$	��150��6N    . |�.  6l   5u      �� 5w      $ 
Horizontal=�6   $��0,0,0,7q�66   .+�5x      �� $��Fecha de vuelta:��6   �� 5y      -   5        $��tb_fecVuelta��5y      
�������$	��150��6I    . $��27,0,0,0q�6g   |�.  6y   5z      �� 5|      $ 
Horizontal=�6   $��0,0,0,7q�66   .+�5}      �� $��Total pasajeros:��6   �� 5~      -   5        $��tb_pasajVuelta��5~      
�������$	��150��6K    . $��30,0,0,0q�6i   |�.  6{   5      �� 5�      $ 
Horizontal=�6   $��0,0,0,7q�66   .+�5�      �� $��Descripción vehículo:��6   �� 5�      -   5        $��tb_descVehVuelta��5�      $ Mitsubishi L200 blanca��66   $	 130��6k   $	��150��6{   5�      �� 5�      $ 
Horizontal=�6   $��70,0,0,7q�66   .+�5�      �� $��Patente:��6   �� 5�      -   5        $��tb_patenteVuelta��5�      $ 7��66   $ XYZPQ42��6D   $ Upper=�6j   $	��150��6�   5�      �� 5�      $ 
Horizontal=�6   $��0,0,0,7q�66   .+�5�      �� $��Conductor designado:��6   �� 5�      -   5        $��cb_conductorVuelta��5�      $	��150��69   $ 	FULL_NAME��6E   $ Nombre conductor��6c   $ ID_CONDUCTOR��6�   5�      �� 5�      $ 
Horizontal=�6   $��0,0,0,7q�66   .+�5�      �� $��Precio del traslado:��6   �� 5�      -   5        $��tb_precioVuelta��5�      
�������$	��150��6L    . $��15,0,0,0q�6j   |�.  6|   5�      5�      5�      �� 5�   
   $��3��6   $��4��6!   ���#04aba3$ 5��6C   �� 5�   
   $��3��6   $��4��6    ���#d5e8e6$��Viaje de vuelta��6B   $
��Left=�6\   $��20(�6w   (� 5�   
   $��1��6   $��7��6"    (� Stretch$ Uniform=�6/   $��Right=�6A   $��40��6]   $��0,0,15,0q�6l   .��5�      �� -   5        $��btn_rechazarTraslado��5�      $��Rechazar traslado��61   $��Center=�6M   ���	#FF761E1E$	��120��6�   ���	#FFAE4646
�������$��DemiBold"�6�   +
  %MahApps.Metro.Controls.ControlsHelper 
  ContentCharacterCasing$ Normal=�6�   6  5�      (� 5�   
   $��2��6   $��7��6"   $��2��6/   $ Uniform=�6C   $
��Left=�6U   $��40��6p   .��5�      �� -   5        $��btn_actualizarTraslado��5�      $��Actualizar traslado��63   $��Center=�6Q   ���#fe7a4a$	��120��6�   
����������#fe7a4a$��DemiBold"�6�   $ Normal=�6�   6  5�      (� 5�   
   $��3��6   $��7��6"   $ Uniform=�6/   $��Right=�6A   $��40��6]   .��5�      �� -   5          ��Name$  btn_aceptarTraslado��5�      $��Aceptar traslado��62   $��Center=�6M   $	��120��6h   ���#fe7a4a
�������$��DemiBold"�6�   $ Normal=�6�   6�   5�      5�      5�      