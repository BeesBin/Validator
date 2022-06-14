using System;
using System.Threading;
using System.Net;
using System.Collections.Generic;

// 도구 -> NuGet 패키지 관리자 -> 솔루션용 NuGet 패키지 관리 -> Newtonsoft .. 설치
using Newtonsoft.Json.Linq;

namespace Question4
{
    class ReportHttpServer
    {
        static Mutex muSeq = new Mutex();
        static Mutex muEnd = new Mutex();
        public HttpListener listener;
        private Dictionary<int, Thread> dicThread = new Dictionary<int, Thread>();

        public void DoHttpServer(Object obj)
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://127.0.0.1:8081/");
            listener.Start();

            try
            {
                while (true)
                {
                    var context = listener.GetContext();

                    Thread thContext = new Thread(DoContext);
                    thContext.Start(context);
                }
            }
            catch(HttpListenerException e)
            {

            }
        }

        private void DoContext(Object obj)
        {
            // HttpListener .GetContext -> HttpListenerContext
            HttpListenerContext context = (HttpListenerContext)obj;

            JObject resJson = new JObject();
            string[] words = context.Request.Url.LocalPath.Split('/');
            string command = words[1];

            if (context.Request.HttpMethod == "GET")
            {
                switch (command)
                {
                    case "REPORT":

                        string manageId = words[2];
                        string strDate = words[3];

                        string report = ReportHandling.MakeReport(strDate);
                        muSeq.WaitOne();
                        string reportId = ReportHandling.IncreaseSeqNo().ToString();
                        muSeq.Rea

                }
            } 
            else if(context.Request.HttpMethod == "POST")
            {

            }


        }
    }
}
