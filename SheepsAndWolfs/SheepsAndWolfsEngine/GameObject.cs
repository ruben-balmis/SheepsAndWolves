namespace SheepsAndWolfsEngine
{
    public enum ObjectType
    {
        NONE,
        WOLF,
        SHEEP,
        OBSTACLE,
        WATER,
        GRASS,
        DEAD_SHEEP,
        DEAD_WOLF
    }
    public abstract class GameObject
    {
        private string _name = string.Empty;
        protected int _x;
        protected int _y;

        public int X => _x;
        public int Y => _y;
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        public GameObject(int x, int y)
        {
            _x = x;
            _y = y;
        }
        public abstract ObjectType GetObjectType();

        public abstract void ExecuteRound(World world);
        public virtual bool CanExecuteAction(Actions action, World world)
        {
            return false;
        }
        public virtual void ExecuteAction(Actions action, World world)
        {
        }
    }
}
