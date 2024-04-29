using SpartaDungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon
{

    internal class Inventory
    {
        public ItemList itemlist = new ItemList();
        public int Gold = 8000;

    
        public void DisplayInventory()
        {

            //인벤토리출력
            Program.MethodPrintLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리");
            Console.ResetColor();
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("\n[아이템목록]\n");
            itemlist.DisplayOwnedItemList(true);
            Console.WriteLine("\n");
            Program.MethodPrintColorRed(1);
            Console.Write(". 장착 관리\n");
            Program.MethodPrintColorRed(0);
            Console.WriteLine(". 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Program.MethodPrintLine();
        }
        public int EquitItemDef()
        {
            return itemlist.EquipedItemDef();
        }
        public int EquitItemHp()
        {
            return itemlist.EquipedItemHp();
        }
        public int EquitItemAd()
        {
            return itemlist.EquipedItemAd();
        }
        public void PlayInventory()
        {
            int input = Console.Read() - 48;
            Console.ReadLine();
            switch (input)
            {
                case 1:
                    DisplayInventoryManage();
                    PlayInventoryManage();
                    break;
                case 0:
                    return;
                default:
                    Console.WriteLine("잘못된 입력입니다");
                    DisplayInventoryManage();
                    PlayInventoryManage();
                    break;
            }
        }

        public void DisplayItemShop(bool isSelectBuy = false, bool isSelectSell = false)
        {
            Program.MethodPrintLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("상점");
            if (isSelectBuy) Console.Write(" - 아이템구매");
            if (isSelectSell) Console.Write(" - 아이템판매");
            Console.ResetColor();
            Console.WriteLine("\n" +
                "필요한 아이템을 관리할 수 있습니다.");
            Console.WriteLine("\n[보유 골드]");
            Console.WriteLine(Gold + "G");
            Console.WriteLine("\n[아이템 목록]");
            itemlist.DisplayItemList(isSelectBuy | isSelectSell);
            Console.Write("\n");
            if (!isSelectSell && !isSelectBuy)
            {
                Program.MethodPrintColorRed(1);
                Console.Write(". 아이템구매\n");
            }
            if (!isSelectSell && !isSelectBuy)
            {
                Program.MethodPrintColorRed(2);
                Console.Write(". 아이템판매\n");
            }
            Program.MethodPrintColorRed(0);
            Console.WriteLine(". 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Program.MethodPrintLine();

        }

        public void PlayItemShopManage(bool isSelectBuy = false, bool isSelectSell = false)
        {
            string? select = Console.ReadLine();    //항목이 두자릿수가 될때를 위해서 ReadLine사용
            int number;
            if (int.TryParse(select, out number))
            {
                if (number == 0)
                {
                    DisplayItemShop();
                    PlayItemShop();
                }
                else
                {
                    if (isSelectBuy)
                    {
                        Gold = itemlist.Buyitem(number, Gold);
                        DisplayItemShop(true, false);
                        PlayItemShopManage(true, false);
                    }
                    if (isSelectSell)
                    {
                        Gold = itemlist.Sellitem(number, Gold);
                        DisplayItemShop(false, true);
                        PlayItemShopManage(false, true);
                    }

                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다");
                DisplayItemShop(true);
                PlayItemShopManage();

            }
        }
        public void PlayItemShop()
        {

            int input = Console.Read() - 48;
            Console.ReadLine();
            switch (input)
            {
                case 1:
                    DisplayItemShop(true, false);
                    PlayItemShopManage(true, false);
                    break;
                case 2:
                    DisplayItemShop(false, true);
                    PlayItemShopManage(false, true);
                    break;

                case 0:
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다");
                    DisplayItemShop();
                    PlayItemShop();
                    break;
            }
        }

        public void DisplayInventoryManage()
        {

            Program.MethodPrintLine();


            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.ResetColor();
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다");
            itemlist.DisplayOwnedItemList(true);

            Console.WriteLine("\n");
            Program.MethodPrintColorRed(0);
            Console.WriteLine(". 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요\n");
            Program.MethodPrintLine();
        }

        public void PlayInventoryManage()
        {


            int count = itemlist.CountOwnedItemList();  //보유한 아이템의 수
            int[] index = new int[count];
            for (int i = 0; i < count; i++)
            {
                index[i] = itemlist.OwnedItemIndex(i + 1);
            }
            //select에 따라 if-else문 실행
            string select = Console.ReadLine();
            if (int.TryParse(select, out int number))
            {
                if (number == 0)
                {
                    DisplayInventory();
                    PlayInventory();
                }
                else
                {
                    if (number <= count)
                    {
                        if (!itemlist.isEquip(index[number - 1] + 1))
                        {
                            itemlist.OptionUnCheck(itemlist.OptionOfItem(index[number - 1]));
                            itemlist.EquipItem(index[number - 1] + 1);
                        }
                        else
                        {
                            itemlist.OptionUnCheck(itemlist.OptionOfItem(index[number - 1]));
                        }
                        DisplayInventoryManage();
                        PlayInventoryManage();
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다");
                        DisplayInventoryManage();
                        PlayInventoryManage();
                    }

                }
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다");
                DisplayInventoryManage();
                PlayInventoryManage();

            }
        }
       
    }
}
