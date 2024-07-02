using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;

namespace MauiCameraViewSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MyCamera_MediaCaptured(object sender, CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
        {
            if (Dispatcher.IsDispatchRequired)
            {
                Dispatcher.Dispatch(() => MyImage.Source = ImageSource.FromStream(() => e.Media));
                return;
            }

            MyImage.Source = ImageSource.FromStream(() => e.Media);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await MyCamera.CaptureImage(CancellationToken.None);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            MyCamera.CameraFlashMode = MyCamera.CameraFlashMode == CameraFlashMode.Off ? 
                CameraFlashMode.On : CameraFlashMode.Off;
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            MyCamera.ZoomFactor += 0.1f;
        }
    }

}
