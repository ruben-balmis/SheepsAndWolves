namespace SheepsAndWolfsEngine
{
    public class Obstacle : GameObject
    {
        public Obstacle(int x, int y) : base(x, y)
        {

        }

        public override ObjectType GetObjectType()
        {
            return ObjectType.OBSTACLE;
        }

        public override void ExecuteRound(World world)
        {
        }
    }
}
