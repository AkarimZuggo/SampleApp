using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Admin.Api.Controllers.Base
{
    public abstract class AppBaseApiController : ControllerBase
    {
        public string UserId
        {
            get
            {
                try
                {
                    return (User.Claims.First(c => c.Type.Contains("UserId")).Value);
                }
                catch
                {
                    throw new Exception("User not exists");
                }
            }
        }
        public int CompanyId
        {
            get
            {
                try
                {
                    return int.Parse(User.Claims.First(c => c.Type.Contains("CompanyId")).Value);
                }
                catch
                {
                    throw new Exception("");
                }
            }
        }
        #region SuccessResponse
        protected ApiResponseModel SuccessMessage(object content)
        {
            var response = new ApiResponseModel
            {
                Content = content,
                IsSuccess = true,
                Messages = new List<ApiMessages> { new ApiMessages
                {
                    Type = EnumMessageType.Information,
                    Message = "Success"
                } }
            };

            return response;
        }
        protected ApiResponseModel SuccessMessage(string msg)
        {
            var response = new ApiResponseModel
            {
                Content = null,
                IsSuccess = true,
                Messages = new List<ApiMessages> { new ApiMessages
                {
                    Type = EnumMessageType.Information,
                    Message = msg
                } }
            };

            return response;
        }
        protected ApiResponseModel SuccessMessage(object obj, string msg)
        {
            var response = new ApiResponseModel
            {
                Content = obj,
                IsSuccess = true,
                Messages = new List<ApiMessages> { new ApiMessages
                {
                    Type = EnumMessageType.Information,
                    Message = msg
                } }
            };

            return response;
        }
        #endregion
        #region ErrorResponse
        protected ApiResponseModel ErrorMessage()
        {
            var response = new ApiResponseModel
            {
                Content = "",
                IsSuccess = false,
                Messages = new List<ApiMessages> { new ApiMessages
                {
                    Type = EnumMessageType.Error,
                    Message = "Error"
                } }
            };

            return response;
        }
        protected ApiResponseModel ErrorMessage(object obj)
        {
            var response = new ApiResponseModel
            {
                Content = obj,
                IsSuccess = false,
                Messages = new List<ApiMessages> { new ApiMessages
                {
                    Type = EnumMessageType.Error,
                    Message = "Error"
                } }
            };

            return response;
        }
        protected ApiResponseModel ErrorMessage(object obj, string msg)
        {
            var response = new ApiResponseModel
            {
                Content = obj,
                IsSuccess = false,
                Messages = new List<ApiMessages> { new ApiMessages
                {
                    Type = EnumMessageType.Error,
                    Message = msg
                } }
            };

            return response;
        }

        protected ApiResponseModel StatusCodeMessage(int statusCode, bool IsSuccess, EnumMessageType type, string status, string message)
        {
            var data = new
            {
                StatusCode = statusCode,
                Status = status
            };
            var responses = new ApiResponseModel
            {
                Content = data,
                IsSuccess = IsSuccess,
                Messages = new List<ApiMessages> { new ApiMessages
                {
                    Type = type,
                    Message = message
                } }
            };

            return responses;
        }

        protected ApiResponseModel StatusCodeMessage(int statusCode, bool IsSuccess, EnumMessageType type, string message)
        {
            var data = new
            {
                StatusCode = statusCode
            };
            var responses = new ApiResponseModel
            {
                Content = data,
                IsSuccess = IsSuccess,
                Messages = new List<ApiMessages> { new ApiMessages
                {
                    Type = type,
                    Message = message
                } }
            };

            return responses;
        }
        protected ApiResponseModel StatusCodeMessage(int statusCode)
        {
            var data = new
            {
                StatusCode = statusCode,

            };
            var responses = new ApiResponseModel
            {
                Content = data,
                IsSuccess = false,
                Messages = new List<ApiMessages>()
            };

            return responses;
        }
        protected ApiResponseModel ErrorMessage(List<Message> msgs)
        {
            var response = new ApiResponseModel
            {
                Content = "",
                IsSuccess = false,
                Messages = msgs.Select(o => new ApiMessages { Type = EnumMessageType.Error, Message = o.MessageText }).ToList()
            };

            return response;
        }


        protected ApiResponseModel ErrorMessage(List<ApiMessages> msgs)
        {
            var response = new ApiResponseModel
            {
                Content = null,
                IsSuccess = false,
                Messages = msgs
            };

            return response;
        }

        protected ApiResponseModel ErrorMessage(string msg)
        {
            var response = new ApiResponseModel
            {
                Content = null,
                IsSuccess = false,
                Messages = new List<ApiMessages> { new ApiMessages
                {
                    Type = EnumMessageType.Error,
                    Message = msg
                } }
            };

            return response;
        }
        #endregion
        public class ApiResponseModel
        {
            public bool IsSuccess { get; set; }

            public object Content { get; set; }

            public List<ApiMessages> Messages { get; set; } = new List<ApiMessages>();
        }
        public class ApiMessages
        {
            public EnumMessageType Type { get; set; }
            public string? Message { get; set; }
        }
        public enum EnumMessageType
        {
            Warning,
            Information,
            Error,
            Fatal
        }
        public class Message
        {
            //public Enumerations.MessageTypeEnum MessageType { get; set; }
            public string MessageText { get; set; }

            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}
