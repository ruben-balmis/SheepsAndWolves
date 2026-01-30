namespace SheepsAndWolfsEngine
{
    public class Grass : GameObject
    {
        public Grass(int x, int y) : base(x,y)
        {

        }

        public override ObjectType GetObjectType()
        {
            return ObjectType.GRASS;
        }

        public override void ExecuteRound(World world)
        {
        }
    }
}
