using System.Text;

namespace PlayerEditor
{
    internal class Program
    {
        private struct Character
        {
            public string Name;
            public float MaxHealth;
            public float Attack;
            public float Defence;
       

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("{");
                sb.AppendLine($"  \"Name\": \"{Name}\",");
                sb.AppendLine($"  \"MaxHealth\": \"{MaxHealth}\",");
                sb.AppendLine($"  \"Attack\": \"{Attack}\",");
                sb.AppendLine($"  \"Defence\": \"{Defence}\",");
                sb.AppendLine("}");

                return sb.ToString();
            }
        }

        static Character CreateCharacter(string name, float maxHealth, float attack, float defence)
        {
            Character character = new Character();
            character.Name = name;
            character.MaxHealth = maxHealth;
            character.Attack = attack;
            character.Defence = defence;

            return character;
        }

        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, "..\\..\\..\\");
            path = Path.GetDirectoryName(path)!;
            path = Path.GetFullPath(path);

            Character player = CreateCharacter("player", 30, 10, 8);

        

            string fileName = Path.Combine(path, "Player.json");
            //File.WriteAllText(fileName, data.ToString());

            Console.WriteLine($"{fileName} 생성됨");


        }
    }
}
