using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Primitives;

namespace MauiCameraViewSample
{
    public partial class MainPage : ContentPage
    {
        private ICameraProvider cameraProvider;

        public MainPage(ICameraProvider cameraProvider)
        {
            InitializeComponent();

            this.cameraProvider = cameraProvider;
        }

        // Implemented as a follow up video https://youtu.be/JUdfA7nFdWw
        protected async override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            
            await cameraProvider.RefreshAvailableCameras(CancellationToken.None);
            MyCamera.SelectedCamera = cameraProvider.AvailableCameras
                .Where(c => c.Position == CameraPosition.Front).FirstOrDefault();
        }

        // Implemented as a follow up video https://youtu.be/JUdfA7nFdWw
        protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
        {
            base.OnNavigatedFrom(args);

            MyCamera.MediaCaptured -= MyCamera_MediaCaptured;
            MyCamera.Handler?.DisconnectHandler();
        }

        private void MyCamera_MediaCaptured(object? sender, CommunityToolkit.Maui.Views.MediaCapturedEventArgs e)
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
