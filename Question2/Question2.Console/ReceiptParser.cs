using Question2.Console.Models;
using System.Text.Json;

namespace Question2.Console
{
    public class ReceiptParser
    {
        public static string Build(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var items = JsonSerializer.Deserialize<List<Models.OcrResponse>>(json, options);
            var output = new List<string>();
            
            
            if (items == null)
                return string.Empty;
            
            var sorted = items
                        .Skip(1) //ignore first item since it outlines entire receipt
                        .Where(i => i.BoundingPoly?.Vertices?.Count > 0)
                        .OrderBy(i => i.BoundingPoly.Vertices[0].Y) // top-to-bottom
                        .ThenBy(i => i.BoundingPoly.Vertices[0].X)  // left-to-right
                        .ToList();

            var currentLine = new List<OcrResponse>();
            int? currentY = null;
            const int errorBuffer = 10;

            foreach (var item in sorted)
            {
                if (currentY == null || Math.Abs(item.AverageY - currentY.Value) <= errorBuffer)
                {
                    currentLine.Add(item);
                    currentY ??= item.AverageY;
                }
                else
                {
                    output.Add(string.Join(" ", currentLine.Select(i => i.Description)));
                    currentLine = new List<OcrResponse> { item };
                    currentY = item.AverageY;
                }
            }

            if (currentLine.Any())
               output.Add(string.Join(" ", currentLine.Select(i => i.Description)));

            return string.Join(Environment.NewLine, output);

        }

    }
}
