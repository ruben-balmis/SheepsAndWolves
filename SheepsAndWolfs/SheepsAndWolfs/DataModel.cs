using SheepsAndWolfsEngine;

namespace SheepsAndWolfs
{
    public class DataModel
    {
        private static DataModel _instance = new DataModel();
        private World _world;

        public World World => _world;
        private DataModel()
        {
            _world = new World(10, 10);
            Utils.GenerateWorld(_world);
        }

        public static DataModel Instance => _instance;
    }
}
