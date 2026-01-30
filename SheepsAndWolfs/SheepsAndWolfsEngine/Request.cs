namespace SheepsAndWolfsEngine
{
    public class ObjectInfo
    {

        public string Name { get; set; } = string.Empty;
        public int X { get; set; }
        public int Y { get; set; }
        public ObjectType Type { get; set; }
    }
    public record class WorldInfo(int Width, int Height);
}
