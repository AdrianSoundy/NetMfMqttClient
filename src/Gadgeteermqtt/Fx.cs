namespace Gadgeteermqtt
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using System.Threading;
    using Microsoft.SPOT;

    /// <summary>
    /// Provides framework specific routines.
    /// </summary>
    public static class Fx
    {
        /// <summary>
        /// Formats a string from a format and an array of arguments.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static string Format(string format, params object[] args)
        {
            if (args == null || args.Length == 0)
            {
                return format;
            }
            else if (args.Length > 10)
            {
                throw new Exception("Too many arguments");
            }

            StringBuilder stringBuilder = new StringBuilder(format.Length * 2);

            char[] charArray = format.ToCharArray();
            for (int i = 0; i < charArray.Length; ++i)
            {
                // max supported number of args is 10
                if (
                    charArray[i] == '{' && 
                    i + 2 < charArray.Length && 
                    charArray[i + 2] == '}' && 
                    charArray[i + 1] >= '0' && 
                    charArray[i + 1] <= '9'
                    )
                {
                    int index = charArray[i + 1] - '0';
                    if (index < args.Length)
                    {
                        stringBuilder.Append(args[index]);
                    }

                    i += 2;
                }
                else
                {
                    stringBuilder.Append(charArray[i]);
                }
            }

            return stringBuilder.ToString();
        }     
    }
}