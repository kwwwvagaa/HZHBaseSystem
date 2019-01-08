using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Tools
{
    public class ExcuteMessage
    {
        #region 错误处理
        #region 执行错误
        /// <summary>
        /// 功能描述:执行错误
        /// </summary>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <param name="strResult">strResult</param>
        /// <returns>返回值</returns>
        public static string Error(string strErrorMsg, string strResult = "")
        {
            return ErrorMsg(strErrorMsg, strResult).ToJsonHasNull();
            // return JsonConvert.SerializeObject(ErrorMsg(strErrorMsg, strResult));
        }
        /// <summary>
        /// 功能描述:执行错误
        /// </summary>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <param name="strResult">strResult</param>
        /// <returns>返回值</returns>
        public static ExcuteResultMsg ErrorMsg(string strErrorMsg, string strResult = "")
        {
            return ExcuteResultEx(ResultCode.Error, strErrorMsg, strResult);
        }

        #endregion

        #region 执行异常
        /// <summary>
        /// 功能描述:执行异常并记录日志
        /// </summary>
        /// <param name="ex">ex</param>
        /// <param name="strResult">strResult</param>
        /// <returns>返回值</returns>
        public static string ErrorOfException(Exception ex, string strResult = "")
        {
            return ErrorOfExceptionMsg(ex, strResult).ToJsonHasNull();
            //return JsonConvert.SerializeObject(ErrorOfExceptionMsg(ex, strResult));
        }
        /// <summary>
        /// 功能描述:执行异常并记录日志
        /// </summary>
        /// <param name="ex">ex</param>
        /// <param name="strResult">strResult</param>
        /// <returns>返回值</returns>
        public static ExcuteResultMsg ErrorOfExceptionMsg(Exception ex, string strResult = "")
        {            
            return ExcuteResultEx(ResultCode.ErrorOfException, ex.Message + (ex.InnerException != null ? ("\n\r" + ex.InnerException.Message) : ""), strResult);
        }
        #endregion

        #region 转换失败
        /// <summary>
        /// 功能描述:转换失败
        /// </summary>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <param name="strResult">strResult</param>
        /// <returns>返回值</returns>
        public static string ConvertError(string strErrorMsg, string strResult = "")
        {
            return ConvertErrorMsg(strErrorMsg, strResult).ToJsonHasNull();
            //return JsonConvert.SerializeObject(ConvertErrorMsg(strErrorMsg, strResult));
        }

        /// <summary>
        /// 功能描述:转换失败
        /// </summary>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <param name="strResult">strResult</param>
        /// <returns>返回值</returns>
        public static ExcuteResultMsg ConvertErrorMsg(string strErrorMsg, string strResult = "")
        {
            return ExcuteResultEx(ResultCode.ConvertError, strErrorMsg, strResult);
        }
        #endregion

        #region 未找到数据
        /// <summary>
        /// 功能描述:未找到数据
        /// </summary>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <param name="strResult">strResult</param>
        /// <returns>返回值</returns>
        public static string NotFoundData(string strErrorMsg, string strResult = "")
        {
            return NotFoundDataMsg(strErrorMsg, strResult).ToJsonHasNull();
            //return JsonConvert.SerializeObject(NotFoundDataMsg(strErrorMsg, strResult));
        }
        /// <summary>
        /// 功能描述:未找到数据
        /// </summary>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <param name="strResult">strResult</param>
        /// <returns>返回值</returns>
        public static ExcuteResultMsg NotFoundDataMsg(string strErrorMsg, string strResult = "")
        {
            return ExcuteResultEx(ResultCode.NotFoundData, strErrorMsg, strResult);
        }


        #endregion

        #endregion

        #region 执行成功
        /// <summary>
        /// 功能描述:执行成功
        /// </summary>
        /// <param name="strResult">strResult</param>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <returns>返回值</returns>
        public static string Sucess(string strResult, string strErrorMsg = "")
        {
            return SucessMsg(strResult, strErrorMsg).ToJsonHasNull();
            //return JsonConvert.SerializeObject(SucessMsg(strResult, strErrorMsg));
        }

        /// <summary>
        /// 功能描述:执行成功
        /// </summary>
        /// <param name="strResult">strResult</param>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <returns>返回值</returns>
        public static ExcuteResultMsg SucessMsg(string strResult, string strErrorMsg = "")
        {
            return ExcuteResultEx(ResultCode.Success, strErrorMsg, strResult);
        }

        /// <summary>
        /// 功能描述:执行成功
        /// </summary>
        /// <param name="objResult">objResult</param>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <returns>返回值</returns>
        public static string Sucess(object objResult, string strErrorMsg = "")
        {
            return SucessMsg(objResult, strErrorMsg).ToJsonHasNull();
            //return JsonConvert.SerializeObject(SucessMsg(objResult, strErrorMsg));
        }

        /// <summary>
        /// 功能描述:执行成功
        /// </summary>
        /// <param name="objResult">objResult</param>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <returns>返回值</returns>
        public static ExcuteResultMsg SucessMsg(object objResult, string strErrorMsg = "")
        {
            string strResult = string.Empty;
            if (objResult != null)
            {
                strResult = objResult.ToJsonHasNull();//JsonConvert.SerializeObject(objResult);
            }
            return ExcuteResultEx(ResultCode.Success, strErrorMsg, strResult);
        }

        /// <summary>
        /// 功能描述:执行成功--传入实体不序列化
        /// </summary>
        /// <param name="objResult">objResult</param>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <returns>返回值</returns>
        public static ExcuteResultMsg SucessObjMsg(object objResult, string strErrorMsg = "")
        {
            return ExcuteResultEx(ResultCode.Success, strErrorMsg, "", objResult);
        }

        #endregion

        #region 未授权
        /// <summary>
        /// 未授权
        /// </summary>
        /// <param name="strResult"></param>
        /// <param name="strErrorMsg"></param>
        /// <returns></returns>
        public static string Unauthorized(string strResult = "", string strErrorMsg = "")
        {
            return UnauthorizedMsg(strResult, strErrorMsg).ToJsonHasNull();
            //return JsonConvert.SerializeObject(UnauthorizedMsg(strResult, strErrorMsg));
        }
        /// <summary>
        /// 未授权
        /// </summary>
        /// <param name="objResult"></param>
        /// <param name="strErrorMsg"></param>
        /// <returns></returns>
        public static string Unauthorized(object objResult, string strErrorMsg = "")
        {
            return UnauthorizedMsg(objResult, strErrorMsg).ToJsonHasNull();
            //return JsonConvert.SerializeObject(UnauthorizedMsg(objResult, strErrorMsg));
        }
        /// <summary>
        /// 未授权
        /// </summary>
        /// <param name="strResult"></param>
        /// <param name="strErrorMsg"></param>
        /// <returns></returns>
        public static ExcuteResultMsg UnauthorizedMsg(string strResult = "", string strErrorMsg = "")
        {
            return ExcuteResultEx(ResultCode.Unauthorized, strErrorMsg, strResult);
        }
        /// <summary>
        /// 未授权
        /// </summary>
        /// <param name="objResult"></param>
        /// <param name="strErrorMsg"></param>
        /// <returns></returns>
        public static ExcuteResultMsg UnauthorizedMsg(object objResult, string strErrorMsg = "")
        {
            string strResult = string.Empty;
            if (objResult != null)
            {
                strResult = objResult.ToJsonHasNull();// JsonConvert.SerializeObject(objResult);
            }
            return ExcuteResultEx(ResultCode.Unauthorized, strErrorMsg, strResult);
        }

        #endregion


        #region 执行条件不满足--需提供Message给前端进行提示

        /// <summary>
        /// 功能描述:执行条件不满足--需提供Message给前端进行提示
        /// </summary>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <param name="strResult">strResult</param>
        /// <returns>返回值</returns>
        public static string UnCondition(string strErrorMsg, string strResult = "")
        {
            return UnConditionMsg(strErrorMsg, strResult).ToJsonHasNull();
            //return JsonConvert.SerializeObject(UnConditionMsg(strErrorMsg, strResult));

        }
        /// <summary>
        /// 功能描述:执行条件不满足--需提供Message给前端进行提示
        /// </summary>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <param name="strResult">strResult</param>
        /// <returns>返回值</returns>
        public static ExcuteResultMsg UnConditionMsg(string strErrorMsg, string strResult = "")
        {
            return ExcuteResultEx(ResultCode.UnCondition, strErrorMsg, strResult);
        }
        #endregion

        /// <summary>
        /// 功能描述:执行结果
        /// </summary>
        /// <param name="resultCode">resultCode</param>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <param name="strResult">strResult</param>
        /// <returns>返回值</returns>
        public static string ExcuteResult(ResultCode resultCode, string strErrorMsg, string strResult)
        {
            return ExcuteResultEx(resultCode, strErrorMsg, strResult).ToJsonHasNull();
            //return JsonConvert.SerializeObject(ExcuteResultEx(resultCode, strErrorMsg, strResult));
        }

        /// <summary>
        /// 功能描述:执行结果
        /// </summary>
        /// <param name="resultCode">resultCode</param>
        /// <param name="strErrorMsg">strErrorMsg</param>
        /// <param name="strResult">strResult</param>
        /// <returns>返回值</returns>
        public static ExcuteResultMsg ExcuteResultEx(ResultCode resultCode, string strErrorMsg, string strResult, object objResult = null)
        {
            ExcuteResultMsg entity = new ExcuteResultMsg()
            {
                Code = resultCode,
                ErrorMsg = strErrorMsg,
                Result = strResult,
                objEntity = objResult
            };
            if (resultCode != ResultCode.Success)
            {
                //未执行成功则写入日志
                //LogHelper.WriteErrorLog(strErrorMsg);
            }
            return entity;
        }
    }
}
