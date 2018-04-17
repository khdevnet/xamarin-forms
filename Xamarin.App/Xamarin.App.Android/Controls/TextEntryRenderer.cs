using Android.Content;
using Xamarin.App.Controls;
using Xamarin.App.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(TextEntry), typeof(TextEntryRendererDroid))]
namespace Xamarin.App.Droid.Controls
{
    public class TextEntryRendererDroid : EntryRenderer
    {
        public TextEntryRendererDroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            Control?.SetHintTextColor(global::Android.Graphics.Color.Rgb(255, 255, 255));
        }
    }
}