   M S B A M L   `   `   `  ����C  ?Pivodeck, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PivodeckPivodeck  5   	   #clr-namespace:PivodeckPivodeck  5   	       Pivodeck.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Pivodeck     5   	   %UIclr-namespace:Pivodeck     5   	   -   5          � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)    � ShowInTaskbar .   � WindowStyle$
 None=�5   	    � AllowsTransparency .  � 
ResizeMode$ NoResize=�5   	   
������ $y�Visible=�5   	    � Topmost . $��1q�5   	    � 	Resources 5         gridFadeInStoryBoard �� 
TargetName g� 	BeginTime	 �� TargetProperty
 W� From	 W� To g� AutoReverse g� Duration  gridFadeOutStoryBoard��%X  &	        &	 �     �� 5   
   .��5      W� $ 
uiGridMain��6   $ 0:0:0.1X�6A   $	 Opacity�5      $

 0.00X�65   $
 1.00X�6A    .  $ 0:0:0.3D�6_   5      �� 5   
   .��5      W� $ 
uiGridMain��6   $ 0:0:0.5X�6A   $	 Opacity�5      $

 1.00X�65   $
 0.00X�6A    .  $ 0:0:0.3D�6_   5      5      .��5      ��  �� CornerRadius$ 5��6   ._�5   
   � -   5         �Name$ 
uiGridMain��5      $��10q�6#   .+�5      �� $
��Auto��6   $
��Auto��6"     MainGridBorder#��[ 60   5       � 5!      $��5,0,12,0q�6    � RowDefinitions 5"      �� 5#      $��0 �6$   �� 5$      $��18 �6$    � ColumnDefinitions 5&      �� 5'      $��* �6'   �� 5(      $��36 �6'   �� 5)      $��42 �6'   .+�5.          Pivodeck.SlickToggleButton  -   5          Name$ CloseButton��5.   >       Pivodeck.SlickToggleButton   Click$ CloseButtonClick��6�      CornerRadius$ 0,0,4,0��6'     SlickButton#��[ 6S      HighlightBackground$	 Red��6x   $��X��6�   $��1��6�   $
��Bold"�6�   ���	#FFFFFFFF$��2��6�   5/      � 50       51      �� 52      $��44 �6$   �� 53      $��* �6$   �� 54      $��44 �6$   .+�56      "   Pivodeck.HorizontalSeparator  $��1��6T     57      $��5��6T   �� 58      -   5         ��Name$ 
TitleLabel��58   /   $��5,3.395,79,15q�6   
������� J� Opacity$	 0.9Q�6V   $��Pivodeck��6d   � 59      $��1��6   $��5q�6$    5:      �� 5;      $��20 �6+   �� 5<      $
��Auto �6+   �� 5=      $��* �6+    5?      �� 5@      $��20 �6(   �� 5A      $
��Auto �6(   �� 5B      $
��Auto �6(   �� 5C      $
��Auto �6(   �� 5D      $��23 �6(   �� 5E      $	��54* �6(   .+�5G      �� -   5        $��lblAcao��5G   F   $
��Bold"�6    $��3��62   O que aconteceu...6i   �� 5H      $��1��6!    �� Orientation$ 
Horizontal=�6.   .+�5I      �� -   5        $��lblTipo��5I       $��16��6/   6<   5J      �� 5K      $��2��6!   $ 
Horizontal=�6.   $��3��6G   .+�5L      �� $
��Bold"�6$   	Labels:6@   �� 5M      -   5        $��	lblLabels��5M   1   $	��5,0q�6$   	Labels:6K   5N      �� 5O      $��3��6!   $ 
Horizontal=�6.   $��3��6G   .+�5P      �� $
��Bold"�6$   Descrição:6C   �� 5Q      -   5        $��lblDescricao��5Q   1   $	��5,0q�6$   Descrição:6Q   5R      �� 5S      $��4��6!   $ 
Horizontal=�6.   $��3��6G   .+�5T      �� $
��Bold"�6$   Tasks:6?   �� 5U      -	   5        $��lblTask��5U   1   $	��5,0q�6$   Tasks:6H   5V      �� 5W      $��5��6!   $ 
Horizontal=�6.   $��3��6G   .+�5X      �� $
��Bold"�6$   Comentarios:6E   �� 5Y      -
   5        $��lblComentarios��5Y   1   $	��5,0q�6$   Comentarios:6U   5Z      �� 5[      $��1��6!   $ 
Horizontal=�6.   $��1��6G   .+�5\      �� -   5        $��	lblPontos��5\   1   $	��2,0q�6$   16E   5]      �� 5^      $��1��6!   $ 
Horizontal=�6.   $��2��6G   .+�5_      �� -   5        $��lblNome��5_   1   $	��5,0q�6$   	Nome aqui6K   5`      5a      �� 5b      $��LeftToRight=�6   $ 
Horizontal=�69   $��5,5,5,5q�6R   $��2��6c   .+�5c      �� $��Center=�6     StealthButton#��[ 6:   $
��auto��6a   .��6n   Abrir Projeto6}   �� 5d      -   5        $��btnCreateTask��5d      $��Center=�62   #��[ 6O   $
��auto��6v   .��6�   
Criar Task6�   �� 5e      -   5        $��Center=�5e      #��[ 6:   $
��auto��6a   .��6�   Configuração6�   5f      5g      5h      5i      5j      