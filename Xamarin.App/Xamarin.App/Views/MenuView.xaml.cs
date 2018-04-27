using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.App.ViewModels;

namespace Xamarin.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView : ContentPage
    {
        public ListView ListView;

        public MenuView()
        {
            InitializeComponent();

            //BindingContext = new LayoutViewMasterViewModel();
            //ListView = MenuItemsListView;
        }

    }
}