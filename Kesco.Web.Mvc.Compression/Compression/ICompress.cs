/*
* Do Not Remove
* Author: Douglas Branca http://www.linkedin.com/in/douglasbranca
* CodePlex: https://mvcscriptcompression.codeplex.com 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kesco.Web.Mvc.Compression.Compression
{
    /// <summary>
    /// Interface for compression classes
    /// </summary>
    public interface ICompress
    {
        /// <summary>
        /// Method to compress script
        /// </summary>
        /// <param name="script">script to compress</param>
        /// <returns>compressed script string</returns>
        string Compress(string script);
    }
}
