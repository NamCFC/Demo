using Nam.Core.ResultBase.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using static Nam.Core.Enum.StatusEnum;

namespace Nam.Core.ResultBase
{
    public class ResultBase
    {
        public ResultType ResultId { get; set; }
        public string Message { get; set; }
        public Object ResultData { get; set; }

        public ResultBase()
        {

        }

        public ResultBase(ResultType ResultId, string Message, Object ResultData)
        {
            this.ResultId = ResultId;
            this.Message = Message;
            this.ResultData = ResultData;
        }

        public static ResultBase Success(Object ResultData)
        {
            ResultBase result = new ResultBase(ResultType.Success, MessageBase.Success, ResultData);
            return result;
        }

        public static ResultBase Fail()
        {
            ResultBase result = new ResultBase(ResultType.Fail, MessageBase.Fail, null);
            return result;
        }

        public static ResultBase Fail(string Message)
        {
            ResultBase result = new ResultBase(ResultType.Fail, Message, null);
            return result;
        }

        public static ResultBase FailWithMessage(string Message)
        {
            ResultBase result = new ResultBase(ResultType.Fail, Message, null);
            return result;
        }

        public static ResultBase FailSystem()
        {
            ResultBase result = new ResultBase(ResultType.FailSystem, MessageBase.FailSystem, null);
            return result;
        }

        public static ResultBase SecCodeWrong()
        {
            ResultBase result = new ResultBase(ResultType.SecCodeWrong, MessageBase.SecCodeWrong, null);
            return result;
        }
    }
}
