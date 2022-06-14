using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PrjEncDec
{
    class EncDec
    {
        static void Base64Sample(string str)
        {
            //string str = "This is a Base64 test.";
            byte[] byteStr = System.Text.Encoding.UTF8.GetBytes(str);
            string encodedStr;
            byte[] decodedBytes;

            Console.WriteLine(str);

            encodedStr = Convert.ToBase64String(byteStr);
            Console.WriteLine(encodedStr);

            decodedBytes = Convert.FromBase64String(encodedStr);
            Console.WriteLine(Encoding.Default.GetString(decodedBytes));
        }

        static void SHA256Sample(string strInput)
        {
            byte[] hashValue;
            //string strInput = "1234";
            byte[] byteInput = System.Text.Encoding.UTF8.GetBytes(strInput);

            SHA256 mySHA256 = SHA256Managed.Create();
            hashValue = mySHA256.ComputeHash(byteInput);

            for (int i=0; i<hashValue.Length; i++)
                Console.Write(String.Format("{0:X2}",hashValue[i]));
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                string strLine = Console.ReadLine();
                if (strLine.Equals("QUIT"))
                    break;

                Base64Sample(strLine);

                SHA256Sample(strLine);
            }
        }
    }
}
