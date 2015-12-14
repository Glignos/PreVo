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
    public sealed partial class A3 : Page
    {
        public A3()
        {
            this.InitializeComponent();
            this.Loaded += A3_Loaded;
        }

        private void A3_Loaded(object sender, RoutedEventArgs e)
        {
            A3TopText.Text = ((SampleDataProvider.Repo["A"].Slides[3].SlideLayout as VerticalDoubleLayout).Top as TextContent).Text;
            A3BottomText.Text = "Thank you for your time!";
        }
    }
}
