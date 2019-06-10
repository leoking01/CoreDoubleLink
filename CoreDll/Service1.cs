using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CoreDll
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class Service1 : IQQS
    {
        IQQSback QQback = OperationContext.Current.GetCallbackChannel<IQQSback>();
        delegate void userenter(List<string> username);
        static event userenter UserEnterEvent;
        delegate void userspeak(string name, string content);
        static event userspeak UserSpeakEvent;
        delegate void userWhisper(string name, string content);
        static SortedList<string, userWhisper> Userspeaklist = new SortedList<string, userWhisper>();
        string Name;

        public List<string> join(string value)
        {
            if (Userspeaklist.Keys.Contains(value))
            {
                return new string[] { "已经有此用户" }.ToList();
            }
            this.Name = value;
            UserSpeakEvent += new userspeak(UserSpeakEventFunction);
            if (UserEnterEvent != null)
            {
                UserEnterEvent(new string[] { value }.ToList());
            }
            UserEnterEvent += new userenter(UserEnterEventFunction);
            Userspeaklist.Add(value, UserWhisperFunction);
            return Userspeaklist.Keys.ToList();

        }

        public void Speak(string value)
        {
            if (UserSpeakEvent != null)
            {
                UserSpeakEvent(this.Name, value);
            }
        }

        public void Whisper(string name, string value)
        {
            if (!Userspeaklist.Keys.Contains(name))
            {
                return;
            }
            Userspeaklist[name](name,value);
        }

        public void Leave()
        {
            UserEnterEvent -= UserEnterEventFunction;
            UserSpeakEvent -= UserSpeakEventFunction;
            Userspeaklist.Remove(this.Name);
            //退出的地方没实现，但是方法跟添加，说话一样。
            QQback.UserLeave(this.Name);
        }



        void UserEnterEventFunction(List<string> username)
        {
            this.QQback.UserEnter(username);
        }

        void UserSpeakEventFunction(string name, string content)
        {
            QQback.Receive(name, content);
        }

        void UserWhisperFunction(string name, string content)
        {
            QQback.ReceiveWhisper(name, content);
        }
    }
}
