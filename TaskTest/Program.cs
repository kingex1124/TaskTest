using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DataDoing dd = new DataDoing();
            //dd.DataCheckNoSeparter();
            dd.DataCheck();
          
            Console.ReadLine();
        }

        public class TaskTest
        {
            /// <summary>
            /// ContinueWith控制任務順序
            /// </summary>
            public void ContinueWithTest()
            {
                Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                Task task = new Task(CommonDelegate.DoSomethingMethod);
                //利用ContinueWith()為任務排序不會阻塞主執行緒
                task.ContinueWith((a) =>
                {
                    Console.WriteLine("ContinueWith-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(3000);
                    Console.WriteLine("ContinueWith-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                task.Start();
                Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            }

            /// <summary>
            /// ContinueWhenAny操作 
            /// Task 數組中的任意一個任務執行完畢後，ContinueWhenAny指定任務開始執行
            /// ContinueWhenAny不會阻塞主線程
            /// </summary>
            public void ContinueWhenAnyOperator()
            {
                Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                TaskFactory taskFactory = new TaskFactory();
                Task task1 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務1-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(1000);
                    Console.WriteLine("任務1-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task2 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務2-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(3000);
                    Console.WriteLine("任務2-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task3 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務3-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(5000);
                    Console.WriteLine("任務3-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                taskFactory.ContinueWhenAny(new Task[] { task1, task2, task3 }, (a) =>
                {
                    Console.WriteLine("ContinueWhenAny-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(1000);
                    Console.WriteLine("ContinueWhenAny-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            }

            /// <summary>
            /// ContinueWhenAll操作
            /// Task 數組中的所有任務都執行完畢後，ContinueWhenAll指定的任務開始執行
            /// ContinueWhenAll不會阻塞主線程
            /// </summary>
            public void ContinueWhenAllOperator()
            {
                Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                TaskFactory taskFactory = new TaskFactory();
                Task task1 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務1-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(1000);
                    Console.WriteLine("任務1-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task2 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務2-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(3000);
                    Console.WriteLine("任務2-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task3 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務3-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(5000);
                    Console.WriteLine("任務3-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                taskFactory.ContinueWhenAll(new Task[] { task1, task2, task3 }, (a) =>
                {
                    Console.WriteLine("ContinueWhenAll-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(1000);
                    Console.WriteLine("ContinueWhenAll-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            }

            /// <summary>
            /// WaitAny操作
            /// 主線程在任務1執行完畢後才繼續向下執行
            /// </summary>
            public void WaitAnyOperator()
            {
                Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                TaskFactory taskFactory = new TaskFactory();
                Task task1 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務1-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(3000);
                    Console.WriteLine("任務1-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task2 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務2-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(4000);
                    Console.WriteLine("任務2-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task3 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務3-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(5000);
                    Console.WriteLine("任務3-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task.WaitAny(new Task[] { task1, task2, task3 });
                Console.WriteLine("WaitAny執行之後【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            }

            /// <summary>
            /// WaitAll操作
            /// 主線程在所有任務執行完畢後，才繼續向下執行
            /// </summary>
            public void WaitAll()
            {
                Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                TaskFactory taskFactory = new TaskFactory();
                Task task1 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務1-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(3000);
                    Console.WriteLine("任務1-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task2 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務2-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(4000);
                    Console.WriteLine("任務2-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task3 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務3-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(5000);
                    Console.WriteLine("任務3-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task.WaitAll(new Task[] { task1, task2, task3 });
                Console.WriteLine("WaitAll執行之後【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            }

            /// <summary>
            /// WaitAny自定義非阻塞操作
            /// WaitAny()放到Task中異步執行，便不會再阻塞主線程
            /// </summary>
            public void WaitAnyDef()
            {
                Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                TaskFactory taskFactory = new TaskFactory();
                Task task = taskFactory.StartNew(() =>
                {
                    Task task1 = taskFactory.StartNew(() =>
                    {
                        Console.WriteLine("任務1-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                        Thread.Sleep(3000);
                        Console.WriteLine("任務1-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    });
                    Task task2 = taskFactory.StartNew(() =>
                    {
                        Console.WriteLine("任務2-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                        Thread.Sleep(4000);
                        Console.WriteLine("任務2-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    });
                    Task task3 = taskFactory.StartNew(() =>
                    {
                        Console.WriteLine("任務3-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                        Thread.Sleep(5000);
                        Console.WriteLine("任務3-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    });
                    Task.WaitAny(new Task[] { task1, task2, task3 });
                    Console.WriteLine("WaitAny執行之後【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            }

            /// <summary>
            /// WaitAll自定義非阻塞操作
            /// 將WaitAll()放到Task中異步執行，便不會再阻塞主線程
            /// </summary>
            public void WaitAllDef()
            {
                Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                TaskFactory taskFactory = new TaskFactory();
                Task task = taskFactory.StartNew(() =>
                {
                    Task task1 = taskFactory.StartNew(() =>
                    {
                        Console.WriteLine("任務1-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                        Thread.Sleep(3000);
                        Console.WriteLine("任務1-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    });
                    Task task2 = taskFactory.StartNew(() =>
                    {
                        Console.WriteLine("任務2-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                        Thread.Sleep(4000);
                        Console.WriteLine("任務2-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    });
                    Task task3 = taskFactory.StartNew(() =>
                    {
                        Console.WriteLine("任務3-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                        Thread.Sleep(5000);
                        Console.WriteLine("任務3-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    });
                    Task.WaitAll(new Task[] { task1, task2, task3 });
                    Console.WriteLine("WaitAll執行之後【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            }

            /// <summary>
            /// WhenAny操作 
            /// WhenAny()不會阻塞主線程
            /// </summary>
            public void WhenAnyOperator()
            {
                Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                TaskFactory taskFactory = new TaskFactory();
                Task task1 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務1-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(3000);
                    Console.WriteLine("任務1-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task2 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務2-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(4000);
                    Console.WriteLine("任務2-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task3 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務3-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(5000);
                    Console.WriteLine("任務3-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task = Task.WhenAny(new Task[] { task1, task2, task3 });
                task.ContinueWith((a) =>
                {
                    Console.WriteLine("ContinueWith-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(3000);
                    Console.WriteLine("ContinueWith-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            }

            /// <summary>
            /// WhenAll操作 
            /// WhenAll()不會阻塞主線程
            /// </summary>
            public void WhenAllOperatro()
            {
                Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                TaskFactory taskFactory = new TaskFactory();
                Task task1 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務1-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(3000);
                    Console.WriteLine("任務1-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task2 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務2-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(4000);
                    Console.WriteLine("任務2-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task3 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務3-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(5000);
                    Console.WriteLine("任務3-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task = Task.WhenAll(new Task[] { task1, task2, task3 });
                task.ContinueWith((a) =>
                {
                    Console.WriteLine("ContinueWith-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(3000);
                    Console.WriteLine("ContinueWith-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            }

            /// <summary>
            /// 有引數有返回值
            /// 獲取Task的返回值會阻塞主線程
            /// </summary>
            public void ParamHasReturn()
            {
                Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                Task<int> task1 = new Task<int>(new Func<object, int>((a) =>
                {
                    Console.WriteLine("a={0}", a);
                    Thread.Sleep(2000);
                    return 66;
                }), "wulaaa");
                task1.Start();
                Task<int> task2 = new Task<int>(new Func<object, int>((b) =>
                {
                    Console.WriteLine("b={0}", b);
                    Thread.Sleep(3000);
                    return 99;
                }), "wulbbb");
                task2.Start();
                int result1 = task1.Result;
                int result2 = task2.Result;
                Console.WriteLine("result1={0},result2={1}", result1, result2);
                Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            }

            /// <summary>
            /// 有引數有返回值 不阻塞
            /// 將獲取結果的代碼放到Task中異步執行，便不會再阻塞主線程
            /// </summary>
            public void ParamHasReturnNoBlock()
            {
                Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                Task.Run(() =>
                {
                    Task<int> task1 = new Task<int>(new Func<object, int>((a) =>
                    {
                        Console.WriteLine("a={0}", a);
                        Thread.Sleep(2000);
                        return 66;
                    }), "wulaaa");
                    task1.Start();
                    Task<int> task2 = new Task<int>(new Func<object, int>((b) =>
                    {
                        Console.WriteLine("b={0}", b);
                        Thread.Sleep(3000);
                        return 99;
                    }), "wulbbb");
                    task2.Start();
                    int result1 = task1.Result;
                    int result2 = task2.Result;
                    Console.WriteLine("result1={0},result2={1}", result1, result2);
                });
                Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            }

            /// <summary>
            /// 10000個執行續同時跑
            /// </summary>
            public void Run10000timesTest()
            {
                Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                TaskFactory taskFactory = new TaskFactory();
                Task[] taskArr = new Task[10000];

                for (int i = 0; i < 10000; i++)
                {
                    taskArr[i] = taskFactory.StartNew(() =>
                    {
                        Console.WriteLine("任務-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                        Thread.Sleep(3000);
                        Console.WriteLine("任務-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);

                    });
                }


                Task.WaitAll(taskArr);

                Console.WriteLine("WaitAll執行之後【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);

                Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            }

            /// <summary>
            /// 測試參數
            /// </summary>
            public void WaitAllParamaterTest()
            {
                int count = 0;

                Console.WriteLine("Main-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                TaskFactory taskFactory = new TaskFactory();
                Task task1 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務1-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(3000);
                    int x = 0;
                    for (int i = 0; i < 20; i++)
                        x++;

                    count += x;
                    Console.WriteLine("任務1-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task2 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務2-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(4000);
                    int x = 0;
                    for (int i = 0; i < 30; i++)
                        x++;

                    count += x;
                    Console.WriteLine("任務2-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task task3 = taskFactory.StartNew(() =>
                {
                    Console.WriteLine("任務3-Start【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                    Thread.Sleep(5000);
                    int x = 0;
                    for (int i = 0; i < 40; i++)
                        x++;

                    count += x;
                    Console.WriteLine("任務3-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                });
                Task.WaitAll(new Task[] { task1, task2, task3 });
                Console.WriteLine(count);
                Console.WriteLine("WaitAll執行之後【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
                Console.WriteLine("Main-End【ThreadId=" + Thread.CurrentThread.ManagedThreadId + "】：" + DateTime.Now);
            }
        }
    }
}
