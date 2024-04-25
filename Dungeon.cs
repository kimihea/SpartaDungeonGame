using SpartaDungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpartaDungeon.ItemList;

namespace SpartaDungeon
{
    internal class Dungeon
    {

        public float reward;
        public int recommandDef;

        Random random = new Random();
        public float DungeonClear(int ad, float reward)
        {
            float total = reward * ((100f + (float)random.Next(ad, 2 * ad)) / 100f);
            return total;
        }
        public int LoseHp(int def, int hp)
        {
            int bonus = def - recommandDef;
            int randomLose = random.Next(20, 35);
            return randomLose - bonus;

        }
        public (float, int) EnterTheDungeon(int def, int ad, int hp, float reward)
        {
            if (def < recommandDef)
            {
                int fail = random.Next(0, 9);
                if (fail < 4)
                {
                    hp -= LoseHp(def, hp) / 2;
                    return (0f, hp);
                }
                else
                {
                    hp -= LoseHp(def, hp);
                    return (DungeonClear(ad, reward), hp);
                }

            }
            else
            {
                DungeonClear(ad, reward);
                hp -= LoseHp(def, hp);
                return (DungeonClear(ad, reward), hp);
            }
        }
        public void DisplayDungeonMenu()
        {
            Program.MethodPrintLine();
            Program.MethodPrintColorOrange("던전입장");
            Program.MethodPrintColorRed(1);
            Console.Write(". 쉬운 던전        | 방어력");
            Program.MethodPrintColorRed(5);
            Console.WriteLine(" 이상 권장");

            Program.MethodPrintColorRed(2);
            Console.Write(". 일반 던전        | 방어력");
            Program.MethodPrintColorRed(11);
            Console.WriteLine(" 이상 권장");

            Program.MethodPrintColorRed(3);
            Console.Write(". 어려움 던전      | 방어력");
            Program.MethodPrintColorRed(17);
            Console.WriteLine(" 이상 권장");

            Program.MethodPrintColorRed(0);
            Console.WriteLine(". 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요");
            Program.MethodPrintLine();
        }
        public void DisplayClear(string str, int hp, int gold, int afterhp, float money)
        {
            Program.MethodPrintLine();
            Program.MethodPrintColorOrange("던전 클리어");
            Console.WriteLine("축하합니다!!");
            Console.WriteLine(str + "던전을 클리어 하셨습니다");
            Console.WriteLine("[탐험결과]");
            Console.Write("체력 ");
            Program.MethodPrintColorRed(hp);
            Console.Write(" -> ");
            Program.MethodPrintColorRed(afterhp);
            Console.Write("\nGold");
            Program.MethodPrintColorRed(gold);
            Console.Write(" -> ");
            Program.MethodPrintColorRed(gold + Convert.ToInt32(money));
            Console.WriteLine("\n\n0.나가기");
            Console.Write("\n원하시는 행동을 입력해주새요");
            Program.MethodPrintLine();
        }
        public void DisplayFail(string str, int hp, int gold, int afterhp)
        {
            Program.MethodPrintLine();
            Program.MethodPrintColorOrange("던전 공략 실패");
            Console.WriteLine("당신은 실패했습니다!!");
            Console.WriteLine(str + "던전을 공략하지 못했습니다");
            Console.WriteLine("[탐험결과]");
            Console.Write("체력 ");
            Program.MethodPrintColorRed(hp);
            Console.Write(" -> ");
            Program.MethodPrintColorRed(afterhp);
            Console.Write("\nGold");
            Program.MethodPrintColorRed(gold);
            Console.Write(" -> ");
            Program.MethodPrintColorRed(gold);
            Console.WriteLine("\n\n0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주새요");
            Program.MethodPrintLine();
        }
        public void PlayQuit()
        {
            int select = Console.Read() - 48;
            Console.ReadLine();
            if (select == 0) { return; }
            else
            {
                Console.WriteLine("잘못된 입력입니다");
                PlayQuit();
            }
        }
        public (float, int) PlayDungeon(int def, int ad, int hp, int gold)
        {
            //Dungeon dungeon = new Dungeon();
            int select = Console.Read() - 48;
            Console.ReadLine();
            float money;
            int afterhp;
            switch (select)
            {
                case 0:
                    Program.DisplayStart();
                    return (0, 0);
                case 1:
                    reward = 1000f;
                    recommandDef = 5;
                    (money, afterhp) = EnterTheDungeon(def, ad, hp, reward);

                    if (money != 0f) { DisplayClear("쉬움", hp, gold, afterhp, money); }
                    else { DisplayFail("쉬움", hp, gold, afterhp); }
                    PlayQuit();
                    return (money, afterhp);
                case 2:
                    reward = 1700;
                    recommandDef = 11;
                    (money, afterhp) = EnterTheDungeon(def, ad, hp, reward);
                    if (money != 0f) { DisplayClear("보통", hp, gold, afterhp, money); }
                    else { DisplayFail("보통", hp, gold, afterhp); }
                    PlayQuit();
                    return (money, afterhp);
                case 3:
                    reward = 2500;
                    recommandDef = 17;
                    (money, afterhp) = EnterTheDungeon(def, ad, hp, reward);
                    if (money != 0f) { DisplayClear("어려움", hp, gold, afterhp, money); }
                    else { DisplayFail("어려움", hp, gold, afterhp); }
                    PlayQuit();
                    return (money, afterhp);
                default:
                    Console.WriteLine("잘못된 입력입니다");
                    DisplayDungeonMenu();
                    PlayDungeon(def, ad, hp, gold);
                    return (0f, 0);
            }
        }

    }
}
