   M S B A M L   `   `   `  ????D  @CarResale, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CarResale.WindowsCarResale.Windows  5   	   &     CarResale.Windows.QuestionWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:CarResale.Windows     5   	   
??????   ? WindowStyle$
  None=?5   "    ? WindowStartupLocation$ CenterOwner=?5	   	   $	??350??5
   	   $	??400??6    ? 
ResizeMode$ NoResize=?6"   .??5      ?  ? RowDefinitions 5   
   ?? 5      $??25 ?6   ?? 5      ?? 5      $??60 ?6   .+?5   
   ?? ???#1e2c3a$??3??6'   ?? 5   
   -   5        ???#303942$??0??5   G    ?? Orientation$ 
Horizontal=?6T   .+?5      ?? -   5         ??Name$ Title??5      $??15(?6#   $??12,0,0,0q?61   5      ?? 5   
   $??1??6   .+?5      ?? -   5        $??
TBQuestion??5      $??30q?6*   $
??Wrap=?66   $	??300??6J   $??22(?5      
??????? ?? TextAlignment$ Center=?69   5      ?? 5   
   $??2??6   $ 
Horizontal=?6"   $??Center=?5      $??Center=?5       .+?5"      ?? -   5        $??	ButtonYes??5"      $??30??6&   $	??120??62   $
??Да??5#      ?? 5%      -   5        $??ButtonNo??5%      $??30??6%   $	??120??61   $??Нет??5&      $??20,0,0,0q?6   5(      5*      5+      