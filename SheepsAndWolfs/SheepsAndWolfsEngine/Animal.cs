using System;
namespace SheepsAndWolfsEngine
{
    public abstract class Animal : GameObject
    {
        public float Hunger { get; set; } = 100f;
        public float Thirst { get; set; } = 100f;
        public float Energy { get; set; } = 100f;
        public float Health { get; private set; } = 100f;
        public bool IsDead { get; private set; } = false;
        public bool ForceRest { get; private set; } = false;


        public Animal(int x, int y) : base(x, y)
        {

        }
        public void UpdateStats()
        {
            Hunger = Math.Max(0, Hunger - 2);
            Thirst = Math.Max(0, Thirst - 2);
            Energy = Math.Max(0, Energy - 2);
            if (Hunger == 0 || Thirst == 0)
                Health = Math.Max(0, Health - 5);
            if (Health == 0)
                IsDead = true;
            if (Energy < 10)
                ForceRest = true;
            if (Energy >= 50)
                ForceRest = false;
        }
        public void ReceiveDamage(float amount)
        {
            Health = Math.Max(0, Health - amount);
            if (Health == 0)
                IsDead = true;
        }


        public override bool CanExecuteAction(Actions action, World world)
        {
            if (IsDead)
                return false;
            switch (action)
            {
                case Actions.MOVE_UP:
                    return world.CanMoveAnimal(this, _x, _y - 1);
                case Actions.MOVE_DOWN:
                    return world.CanMoveAnimal(this, _x, _y + 1);
                case Actions.MOVE_LEFT:
                    return world.CanMoveAnimal(this, _x - 1, _y);
                case Actions.MOVE_RIGHT:
                    return world.CanMoveAnimal(this, _x + 1, _y);
                case Actions.EAT:
                    if (this is Sheep)
                        return world.GetObjectTypeAt(_x, _y, this) == ObjectType.GRASS;
                    if (this is Wolf)
                    {
                        return world.GetObjectTypeAt(_x, _y - 1) == ObjectType.DEAD_SHEEP ||
                               world.GetObjectTypeAt(_x, _y + 1) == ObjectType.DEAD_SHEEP ||
                               world.GetObjectTypeAt(_x - 1, _y) == ObjectType.DEAD_SHEEP ||
                               world.GetObjectTypeAt(_x + 1, _y) == ObjectType.DEAD_SHEEP;
                    }
                    return false;
                case Actions.ATTACK:
                    if (this is Wolf)
                    {
                        return world.GetObjectAt(_x, _y - 1) is Sheep s1 && !s1.IsDead ||
                               world.GetObjectAt(_x, _y + 1) is Sheep s2 && !s2.IsDead ||
                               world.GetObjectAt(_x - 1, _y) is Sheep s3 && !s3.IsDead ||
                               world.GetObjectAt(_x + 1, _y) is Sheep s4 && !s4.IsDead;
                    }
                    return false;
                case Actions.DRINK:
                    return world.GetObjectTypeAt(_x, _y, this) == ObjectType.WATER;

                case Actions.REST:
                    return true;
            }
            return false;
        }

        public override void ExecuteAction(Actions action, World world)
        {
            if (CanExecuteAction(action, world))
            {
                switch (action)
                {
                    case Actions.MOVE_UP:
                        _y--;
                        break;
                    case Actions.MOVE_DOWN:
                        _y++;
                        break;
                    case Actions.MOVE_LEFT:
                        _x--;
                        break;
                    case Actions.MOVE_RIGHT:
                        _x++;
                        break;
                    case Actions.ATTACK:
                        if (this is Wolf)
                        {
                            Sheep target = null;

                            var up = world.GetObjectAt(_x, _y - 1) as Sheep;
                            if (up != null && !up.IsDead)
                                target = up;

                            if (target == null)
                            {
                                var down = world.GetObjectAt(_x, _y + 1) as Sheep;
                                if (down != null && !down.IsDead)
                                    target = down;
                            }

                            if (target == null)
                            {
                                var left = world.GetObjectAt(_x - 1, _y) as Sheep;
                                if (left != null && !left.IsDead)
                                    target = left;
                            }

                            if (target == null)
                            {
                                var right = world.GetObjectAt(_x + 1, _y) as Sheep;
                                if (right != null && !right.IsDead)
                                    target = right;
                            }

                            if (target != null)
                            {
                                target.ReceiveDamage(30f);
                                Energy = Math.Max(0, Energy - 10f);


                                Console.WriteLine($"[ACCION] el lobo en ({_x},{_y}) ataca a la oveja ({target.X},{target.Y}) — oveja HP: {target.Health}");
                            }
                        }
                        break;
                    case Actions.EAT:
                        if (this is Wolf)
                        {
                            if (Hunger >= 90f)
                                break;
                            Sheep target = null;

                            var up = world.GetObjectAt(_x, _y - 1) as Sheep;
                            if (up != null && up.IsDead && up.MeatLeft > 0)
                                target = up;

                            if (target == null)
                            {
                                var down = world.GetObjectAt(_x, _y + 1) as Sheep;
                                if (down != null && down.IsDead && down.MeatLeft > 0)
                                    target = down;
                            }

                            if (target == null)
                            {
                                var left = world.GetObjectAt(_x - 1, _y) as Sheep;
                                if (left != null && left.IsDead && left.MeatLeft > 0)
                                    target = left;
                            }

                            if (target == null)
                            {
                                var right = world.GetObjectAt(_x + 1, _y) as Sheep;
                                if (right != null && right.IsDead && right.MeatLeft > 0)
                                    target = right;
                            }

                            if (target != null)
                            {
                                Hunger = 100f;
                                target.ConsumeMeat();


                                Console.WriteLine($"[ACCION] el lobo en ({_x},{_y}) come del cuerpo ({target.X},{target.Y}) — Queda {target.MeatLeft} de carne");


                                if (target.MeatLeft == 0)
                                    world.MarkForRemoval(target);
                            }
                        }
                        else if (this is Sheep)
                            Hunger = Math.Min(100f, Hunger + 50f);
                        break;
                    case Actions.DRINK:
                        Thirst = Math.Min(100f, Thirst + 50f);
                        break;
                    case Actions.REST:
                        Energy = Math.Min(100f, Energy + 5f);
                        break;
                }
            }
        }

    }
}
