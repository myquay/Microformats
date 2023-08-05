using Microformats.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Microformats.Definitions.Constants;

namespace Microformats.Grammar
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

        public MfValue(MfSpec value)
        {
            _value = value;
        }

        public MfValue(MfImage value)
        {
            _value = value;
        }
        public MfValue(MfEmbedded value)
        {
            _value = value;
        }

        public object Get()
        {
            return _value;
        }

        public bool TryGet<T>(out T value) where T : class
        {
            value = default;

            if (_value is T)
            {
                value = _value as T;
                return true;
            }
            
            return false;
        }

        public T Get<T>() where T : class
        {
            if (TryGet<T>(out T value))
                return value;
            return default;
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
            else if (_value is MfEmbedded emb)
            {
                return emb.Value;
            }
            else
            {
                return ((MfSpec)_value).Value;
            }
        }

        public string GetName()
        {
            if(_value is  string v)
            {
                return v;
            }
            else if (_value is MfImage img)
            {
                return img.Value;
            }
            else if (_value is MfEmbedded emb)
            {
                return emb.Value;
            }
            else
            {
                return ((MfSpec)_value)?.Get<String>(Props.NAME)?.First();
            }
        }
    }
}
