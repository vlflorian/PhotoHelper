using ImageMagick;
using vlflorian.PhotoHelper.Models;

namespace vlflorian.PhotoHelper;

public interface IBorderService
{
    Task AddBorders(BorderToolOptions options);
}

public class BorderService : IBorderService
{
    public async Task AddBorders(BorderToolOptions options)
    {
        if (!Directory.Exists(options.Directory))
            throw new Exception($"Directory {options.Directory} does not exist");

        var imagePaths = Directory.GetFiles(options.Directory, "*.jpg");
        Console.WriteLine($"{imagePaths.Length} images found in {options.Directory}");

        var parallelOptions = new ParallelOptions
        {
            MaxDegreeOfParallelism = 5
        };
        await Parallel.ForEachAsync(imagePaths, parallelOptions, async (imgPath, _) =>
        {
            await AddBorderToImage(imgPath, options.BorderSize, options.Directory);
        });

        Console.WriteLine($"All {imagePaths.Length} images have been processed");
    }
    
    private async Task AddBorderToImage(string imgPath, double borderRatio, string outputDir)
    {
        using var image = new MagickImage(imgPath);
        var fileName = Path.GetFileName(imgPath);
        Console.WriteLine($"Processing {fileName}: {image}");

        double targetWidth;
        double targetHeight;
        if (image.Width > image.Height)
        {
            // Landscape mode
            targetWidth = image.Width * (1 + borderRatio / 100);
            targetHeight = targetWidth;
        }
        else
        {
            // Portrait mode
            targetHeight = image.Height * (1 + borderRatio / 100);
            targetWidth = targetHeight;
        }

        // Calculate the size of the border to be added
        var borderWidth = (targetWidth - image.Width) / 2;
        var borderHeight = (targetHeight - image.Height) / 2;

        // Add white border to both sides
        image.BorderColor = MagickColors.White;
        image.Border((int)borderWidth, (int)borderHeight);

        var outputFilePath = Path.Combine(outputDir, "border_" + Path.GetFileNameWithoutExtension(imgPath) + ".jpg");
        await image.WriteAsync(outputFilePath);
    }
}

