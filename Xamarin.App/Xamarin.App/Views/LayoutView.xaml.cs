using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LayoutView : MasterDetailPage
    {
        public LayoutView()
        {
            InitializeComponent();
        }
    }
}