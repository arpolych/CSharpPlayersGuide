namespace TheFinalBattle
{
    public class Party
    {
        public List<Character> Characters { get; } = new List<Character>();

        public void AddCharacter(Character newCharacter)
        {
            if (newCharacter != null) Characters.Add(newCharacter);
        }
    }
}
