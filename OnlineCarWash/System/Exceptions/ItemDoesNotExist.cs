﻿namespace OnlineCarWash.System.Exceptions
{
    public class ItemDoesNotExist : Exception
    {
        public ItemDoesNotExist(string? message):base(message) { }
    }
}
