using MauiCRUD.Views;

namespace MauiCRUD
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(StudentListPage), typeof(StudentListPage));
            Routing.RegisterRoute(nameof(AddUpdateStudent), typeof(AddUpdateStudent));
        }
    }
}
