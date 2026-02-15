using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using DsDiscEditor.Commands;
using DsDiscEditor.ViewModels;
using RelayCommand = CommunityToolkit.Mvvm.Input.RelayCommand;

namespace DsDiscEditor.Views;

public partial class MainWindow : Window
{

    private void SpeakerAddItem(object? sender, RoutedEventArgs e) => MainWindowViewModel.SpeakersCollection.Add(new SpeakerItem());
    private void DictAddItem(object? sender, RoutedEventArgs e) => DictsCollection.Add(new DictItem());
    
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }

    private async void OnExportClick(object? sender, RoutedEventArgs e)
    {
        var sfd = new SaveFileDialog
        {
            Title = "Export YAML Config",
            Filters =
            {
                new FileDialogFilter{ Name = "YAML File", Extensions = {"yaml", "yml"} },
                new FileDialogFilter{ Name = "All Files", Extensions = {"*"} }
            },
            InitialFileName = "config.yaml"
        };

        var path = await sfd.ShowAsync(this);
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }

        var yamlContents = BuildYamlContent();
        await File.WriteAllTextAsync(path, yamlContents, Encoding.UTF8);
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

