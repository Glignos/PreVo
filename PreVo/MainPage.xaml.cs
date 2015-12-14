using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using PreVo.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.SpeechRecognition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PreVo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static Dictionary<String, Type> menuNavigation = new Dictionary<string, Type>
        {
            {"dashboard_nav", typeof(DashboardPage) },
            {"manage_nav", typeof(ManagePage) },
            {"pref_nav", typeof(PrefPage) },
            {"libr_nav", typeof(LibPage) }
        };

        public static Frame AppFrame = null;
        public static MainPage Current = null;

        private static IAsyncOperation<SpeechRecognitionResult> recognitionOperation;
        private static uint HResultPrivacyStatementDeclined = 0x80045509;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += InitializeStatics;
        }

        private async void InitializeStatics(object sender, RoutedEventArgs e)
        {
            Current = this;
            AppFrame = this.frame;

            // Initialize the speechRecognizer
            await SpeechManager.InitializeRecognizer(SpeechRecognizer.SystemSpeechLanguage);
            // Load Sample data for the presentations
            SampleDataProvider.GeneratePresentations();
            frame.Navigate(menuNavigation["dashboard_nav"]);
        }

        private void OnNavigatedToPage(object sender, NavigationEventArgs e)
        {
            // After a successful navigation set keyboard focus to the loaded page
            if (e.Content is Page && e.Content != null)
            {
                var control = (Page)e.Content;
                control.Loaded += Page_Loaded;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ((Page)sender).Focus(FocusState.Programmatic);
            ((Page)sender).Loaded -= Page_Loaded;
        }



        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var itemName = (e.AddedItems[0] as ListViewItem).Name;

                if (menuNavigation.ContainsKey(itemName))
                {
                    AppFrame.Navigate(menuNavigation[itemName]);
                }
            }

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }
        private async void RecognizeWithUI_Click(object sender, RoutedEventArgs e)
        {
            // Start recognition.
            try
            {

                recognitionOperation = SpeechManager.speechRecognizer.RecognizeWithUIAsync();
                SpeechRecognitionResult speechRecognitionResult = await recognitionOperation;
                if (speechRecognitionResult.Status == SpeechRecognitionResultStatus.Success)
                {
                    HandleRecognitionResult(speechRecognitionResult);
                }
                else
                {
                    // DebugTextBlock.Visibility = Visibility.Visible;
                    // DebugTextBlock.Text = string.Format("Speech Recognition Failed, Status: {0}", speechRecognitionResult.Status.ToString());
                }
            }
            catch (TaskCanceledException exception)
            {
                // TaskCanceledException will be thrown if you exit the scenario while the recognizer is actively
                // processing speech. Since this happens here when we navigate out of the scenario, don't try to 
                // show a message dialog for this exception.
                System.Diagnostics.Debug.WriteLine("TaskCanceledException caught while recognition in progress (can be ignored):");
                System.Diagnostics.Debug.WriteLine(exception.ToString());
            }
            catch (Exception exception)
            {
                // Handle the speech privacy policy error.
                if ((uint)exception.HResult == HResultPrivacyStatementDeclined)
                {
                    // DebugTextBlock.Visibility = Visibility.Visible;
                    // DebugTextBlock.Text = "The privacy statement was declined.";
                }
                else
                {
                    // DebugTextBlock.Text = "Something went wrong with the HResult";
                    var messageDialog = new Windows.UI.Popups.MessageDialog(exception.Message, "Exception");
                    await messageDialog.ShowAsync();
                }
            }
        }

        private void HandleRecognitionResult(SpeechRecognitionResult recoResult)
        {
            // Check the confidence level of the recognition result.
            if (recoResult.Confidence == SpeechRecognitionConfidence.High ||
            recoResult.Confidence == SpeechRecognitionConfidence.Medium)
            {

                /* if (recoResult.SemanticInterpretation.Properties.ContainsKey("") && recoResult.SemanticInterpretation.Properties["KEY_BACKGROUND"][0].ToString() != "...")
                    {
                        string backgroundColor = recoResult.SemanticInterpretation.Properties["KEY_BACKGROUND"][0].ToString();
                        colorRectangle.Fill = new SolidColorBrush(getColor(backgroundColor));
                    }

                    // If "background" was matched, but the color rule matched GARBAGE, prompt the user.
                    else if (recoResult.SemanticInterpretation.Properties.ContainsKey("KEY_BACKGROUND") && recoResult.SemanticInterpretation.Properties["KEY_BACKGROUND"][0].ToString() == "...")
                    {

                        garbagePrompt += speechResourceMap.GetValue("SRGSBackgroundGarbagePromptText", speechContext).ValueAsString;
                        resultTextBlock.Text = garbagePrompt;
                    }

                    // BORDER: Check to see if the recognition result contains the semantic key for the border color,
                    // and not a match for the GARBAGE rule, and change the color.
                    if (recoResult.SemanticInterpretation.Properties.ContainsKey("KEY_BORDER") && recoResult.SemanticInterpretation.Properties["KEY_BORDER"][0].ToString() != "...")
                    {
                        string borderColor = recoResult.SemanticInterpretation.Properties["KEY_BORDER"][0].ToString();
                        colorRectangle.Stroke = new SolidColorBrush(getColor(borderColor));
                    }*/
                /* DebugTextBlock.Text = recoResult.Text;*/
                // DebugTextBlock.Text =recoResult.SemanticInterpretation.Properties["KEY_COMMAND_TYPE"][0].ToString();
            }

            // Prompt the user if recognition failed or recognition confidence is low.
            else if (recoResult.Confidence == SpeechRecognitionConfidence.Rejected ||
            recoResult.Confidence == SpeechRecognitionConfidence.Low)
            {
                // DebugTextBlock.Text = "Low confidence, or failure";
            }
        }
    }
}
