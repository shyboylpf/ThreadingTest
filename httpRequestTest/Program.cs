using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Timers;
using System.Threading;

namespace httpRequestTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            System.Timers.Timer tmr = new System.Timers.Timer();
            tmr.Interval = 3000;
            tmr.Elapsed += worker;
            tmr.Start();
            Thread.Sleep(Timeout.Infinite);
        }

        private static void worker(object sender, ElapsedEventArgs e)
        {
            try
            {
                var httpWebRequest = WebRequest.Create("http://115.28.182.39:8999/api/QueryCurrentInfusionList");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "POST";
                //httpWebRequest.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.70 Safari/537.36");
                string WardNo = "2913";
                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = "{\"Ward\": \"" + WardNo + "\"}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var httpResponse = httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string responseText = streamReader.ReadToEnd();
                    Console.WriteLine(responseText);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}