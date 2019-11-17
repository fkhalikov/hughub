using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HugHub.PriceEngine.Models.Extensions
{
    public static class ResponseResultExtensions
    {
        public static ResponseResult<T> AddError<T>(this ResponseResult<T> responseResult, string errorCode, string error)
        {
            responseResult.Error.TryGetValue(errorCode, out var errorList);
            
            if (errorList == null) responseResult.Error.Add(errorCode, new List<string>{error});
            else responseResult.Error[errorCode].Add(error);

            return responseResult;
        }

        public static ResponseResult<T> Validate<T>(
            this ResponseResult<T> responseResult,
            Func<bool> isValidDelegate,
            string errorCode,
            string error)
        {
            if (!isValidDelegate())
            {
                responseResult.AddError(errorCode, error);
            }

            return responseResult;
        }
        
        public static string SquashErrors<T>(this ResponseResult<T> serviceResult)
        {
            if (serviceResult.Error == null ||
                !serviceResult.Error.Any()) return string.Empty;
            
            var stringBuilder = new StringBuilder();
            
            foreach (var errorCode in serviceResult.Error)
            {
                stringBuilder.AppendLine(errorCode.Key);

                foreach (var error in errorCode.Value)
                {
                    stringBuilder.AppendLine($"{error}");
                }
            }
            
            return stringBuilder.ToString();
        }
    }
}