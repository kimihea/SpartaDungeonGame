using SpartaDungeon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpartaDungeon.Inventory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SpartaDungeon
{
    internal class ItemList
    {
        public static int len = 11;
        public Item[] itemList = new Item[len];
        int i;

        public ItemList()
        {

            itemList[0] = new Item("스파르타의 창", "공격력", 7, "스파르타의 전사들이 사용했다는 보급형 창입니다.", "3000");
            itemList[1] = new Item("청동 도끼 ", "공격력", 5, "페르시아 전사들이 사용했다는 낡은 도끼입니다.", "1500");
            itemList[2] = new Item("낡은 검", "공격력", 2, "쉽게 볼 수 있는 낡은 검입니다", "600");
            itemList[3] = new Item("스파르타의 갑옷", "방어력", 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", "3500");
            itemList[4] = new Item("무쇠 갑옷", "방어력", 10, "페르시의 백인대장들이 사용했다는 갑옷입니다.", "2000");
            itemList[5] = new Item("가죽 갑옷", "방어력", 3, "페르시아의 전사들이 사용했다는 갑옷입니다. 창을 막을 수 있을지 모르겠네요", "500");
            itemList[6] = new Item("수련자 갑옷", "방어력", 5, "수련에 도움을 주는 갑옷입니다", "1000");
            itemList[7] = new Item("스파르타의 군화", "방어력", 7, "스파르타의 전사들이 사용했다는 군화입니다 ", "2000");
            itemList[8] = new Item("낡은 가죽 군화", "방어력", 2, "구멍이 뚫릴 듯한 가죽 군화입니다", "500");
            itemList[9] = new Item("스파르타의 투구", "방어력", 7, "스파르타의 전사들이 사용했다는 투구입니다.", "2000");
            itemList[10] = new Item("찌그러진 투구", "방어력", 4, "어딘가 한대 맞은 듯한 투구입니다.", "500");
        }
        public int OwnedItemIndex(int num)    // num은 내가 가지고 있는 아이템의 수량
        {                                      ////내가 보유한 아이템의 num번째 index를 반환하는 함수
            int j = 0;
            int temp = 0;
            for (i = 0; i < len; i++)
            {
                if (isBuy(i + 1))
                {
                    temp = i;
                    j++;
                }
                if (j == num) break;
            }
            return temp;
        }

        public int EquipedItemDef()
        {
            int value = 0;
            for (i = 0; i < len; i++)
            {
                if (itemList[i].isEquip == true && itemList[i].option == "방어력")
                {
                    value += itemList[i].value;
                }
            }
            return value;
        }
        public int EquipedItemHp()
        {
            int value = 0;
            for (i = 0; i < len; i++)
            {
                if (itemList[i].isEquip == true && itemList[i].option == "체력")
                {
                    value += itemList[i].value;
                }
            }
            return value;
        }
        public int EquipedItemAd()
        {
            int value = 0;
            for (i = 0; i < len; i++)
            {
                if (itemList[i].isEquip == true && itemList[i].option == "공격력")
                {
                    value += itemList[i].value;
                }
            }
            return value;
        }
        public string OptionOfItem(int num)
        {
            return itemList[num - 1].option;
        }
        public void OptionUnCheck(string option)
        {
            for (i = 0; i < len; i++)
            {
                if (itemList[i].isHave)
                {
                    if (itemList[i].option == option)
                    {
                        if (itemList[i].isEquip == true) { itemList[i].isEquip = false; }
                    }
                }
            }
        }
        public void DisplayOwnedItemList(bool isSelectEquip = false)
        {
            int j = 0;
            for (i = 0; i < len; i++)
            {
                if (itemList[i].isHave)
                {
                    j++;
                    PrintDash();
                    Console.Write(j + " ");
                    if (isSelectEquip)
                        itemList[i].EnumItem(true, false);
                }
            }
        }

        public int CountOwnedItemList()
        {
            int count = 0;
            for (i = 0; i < len; i++)
            {
                if (itemList[i].isHave)
                {
                    count++;
                }
            }
            return count;
        }
        public void DisplayItemList(bool isNum = false)
        {
            for (i = 0; i < len; i++)
            {
                {
                    if (isNum)
                    {
                        PrintDash();
                        Program.MethodPrintColorRed(i + 1); //
                        Console.Write(" ");
                        itemList[i].EnumItem();
                    }
                    else
                    {
                        PrintDash();
                        itemList[i].EnumItem();
                    }
                }
            }
        }
        public void PrintDash()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n - ");
            Console.ResetColor();
            Console.Write(" ");
        }
        public void EquipItem(int n)
        {
            itemList[n - 1].isEquip = !itemList[n - 1].isEquip;
        }
        public int Buyitem(int n, int OwnedGold)
        {

            if (!itemList[n - 1].isHave)
            {

                if (OwnedGold >= Convert.ToInt32(itemList[n - 1].price))
                {
                    int temp = Convert.ToInt32(itemList[n - 1].price);
                    itemList[n - 1].isHave = true;
                    Console.WriteLine("구매 완료");
                    return OwnedGold - temp;
                }
                else
                {
                    Console.WriteLine("돈이 부족합니다");
                    return OwnedGold;
                }
            }
            else
            {
                Console.WriteLine("이미 구매한 아이템입니다");
                return OwnedGold;
            }
        }
        public int Sellitem(int n, int OwnedGold)
        {
            if (itemList[n - 1].isHave)
            {
                float temp = Convert.ToInt32(itemList[n - 1].price) * 0.85f;
                itemList[n - 1].isHave = false;
                itemList[n - 1].isEquip = false;
                Console.WriteLine("판매 완료");
                return OwnedGold + Convert.ToInt32(temp);
            }
            else return OwnedGold;
        }
        public bool isBuy(int n)
        {
            return itemList[n - 1].isHave;
        }
        public bool isEquip(int n)
        {
            return itemList[n - 1].isEquip;
        }

        public struct Item
        {
            public string name;
            public string option;
            public string explain;
            public int value;
            public string price;

            public bool isHave = false;
            public bool isEquip = false;


            public Item(string name, string option, int value, string explain, string price)
            {
                this.option = option;
                this.explain = explain;
                this.name = name;
                this.value = value;
                this.price = price;
            }
            public void EnumItem(bool isPrintEquip = true, bool isPrintPrice = true)
            {
                if (isPrintEquip)
                {
                    if (isEquip) Console.Write("[E]");
                }
                Console.Write(name + "      |  " + option);
                Program.MethodPrintColorRed(value);
                Console.Write("  |  " + explain);
                if (isPrintPrice)
                {
                    if (isHave) Console.Write("  |  " + "구매완료");
                    else Console.Write("  |  " + price);
                }

            }
        }

    }

}
