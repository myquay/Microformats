using Microformats.Definitions.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microformats.Result
{
    /// <summary>
    /// Value of a property
    /// </summary>
    public class MfValue
    {
        private object _value { get; set; }

        public MfValue(string value)
        {
            _value = value;
        }

        public MfValue(MfType value)
        {
            _value = value;
        }

        public MfValue(MfImage value)
        {
            _value = value;
        }

        public string GetValue()
        {
            if (_value is string v)
            {
                return v;
            }
            else if (_value is MfImage img)
            {
                return img.Value;
            }
            else
            {
                return ((MfType)_value).Value;
            }
        }

        public string GetName()
        {
            if(_value is  string v)
            {
                return v;
            }else if(_value is MfImage img)
            {
                return img.Value;
            }
            else
            {
                return ((MfType)_value).GetProperty(new PName())?.First();
            }
        }
    }
}
