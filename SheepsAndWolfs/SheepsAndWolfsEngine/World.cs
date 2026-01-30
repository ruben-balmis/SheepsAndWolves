namespace SheepsAndWolfsEngine
{
    public class World
    {
        private readonly List<GameObject> _gameObjects = new();
        private int _width;
        private int _height; 
        public int ObjectCount => _gameObjects.Count;
        public int Width => _width;
        public int Height => _height;
        private List<GameObject> _toRemove = new List<GameObject>();
        public World(int width, int height)
        {
            _width = width;
            _height = height;
        }
        public GameObject GetObjectAt(int index)
        {
            if(index >=0 && index < _gameObjects.Count)
                return _gameObjects[index];
            throw new ArgumentOutOfRangeException("index");
        }
        public GameObject? GetObjectAt(int x, int y)
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                var obj = _gameObjects[i];
                if (obj.X == x && obj.Y == y)
                    return obj;
            }
            return null;
        }

        public ObjectType GetObjectTypeAt(int x, int y)
        {
            var obj = GetObjectAt(x, y);
            return obj != null ? obj.GetObjectType() : ObjectType.NONE;
        }

        public ObjectType GetObjectTypeAt(int x, int y, GameObject caller) //para excluir el animal que lo llama
        {
            for (int i = 0; i < _gameObjects.Count; i++)
            {
                var obj = _gameObjects[i];
                if (obj.X == x && obj.Y == y && obj != caller)
                    return obj.GetObjectType();
            }
            return ObjectType.NONE;
        }

        public void Add(GameObject? gameObject)
        {
            if (gameObject == null)
                return;
            if (IsOutOfBounds(gameObject))
                return;
            if (Contains(gameObject))
                return;
            _gameObjects.Add(gameObject);
        }

        public bool IsOutOfBounds(GameObject? obj)
        {
            return !IsInBounds(obj);
        }

        public bool IsInBounds(GameObject obj)
        {
            return obj != null && 0 <= obj.X && obj.X < _width && 0 <= obj.Y && obj.Y < _height;
        }

        public bool IsInBounds(int x, int y)
        {
            return x >= 0 && x < _width && y >= 0 && y < _height;
        }

        public void RemoveObject(GameObject? obj)
        {
            int index = IndexOf(obj);
            if(index >= 0)
                _gameObjects.RemoveAt(index);
        }

        public bool Contains(GameObject? gameObject)
        {
            return IndexOf(gameObject) >= 0;
        }

        public int IndexOf(GameObject? gameObject)
        {
            if (gameObject == null)
                return -1;
            for (int i = 0; i < _gameObjects.Count; i++)
                if (_gameObjects[i] == gameObject)
                    return i;
            return -1;
        }
        public void MarkForRemoval(GameObject obj)
        {
            if (!_toRemove.Contains(obj))
                _toRemove.Add(obj);
        }
        public void RunTurn()
        {
            _toRemove.Clear();

            var copy = _gameObjects.ToList();

            foreach (var obj in copy)
            {
                obj.ExecuteRound(this);

                if (obj is Animal animal)
                    animal.UpdateStats();
            }
            foreach (var obj in _toRemove)
                _gameObjects.Remove(obj);
        }

        public bool CanMoveAnimal(Animal animal, int x, int y) //////////////////////////////////////////////
        {
            if (x < 0)
                return false;
            if (y < 0)
                return false;
            if (x >= Width)
                return false;
            if (y >= Height)
                return false;

            var obj = GetObjectAt(x, y);
            if (obj == null)
                return true;
            var objectType = obj.GetObjectType();
            if (objectType == ObjectType.WOLF || objectType == ObjectType.SHEEP || objectType == ObjectType.OBSTACLE 
                || objectType == ObjectType.DEAD_SHEEP || objectType == ObjectType.DEAD_WOLF)
                return false;
            return true;
        }
    }
}
