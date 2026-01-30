namespace SheepsAndWolfsEngine
{
    public class Sheep : Animal
    {
        public int MeatLeft { get; private set; } = 2; // si la oveja esta muerta
        public void ConsumeMeat()
        {
            MeatLeft--;
        }

        public Sheep(int x, int y) : base(x, y)
        {

        }
        public override void ExecuteRound(World world)
        {
            var action = IA.ExecuteSheep(this, world);
            ExecuteAction(action, world);
        }

        public override ObjectType GetObjectType()
        {
            return IsDead ? ObjectType.DEAD_SHEEP : ObjectType.SHEEP;
        }
    }
}
