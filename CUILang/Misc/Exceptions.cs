using System;
using System.Collections;
using System.Collections.Generic;
/*
 * CelesteUI language library
 * CelesteUI exception implementations
 * 
 * Author: SkLz
 * Last edit: 19/06/2021
 * 
 * Recent changes:
 * + Added ParsingException
 */

namespace Celeste.Mod.CelesteUI
{
    /// <summary>
    /// Represents an error that happened while parsing a CelesteUI document.
    /// </summary>
    public class ParsingException : Exception
    {
        protected string _Message;
        protected string File;

        /// <summary>
        /// Represents the message of the exception.
        /// </summary>
        public override string Message => _Message;

        public override IDictionary Data
        {
            get
            {
                var dict = new Dictionary<string, string>();
                SerializeData(dict);
                return dict;
            }
        }

        /// <summary>
        /// Serializes the exception's data and puts it into the specified dictionary.
        /// </summary>
        /// <remarks>
        /// This method can be overridden in child exceptions 
        /// </remarks>
        /// <param name="data">The string dictionary to output to.</param>
        protected virtual void SerializeData(Dictionary<string, string> data)
        {
            if (File != null) data.Add("File", File);
        }

        /// <summary>
        /// Creates a new parsing exception with a message and a filename.
        /// </summary>
        /// <param name="message">The message of the exception</param>
        /// <param name="file">The filename of which parsing failed.</param>
        public ParsingException(string message, string file)
            : this(message)
        {
            File = file;
        }

        /// <summary>
        /// Creates a new parsing exception with a message and a filename.
        /// </summary>
        /// <param name="message">The message of the exception</param>
        public ParsingException(string message)
        {
            _Message = message;
        }
    }
}
