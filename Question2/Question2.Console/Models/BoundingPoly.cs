using System.Text.Json.Serialization;

namespace Question2.Console.Models
{
    public class BoundingPoly
    {
        public List<Vertex> Vertices { get; set; } = new();
    }
}
