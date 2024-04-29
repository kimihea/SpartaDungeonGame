using System.ComponentModel.Design;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
namespace SpartaDungeon
{
    class Program
    {
        public static void Main()
        {
            bool active = true;
            Status status = new Status();
            ItemList itemListM = new ItemList();
            Inventory inventory_storage1 = new Inventory();
            //첫실행 때 파일을 만들어주는 코드 
            //string jsonFromFile1 = JsonConvert.SerializeObject(status);
            //File.WriteAllText("status.json", jsonFromFile1);
            //string jsonFromFile2 = JsonConvert.SerializeObject(inventory_storage1);
            //File.WriteAllText("inventory.json", jsonFromFile2);
            
            //첫 실행때 여기는 주석처리
            string jsonFromFile1 = File.ReadAllText("status.json");
            status = JsonConvert.DeserializeObject<Status>(jsonFromFile1);
            string jsonFromFile2 = File.ReadAllText("inventory.json");
            inventory_storage1 = JsonConvert.DeserializeObject<Inventory>(jsonFromFile2);
            string jsonFromFile3 = File.ReadAllText("itemlist.json");
            itemListM = JsonConvert.DeserializeObject<ItemList>(jsonFromFile3);

            //여기까지
            int levelCount = 0;
            DisplayStart();

            //계속 선택을 고르게 한다.
            do
            {
                jsonFromFile3 = JsonConvert.SerializeObject(itemListM);
                jsonFromFile2 = JsonConvert.SerializeObject(inventory_storage1);
                //jsonFromFile1 = JsonConvert.SerializeObject(status);

                status.Gold = inventory_storage1.Gold;
                status.ItemHp = inventory_storage1.EquitItemHp();  
                status.ItemAd = inventory_storage1.EquitItemAd();
                status.ItemDef = inventory_storage1.EquitItemDef();
                status.level = status.level;

                Dungeon dungeon = new Dungeon();


                int select = Console.Read() - 48;
                Console.ReadLine();

                switch (select)
                {
                    case 0:
                        active = false;
                        string json1 = JsonConvert.SerializeObject(status);
                        File.WriteAllText("status.json", json1);
                        string json2 = JsonConvert.SerializeObject(inventory_storage1);
                        File.WriteAllText("inventory.json", json2);
                        break;
                    case 1:
                        status.DisplayStatus();
                        status.PlayStatus();
                        DisplayStart();
                        break;
                    case 2:
                        inventory_storage1.DisplayInventory();
                        inventory_storage1.PlayInventory();
                        DisplayStart();
                        break;
                    case 3:
                        inventory_storage1.DisplayItemShop();
                        inventory_storage1.PlayItemShop();
                        DisplayStart();
                        break;
                    case 4:
                        dungeon.DisplayDungeonMenu();
                        (float reward, int afterhp) = dungeon.PlayDungeon((status.Def+status.ItemDef),(status.Ad+status.ItemAd), status.Hp, status.Gold);
                        if (reward > 0)
                        {
                            levelCount++;
                            if (levelCount == status.level)
                            {
                                status.level++;
                                levelCount = 0;
                            }
                        }
                        inventory_storage1.Gold += Convert.ToInt32(reward);
                        status.Hp = afterhp;
                        DisplayStart();
                        break;
                    case 5:
                        DisplayRest(status.Gold);
                        (status.Hp, inventory_storage1.Gold) = PlayRest(status.Hp, inventory_storage1.Gold);
                        DisplayStart();
                        break;
                    default:
                        Console.WriteLine("잘못된 입력입니다");
                        break;
                }
            } while (active);
        }
        public static void MethodPrintLine()
        {
            Console.Write("\n");
            for (int i = 0; i < 90; i++)
            {
                Console.Write("/");
            }
            Console.Write("\n");
        }
        public static void MethodPrintColorRed(int num)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(num);
            Console.ResetColor();
        }
        public static void MethodPrintColorOrange(string str)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(str);
            Console.ResetColor();
        }
        public static void DisplayStart()
        {
            MethodPrintLine();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.Write("\n0. 나가기 \n1. 상태 보기" + "\n2. 인벤토리" + "\n3. 상점" + "\n4. 던전입장" + "\n5. 휴식하기\n");
            Console.WriteLine("원하시는 행동을 입력해주세요");
            MethodPrintLine();
        }
        public static void DisplayRest(int gold)
        {
            MethodPrintLine();
            MethodPrintColorOrange("휴식하기");
            MethodPrintColorRed(500);
            Console.Write(" G를 내면 체력을 회복할 수 있습니다. (보유 골드 : ");
            MethodPrintColorRed(gold);
            Console.Write(" G)\n");
            MethodPrintColorRed(1);
            Console.WriteLine(". 휴식하기");
            MethodPrintColorRed(0);
            Console.WriteLine(". 나가기");
            MethodPrintLine();
        }
        public static (int, int) PlayRest(int hp, int gold)
        {
            int num = Console.Read() - 48;
            Console.ReadLine();
            if (num == 1)
            {
                Console.WriteLine("Hp가 회복되었습니다");
                gold -= 500;
                hp = 100;
                return (hp, gold);
            }
            else if (num == 0)
            {
                return (hp, gold);
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다");
                PlayRest(hp, gold);
                return (hp, gold);
            }

        }
    }
}
