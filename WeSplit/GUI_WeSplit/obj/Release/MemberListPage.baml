   M S B A M L   `   `   `  ���� F  BGUI_WeSplit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:GUI_WeSplitGUI_WeSplit       GUI_WeSplit.MemberListPage   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     +localclr-namespace:GUI_WeSplit     -     N� Title$  MemberListPage�� � 	Resources    	NameStyle �� 
TargetType �� Property �� Value  	InfoStyle��%�   &	        &	 Y     �� " ��.���  � ��$ 20(���  � ��$ 
Bree Serif)���  � ��
 ������� " ��.���  � ��$ 25(���  � ��$ 
Bree Serif)���  � �� �#0cebeb��  � ��$ Center=���  � ��$ Center=�.���� -    ��Name$ 
MemberList��
������ $��Stretch=�$��Hidden=�$��50��$��50��$��0q����� .R��� .+�9� 
����������� ���� ���� .R�� 
����d�����  �� Path$ HorizontalAlignment� �� RelativeSource �� *FindAncestor+ �� AncestorType" ��	 �� Mode$	 OneWayToSource=�.+�]� $��0 5 0 0q�.+��� 
 ��Name$
 MemberNameTextBlock��$��1��$��Center=�$��Center=�#���   ���� *
MemberName+���� .R��  � ColumnDefinitions �� $��* ��� $��7* � � RowDefinitions �� $��* ��� $��* �.+���  ��Name$ MemberAvatarBorder��$	��250��$��50��$	��200��$��50�� �� CornerRadius$ 15��$��5q�$��0��$��2������  �� 
StartPoint$	 0,0&� �� EndPoint$	 1,1&�.���  � Color$ #91EAE4�� � Offset$	 0.0Q�� $ #20e3b2��$
 0.25Q�� $ #0cebeb��$
 0.75Q����� MemberAvatarImage�� �� BitmapScalingMode$ HighQuality=� j� Stretch$ UniformToFill=� �� ImageSource �� *MemberAvatar+�� $��0��$��1��
���������� *
MemberName+�� $
 MemberDOBTextBlock��$��1��$��1������ *	MemberDOB+