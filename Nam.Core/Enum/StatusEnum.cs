using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Core.Enum
{
    public class StatusEnum
    {
        public enum ResultType : byte
        {
            Success = 1,
            Fail = 2,
            FailSystem = 3,
            SecCodeWrong = 0
        }

        public enum Gender : byte
        {
            Male = 1,
            Female = 2,
            Other = 0
        }

        public enum PayType : byte
        {
            Direct = 1,
            Momo = 2,
            Bank = 3
        }

        public enum GiftType: byte
        {
            Percent = 1,
            Money = 2
        }
    }
}
