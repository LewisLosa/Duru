﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Duru.Views;

/// <summary>
/// Interaction logic for LoginView.xaml
/// </summary>
public partial class LoginView : Page
{
    public LoginView()
    {
        InitializeComponent();
    }

    private void pwTxtBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext != null) ((dynamic)DataContext).SecurePassword = ((PasswordBox)sender).SecurePassword;
    }
}