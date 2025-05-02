using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Question2.Console.Models
{
    public class OcrResponse
    {
        public string? Locale { get; set; }  // Optional
        public string Description { get; set; } = string.Empty;
        public BoundingPoly BoundingPoly { get; set; } = new();

        public int AverageY => BoundingPoly?.Vertices?.Count > 0
        ? (int)BoundingPoly.Vertices.Average(v => v.Y)
        : 0;
    }
}
