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
            DOUBLE,
            BOOL,
            STRING
        }

        public struct Vo
        {
            public int IntValue { get; set; }
            public float FloatValue { get; set; }
            public bool BoolValue { get; set; }
            public string StringValue { get; set; }
            public double DoubleValue { get; set; }

            public Vo(int intValue = default(int), float floatValue = default(float), bool boolValue = default(bool), string stringValue = null, double doubleValue = default(double))
            {
                IntValue = intValue;
                FloatValue = floatValue;
                BoolValue = boolValue;
                StringValue = stringValue;
                DoubleValue = doubleValue;
            }
        }

        public class Values
        {
            private Dictionary<string, Vo> Dic { get; set; }
            public Values(Dictionary<string, Vo> dic)
            {
                Dic = dic;
            }

            public Vo GetValue(string name)
            {
                if(Dic.ContainsKey(name))
                {
                    return Dic[name];
                }

                return new Vo();
            }
        }

        public string KeyName { get; set; }
        public CommandValueType Type { get; set; }
        public object DefaultValue { get; set; }
        public int MaxIntValue { get; set; }

        public Vo GetValue(string value)
        {
            switch(Type)
            {
                case CommandValueType.INT:
                    int i = default(int);
                    if(int.TryParse(value, out i))
                    {
                        if(i > MaxIntValue)
                        {
                            i = MaxIntValue;
                        }

                        return new Vo(intValue: i);
                    }
                    else
                    {
                        return new Vo(intValue: (int)DefaultValue);
                    }
                case CommandValueType.FLOAT:
                    float f = default(float);
                    if (float.TryParse(value, out f))
                    {
                        return new Vo(floatValue: f);
                    }
                    else
                    {
                        return new Vo(floatValue: (float)DefaultValue);
                    }
                case CommandValueType.DOUBLE:
                    double d = default(double);
                    if (double.TryParse(value, out d))
                    {
                        return new Vo(doubleValue: d);
                    }
                    else
                    {
                        return new Vo(doubleValue: (double)DefaultValue);
                    }
                case CommandValueType.BOOL:
                    bool b = default(bool);
                    if (bool.TryParse(value, out b))
                    {
                        return new Vo(boolValue: b);
                    }
                    else
                    {
                        return new Vo(boolValue: (bool)DefaultValue);
                    }
                case CommandValueType.STRING:
                    if (!string.IsNullOrEmpty(value))
                    {
                        return new Vo(stringValue: value);
                    }
                    else
                    {
                        return new Vo(stringValue: (string)DefaultValue);
                    }
            }

            return new Vo();
        }
    }
}