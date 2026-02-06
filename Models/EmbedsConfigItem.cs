namespace DsDiscEditor.Models;

public class EmbedsConfigItem
{
    public bool Duration { get; set; }
    public bool Pitch { get; set; }
    public bool Energy { get; set; }
    public bool Breath { get; set; }
    public bool Voicing { get; set; }
    public bool Tension { get; set; }
    public string Type { get; set; }
    public string PitchExtract { get; set; }
    public string Hnsep { get; set; }
}