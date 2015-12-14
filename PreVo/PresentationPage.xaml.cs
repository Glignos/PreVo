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
using Windows.UI.Xaml.Media.Animation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PreVo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PresentationPage : Page
    {
        public static Presentation CurrentPresentation;
        public static Slide CurrentSlide;
        public static int CurrentSlideIndex;
        private static Dictionary<String, Action> voiceCommands;

        private static IAsyncOperation<SpeechRecognitionResult> recognitionOperation;
        private static uint HResultPrivacyStatementDeclined = 0x80045509;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var presentationId = e.Parameter as String;
            CurrentPresentation = SampleDataProvider.Repo[presentationId];

            // First slide
            CurrentSlideIndex = 1;
            CurrentSlide = CurrentPresentation.Slides[1];
            slideFrame.Navigate(CurrentSlide.SlidePage);
        }

        public PresentationPage()
        {
            this.InitializeComponent();
            voiceCommands = new Dictionary<string, Action>();
            
            voiceCommands.Add("COMMAND_NEXT", () =>
            {
                // Check if last slide (it's 1-indexed)
                if (CurrentSlideIndex <= CurrentPresentation.Slides.Count)
                {
                    CurrentSlide = CurrentPresentation.Slides[++CurrentSlideIndex];
                    slideFrame.Navigate(CurrentSlide.SlidePage);
                }
            });
            voiceCommands.Add("COMMAND_PREVIOUS", () =>
            {
                // Check if last slide (it's 1-indexed)
                if (CurrentSlideIndex > 1)
                {
                    CurrentSlide = CurrentPresentation.Slides[--CurrentSlideIndex];
                    slideFrame.Navigate(CurrentSlide.SlidePage);
                }
            });
        }
        
        private async void VoiceCommand_Click(object sender, RoutedEventArgs e)
        {
            // Start recognition.
            try
            {
                recognitionOperation = SpeechManager.speechRecognizer.RecognizeAsync();
                SpeechRecognitionResult speechRecognitionResult = await recognitionOperation;
                if (speechRecognitionResult.Status == SpeechRecognitionResultStatus.Success)
                {
                    if (!String.IsNullOrEmpty(speechRecognitionResult.Text))
                    {
                        HandleRecognitionResult(speechRecognitionResult);
                    }
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
                    var messageDialog = new Windows.UI.Popups.MessageDialog(exception.Message, "Exception");
                    await messageDialog.ShowAsync();
                }
                else
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog(exception.Message, "Exception");
                    await messageDialog.ShowAsync();
                }
            }
        }

        private void HandleRecognitionResult(SpeechRecognitionResult recoResult)
        {
            if (recoResult.Status == SpeechRecognitionResultStatus.Success)
            {
                // Use commands from helper
                if (recoResult.SemanticInterpretation.Properties.ContainsKey("KEY_COMMAND_TYPE"))
                {
                    var cmdType = recoResult.SemanticInterpretation.Properties["KEY_COMMAND_TYPE"][0];
                    if (!String.IsNullOrEmpty(cmdType))
                    {
                        voiceCommands[cmdType].Invoke();
                    }
                }
            }
        }

        private void NextSlide_Click(object sender, RoutedEventArgs e)
        {
            // Check if last slide (it's 1-indexed)
            if (CurrentSlideIndex < CurrentPresentation.Slides.Count)
            {
                CurrentSlide = CurrentPresentation.Slides[++CurrentSlideIndex];
                slideFrame.Navigate(CurrentSlide.SlidePage);
            }
        }

        private void PrevSlide_Click(object sender, RoutedEventArgs e)
        {
            // Check if last slide (it's 1-indexed)
            if (CurrentSlideIndex > 1)
            {
                CurrentSlide = CurrentPresentation.Slides[--CurrentSlideIndex];
                slideFrame.Navigate(CurrentSlide.SlidePage);
            }
        }

        

        private async void VoiceCommandWithUI_Click(object sender, RoutedEventArgs e)
        {
            // Start recognition.
            try
            {
                recognitionOperation = SpeechManager.speechRecognizer.RecognizeWithUIAsync();
                SpeechRecognitionResult speechRecognitionResult = await recognitionOperation;
                if (speechRecognitionResult.Status == SpeechRecognitionResultStatus.Success)
                {
                    if (!String.IsNullOrEmpty(speechRecognitionResult.Text))
                    {
                        HandleRecognitionResult(speechRecognitionResult);
                    }
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
                    var messageDialog = new Windows.UI.Popups.MessageDialog(exception.Message, "Exception");
                    await messageDialog.ShowAsync();
                }
                else
                {
                    // DebugTextBlock.Text = "Something went wrong with the HResult";
                    var messageDialog = new Windows.UI.Popups.MessageDialog(exception.Message, "Exception");
                    await messageDialog.ShowAsync();
                }
            }
        }
    }
}
