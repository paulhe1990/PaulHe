using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using static PaulHe.Utility.Flag;

namespace PaulHe.Utility
{
    /// <summary> 消息队列类-MessageQueue </summary>
    public static class MSMQUtil
    {
        #region 创建、删除队列

        /// <summary> 创建公用队列 </summary>
        public static void CreatePublicQueue()
        {
            CreateQueue(QueueTypeFlag.公用队列);
        }

        /// <summary> 创建专用队列 </summary>
        public static void CreatePrivateQueue()
        {
            CreateQueue(QueueTypeFlag.专用队列);
        }

        private static void CreateQueue(QueueTypeFlag queueType)
        {
            string path = GetQueuePath(queueType);
            MessageQueue mq = MessageQueue.Create(path);

            //Message msg1 = new Message { Body = "hello world1" };
            //Message msg2 = new Message { Body = "hello world2" };
            //Message msg3 = new Message { Body = "hello world3" };

            //mq.Send(msg1);
            //mq.Send(msg2);
            //mq.Send(msg3);
        }

        public static void DeleteQueue(QueueTypeFlag queueType)
        {
            string path = GetQueuePath(queueType);
            MessageQueue.Delete(path);
        }

        /// <summary> 返回队列路径 </summary>
        /// <param name="queueType"></param>
        /// <returns></returns>
        private static string GetQueuePath(QueueTypeFlag queueType)
        {
            string path = string.Empty;
            switch (queueType)
            {
                case QueueTypeFlag.公用队列:
                    path = @".\PublicQueue";
                    break;
                case QueueTypeFlag.专用队列:
                    path = @".\Private$\PrivateQueue";
                    break;
                case QueueTypeFlag.日记队列:
                    path = @".\LogQueue\Journal$";
                    break;
                case QueueTypeFlag.计算机日志队列:
                    path = @".\Journal$";
                    break;
                case QueueTypeFlag.计算机死信队列:
                    path = @".\Deadletter$";
                    break;
                case QueueTypeFlag.计算机事务性死信队列:
                    path = @".\XactDeadletter$";
                    break;
                default:
                    break;
            }
            return path;
        }

        #endregion

        public static void SendMsg(object content)
        {
            MessageQueue mq = new MessageQueue(GetQueuePath(QueueTypeFlag.专用队列));
            mq.Send(content);
        }

        public static Message[] GetAllMessages()
        {
            MessageQueue mq = new MessageQueue(GetQueuePath(QueueTypeFlag.专用队列));
            mq.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
            return mq.GetAllMessages();
        }

        public static Message Receive()
        {
            MessageQueue mq = new MessageQueue(GetQueuePath(QueueTypeFlag.专用队列));
            return mq.Receive();
        }

    }

    public class Flag
    {
        /// <summary> 消息队列分类 </summary>
        public enum QueueTypeFlag
        {
            公用队列 = 1,//MachineName\QueueName
            专用队列 = 2,//MachineName\Private$\QueueName
            日记队列 = 3,//MachineName\QueueName\Journal$
            计算机日志队列 = 4,//MachineName\Journal$
            计算机死信队列 = 5,//MachineName\Deadletter$
            计算机事务性死信队列 = 6//MachineName\XactDeadletter$
        }
    }
}
