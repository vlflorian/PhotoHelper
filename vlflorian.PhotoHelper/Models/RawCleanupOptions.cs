using CommandLine;

namespace vlflorian.PhotoHelper.Models;

[Verb("rawcleanup", HelpText = "Cleanup raw files based on deleted jpg files.")]
public class RawCleanupOptions
{
    [Option('v', "verbose", Required = false, HelpText = "Set output to verbose.")]
    public bool Verbose { get; set; }

    [Option( 'd', "directory", Required = true, HelpText = "Working directory containing jpg and raw directories.")]
    public string Directory { get; set; }

    [Option("rawExtension", Required = true, HelpText = "Provide the extension of the RAW files. e.g. --rawExtension RAF")]
    public string RawExtension { get; set; }

    [Option("delete", Required = false, HelpText = "If provided, raw files will be automatically deleted instead of being moved to 'delete' directory.")]
    public bool DeleteImmediately { get; set; }
}