using Android.Content;
using Xamarin.App.Controls;
using Xamarin.App.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundCornersButton), typeof(RoundCornersButtonRendererDroid))]
namespace Xamarin.App.Droid.Controls
{
    public class RoundCornersButtonRendererDroid : ButtonRenderer
    {
        public RoundCornersButtonRendererDroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            Control?.SetHintTextColor(global::Android.Graphics.Color.Rgb(255, 255, 255));
        }
    }
}