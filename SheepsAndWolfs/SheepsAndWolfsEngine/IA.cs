using System;

namespace SheepsAndWolfsEngine
{
    public enum Actions
    {
        MOVE_UP,
        MOVE_DOWN,
        MOVE_LEFT,
        MOVE_RIGHT,
        REST,
        DRINK,
        ATTACK,
        EAT,
        COUNT
    }


    public class IA
    {
        public static Actions ExecuteWolf(Wolf wolf, World world)
        {
            if (wolf.ForceRest)
                return Actions.REST;

            bool sheepNearby =
                world.GetObjectTypeAt(wolf.X, wolf.Y - 1) == ObjectType.SHEEP ||
                world.GetObjectTypeAt(wolf.X, wolf.Y + 1) == ObjectType.SHEEP ||
                world.GetObjectTypeAt(wolf.X - 1, wolf.Y) == ObjectType.SHEEP ||
                world.GetObjectTypeAt(wolf.X + 1, wolf.Y) == ObjectType.SHEEP;
            bool deadSheepNearby =
                world.GetObjectTypeAt(wolf.X, wolf.Y - 1) == ObjectType.DEAD_SHEEP ||
                world.GetObjectTypeAt(wolf.X, wolf.Y + 1) == ObjectType.DEAD_SHEEP ||
                world.GetObjectTypeAt(wolf.X - 1, wolf.Y) == ObjectType.DEAD_SHEEP ||
                world.GetObjectTypeAt(wolf.X + 1, wolf.Y) == ObjectType.DEAD_SHEEP;
            int[] ia = new int[(int)Actions.COUNT];
            if (wolf.Hunger < 70)
            {
                if (sheepNearby)
                {
                    ia[(int)Actions.ATTACK] += 150;

                    Console.WriteLine($"[IA] El lobo en ({wolf.X},{wolf.Y}) tiene una oveja cerca — ATTACK += 90");

                }
                else
                {
                    var nearest = FindNearest(world, wolf, ObjectType.SHEEP);
                    if (nearest.HasValue)
                    {
                        int dx = nearest.Value.dx;
                        int dy = nearest.Value.dy;

                        Console.WriteLine($"[IA] El lobo en ({wolf.X},{wolf.Y}) busca una oveja ({wolf.X + dx},{wolf.Y + dy})");

                        if (dx < 0) 
                            ia[(int)Actions.MOVE_LEFT] += 60;
                        if (dx > 0) 
                            ia[(int)Actions.MOVE_RIGHT] += 60;
                        if (dy < 0) 
                            ia[(int)Actions.MOVE_UP] += 60;
                        if (dy > 0) 
                            ia[(int)Actions.MOVE_DOWN] += 60;
                    }
                }
            }
            if (wolf.Hunger < 70)
            {
                if (deadSheepNearby)
                {
                    ia[(int)Actions.EAT] += 150;

                    Console.WriteLine($"[IA] el lobo en ({wolf.X},{wolf.Y}) tiene un cuerpo cerca — EAT += 150");

                }
                else
                {
                    var nearest = FindNearest(world, wolf, ObjectType.DEAD_SHEEP);
                    if (nearest.HasValue)
                    {
                        int dx = nearest.Value.dx;
                        int dy = nearest.Value.dy;

                        Console.WriteLine($"[IA] el lobo en ({wolf.X},{wolf.Y}) busca un cadaver ({wolf.X + dx},{wolf.Y + dy})");

                        if (dx < 0)
                            ia[(int)Actions.MOVE_LEFT] += 60;
                        if (dx > 0)
                            ia[(int)Actions.MOVE_RIGHT] += 60;
                        if (dy < 0)
                            ia[(int)Actions.MOVE_UP] += 60;
                        if (dy > 0)
                            ia[(int)Actions.MOVE_DOWN] += 60;
                    }
                }
            }
            if (wolf.Thirst < 50)
            {
                if (world.GetObjectTypeAt(wolf.X, wolf.Y, wolf) == ObjectType.WATER)
                    ia[(int)Actions.DRINK] += 150;
                else
                    MoveTowards(world, wolf, ObjectType.WATER, ia);
            }
            if (wolf.Energy < 30)
                ia[(int)Actions.REST] += 40;
            //penalizaciones
            if (world.GetObjectTypeAt(wolf.X, wolf.Y, wolf) != ObjectType.WATER)
                ia[(int)Actions.DRINK] -= 1000;
            if (!sheepNearby)
                ia[(int)Actions.ATTACK] -= 1000;
            if (!deadSheepNearby)
                ia[(int)Actions.EAT] -= 1000;

            Utils.AddRandomValues(ia, -20, 20);
            int index = Utils.GetHighScoreIndex(ia);

            return (Actions)index;
        }


