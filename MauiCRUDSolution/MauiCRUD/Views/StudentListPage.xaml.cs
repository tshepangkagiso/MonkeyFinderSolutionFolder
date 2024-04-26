using MauiCRUD.ViewModels;

namespace MauiCRUD.Views;

public partial class StudentListPage : ContentPage
{
	//Properties and Fields
	private StudentListPageViewModel viewModel;

	//Class Constructor
	public StudentListPage(StudentListPageViewModel vm)
	{
		InitializeComponent();
		viewModel = vm;
		BindingContext = vm;
	}

	//Methods
    protected override void OnAppearing()
    {
		//when page appears this happens
        base.OnAppearing();
		viewModel.GetAllStudentsCommand.Execute(null);
    }

}