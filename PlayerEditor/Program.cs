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

        private static void WriteJsonFile(Character character, string path, string name)
        {
            path = Path.Combine(path, name);

            JsonSerializerOptions option = new JsonSerializerOptions();
            option.WriteIndented = true;

            string text = JsonSerializer.Serialize<Character>(character, option);
            File.WriteAllText(path, text);

            Console.WriteLine($"{path} 파일에 저장됨");
        }

        private static void ReadJsonFile(string path, string name)
        {
            path = Path.Combine(path, name);

            string content = File.ReadAllText(path);
            Console.WriteLine(content);
        }

        private static void EditJsonFile(string path, string name)
        {
            string filePath = Path.Combine(path, name);
            string content = File.ReadAllText(filePath);
            Character character = JsonSerializer.Deserialize<Character>(content);

            while (true)
            {
                Console.Write("편집할 플레이어 이름 : ");
                string changetName = Console.ReadLine()!;

                if (character.Name != changetName)
                {
                    Console.WriteLine($"'{changetName}'을 잘못입력하거나 존재하지 않음.");
                    continue;      
                }
                break;
            }//while(true)

            int select;
            while (true)
            {
                Console.Write("변경할 스테이터스 입력(1:MaxHealth, 2:Attack, 3:Defence) : ");
                string input2 = Console.ReadLine()!;

                if (int.TryParse(input2, out select) == false)
                {
                    Console.WriteLine("잘못된 입력");
                    continue;
                }

                if (select < 1 || select > 4)
                {
                    Console.WriteLine("잘못된 입력");
                    continue;
                }

                break;
            }//while(true)

            float value;
            while (true)
            {
                Console.Write("값 입력 : ");
                string input3 = Console.ReadLine()!;

                if (float.TryParse(input3, out value) == false)
                {
                    Console.WriteLine("잘못된 입력");
                    continue;
                }

                break;
            }//while(true)

            switch (select)
            {
                case 1:
                    {
                        character.MaxHealth = value;
                    }
                    break;

                case 2:
                    {
                        character.Attack = value;
                    }
                    break;

                case 3:
                    {
                        character.Defence = value;
                    }
                    break;
            }//switch(true)
            WriteJsonFile(character, path, name);

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
                            WriteJsonFile(player, path, "Player.json");
                        }
                        break;

                     case 2:
                         {
                            ReadJsonFile(path, "Player.json");                                                 
                         }
                         break;

                    case 3:
                        {
                            EditJsonFile(path, "Player.json");
                        }
                        break;

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
