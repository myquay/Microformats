using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions
{
    public interface IVocabulary
    {
        /// <summary>
        /// Version of this vocabulary
        /// </summary>
        MicroformatsVersion Version { get; }

        /// <summary>
        /// The name of the vocabulary
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Valid properties
        /// </summary>
        IProperty[] Properties { get; }
    }
}
