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
        /* 1. 문자열을 입력 받아 Base64로 Encoding한 값을 출력하고, 그 값을 다시 Decoding하여 입력한 값과
        동일한지 확인해 보시오.
        Ex) ‘This is a Base64 test.’ → ‘VGhpcyBpcyBhIEJhc2U2NCB0ZXN0Lg==‘ → ‘This is a Base64 test.’*/
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

        /* 2. 1번에서 입력 받은 값을 SHA-256으로 Encryption 해서 결과를 출력해 보시오.
Ex) ‘1234’ -> ‘03AC674216F3E15C761EE1A5E255F067953623C8B388B4459E13F978D7C846F4' */

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
