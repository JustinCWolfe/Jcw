using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Jcw.Charting.Interfaces
{
    public interface IMark
    {
        [XmlIgnore]
        bool UserEditable { get; set; }
    }
}
