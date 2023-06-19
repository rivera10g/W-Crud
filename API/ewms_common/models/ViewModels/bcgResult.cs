using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ewms.common.models.ViewModels
{
    public class bcgResult
    {

        public bool success;
        public string message;
        public string error_code;
        public string source;
        public int debugLevel;
        public string error_detail = "";
        public string data;
        public Object related_data;
        public string api_response = "";


        public bcgResult notify(bool isSuccess, string theErrorCode, string theMessage, string theSource, int theDebugLevel = 0, string theErrorDetail = "", string theRelatedData = "")
        {

            bcgResult iResult = new bcgResult();

            iResult.message = "";
            iResult.error_code = "";
            iResult.source = "";
            iResult.debugLevel = 0;
            iResult.error_detail = "";
            iResult.data = "";

            try
            {
                iResult.success = isSuccess;
                iResult.error_code = theErrorCode;
                iResult.message = theMessage.Replace("\"", "").Replace("'", "");
                iResult.source = theSource;
                iResult.debugLevel = theDebugLevel;
                iResult.error_detail = theErrorDetail;
                iResult.data = theRelatedData;

            }
            catch (Exception ex)
            {
                iResult.message = ex.Message;
            }

            return iResult;
        }
    }
}
