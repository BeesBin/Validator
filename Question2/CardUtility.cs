using System;
using System.Collections.Generic;
using System.Text;

namespace Question2
{
    class CardUtility
    {
        // 패스워드 암호화 기능
        // 검증 용 시간 차이 계산 기능
        public static long HourDiff(string strTime2, string strTime1)
        {
            DateTime dt1 = DateTime.ParseExact(strTime1, "yyyyMMddHHmmss", null);
            DateTime dt2 = DateTime.ParseExact(strTime2, "yyyyMMddHHmmss", null);

            TimeSpan ts = dt2 - dt1;
            return (long)ts.TotalHours;
        }
    }
}
