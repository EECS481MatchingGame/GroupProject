   M S B A M L   `   `   `  ����^  ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers  5   	    
CameraTilt   CameraTilt.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   mmy^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     5   	   -   5          � Title$  
MainWindow��5   	   $	��350��6   $	��650��6)   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��366* �6   �� 5	      $
��262* �6   .+�5   
   �� -   5        $��slider1��5   M   $	��171��6   $
��Left=�6   $��
124,31,0,0q�69   $	��Top=�6\   $��38��6t    �� Orientation$ Vertical=�6    � Maximum$ 27Q�6�    � Minimum$	 -27Q�6�    � SmallChange$ 1Q�6�    �� TickPlacement$ TopLeft=�6�    �� Interval$ 1��6�    �� IsSnapToTickEnabled . $��1��6  �� 5   
   -   5        $��label1��5      $��61��6D   $
��Left=�6P   $��
161,84,0,0q�6k   $	��Top=�6�   $��36(�6�   $��1��6�   ��6   �� 	 �� Path$	 Value�
 �� ElementName$
 slider1��6�   �� 5   
   -   5        $��button1��5   _   $��Set Tilt��6   $��51��6$   $
��Left=�60   $��
362,91,0,0q�6K   $	��Top=�6n   $	��122��6�   $��28(�6�   $��2��6�   =   7Microsoft.Samples.Kinect.WpfViewers.KinectSensorChooser  5   
   -   5        $��kinectSensorChooser1��5   <   $
��Left=�6!   $	��Top=�6X   $	��328��6p   $��	19,27,0,0q�6|   �� 5   
   -   5        $��label2��5   s   $��Current Angle:��6   $��36(�6)   $��61��67   $
��Left=�6C   $��282,214,0,0q�6^   $	��Top=�6�   $��2��6�   �� 5   
   -   5        $��lblCurrentAngle��5   Z   $��36(�6   $��61��6   $
��Left=�6*   $��161,214,0,0q�6E   $	��Top=�6q   $	��...��6�   $	��123��6�   $��1��6�   5      5      