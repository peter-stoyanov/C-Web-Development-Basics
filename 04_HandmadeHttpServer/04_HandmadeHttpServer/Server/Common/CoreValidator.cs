﻿namespace HandmadeHttpServer.Server.Common
{
    using System;

    public static class CoreValidator
    {
        public static void ThrowIfNull(object obj, string name)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }
        }

        public static void ThrowIfNullOrEmpty(string text, string name)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new ArgumentException($"{name} can not be null or empty.");
            }
        }
    }
}
