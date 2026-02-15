using System.Windows.Input;
using System.Collections.ObjectModel;
using DsDiscEditor.Views;
using DsDiscEditor.Commands;

namespace DsDiscEditor.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
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

    public MainWindowViewModel()
    {
        SpeakerRemoveItemCommand = new RelayCommand<SpeakerItem>(item =>
        {
            if (item != null) SpeakersCollection.Remove(item);
        });
        DictRemoveItemCommand = new RelayCommand<DictItem>(item =>
        {
            if (item != null) DictsCollection.Remove(item); 
        });
        // SpeakersCollection.Add(new SpeakerItem { Id = 1, Name = "Test", SpkPath = "path", Dict = "dict" });
        // DictsCollection.Add(new DictItem() { Name = "Dict1", DictPath = "dict-path" });
        // EmbedsConfigCollection.Add(new EmbedsConfigItem() {Duration = true});
    }
    
    public string Greeting { get; } = "Welcome to Avalonia!";
}