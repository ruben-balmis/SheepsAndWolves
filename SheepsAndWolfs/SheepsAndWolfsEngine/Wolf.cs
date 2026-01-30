namespace SheepsAndWolfsEngine
{
    public class Wolf : Animal
    {
        public Wolf(int x, int y) : base(x, y)
        {

        }
        public override ObjectType GetObjectType()
        {
            return IsDead ? ObjectType.DEAD_WOLF : ObjectType.WOLF;
        }

        public override void ExecuteRound(World world)
        {
            var a = IA.ExecuteWolf(this, world);
            ExecuteAction(a, world);
        }
    }
}
