using Android.Content;
using Xamarin.App.Controls;
using Xamarin.App.Droid.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EmptyStyleButton), typeof(EmptyStyleButtonRendererDroid))]
namespace Xamarin.App.Droid.Controls
{
    public class EmptyStyleButtonRendererDroid : ButtonRenderer
    {
        public EmptyStyleButtonRendererDroid(Context context) : base(context)
        {
        }
    }
}