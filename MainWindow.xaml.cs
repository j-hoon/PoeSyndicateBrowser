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

using System.Diagnostics;

namespace PoeSyndicateBrowser
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void WindowContentRendered(object sender, EventArgs e)
        {
            Button TestButton = (Button)MemberHeaderControl.Template.FindName("TestButton", MemberHeaderControl);
            TestButton.Click += TestButtonClick;
        }

        private void TestButtonClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("- TestButtonClick()");

            MemberControl.TestConnection();
        }

    }


}
