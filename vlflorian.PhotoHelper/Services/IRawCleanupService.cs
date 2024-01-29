using vlflorian.PhotoHelper.Models;

namespace vlflorian.PhotoHelper;

public interface IRawCleanupService
{
    Task CleanupRawFiles(RawCleanupOptions options);
}

public class RawCleanupService : IRawCleanupService
{
    // When shooting RAW + JPEG and going through photos afterwards,
// I only inspect and delete the JPGs I don't want because that is faster.
// This program can be used to detect which JPGs have been deleted and to delete the corresponding RAW files as well.  
    public Task CleanupRawFiles(RawCleanupOptions options)
    {
        var jpgPath = $"{options.Directory}/jpg";
        var rawPath = $"{options.Directory}/raw";
        var toDeletePath = $"{options.Directory}/raw/to_delete";

        EnsureRequiredDirectoriesExist(options.Directory, jpgPath, rawPath, toDeletePath, options);

        var jpgImgNames = Directory.GetFiles(jpgPath) // list jpg files
            .Select(Path.GetFileNameWithoutExtension) // extract filename from full path
            .ToHashSet();

        var rawFilePaths = Directory
            .GetFiles(rawPath, $"*.{options.RawExtension}")
            .ToList();

        foreach (var rawFile in rawFilePaths)
        {
            var imgName = Path.GetFileNameWithoutExtension(rawFile);
            if (!jpgImgNames.Contains(imgName))
            {
                // jpg has been deleted --> also delete RAW file or move to "delete" folder
                var destinationPath = $"{toDeletePath}/{imgName}.{options.RawExtension}";
                if (options.DeleteImmediately)
                {
                    if (options.Verbose) Console.WriteLine($"Deleting {imgName}");
                    File.Delete(rawFile);
                }
                else
                {
                    if (options.Verbose) Console.WriteLine($"Moving {imgName} to delete");
                    File.Move(rawFile, destinationPath);
                }
            }
        }

        return Task.CompletedTask;
    }

    static void EnsureRequiredDirectoriesExist(string workingDirectory, string jpgDir, string rawDir, string toDeleteDir,
        RawCleanupOptions runOptions)
    {
        if (!Directory.Exists(workingDirectory))
            throw new DirectoryNotFoundException($"Directory {workingDirectory} does not exist");
        if (!Directory.Exists(jpgDir))
            throw new DirectoryNotFoundException($"Directory {jpgDir} does not exist");
        if (!Directory.Exists(rawDir))
            throw new DirectoryNotFoundException($"Directory {rawDir} does not exist");
        if (!Directory.Exists(toDeleteDir) && !runOptions.DeleteImmediately)
            Directory.CreateDirectory(toDeleteDir);
    }
}