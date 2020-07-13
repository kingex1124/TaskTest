using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskTest.Model;

namespace TaskTest
{
    /// <summary>
    /// 實驗一包資料，使用多執行緒同時處理
    /// 最後整合成一包資料
    /// </summary>
    public class DataDoing
    {
        public List<DataModel> Data = new List<DataModel>();
      
        public DataDoing()
        {
            for (int i = 0; i < 600000; i++)
            {
                Data.Add(new DataModel()
                {
                    ID = i,
                    Data1 = new Guid().ToString(),
                    Data2 = new Guid().ToString(),
                    Data3 = new Guid().ToString()
                }) ;
            }

            Console.WriteLine(GetObjectSize(JsonConvert.SerializeObject(Data)));
        }

        /// <summary>
        /// 回傳資料大小
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int GetObjectSize(object data)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            byte[] Array;
            bf.Serialize(ms, data);
            Array = ms.ToArray();
            return Array.Length;
        }

        public async void DataCheck()
        {
            List<DataModel> result1 = new List<DataModel>();
            List<DataModel> result2 = new List<DataModel>();
            List<DataModel> result3 = new List<DataModel>();

            Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            var data1 = Data.Take(300000).ToList();
            var data2 = Data.Skip(300000).Take(300000).ToList();
            var data3 = Data.Skip(600000).Take(300000).ToList();

            DateTime start = DateTime.Now;

            try
            {
                TaskFactory taskFactory = new TaskFactory();
                Task task1 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務1-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);

                    foreach (var item in data1)
                    {
                        //Console.WriteLine(string.Format("ID:{0} Data1:{1} Data2:{2} Data3:{3}", item.ID, item.Data1, item.Data2, item.Data3));
                        bool flag = true;
                        if (string.IsNullOrEmpty(item.ID.ToString()))
                            flag = false;
                        if (string.IsNullOrEmpty(item.Data1))
                            flag = false;
                        if (string.IsNullOrEmpty(item.Data2))
                            flag = false;
                        if (string.IsNullOrEmpty(item.Data3))
                            flag = false;
                        if (flag)
                            result1.Add(item);
                    }

                    Console.WriteLine("任務1-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });

                Task task2 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務2-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);

                    foreach (var item in data2)
                    {
                        //Console.WriteLine(string.Format("ID:{0} Data1:{1} Data2:{2} Data3:{3}", item.ID, item.Data1, item.Data2, item.Data3));
                        bool flag = true;
                        if (string.IsNullOrEmpty(item.ID.ToString()))
                            flag = false;
                        if (string.IsNullOrEmpty(item.Data1))
                            flag = false;
                        if (string.IsNullOrEmpty(item.Data2))
                            flag = false;
                        if (string.IsNullOrEmpty(item.Data3))
                            flag = false;
                        if (flag)
                            result2.Add(item);
                    }

                    Console.WriteLine("任務2-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });

                Task task3 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務3-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);

                    foreach (var item in data3)
                    {
                        //Console.WriteLine(string.Format("ID:{0} Data1:{1} Data2:{2} Data3:{3}", item.ID, item.Data1, item.Data2, item.Data3));
                        bool flag = true;
                        if (string.IsNullOrEmpty(item.ID.ToString()))
                            flag = false;
                        if (string.IsNullOrEmpty(item.Data1))
                            flag = false;
                        if (string.IsNullOrEmpty(item.Data2))
                            flag = false;
                        if (string.IsNullOrEmpty(item.Data3))
                            flag = false;
                        if (flag)
                            result3.Add(item);
                    }

                    Console.WriteLine("任務3-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });

                Task.WaitAll(new Task[] { task1, task2, task3 });

                //必須要拆兩包資料 再裝在一起 不然會有錯誤
                List<DataModel> result = new List<DataModel>();
                result.AddRange(result1);
                result.AddRange(result2);
                result.AddRange(result3);

                Console.WriteLine("WaitAll執行之後【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);

                DateTime end = DateTime.Now;
                TimeSpan ts = end - start;
                String s1 = ts.TotalSeconds.ToString();

                Console.WriteLine("總共耗時:" + s1);

                Console.WriteLine(GetObjectSize(JsonConvert.SerializeObject(result)));
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    
        public void DataCheckNoSeparter()
        {
            Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);

            DateTime start = DateTime.Now;

            List<DataModel> resultTmp = new List<DataModel>();

            foreach (var item in Data)
            {
                //Console.WriteLine(string.Format("ID:{0} Data1:{1} Data2:{2} Data3:{3}", item.ID, item.Data1, item.Data2, item.Data3));
                bool flag = true;
                if (string.IsNullOrEmpty(item.ID.ToString()))
                    flag = false;
                if (string.IsNullOrEmpty(item.Data1))
                    flag = false;
                if (string.IsNullOrEmpty(item.Data2))
                    flag = false;
                if (string.IsNullOrEmpty(item.Data3))
                    flag = false;
                if (flag)
                    resultTmp.Add(item);
            }

            List<DataModel> result = new List<DataModel>();

            result.AddRange(resultTmp);

            Console.WriteLine("WaitAll執行之後【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);

            DateTime end = DateTime.Now;
            TimeSpan ts = end - start;
            String s1 = ts.TotalSeconds.ToString();

            Console.WriteLine("總共耗時:" + s1);

            Console.WriteLine(GetObjectSize(JsonConvert.SerializeObject(resultTmp)));
        }



    }
}
