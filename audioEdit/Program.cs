using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Newtonsoft.Json.Linq;

namespace audioEdit
{
    class Program
    {
        static void Main(string[] args)
        {

            //FileStream AddStream = new FileStream(@"C:\C.mp3", FileMode.Create); //C.MP3
            //BinaryWriter AddWriter = new BinaryWriter(AddStream);

            //FileStream TempStreamA = new FileStream(@"C:\A.mp3", FileMode.Open);
            //BinaryReader TempReaderA = new BinaryReader(TempStreamA);

            //AddWriter.Write(TempReaderA.ReadBytes((int)TempStreamA.Length));
            //TempReaderA.Close();
            //TempStreamA.Close();


            //FileStream TempStreamB = new FileStream(@"C:\B.mp3", FileMode.Open);
            //BinaryReader TempReaderB = new BinaryReader(TempStreamB);

            //AddWriter.Write(TempReaderB.ReadBytes((int)TempStreamB.Length));
            //TempReaderB.Close();
            //TempStreamB.Close();

            //AddWriter.Close();
            //AddStream.Close();

            // 构建一个客户端实例，用于发起请求
            IClientProfile profile = DefaultProfile.GetProfile(
                "cn-shanghai",
                "LTAI1OcByfVyKrmq",
                "5PMJoMMHM9JfJX6fFBgSI2rv9HJzfQ");
            DefaultAcsClient client = new DefaultAcsClient(profile);
            try
            {
                // 构造请求
                CommonRequest request = new CommonRequest();
                request.Domain = "nls-meta.cn-shanghai.aliyuncs.com";
                request.Version = "2019-02-28";
                // 因为是 RPC 风格接口，需指定 ApiName(Action)
                request.Action = "CreateToken";
                // 发起请求，并得到 Response
                CommonResponse response = client.GetCommonResponse(request);
                ;
                System.Console.WriteLine(JObject.Parse(response.Data)["Token"]["ExpireTime"].ToString());
                Console.WriteLine((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
            }
            catch (ServerException ex)
            {
                System.Console.WriteLine(ex.ToString());
            }
            catch (ClientException ex)
            {
                System.Console.WriteLine(ex.ToString());
            }

        }
    }
}
