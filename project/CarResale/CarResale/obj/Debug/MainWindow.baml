   M S B A M L   `   `   `  ????D  @CarResale, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:CarResale	CarResale  5   	       CarResale.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:CarResale     5   	     ? WindowStartupLocation$  CenterScreen=?5   	    ? Title$ 
MainWindow??5	   	   $	??960??6   $
??1280??6)   $	??800??5
   	   $	??980??6   .??5      ? -   5         ?Name$ MainGrid??5         MainGridBackground#??[  6    ? ColumnDefinitions 5   
   ?? 5      $	??180 ?6   ?? 5      $??* ?6    ? RowDefinitions 5   
   ?? 5      $??60 ?6   ?? 5      .+?5   
   ? $??0??6   $??2??6   ???#303942  DropShadowEffectDown J? Effect# ?  5      ? 5   
   $??1??6   $??0??6   ???#303942  DropShadowEffectRight# ?  5      ?? 5   
   $??0??6   $??0??6!   $??32(?5      $??Black"?6"   $??
Car Resale??5      ?? 5!   
   $??1??6   $??0??6"    ?? Orientation$ Vertical=?62   .+?5#      ?? $??0,20,0,0q?6   $??30(?5$      $??DemiBold"?6   $
??Menu??5%      ?? 5'      -   5         ??Name$ CarsBtn??5'      $
??Cars??6&   $??0,30,0,0q?5(      ?? 5*      -   5        $ 
ClientsBtn??5*      $??Clients??6)   $??0,20,0,0q?5+      ?? 5-      -   5        $ MarksBtn??5-      $??Marks??6'   $??0,20,0,0q?5.      ?? 50      -   5        $ 	ModelsBtn??50      $??Models??6(   $??0,20,0,0q?51      ?? 53      -   5        $ 	OrdersBtn??53      $??Orders??6(   $??0,20,0,0q?54      ?? 56      -   5        $ 
ReportsBtn??56      $??Reports??6)   $??0,20,0,0q?57      ?? 59      -   5        $ ThemeBtn??59      $??Theme??6'   $??0,20,0,0q?5:      5<      ?? 5>   
   -	   5         ??Name$ CurrentPageName??5@      $??0??5>      $??1??6!   $??32(?5?      $??DemiBold"?6"   $??Orders??5@   -    ? 5B   
   -
   5        	  ?Name$	 	MainFrame??5B   -   $??1??6   $??1??6   
  ? NavigationUIVisibility$
 Hidden=?5C      5E      5F      