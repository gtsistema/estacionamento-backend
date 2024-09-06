using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gp.Domain.Output
{
    public static class ResponseResult
    {
    public static ActionResult GetResponse<T>(bool sucess, IList<string> message, T data, int statusCode = 200, string errorCode = null)
    {
        object value = new object();
        object obj = new object();
        obj = ((!data.GetType().GetProperties().Any((PropertyInfo p) => p.Name == "Result")) ? ((object)data) : data.GetType().GetProperty("Result").GetValue(data));
        switch (statusCode)
        {
            case 200:
                value = new
                {
                    Success = sucess,
                    Message = message,
                    Result = obj
                };
                break;
            case 400:
                value = new
                {
                    Success = sucess,
                    Notifications = message,
                    Result = obj,
                    ErrorCode = errorCode
                };
                break;
            case 500:
                throw new Exception(message.FirstOrDefault());
        }

        return new ObjectResult(value)
        {
            StatusCode = statusCode
        };
    }

    public static ActionResult GetResponse<T>(bool sucess, string message, T data, int statusCode = 200, string errorCode = null)
    {
        object value = new object();
        object obj = new object();
        obj = ((!data.GetType().GetProperties().Any((PropertyInfo p) => p.Name == "Result")) ? ((object)data) : data.GetType().GetProperty("Result").GetValue(data));
        switch (statusCode)
        {
            case 200:
                value = new
                {
                    Success = sucess,
                    Message = message,
                    Result = obj
                };
                break;
            case 400:
                value = new
                {
                    Success = sucess,
                    Notifications = message,
                    Result = obj,
                    ErrorCode = errorCode
                };
                break;
            case 500:
                throw new Exception(message);
        }

        return new ObjectResult(value)
        {
            StatusCode = statusCode
        };
    }

    public static ActionResult GetResponse<T>(bool sucess, IList<ValidationFailure> message, T data, int statusCode = 200, string errorCode = null)
    {
        if (message == null)
        {
            return null;
        }

        List<string> list = new List<string>();
        foreach (ValidationFailure item in message)
        {
            list.Add(item.ErrorMessage);
        }

        object value = new object();
        StringBuilder str = new StringBuilder();
        List<string> listStr = new List<string>();
        message.ToList().ForEach(delegate (ValidationFailure x)
        {
            str.AppendLine(x.ErrorMessage);
            listStr.Add(x.ErrorMessage);
        });
        new object();
        object obj = new object();
        obj = ((!data.GetType().GetProperties().Any((PropertyInfo p) => p.Name == "Result")) ? ((object)data) : data.GetType().GetProperty("Result").GetValue(data));
        switch (statusCode)
        {
            case 200:
                value = new
                {
                    Success = sucess,
                    Message = message,
                    Result = obj
                };
                break;
            case 400:
                value = new
                {
                    Success = sucess,
                    Notifications = listStr,
                    Result = obj,
                    ErrorCode = errorCode
                };
                break;
            case 500:
                value = new
                {
                    Success = sucess,
                    ErrorDescription = str.ToString(),
                    Result = obj
                };
                break;
        }

        return new ObjectResult(value)
        {
            StatusCode = statusCode
        };
    }

    public static ActionResult GetResponse(object response)
    {
        return new ObjectResult(response)
        {
            StatusCode = 200
        };
    }

    public static object GetResponseMessage(string message, int statusCode = 500)
    {
        if (statusCode == 400)
        {
            return new
            {
                success = false,
                notifications = new string[1] { message },
                result = new { }
            };
        }

        return new
        {
            success = false,
            errorDescription = new string[1] { message },
            result = new { }
        };
    }

    public static object GetResponseMessage(string[] message, int statusCode = 500)
    {
        if (statusCode == 400)
        {
            return new
            {
                success = false,
                notifications = message,
                result = new { }
            };
        }

        return new
        {
            success = false,
            errorDescription = message,
            result = new { }
        };
    }

    public static ActionResult GetResponse<T>(T response)
    {
        return new ObjectResult(response)
        {
            StatusCode = 200
        };
    }
}
}
