using System.Collections.ObjectModel;
using System.Text;
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
        var sb = new StringBuilder();
        sb.AppendLine("speakers:");
        foreach (var speaker in SpeakersCollection)
        {
            sb.AppendLine("  - id: " + speaker.Id);
            sb.AppendLine("  name: " + speaker.Name);
            sb.AppendLine("  path: " + speaker.SpkPath);
            sb.AppendLine("  dict: " + speaker.Dict);
        }
        sb.AppendLine();

        sb.AppendLine("dictionaries:");
        foreach (var dict in DictsCollection)
        {
            sb.AppendLine("  - name: " + dict.Name);
            sb.AppendLine("  path: " + dict.DictPath);
        }
        sb.AppendLine();

        sb.AppendLine("embeds:");
        foreach (var embed in EmbedsConfigCollection)
        {
            sb.AppendLine("  - duration: " + embed.Duration);
            sb.AppendLine("  pitch: " + embed.Pitch);
            sb.AppendLine("  energy: " + embed.Energy);
            sb.AppendLine("  breathiness: " + embed.Breath);
            sb.AppendLine("  voicing: " + embed.Voicing);
            sb.AppendLine("  tenstion: " + embed.Tension);
            sb.AppendLine("  type: " + embed.Type);
            sb.AppendLine("  pe: " + embed.PitchExtract);
            sb.AppendLine("  hnsep: " + embed.Hnsep);
        }

        sb.AppendLine();

        sb.AppendLine("other:");
        foreach (var otherItem in OtherConfigCollection)
        {
            sb.AppendLine("  - saveevery: " + otherItem.SaveEvery);
            sb.AppendLine("  maxstepcount: " + otherItem.MaxSteps);
            sb.AppendLine("  maxkeepckpt: " + otherItem.MaxKeepCheckpoint);
            sb.AppendLine("  startpermanentckpt: " + otherItem.StartPermCheckpoint);
            sb.AppendLine("  savepermanentckpt: " + otherItem.SavePermCheckpoint);
            sb.AppendLine("  maxbatch: " + otherItem.MaxBatchSize);
            sb.AppendLine("  output: " + otherItem.OutputPath);
        }

        return sb.ToString();
    }
}

