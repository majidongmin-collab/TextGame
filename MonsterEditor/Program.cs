using System.Reflection.PortableExecutable;

namespace MonsterEditor
{

    internal class Program
    {
        private static int DataCount = 5;

        public struct Character
        {
            public string Name;
            public float MaxHealth;
            public float Attack;
            public float Defence;

            public override string ToString()
            {
                return $"{Name},{MaxHealth},{Attack},{Defence}";
            }

        }

        private static void WriteFile(string[] names, Random random, string path, string name)
        {
            string print = "";

            Character[] MonsterDatas = new Character[DataCount];
            for (int i = 0; i < MonsterDatas.Length; i++)
            {
                float maxHealth = random.NextSingle() * 10.0f;
                float attack = random.NextSingle() * 100.0f;
                float deffence = random.NextSingle() * 10.0f;

                MonsterDatas[i] = new Character()
                {
                    Name = names[i],
                    MaxHealth = maxHealth,
                    Attack = attack,
                    Defence = deffence,
                };                

                print += $"{MonsterDatas[i]}\n";
            }          

            path = Path.Combine(path, name);
            File.WriteAllText(path, print);

            path = Path.GetFullPath(path);
            Console.WriteLine($"{path} 파일에 저장됨");
        }

        private static void ReadFile(string path, string name)
        {
            path = Path.Combine(path, name);
            //string content = File.ReadAllText(path);
            //Console.WriteLine(content);

            string[] lines = File.ReadAllLines(path);
            //Console.WriteLine(lines.Length);

            Character[] MonsterDatas = new Character[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] tokens = lines[i].Split(',');

                MonsterDatas[i] = new Character()
                {
                    Name = tokens[0],
                    MaxHealth = float.Parse(tokens[1]),
                    Attack = float.Parse(tokens[2]),
                    Defence = float.Parse(tokens[3]),
                };
            }

            foreach (Character data in MonsterDatas)
            {
                Console.WriteLine(data);
            }
        }

        private static void EditFile(string path, string name)
        {
            path = Path.Combine(path, name);

            string[] lines = File.ReadAllLines(path);

            int line;
            while (true)
            {
                Console.Write("편집할 몬스터 번호:");
                string input = Console.ReadLine()!;

                if (int.TryParse(input, out line) == false)
                {
                    Console.WriteLine("다시 입력");

                    continue;
                }

                if (line >= lines.Length)
                {
                    Console.WriteLine($"최대 편집할 수 있는 번호 : {lines.Length - 1}");

                    continue;
                }

                break;
            }

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
            }


            string[] content = lines[line].Split(',');
            int index = select + 1;

            content[index] = value.ToString("F8");

            string print = "";
            for (int i = 0; i < content.Length; i++)
            {
                if (i > 0)
                    print += ",";

                print += content[i];
            }

            lines[line] = print;


            File.WriteAllLines(path, lines);
            Console.WriteLine($"{path} 파일 수정됨");
        }


        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, "..\\..\\..\\");
            path = Path.GetDirectoryName(path)!;


            Random random = new Random(10000);

            string[] names = new string[]
            {
                "Slime", "Ork", "Lizard", "Dragon", "Ghost"
            };

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
                            WriteFile(names, random, path, "Monsters.csv");
                        }
                        break;
                    
                    case 2:
                        {
                            ReadFile(path, "Monsters.csv");
                        }
                        break;
                    
                case 3:
                    {
                        EditFile(path, "Monsters.csv");
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
