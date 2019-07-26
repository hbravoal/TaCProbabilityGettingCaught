namespace Solver.Common.Helpers
{
    using System;
    using System.Collections.Generic;
    using Models;
    public static class ResponseHelper<T>
    {
        public static Response<T> SuccessResponse(string message, T result)
        {
            List<MessageResult> messageResult = new List<MessageResult>
            {
                new MessageResult { Message = message }
            };
            return new Response<T>
            {
                IsSuccess = true,
                Message = messageResult,
                Result = (T)result
            };
        }
        public static Response<T> ErrorResponse(Exception ex, T result)
        {
            List<MessageResult> messageResult = new List<MessageResult>
            {
                new MessageResult { Message = string.Format("Ha ocurrido un error: {0}",ex.ToString()) }
            };
            return new Response<T>
            {
                IsSuccess = false,
                Message = messageResult,
                Result = result
            };
        }
        public static Response<T> ExceptionDatabase(Exception ex, string tableAction, string table, T result)
        {
            List<MessageResult> messageResult = new List<MessageResult>
            {
                new MessageResult { Message = string.Format("Error en al {0} en la tabla: {1}. Error Detallado: {2}", tableAction, table, ex)}
            };
            return new Response<T>
            {
                IsSuccess = false,
                Message = messageResult,
                Result = result
            };
        }
    }
}