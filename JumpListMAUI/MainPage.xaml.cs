using static CommunityToolkit.Maui.Markup.GridRowsColumns;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Markup;

namespace JumpListMAUI;

public partial class MainPage : ContentPage
{
    private readonly MainPageViewModel _vm;
    public MainPage(MainPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;

        CollectionSeries.Loaded += CollectionSeries_Loaded;
    }

    protected override async void OnAppearing()
    {
        await _vm.InitialSetup();
        base.OnAppearing();
    }

    private void CollectionSeries_Loaded(object sender, EventArgs e)
    {
        var collectionSeries = CollectionSeries.ItemsSource as ObservableCollection<GroupMyModel>;

        GridAlphabetList.RowDefinitions = Rows.Define(_vm.AlphabetList.Select(x => GridLength.Auto).ToArray());

        for (int i = 0; i < _vm.AlphabetList.Count; i++)
        {
            var label = new Label()
            {
                Text = _vm.AlphabetList[i],
                TextColor = Color.FromArgb("#09BFEF"),
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 12,
                FontFamily = "OpenSansSemibold",
                Padding = new Thickness(0, 3)

            }.Row(i).Column(0);
            var labelTapped = new TapGestureRecognizer() { Command = new Command<string>(AlphabetTapped), CommandParameter = _vm.AlphabetList[i].ToUpper() };
            label.GestureRecognizers.Add(labelTapped);
            GridAlphabetList.Children.Add(label);
        }
    }

    private void AlphabetTapped(string text)
    {
        try
        {
            var collectionSeries = CollectionSeries.ItemsSource as ObservableCollection<GroupMyModel>;

            foreach (var group in collectionSeries)
            {
                foreach (var item in group)
                {
                    if (item.Name.Equals(text))
                    {
                        CollectionSeries.ScrollTo(item, null, ScrollToPosition.Start, true);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

}

