using AppNotes.ViewModels;

namespace AppNotes.Views;

public partial class NoteView : ContentView
{
	public NoteView()
	{
		InitializeComponent();
		BindingContext = new NoteViewModel();
	}
}