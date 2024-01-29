using CommandLine;
using vlflorian.PhotoHelper;
using vlflorian.PhotoHelper.Models;

await Parser.Default.ParseArguments<RawCleanupOptions, BorderToolOptions>(args)
    .MapResult(
        (RawCleanupOptions opts) => new RawCleanupService().CleanupRawFiles(opts),
        (BorderToolOptions opts) => new BorderService().AddBorders(opts), // TODO how to use DI here?
        HandleInvalidArgs);

return;

Task HandleInvalidArgs(IEnumerable<Error> errors)
{
    // Don't do anything, CommandLineParser will already print out an error screen with all available arguments.
    return Task.CompletedTask;
}