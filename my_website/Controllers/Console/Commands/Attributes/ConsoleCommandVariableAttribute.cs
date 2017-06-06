using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace my_website.Controllers.Console.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ConsoleCommandVariableAttribute : Attribute
    {
        public enum CommandValueType
        {
            INT,
            FLOAT,
            BOOL,
            STRING
        }

        public struct Vo
        {
            public int? IntValue { get; set; }
            public float? FloatValue { get; set; }
            public bool? BoolValue { get; set; }
            public string StringValue { get; set; }

            public Vo(int? intValue = null, float? floatValue = null, bool? boolValue = null, string stringValue = null)
            {
                IntValue = intValue;
                FloatValue = floatValue;
                BoolValue = boolValue;
                StringValue = stringValue;
            }
        }

        public byte Order { get; set; }
        public string KeyName { get; set; }
        public CommandValueType Type { get; set; }

        public Vo GetValue(string value)
        {
            switch(Type)
            {
                case CommandValueType.INT:
                    int i = default(int);
                    if(int.TryParse(value, out i))
                    {
                        return new Vo(intValue: i);
                    }
                    break;
                case CommandValueType.FLOAT:
                    float f = default(float);
                    if (float.TryParse(value, out f))
                    {
                        return new Vo(floatValue: f);
                    }
                    break;
                case CommandValueType.BOOL:
                    bool b = default(bool);
                    if (bool.TryParse(value, out b))
                    {
                        return new Vo(boolValue: b);
                    }
                    break;
                case CommandValueType.STRING:
                    string s = null;
                    if (!string.IsNullOrEmpty(s))
                    {
                        return new Vo(stringValue: s);
                    }
                    break;
            }

            return new Vo();
        }
    }
}