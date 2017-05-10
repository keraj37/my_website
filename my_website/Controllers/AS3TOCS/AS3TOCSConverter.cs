using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace AS3TOCS
{
    public class AS3TOCSConverter
    {
        private string className = null;

        public AS3TOCSConverter()
        {
        }

        public string[] Convert(string as3)
        {
            string as3content = as3;

            className = GetClassName(as3content);

            as3content = BasicChanges(as3content);
            as3content = AdvancedConvertion(as3content);
            as3content = Finalizing(as3content);

            return new string[] { as3content, className };
        }

        private string GetClassName(string as3content)
        {
            int i = as3content.IndexOf(" class ");

            if (i != -1)
            {
                string afterClass = as3content.Substring(i + " class ".Length, as3content.Length - (i + " class ".Length) - 1);
                string[] ss = afterClass.Split(" "[0]);

                return ss[0];
            }

            return null;
        }

        private string BasicChanges(string as3connent)
        {
            as3connent = as3connent.Replace("'", "\"");
            as3connent = as3connent.Replace("static const", "const");
            as3connent = as3connent.Replace("const static", "const");
            as3connent = as3connent.Replace("Vector.", "List");
            as3connent = as3connent.Replace("uint", "int");
            as3connent = as3connent.Replace("String", "string");
            as3connent = as3connent.Replace("Number", "float");
            as3connent = as3connent.Replace("Boolean", "bool");
            as3connent = as3connent.Replace("override protected", "protected override");
            as3connent = as3connent.Replace("override public", "public override");
            as3connent = as3connent.Replace("TextField", "Text");
            as3connent = as3connent.Replace("for each", "foreach");

            return as3connent;
        }

        private string Finalizing(string as3connent)
        {
            as3connent = as3connent.Replace("extends", ":");
            as3connent = as3connent.Replace("implements", ":");

            as3connent = as3connent.Replace("function", "");
            as3connent = as3connent.Replace("      ", " ");
            as3connent = as3connent.Replace("     ", " ");
            as3connent = as3connent.Replace("    ", " ");
            as3connent = as3connent.Replace("   ", " ");
            as3connent = as3connent.Replace("  ", " ");

            as3connent = as3connent.Replace(" )", ")");
            as3connent = as3connent.Replace("( ", "(");
            as3connent = as3connent.Replace(" ,", ",");
            as3connent = as3connent.Replace(",  ", ", ");
            as3connent = as3connent.Replace(",   ", ", ");
            as3connent = as3connent.Replace(" :", ":");

            as3connent = as3connent.Insert(0, "using UnityEngine.UI;\n\n");
            as3connent = as3connent.Insert(0, "using UnityEngine;\n");
            as3connent = as3connent.Insert(0, "using System.Collections.Generic;\n");
            as3connent = as3connent.Insert(0, "using System.Collections;\n");
            as3connent = as3connent.Insert(0, "using System;\n");

            return as3connent;
        }

        private int GetLowestIndex(params int[] ints)
        {
            int lowest = int.MaxValue;
            foreach (int i in ints)
            {
                if (i > -1)
                {
                    if (i < lowest)
                    {
                        lowest = i;
                    }
                }
            }

            return lowest;
        }

        private int GetHighestIndex(params int[] ints)
        {
            int highest = -2;
            foreach (int i in ints)
            {
                if (i > -1)
                {
                    if (i > highest)
                    {
                        highest = i;
                    }
                }
            }

            return highest;
        }

        private string[] RemoveOneTab(string[] lines)
        {
            List<string> result = new List<string>();

            foreach (string l in lines)
            {
                int i = l.IndexOf("\t");

                if (i != -1)
                {
                    result.Add(l.Remove(i, 1));
                }
                else
                {
                    result.Add(l);
                }
            }

            return result.ToArray();
        }

        private string GatherLines(string[] lines)
        {
            string result = "";

            foreach (string l in lines)
            {
                result += l + "\n";
            }

            return result;
        }

        private string AdvancedConvertion(string as3content)
        {
            string[] lines = as3content.Split("\n"[0]);

            lines = TypeConvertion(lines);
            lines = Formating(lines);

            string result = GatherLines(lines);
            return result;
        }

        private string[] Formating(string[] lines)
        {
            List<string> result = new List<string>();

            foreach (string l in lines)
            {
                string l2 = l;
                if (l2.Contains("{"))
                {
                    if (l2.Trim().Length > 1)
                    {
                        l2 = l2.Replace("{", "");

                        l2 = AddTabs(l2);
                        l2 += "{";
                    }
                }

                result.Add(l2);
            }

            return result.ToArray();
        }

        private int GetIndexAfterType(string l)
        {
            int indexOfEmpty = l.IndexOf(" ");
            int indexOfBarcket = l.IndexOf(")");
            int indexOfSemiclolon = l.IndexOf(";");
            int indexOfequal = l.IndexOf("=");
            int indexOfComma = l.IndexOf(",");
            int indexOfBracket2 = l.IndexOf("{");

            int index = GetLowestIndex(indexOfEmpty, indexOfBarcket, indexOfSemiclolon, indexOfequal, indexOfComma, indexOfBracket2);

            return index;
        }

        private int GetIndexBeforeVariableName(string l)
        {
            int indexOf1 = l.LastIndexOf("(");
            int indexOf2 = l.LastIndexOf(",");

            int index = GetHighestIndex(indexOf1, indexOf2);
            return index;
        }

        private string SingleTypeConvertion(string l, bool isFunction = false)
        {
            int i = l.IndexOf(":");
            string firstPart = l.Substring(0, i);
            string secondPart = l.Replace(firstPart, "");

            int index = GetIndexAfterType(secondPart);

            string type = "";

            if (index < 0 || index >= secondPart.Length)
            {
                Console.WriteLine("Error in type detection: " + l);
                return l;
            }
            else
            {
                type = secondPart.Substring(1, index - 1);

                if (!isFunction)
                {
                    int indexOfVar = firstPart.IndexOf("var");

                    if (indexOfVar > -1)
                    {
                        firstPart = firstPart.Replace("var", type);
                    }
                    else
                    {
                        int lastIndexOfempty = firstPart.LastIndexOf(" ");
                        firstPart = firstPart.Insert(lastIndexOfempty + 1, type + " ");
                    }
                }
                else
                {
                    firstPart = firstPart.Replace("function", type);
                }

                secondPart = secondPart.Replace(":" + type, "");

                string finalLine = firstPart + secondPart;
                return finalLine;
            }
        }

        private string MultipleTypeConvertion(string l)
        {
            string[] ll = l.Split(":"[0]);

            if (ll.Length <= 1)
                return l;

            int index = GetIndexAfterType(ll[ll.Length - 1]);
            string lastType = ll[ll.Length - 1].Substring(0, index);

            ll[ll.Length - 1] = ll[ll.Length - 1].Replace(lastType, "");
            ll[0] = ll[0].Replace("function", lastType);

            for (int i = ll.Length - 2; i >= 1; i--)
            {
                int index2 = GetIndexAfterType(ll[i]);
                string currentType = ll[i].Substring(0, index2);
                ll[i] = ll[i].Remove(0, currentType.Length);

                int index3 = GetIndexBeforeVariableName(ll[i - 1]);

                ll[i - 1] = ll[i - 1].Insert(index3 + 1, " " + currentType + " ");
            }

            string finalLine = "";
            foreach (string s in ll)
                finalLine += s + " ";

            return finalLine;
        }

        private string ChangeFunctionNameToUpperCase(string l)
        {
            int index = l.IndexOf("function");
            int index2 = index + "function".Length + 1;

            string uppercase = l[index2].ToString().ToUpper();
            l = l.Remove(index2, 1);
            l = l.Insert(index2, uppercase);

            return l;
        }

        private string AddTabs(string belowLine, bool includeCurrentString = true)
        {
            string result = includeCurrentString ? belowLine : "";
            int i = belowLine.Count(f => f == "\t"[0]);
            for (int k = 0; k < i; k++)
                result += "\t";

            return result;
        }

        private string CreateGetter(string l)
        {
            const string getFull = " get ";

            l = l.Replace(" Get ", getFull);

            int index = l.IndexOf(getFull);
            int index2 = index + getFull.Length;

            string uppercase = l[index2].ToString().ToUpper();
            l = l.Remove(index2, 1);
            l = l.Insert(index2, uppercase);

            string result = SingleTypeConvertion(l, true);
            result = result.Replace(getFull, " ");
            result = result.Replace("(", "");
            result = result.Replace(")", "");

            string comment = AddTabs(result, false);

            comment += "//Getter. Please add \"get\" block manually\n";
            result = result.Insert(0, comment);

            return result;
        }

        private string CreateSetter(string l)
        {
            const string setFull = " set ";
            l = l.Replace(" Set ", setFull);

            int index = l.IndexOf(setFull);
            int index2 = index + setFull.Length;

            string uppercase = l[index2].ToString().ToUpper();
            l = l.Remove(index2, 1);
            l = l.Insert(index2, uppercase);

            string result = SingleTypeConvertion(l);

            result = result.Remove(result.IndexOf("("[0]), result.IndexOf(")"[0]) - result.IndexOf("("[0]) + 1);
            result = result.Replace(":void", "");
            result = result.Replace(setFull, "");

            string comment = AddTabs(result, false);

            comment += "//Setter. Please add \"set\" block manually\n";
            result = result.Insert(0, comment);

            return result;
        }

        private string[] TypeConvertion(string[] lines)
        {
            List<string> result = new List<string>();
            int k = 0;
            foreach (string l in lines)
            {
                if (l.Contains(":") && !l.Contains("case ") && !l.Contains("function"))
                {
                    string finalLine = SingleTypeConvertion(l);
                    result.Add(finalLine);
                }
                else if (l.Contains("function"))
                {
                    string l2 = ChangeFunctionNameToUpperCase(l);

                    string[] ll = l2.Split(":"[0]);
                    if (ll.Length == 2)
                    {
                        if (l.ToLower().Contains(" get "))
                        {
                            string finalLine = CreateGetter(l2);
                            result.Add(finalLine);
                        }
                        else
                        {
                            string finalLine = SingleTypeConvertion(l2, true);
                            result.Add(finalLine);
                        }
                    }
                    else if (ll.Length > 2)
                    {
                        if (l.ToLower().Contains(" set "))
                        {
                            string finalLine = CreateSetter(l2);
                            result.Add(finalLine);
                        }
                        else
                        {
                            string finalLine = MultipleTypeConvertion(l2);
                            result.Add(finalLine);
                        }
                    }
                    else
                    {
                        result.Add(l2);
                    }
                }
                else
                {
                    string l2 = l;
                    /*
                    if (l2.Replace(" ", "").Contains("}else"))
                    {
                        int i = l2.IndexOf("else");
                        l2 = l2.Insert(i, AddTabs("\n" + lines[k + 1]));
                    }
                    */

                    result.Add(l2);
                }

                k++;
            }

            string[] linesResult1 = result.ToArray();
            linesResult1 = RemoveOneTab(linesResult1);
            string fulltext = GatherLines(linesResult1);
            string[] linesResult2 = fulltext.Split("\n"[0]);

            return linesResult2;
        }
    }
}
