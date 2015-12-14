using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using PreVo.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PreVo.SamplePresentations
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class A1 : Page
    {
        public A1()
        {
            this.InitializeComponent();
            this.Loaded += A1_Loaded;
        }

        private void A1_Loaded(object sender, RoutedEventArgs e)
        {
            A1Text.Text = ((SampleDataProvider.Repo["A"].Slides[1].SlideLayout as SingleLayout).Main as TextContent).Text;
        }
    }
}
