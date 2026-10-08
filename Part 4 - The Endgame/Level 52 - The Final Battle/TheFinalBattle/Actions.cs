namespace TheFinalBattle
{
    public interface IAction
    {
        void Run(Character unit, Battle battle);
    }

    public class DoNothingAction : IAction
    {
        public void Run(Character unit, Battle battle) => Console.WriteLine($"{unit.Name} did NOTHING.");
    }

    public class AttackAction : IAction
    {
        public void Run(Character unit, Battle battle)
        {
            var attackType = unit.DefaultAttack;
            var target = battle.GetEnemyPartyFor(unit).Units[0];
            var attackData = attackType.GenerateAttackData();
            int damageAmount = attackData.Damage;
            target.ReceivedDamage(damageAmount);

            Console.WriteLine($"{unit.Name} used {attackType.Name} on {target.Name}.");
            Console.WriteLine($"{attackType.Name} dealt {damageAmount} to {target.Name}.");
            Console.WriteLine($"{target.Name} is now at {target.GetHPStatus()}");

            if (target.CurrentHP == 0)
            {
                Console.WriteLine($"{target.Name} has been defeated!");
                battle.GetPartyFor(target).Units.Remove(target);
            }
        }
    }
}
