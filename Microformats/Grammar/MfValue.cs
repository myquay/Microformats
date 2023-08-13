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
        public MfProperty Property { get; set; }

        private object Value { get; set; }

        public MfValue(string property, object value)
        {
            Value = value;
            if (MfProperty.TryFromName(property, out MfProperty result))
                Property = result;
        }

        public object Get()
        {
            return Value;
        }

        public bool TryGet<T>(out T value) where T : class
        {
            value = default;

            if (Value is T)
            {
                value = Value as T;
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
            if (Value is string v)
            {
                return v;
            }
            else if (Value is MfImage img)
            {
                return img.Value;
            }
            else if (Value is MfEmbedded emb)
            {
                return emb.Value;
            }
            else
            {
                return ((MfSpec)Value).Value;
            }
        }

        public string GetName()
        {
            if(Value is  string v)
            {
                return v;
            }
            else if (Value is MfImage img)
            {
                return img.Value;
            }
            else if (Value is MfEmbedded emb)
            {
                return emb.Value;
            }
            else
            {
                return ((MfSpec)Value)?.Get<String>(Props.NAME)?.First();
            }
        }
    }
}
