using System.Collections.ObjectModel;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DsDiscEditor.Commands;
using RelayCommand = CommunityToolkit.Mvvm.Input.RelayCommand;

namespace DsDiscEditor.Views;

public partial class MainWindow : Window
{
    public ObservableCollection<DictItem> DictsCollection { get; } = new();
    public ObservableCollection<SpeakerItem> SpeakersCollection { get; } = new();
    public ObservableCollection<EmbedsConfigItem> EmbedsConfigCollection { get; } = new();
    public ObservableCollection<OtherConfigItem> OtherConfigCollection { get; } = new();

    public ICommand SpeakerAddItemCommand { get; }
    public ICommand SpeakerRemoveItemCommand { get; }
    public ICommand DictAddItemCommand { get; }
    public ICommand DictRemoveItemCommand { get; }
    public ICommand ExportConfigCommand { get; }

    private void SpeakerAddItem(object? sender, RoutedEventArgs e) => SpeakersCollection.Add(new SpeakerItem());
    private void DictAddItem(object? sender, RoutedEventArgs e) => DictsCollection.Add(new DictItem());
    
    public MainWindow()
    {
        InitializeComponent();
        SpeakerRemoveItemCommand = new RelayCommand<SpeakerItem>(item =>
        {
            if (item != null) SpeakersCollection.Remove(item);
        });
        DictRemoveItemCommand = new RelayCommand<DictItem>(item =>
        {
            if (item != null) DictsCollection.Remove(item); 
        });
        DataContext = this;
    }

    private string BuildYamlContent()
    {
        return "Placeholder";
    }
}

