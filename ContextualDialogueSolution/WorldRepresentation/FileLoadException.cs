using System;
using System.Collections.Generic;
using System.Text;

namespace ContextualDialogue.WorldManager
{
    class FileLoadException : Exception
    {
        public FileLoadException()
        {
        }

        public FileLoadException(string message)
        : base(message)
        {
        }

        public FileLoadException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
