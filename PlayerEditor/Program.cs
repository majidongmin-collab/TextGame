using System;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PlayerEditor
{
    internal class Program
    {
        private struct Character
        {
            public string Name { get; set; }
            public float MaxHealth { get; set; }
            public float Attack { get; set; }
            public float Defence { get; set; }

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

        private static void WriteFile(Character character, string path, string name)
        {
            path = Path.Combine(path, name);

            JsonSerializerOptions option = new JsonSerializerOptions();
            option.WriteIndented = true;

            string text = JsonSerializer.Serialize<Character>(character, option);
            File.WriteAllText(path, text);

            Console.WriteLine($"{path} 파일에 저장됨");
        }

        private static Character ReadJsonFile(string path, string name)
        {
            path = Path.Combine(path, name);

            string content = File.ReadAllText(path);
            Character character = JsonSerializer.Deserialize<Character>(content);

            return character;
        }

        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, "..\\..\\..\\");
            path = Path.GetDirectoryName(path)!;
            path = Path.GetFullPath(path);

            Character player = CreateCharacter("player", 30, 10, 8);

            //WriteFile(player, path, "Player.json");

            while (true)
            {
                Console.Write("선택(1:파일 쓰기, 2:파일 읽기, 3:파일 수정) : ");
                string input = Console.ReadLine()!;

                int select;
                if (int.TryParse(input, out select) == false)
                {
                    Console.WriteLine("잘못된 입력");

                    continue;
                }

                switch (select)
                {
                    case 1:
                        {
                            WriteFile(player, path, "Player.json");
                        }
                        break;

                     case 2:
                         {
                            ReadJsonFile(path, "Player.json");
                            Console.WriteLine($"{player.Name}, {player.MaxHealth}, {player.Attack}, {player.Defence}");                            
                         }
                         break;

                    /*case 3:
                        {
                            EditFile(path, "Player.json);
                        }
                        break;*/

                    default:
                        {
                            Console.WriteLine("잘못된 입력");
                        }
                        break;
                }//switch(select)
            }//while(true)  


        }
    }
}
