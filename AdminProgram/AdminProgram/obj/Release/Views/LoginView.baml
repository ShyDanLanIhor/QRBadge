   M S B A M L   `   `   `  ���� G  CAdminProgram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:AdminProgram.ViewsAdminProgram.Views  A%clr-namespace:AdminProgram.ViewModelsAdminProgram.ViewModels  G(clr-namespace:AdminProgram.CustomControlAdminProgram.CustomControl  "    AdminProgram.Views.LoginView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:AdminProgram.Views     <
viewModels%clr-namespace:AdminProgram.ViewModels     Ccustomcontrols(clr-namespace:AdminProgram.CustomControl       � WindowStartupLocation$  CenterScreen=� � Title$3 -Вхід у особистий кабінет��$	��567��$	��400�� � Icon$( "/Images/WhiteVectorProgramIcon.png�� � DataContext ,  &AdminProgram.ViewModels.LoginViewModel   � 	Resources    BooleanToVisibility��%   &	        �� y���  �� Path$ IsViewVisible� �� Mode$ TwoWay=� �� 	Converter# [  .��� -   $��
AdminLogin��  BasicGridStyle#���  $��0,0,2,1q�.+��� $��20(�$��Вхід��$��Center=�$��	0,475,0,0q�$	��Top=�  BasicButtonStyle#���  $��40��$	��110������ *LoginCommand+�� -    ��Name$ 
LoginInput��$��18(�  BasicTextBoxStyle#���  $��Center=�$��	0,301,0,0q�	 �� TextWrapping$
	 Wrap=�$	��Top=�$	��250��$��40������ *
Username+
 �� UpdateSourceTrigger$
 PropertyChanged=�4   .AdminProgram.CustomControl.BindablePasswordBox  -     Name$ PasswordInput��$��18(�$��Center=�$��	0,378,0,0q�$	��Top=�$��55��   Password �� *
Password+$ TwoWay=�$
 PropertyChanged=��� $��18(�$
��Left=�$��
73,351,0,0q�$
��Wrap=�$��Пароль��$	��Top=�  BasicTextBlockStyle#���  �� $��18(�$
��Left=�$��
72,270,0,0q�$
��Wrap=�$	��Top=�#���  ��  � Language$ uk-ua�$$�Ім'я користувача���� $��18(�$��Center=� �� TextAlignment$ Center=�$��	0,437,0,0q�$
��Wrap=�$	��Top=�#���  $	��394������ *ErrorMessage+�� $ uk-ua��� $#��/Images/VectorProgramIcon.png��$
��Left=�$	��248��$��	78,12,0,0q�$	��Top=�$	��246��