namespace SheepsAndWolfsEngine
{
    public class Water : GameObject
    {
        public Water(int x, int y) : base(x, y)
        {

        }
        public override ObjectType GetObjectType()
        {
            return ObjectType.WATER;
        }

        public override void ExecuteRound(World world)
        {
        }
    }
}
