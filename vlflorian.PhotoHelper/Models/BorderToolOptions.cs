using CommandLine;

namespace vlflorian.PhotoHelper.Models;

[Verb("add-border", HelpText = "Cleanup raw files based on deleted jpg files.")]
public class BorderToolOptions
{
    [Option('v', "verbose", Required = false, HelpText = "Set output to verbose.")]
    public bool Verbose { get; set; }

    [Option('d', "directory", Required = true, HelpText = "Working directory containing jpg files.")]
    public string Directory { get; set; }

    [Option("border-size", Required = true, HelpText = "Provide the size of the border in percent. e.g. --borderSize 2")]
    public double BorderSize { get; set; }
}