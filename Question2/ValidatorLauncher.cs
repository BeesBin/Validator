using System;

namespace Question2
{
    class ValidatorLauncher
    {
        static void Main(string[] args)
        {
            Validator validator = new Validator();
            string strId, strPsw, str_busid, str_cardinfo;

            while (true)
            {
                // Login Part, 성공 시 break;
                string[] words = Console.ReadLine().Split(' ');

                strId = words[0];
                strPsw = words[1];

                if(validator.CheckIdPsw(strId, strPsw))
                {
                    Console.WriteLine("LOGIN SUCCESS");
                    break;
                }
                else
                {
                    Console.WriteLine("LOGIN FAIL");
                }
            }

            while (true)
            {
                // Inspection Part, busid가 아닌, 로그아웃 입력 시 break;
                while (true)
                {
                    // Card Validation Part, 완료 시 break;
                    str_busid = Console.ReadLine();
                    if (str_busid.Equals("LOGOUT"))
                    {
                        break;
                    }
                    else if(str_busid.Length < 7 || !str_busid.Substring(0, 4).Equals("BUS_"))
                    {
                        Console.WriteLine("Wrong Bus ID");
                        continue;
                    }

                    string strTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                    while (true)
                    {
                        str_cardinfo = Console.ReadLine();

                        if (str_cardinfo.Equals("DONE"))
                        {
                            break;
                        }
                        else if(str_cardinfo.Length < 30)
                        {
                            Console.WriteLine("Wrong Card Info");
                            continue;
                        }
                        validator.InspectCard(strTime, strId, str_busid, str_cardinfo);
                    }
                }
            }
        }
    }
}
