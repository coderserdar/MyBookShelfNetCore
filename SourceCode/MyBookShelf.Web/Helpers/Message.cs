using Microsoft.AspNetCore.Mvc;

namespace MyBookShelf.Web.Helpers
{
    public enum MessageTypeEnum
    {
        Success,
        Error,
        Warning,
        Info
    }
    
    public class JavaScriptResult : ContentResult
    {
        public JavaScriptResult(string script)
        {
            this.Content = script;
            this.ContentType ="application/javascript";
        }

        public JavaScriptResult()
        {
        }
    }
    
    /// <summary>
    /// Arayüzde message içeriğini Javacript olarak döndürür.
    /// </summary>
    public static class Message
    {
        private static string MessageClass => "Lobibox.notify";
        private static string MessageType { get; set; }

        public static JavaScriptResult ShowMessage(MessageSettings messageSettings)
        {
            try
            {
                switch (messageSettings.MessageType)
                {
                    case MessageTypeEnum.Success:
                        MessageType = "success"; break;
                    case MessageTypeEnum.Error:
                        MessageType = "error"; break;
                    case MessageTypeEnum.Warning:
                        MessageType = "warning"; break;
                    case MessageTypeEnum.Info:
                        MessageType = "info"; break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(messageSettings.MessageType), messageSettings.MessageType, null);
                }

                var message = MessageClass + "('" + MessageType + "'," +
                    "{title:'" + messageSettings.MessageHeader + "'," +
                    "msg:\"" + messageSettings.Message + "\"});";

                var java = new JavaScriptResult
                {
                    Content = message
                };

                return java;
            }
            catch (Exception exception)
            {
                var java = new JavaScriptResult
                {
                    Content = @"alert('" + exception.Message + "');"
                };
                return java;
            }
        }

        public static JavaScriptResult ShowMessage(string messageHeader, string message, MessageTypeEnum messageType)
        {
            var messageSettings = new MessageSettings
            {
                MessageHeader = messageHeader,
                Message = message,
                MessageType = messageType
            };
            return ShowMessage(messageSettings);
        }
    }

    public class MessageSettings
    {
        public string Message { get; set; } = "";
        public string MessageHeader { get; set; } = "";
        public MessageTypeEnum MessageType { get; set; } = MessageTypeEnum.Success;
        public int WaitTime { get; set; } = 3000;
        public bool HaveHeader { get; set; } = true;
        public string MessageDetail { get; set; }
    }
}