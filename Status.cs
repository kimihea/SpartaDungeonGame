using SpartaDungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SpartaDungeon
{
    internal class Status
    {
        //상태창 변수
        public int level = 1;
        public string name = "mynameisreal";
        public string classType = "전사";
        public int Ad = 10;   //attackDamage
        public int Def = 5;   //defense
        public int Hp = 100;  //healthPoints
        public int ItemAd = 0;
        public int ItemDef = 0;
        public int ItemHp = 0;
        public int Gold = 800;

        public void DisplayStatus()
        {
            Program.MethodPrintLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("상태 보기");
            Console.ResetColor();
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.Write("\nLv.");
            Program.MethodPrintColorRed(level);
            Console.Write("\n" + name + "(" + classType + ")");
            Console.Write("\n공격력 : ");
            Program.MethodPrintColorRed(Ad);
            if (ItemAd != 0) { Console.Write(" + "); Program.MethodPrintColorRed(ItemAd); }
            Console.Write("\n방어력 : ");
            Program.MethodPrintColorRed(Def);
            if (ItemDef != 0) { Console.Write(" + "); Program.MethodPrintColorRed(ItemDef); }
            Console.Write("\n체력 : ");
            Program.MethodPrintColorRed(Hp);
            if (ItemHp != 0) { Console.Write(" + "); Program.MethodPrintColorRed(ItemHp); }

            Console.Write("\nGold : ");
            Program.MethodPrintColorRed(Gold);
            Console.WriteLine("\n0.나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Program.MethodPrintLine();
        }
        public void PlayStatus()
        {
            int select = Console.Read() - 48;
            Console.ReadLine();
            if (select == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다");
                DisplayStatus();
                PlayStatus();
            }
        }
    }
}