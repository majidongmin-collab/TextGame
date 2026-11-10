using System;
using System.Net.Mail;

namespace TextGame
{
    internal class Program
    {
        private static int DataCount = 6;

        private struct Character
        {
            public string Name;
            public float MaxHealth;
            public float Attack;
            public float Defence;

            public void Damaged(float Dmg)
            {
                MaxHealth -= Dmg;
                MaxHealth = Math.Clamp(MaxHealth, 0.0f, MaxHealth);
            }

            public override string ToString()
            {
                return $"{Name},{MaxHealth},{Attack},{Defence}";
            }
        }        

        static void PrintStatus(Character character)
        {
            string hpBar = "";   

            if (30 == character.MaxHealth)
            {
                hpBar = new string('■', 10);
            }
            else
            {
                hpBar = new string('■', (int)character.MaxHealth / 3) + new string('□', 10 - ((int)character.MaxHealth / 3));
            }

            Console.WriteLine("=========== 상태창 ===========");
            Console.WriteLine($"이름 : {character.Name}");
            Console.WriteLine($"체력 : {character.MaxHealth}/30 {hpBar}");
            Console.WriteLine($"ATK : {character.Attack}\tDEF : {character.Defence}");
            Console.WriteLine("==============================");
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

            Character player = CreateCharacter("player", 30, 10, 8);
            //PrintStatus(player);
            
            Character monster = CreateCharacter("슬라임", 30, 8, 2);

            string[] names = new string[]
            {
                "Ork", "Lizard", "Dragon", "Ghost","Daemon"
            };

            Random random = new Random();
            float damage = player.Attack + random.NextSingle() * 3.0f;

            PrintStatus(monster);

            while (true)
            {
                
                Console.Write("선택 (1:공격) : ");

                if (int.TryParse(Console.ReadLine(), out int index) == false || index != 1)
                {
                    Console.WriteLine("번호 입력 오류");
                    continue;
                }

                float dmg = player.Attack;
                monster.Damaged(dmg);
                Console.Clear(); 
                PrintStatus(monster);
            }//while(true)         
             

        }

        private static void WriteFile(string[] names, Random random, string path, string name)
        {
            string print = "";

            Character[] MonsterDatas = new Character[DataCount];
            for (int i = 0; i < MonsterDatas.Length; i++)
            {
                float maxHealth = random.NextSingle() * 10.0f;
                float attack = random.NextSingle() * 10.0f;
                float deffence = random.NextSingle() * 10.0f;

                MonsterDatas[i] = new Character()
                {
                    Name = names[i],
                    MaxHealth = maxHealth,
                    Attack = attack,
                    Defence = deffence,
                };
                //Console.WriteLine($"{i} : {weaponDatas[i]}");

                print += $"{MonsterDatas[i]}\n";
            }            


            path = Path.Combine(path, name);
            File.WriteAllText(path, print);

            path = Path.GetFullPath(path);
            Console.WriteLine($"{path} 파일에 저장됨");
        }
    }
}
