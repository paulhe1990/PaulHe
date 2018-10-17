using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaulHe.Utility;
using System.Messaging;

namespace Console1
{
    class Program
    {
        static void Main(string[] args)
        {
            //MSMQUtil.CreatePrivateQueue();
            for (int i = 0; i < 3; i++)
            {
                Message msg = new Message() { Body = "hello world" + i };
                msg.Recoverable = true;
                //MSMQUtil.SendMsg(msg);
            }

            Message[] arrMsg = MSMQUtil.GetAllMessages();

            foreach (var item in arrMsg)
            {
                Console.WriteLine(item.Body);
            }


            Console.ReadKey();
        }
    }
}
