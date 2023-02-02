using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSLibrary
{
    public class MessagesBlock
    {
        public MessagesBlock() { }

        private String _index, _status, _sender, _alphabet, _sent, _message; 

        public String Index {
            get { return _index; }
            set { _index = value; }
        }
        public String Status {
            get { return _status; }
            set { _status = value; }
        }
        public String Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }
        public String Alphabet
        {
            get { return _alphabet; }
            set { _alphabet = value; }
        }
        public String Sent
        {
            get { return _sent; }
            set { _sent = value; }
        }
        public String Message
        {
            get { return _message; }
            set { _message = value; }
        }

        ~MessagesBlock() { }
    }
}
