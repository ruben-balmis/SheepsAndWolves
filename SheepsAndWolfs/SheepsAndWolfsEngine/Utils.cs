namespace SheepsAndWolfsEngine
{
    public class Utils
    {
        private static Random _random = new Random();
        public static int GetRandom(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue + 1);
        }

        public static int GetHighScoreIndex(int[] values)
        {
            if (values == null || values.Length == 0)
                throw new ArgumentOutOfRangeException();
            int result = 0;
            for (int i = 1; i < values.Length; i++)
                if (values[i] > values[result])
                    result = i;
            return result;
        }

        public static void AddRandomValues(int[] values, int minValue, int maxValue)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] += Utils.GetRandom(minValue, maxValue);
        }
        public static World CreateWorld(int width, int height)
        {
            World mundo = new World(width, height);
            GenerateWorld(mundo);
            return mundo;
        }
        public static void GenerateWorld(World mundo)
        {
            Random rand = new Random();
            int width = mundo.Width;
            int height = mundo.Height;

            List<(int x, int y)> usedPositions = new List<(int, int)>();

            AddRandomObject(mundo, usedPositions, width, height, ObjectType.WOLF, 1, rand);
            AddRandomObject(mundo, usedPositions, width, height, ObjectType.SHEEP, 5, rand);
            AddRandomObject(mundo, usedPositions, width, height, ObjectType.OBSTACLE, 5, rand);
            AddRandomObject(mundo, usedPositions, width, height, ObjectType.WATER, 15, rand);
            AddRandomObject(mundo, usedPositions, width, height, ObjectType.GRASS, 15, rand);
        }

        private static void AddRandomObject(World mundo, List<(int x, int y)> usedPositions, int width, int height, ObjectType type, int count, Random rand)
        {
            for (int i = 0; i < count; i++)
            {
                int x, y;
                bool positionUsed;

                do
                {
                    x = rand.Next(width);
                    y = rand.Next(height);
                    positionUsed = usedPositions.Any(pos => pos.x == x && pos.y == y);
                } while (positionUsed);

                usedPositions.Add((x, y));

                GameObject obj = CreateObject(type, x, y);
                obj.Name = type.ToString();
                mundo.Add(obj);
            }
        }

        private static GameObject CreateObject(ObjectType type, int x, int y)
        {
            switch (type)
            {
                case ObjectType.WOLF:
                    return new Wolf(x, y);
                case ObjectType.SHEEP:
                    return new Sheep(x, y);
                case ObjectType.OBSTACLE:
                    return new Obstacle(x, y);
                case ObjectType.WATER:
                    return new Water(x, y);
                case ObjectType.GRASS:
                    return new Grass(x, y);
                default:
                    throw new ArgumentException("Tipo de objeto no soportado: " + type);
            }
        }
        public static ObjectInfo[] ToObjectsInfo(World world)
        {
            List<ObjectInfo> result = new();
            for (int i = 0; i < world.ObjectCount; i++)
            {
                GameObject obj = world.GetObjectAt(i);
                var oInfo = new ObjectInfo();
                oInfo.Name = obj.Name;
                oInfo.X = obj.X;
                oInfo.Y = obj.Y;
                oInfo.Type = obj.GetObjectType();
                result.Add(oInfo);
            }
            return result.ToArray();
        }

    }
}
