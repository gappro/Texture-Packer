   M S B A M L   `   `   `  ����K  GTexturePackerApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:TexturePackerApp.ViewsTexturePackerApp.Views  5      I)clr-namespace:TexturePackerApp.ViewModelsTexturePackerApp.ViewModels  6A   '    !TexturePackerApp.Views.PackerView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:TexturePackerApp.Views     5      7v)clr-namespace:TexturePackerApp.ViewModels     6A   .��5	      � $	��432��6   $	��Top=�6   $��	10,0,10,0q�60   .+�5
   
   �� 
�������$��3q�6$     �� CornerRadius$  5��68   $��
4,188,4,10q�6I   ��5      �� 5       j� Stretch$
 Fill=�6    �� ImageSource 6,   �� *	Display+5      5      �� 5   
   -   5         ��Name$ SelectButton��5   8   $
��15 5q�6O   $��Select Files��6^   $��Right=�6u   $��	0,18,10,0q�6�   $	��Top=�6�   $��69��6�   $	��156��6�   $
��Bold"�6�   ���	#FF547AFF
���������6   �� *SelectFilesCommand+6�   �� 5   
   $��Resolution of Atlas��6   $
��Left=�6.   $��4,44,0,0q�6I   $	��Top=�6[   $	��125��6s   $
��Bold"�6   �� 5   
   -   5         ��Name$ 
Resolution��5   =   $
��Left=�6Q   $��	10,70,0,0q�6l   $	��Top=�6   $	��123��6�   ��6   �� *AtResolution+ �� Mode$ TwoWay=�6�   �� 5      $
��1024��6   $
��Left=�6;   $��	10,93,0,0q�6V   $	��Top=�6i   
����������	#FF547AFF$��0,1,1,1q�6�   ��6$   �� *K1+6�   �� 5      $
��2048��6   $
��Left=�6<   $��	40,93,0,0q�6W   $	��Top=�6j   
����������	#FF547AFF��6$   �� *K2+6�   �� 5      $
��3072��6   $
��Left=�6<   $��	71,93,0,0q�6W   $	��Top=�6j   
����������	#FF547AFF��6$   �� *K3+6�   �� 5      $
��4096��6   $
��Left=�6<   $��
102,93,0,0q�6W   $	��Top=�6k   
����������	#FF547AFF��6$   �� *K4+6�   �� 5   
   $��	File Name��6   $
��Left=�6$   $��4,0,0,0q�6?   $	��Top=�6P   $
��Bold"�6h   �� 5   
   -   5        $ FileName��5   5   $
��Left=�6G   $��	10,26,0,0q�6b   $	��Top=�6u   $	��123��6�   ��6   �� *File+$ TwoWay=�6�   �� 5   
   $��Horizontal Limit��6   $
��Left=�6,   $��	4,114,0,0q�6G   $	��Top=�6Z   $	��125��6r   $
��Bold"�6~   �� 5   
   -   5        $ Limiter��5   9   $
��Left=�6J   $��
10,140,0,0q�6e   $	��Top=�6y   $	��123��6�   ��6   �� *
RowLimit+$ TwoWay=�6�   �� 5    
   $��Override��6   $
��Left=�6&   $��
10,159,0,0q�6A   $	��Top=�6U   $
��Bold"�6�   ���	#FF80CDFF
���   �
���   � d� 	IsChecked 6m   �� *
Override+6�   5"      5#      