using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Question2
{
    class Validator
    {
        // 기계 통
        //  입력받은 Id + Psw 조합이 읽어들인 파일 내에 있는지 체크하는 함수

        // InspectCard 함수 무슨 기능인가?
        // cardInfo가, 다른 파라미터에 주어진 정보에 적절한 지 판정하는 기능
        // CARD_001BUS_001N20171019143610
        public void InspectCard(string startTime, string id, string busID, string cardInfo)
        {
            string strValidateCode;

            string cardBusID = cardInfo.Substring(8, 7);
            string code = cardInfo.Substring(15, 1);
            string rideTime = cardInfo.Substring(16);

            string strInspectTime = DateTime.Now.ToString("yyyyMMddHHmmss");

            if (busID.Equals(cardBusID))
            {
                if (code.Equals("N"))
                {
                    if(CardUtility.HourDiff(strInspectTime, rideTime) < 3)
                    {
                        strValidateCode = "R1";
                    } 
                    else
                    {
                        strValidateCode = "R4";
                    }
                }
                else
                {
                    strValidateCode = "R3";
                }
            }
            else
            {
                strValidateCode = "R2";
            }

            string destFolder = "..\\" + id;
            
            string strFilename = destFolder + "\\" + id + "_" + startTime + ".TXT";
            string strWrite = id + "#" + busID + "#" + cardInfo + "#" + strValidateCode + "#" + strInspectTime + "\n";
            FileStream fs = new FileStream(strFilename, FileMode.Append);
            fs.Write(Encoding.UTF8.GetBytes(strWrite), 0, strWrite.Length);
            fs.Close();
        }
    }
}
