using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OpenAuth.App
{
    public class SendSmsApp
    {
        public SendSmsApp()
        {
            //
        }

        public static void SmsSend(string phone, string content)
        {
            string extno = "106903223908910";//接入码
            string account = "000604";			//用户名
            string password = "GkZ9XH31";	//密码    
            StringBuilder sbTemp = new StringBuilder();
            //POST 传值
            sbTemp.Append("action=send&extno=" + extno + "&account=" + account + "&password=" + password + "&mobile=" + phone + "&content=" + content);
            byte[] bTemp = System.Text.Encoding.GetEncoding("GBK").GetBytes(sbTemp.ToString());
            String postReturn = doPostRequest("http://www.smswang.net:7803/sms", bTemp);
            //Response.Write("Post response is: " + postReturn);  //测试返回结果
        }

        private static String doPostRequest(string url, byte[] bData)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "POST";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
                hwRequest.ContentLength = bData.Length;

                System.IO.Stream smWrite = hwRequest.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                WriteErrLog(err.ToString());
            }

            return strResult;
        }

        private static void WriteErrLog(string strErr)
        {
            Console.WriteLine(strErr);
            System.Diagnostics.Trace.WriteLine(strErr);
        }

        public static void SmsSendSoap(string phone, string content)
        {
            string extno = "106903223908910";//接入码
            string account = "000604";			//用户名
            string password = "GkZ9XH31";	//密码   
            ServiceReference1.Soap57ProviderPortTypeClient ServiceReference2 = new ServiceReference1.Soap57ProviderPortTypeClient();
            string result = ServiceReference2.Submit(account, password, extno, content, phone);
            System.Diagnostics.Debug.WriteLine(result);
        }

    }
}
