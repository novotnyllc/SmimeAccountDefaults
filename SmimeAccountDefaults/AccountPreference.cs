using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace SmimeAccountDefaults
{
    [DataContract]
    class AccountPreference : ObservableObject
    {
        string smtpAddress;
        bool sign;
        bool encrypt;

        [DataMember]
        public string SmtpAddress
        {
            get { return smtpAddress; }
            set { Set(ref smtpAddress, value); }
        }

        [DataMember]
        public bool Sign
        {
            get { return sign; }
            set { Set(ref sign, value); }
        }

        [DataMember]
        public bool Encrypt
        {
            get { return encrypt; }
            set { Set(ref encrypt, value); }
        }

        public AccountPreference Clone()
        {
            return new AccountPreference
            {
                SmtpAddress = SmtpAddress,
                Sign = Sign,
                Encrypt = Encrypt
            };
        }
    }
}
