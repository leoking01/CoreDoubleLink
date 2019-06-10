using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CoreDll
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IQQSback))]
    public interface IQQS
    {
        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void Speak(string value);

        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void Whisper(string name, string value);

        [OperationContract(IsOneWay = true, IsInitiating = true, IsTerminating = false)]
        void Leave();

        [OperationContract(IsOneWay = false, IsInitiating = true, IsTerminating = false)]
        List<string> join(string value);

    }

    interface IQQSback
    {
        [OperationContract(IsOneWay = true)]
        void Receive(string senderName, string message);

        [OperationContract(IsOneWay = true)]
        void ReceiveWhisper(string senderName, string message);

        [OperationContract(IsOneWay = true)]
        void UserEnter(List<string> name);

        [OperationContract(IsOneWay = true)]
        void UserLeave(string name);
    }
}