        public static Actions ExecuteSheep(Sheep sheep, World world)
        {
            if (sheep.ForceRest)
                return Actions.REST;
            int[] ia = new int[(int)Actions.COUNT];

            if (world.GetObjectTypeAt(sheep.X, sheep.Y - 1) == ObjectType.WOLF)
            {
                ia[(int)Actions.MOVE_DOWN] += 100;
                ia[(int)Actions.MOVE_RIGHT] += 90;
                ia[(int)Actions.MOVE_LEFT] += 90;
            }
            if (world.GetObjectTypeAt(sheep.X, sheep.Y + 1) == ObjectType.WOLF)
            {
                ia[(int)Actions.MOVE_UP] += 100;
                ia[(int)Actions.MOVE_RIGHT] += 90;
                ia[(int)Actions.MOVE_LEFT] += 90;
            }
            if (world.GetObjectTypeAt(sheep.X + 1, sheep.Y) == ObjectType.WOLF)
            {
                ia[(int)Actions.MOVE_LEFT] += 100;
                ia[(int)Actions.MOVE_UP] += 90;
                ia[(int)Actions.MOVE_DOWN] += 90;
            }
            if (world.GetObjectTypeAt(sheep.X - 1, sheep.Y) == ObjectType.WOLF)
            {
                ia[(int)Actions.MOVE_RIGHT] += 100;
                ia[(int)Actions.MOVE_UP] += 90;
                ia[(int)Actions.MOVE_DOWN] += 90;
            }
            if (sheep.Hunger < 50)
            {
                if (world.GetObjectTypeAt(sheep.X, sheep.Y, sheep) == ObjectType.GRASS)
                    ia[(int)Actions.EAT] += 150;
                else
                    MoveTowards(world, sheep, ObjectType.GRASS, ia);
            }
            if (sheep.Thirst < 50)
            {
                if (world.GetObjectTypeAt(sheep.X, sheep.Y, sheep) == ObjectType.WATER)
                    ia[(int)Actions.DRINK] += 150;
                else
                    MoveTowards(world, sheep, ObjectType.WATER, ia);
            }
            if (sheep.Energy < 30)
                ia[(int)Actions.REST] += 40;
            //penalizaciones
            if (world.GetObjectTypeAt(sheep.X, sheep.Y, sheep) != ObjectType.GRASS)
                ia[(int)Actions.EAT] -= 1000;
            if (world.GetObjectTypeAt(sheep.X, sheep.Y, sheep) != ObjectType.WATER)
                ia[(int)Actions.DRINK] -= 1000;
            if (sheep.Hunger > 90)
                ia[(int)Actions.EAT] -= 1000;
            if (sheep.Thirst > 90)
                ia[(int)Actions.DRINK] -= 1000;
            if (sheep.Energy > 90)
                ia[(int)Actions.REST] -= 1000;

            Utils.AddRandomValues(ia, -20, 20);
            int index = Utils.GetHighScoreIndex(ia);
            return (Actions)index;
        }

        private static void MoveTowards(World world, Animal animal, ObjectType target, int[] ia)
        {
            if (world.GetObjectTypeAt(animal.X, animal.Y - 1) == target)
                ia[(int)Actions.MOVE_UP] += 60;
            if (world.GetObjectTypeAt(animal.X, animal.Y + 1) == target)
                ia[(int)Actions.MOVE_DOWN] += 60;
            if (world.GetObjectTypeAt(animal.X + 1, animal.Y) == target)
                ia[(int)Actions.MOVE_RIGHT] += 60;
            if (world.GetObjectTypeAt(animal.X - 1, animal.Y) == target)
                ia[(int)Actions.MOVE_LEFT] += 60;
        }

        private static (int dx, int dy)? FindNearest(World world, Animal source, ObjectType targetType)
        {
            int radius = 3;

            for (int dist = 1; dist <= radius; dist++)
            {
                for (int dx = -dist; dx <= dist; dx++)
                {
                    for (int dy = -dist; dy <= dist; dy++)
                    {
                        if (Math.Abs(dx) + Math.Abs(dy) != dist)
                            continue;

                        int nx = source.X + dx;
                        int ny = source.Y + dy;

                        if (!world.IsInBounds(nx, ny))
                            continue;

                        if (world.GetObjectTypeAt(nx, ny) == targetType)
                            return (dx, dy);
                    }
                }
            }

            return null;
        }
    }
}
