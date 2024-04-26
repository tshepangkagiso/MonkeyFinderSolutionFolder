using MauiCRUD.ViewModels;

namespace MauiCRUD.Views;

public partial class AddUpdateStudent : ContentPage
{
	public AddUpdateStudent(AddUpdateStudentViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}