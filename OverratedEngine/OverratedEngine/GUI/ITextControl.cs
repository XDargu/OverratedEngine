using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OverratedEngine.GUI
{
    /// <summary>
    /// The interface for controls that show text
    /// </summary>
    public interface ITextControl
    {
        /// <summary>
        /// The text to display in this control
        /// </summary>
        string Text { get; set; }
    }
}
