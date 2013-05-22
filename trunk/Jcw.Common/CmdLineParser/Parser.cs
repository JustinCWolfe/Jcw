using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Reflection;

namespace Jcw.Common.CmdLineParser
{
    public sealed class Parser
    {
        [Flags]
        private enum ParseResult
        {
            Success = 0x01,
            Failure = 0x02,
            MoveOnNextElement = 0x04
        }

        private OptionMap m_optionMap;

        private interface IParser
        {
            ParseResult Parse (StringEnumerator argumentEnumerator, IDictionary map, object options);
        }

        private class LongOptionParser : IParser
        {
            public ParseResult Parse (StringEnumerator argumentEnumerator, IDictionary map, object options)
            {
                string[] parts = argumentEnumerator.Current.Substring (2).Split (new char[] { '=' }, 2);

                OptionInfo option = (OptionInfo)map[parts[0]];
                if (option == null)
                {
                    return ParseResult.Failure;
                }

                option.IsDefined = true;

                if (!option.IsBoolean)
                {
                    if (parts.Length == 1 && !argumentEnumerator.IsLast && !IsInputValue (argumentEnumerator.Next))
                    {
                        return ParseResult.Failure;
                    }

                    if (parts.Length == 2)
                    {
                        if (option.SetValue (parts[1], options))
                        {
                            return ParseResult.Success;
                        }
                        else
                        {
                            return ParseResult.Failure;
                        }
                    }
                    else
                    {
                        if (option.SetValue (argumentEnumerator.Next, options))
                        {
                            return ParseResult.Success | ParseResult.MoveOnNextElement;
                        }
                        else
                        {
                            return ParseResult.Failure;
                        }
                    }
                }
                else
                {
                    if (parts.Length == 2)
                    {
                        return ParseResult.Failure;
                    }
                    if (option.SetValue (true, options))
                    {
                        return ParseResult.Success;
                    }
                    else
                    {
                        return ParseResult.Failure;
                    }
                }
            }
        }

        private class OptionGroupParser : IParser
        {
            public ParseResult Parse (StringEnumerator argumentEnumerator, IDictionary map, object options)
            {
                LetterEnumerator group = new LetterEnumerator (argumentEnumerator.Current.Substring (1));

                while (group.MoveNext ())
                {
                    OptionInfo option = (OptionInfo)map[group.Current];

                    if (option == null)
                    {
                        return ParseResult.Failure;
                    }

                    option.IsDefined = true;

                    if (!option.IsBoolean)
                    {
                        if (argumentEnumerator.IsLast && group.IsLast)
                        {
                            return ParseResult.Failure;
                        }

                        if (!group.IsLast)
                        {
                            if (option.SetValue (group.SubstringFromNext (), options))
                            {
                                return ParseResult.Success;
                            }
                            else
                            {
                                return ParseResult.Failure;
                            }
                        }

                        if (!argumentEnumerator.IsLast && !IsInputValue (argumentEnumerator.Next))
                        {
                            return ParseResult.Failure;
                        }
                        else
                        {
                            if (option.SetValue (argumentEnumerator.Next, options))
                            {
                                return ParseResult.Success | ParseResult.MoveOnNextElement;
                            }
                            else
                            {
                                return ParseResult.Failure;
                            }
                        }
                    }
                    else
                    {
                        if (!group.IsLast && map[group.Next] == null)
                        {
                            return ParseResult.Failure;
                        }

                        if (!option.SetValue (true, options))
                        {
                            return ParseResult.Failure;
                        }
                    }
                }

                return ParseResult.Success;
            }
        }

        private Parser ()
        {
        }

        public Parser (Type type)
        {
            m_optionMap = OptionInfo.CreateMap (type);
        }

        public static string ParseArguments (string[] args, object options)
        {
            Parser parser = new Parser (options.GetType ());
            return parser.ParseArgumentList (args, options) ? "" : null;
        }

        private bool ParseArgumentList (string[] args, object options)
        {
            bool hadError = false;

            ValueContainer valueContainer = options as ValueContainer;
            StringEnumerator arguments = new StringEnumerator (args);

            while (arguments.MoveNext ())
            {
                string argument = arguments.Current;

                if (argument != null && argument.Length > 0)
                {
                    IParser parser = CreateParser (argument);

                    if (parser != null)
                    {
                        ParseResult result = parser.Parse (arguments, this.m_optionMap, options);

                        if ((result & ParseResult.Failure) == ParseResult.Failure)
                        {
                            return false;
                        }

                        if ((result & ParseResult.MoveOnNextElement) == ParseResult.MoveOnNextElement)
                        {
                            arguments.MoveNext ();
                        }
                    }
                    else
                    {
                        if (valueContainer != null)
                        {
                            valueContainer.Values.Add (argument);
                        }
                    }
                }
            }

            hadError |= !CheckRules (this.m_optionMap);
            return !hadError;
        }

        private static bool IsInputValue (string argument)
        {
            return argument[0] != '-';
        }

        private static IParser CreateParser (string argument)
        {
            // if the first 2 characters are -- it is a long option
            if (argument[0] == '-' && argument[1] == '-')
            {
                return new LongOptionParser ();
            }

            // if only the first character is -
            if (argument[0] == '-')
            {
                return new OptionGroupParser ();
            }

            return null;
        }

        private static bool CheckRules (Hashtable map)
        {
            foreach (DictionaryEntry entry in map)
            {
                OptionInfo option = (OptionInfo)entry.Value;

                if (option.Required && !option.IsDefined)
                {
                    Error.LastError = "Missing required argument. ";
                    Error.LastError += option.HasBothNames ?
                        "Long name: " + option.LongName : "Short name: " + option.ShortName;

                    return false;
                }
            }

            return true;
        }

        private class OptionMap : Hashtable
        {
            private StringDictionary names;

            public OptionMap (int capacity)
                : base (capacity)
            {
                names = new StringDictionary ();
            }

            public sealed override object this[object key]
            {
                get
                {
                    object option = base[key];

                    if (option == null)
                    {
                        string optionKey = names[key.ToString ()];

                        if (optionKey != null)
                        {
                            option = base[optionKey];
                        }
                    }

                    return option;
                }
                set
                {
                    OptionInfo option = (OptionInfo)value;

                    base[key] = option;

                    if (option.HasBothNames)
                    {
                        names[option.LongName] = option.ShortName;
                    }
                }
            }
        }

        private class OptionInfo
        {
            private bool required;
            private bool isDefined;
            private string longName;
            private FieldInfo field;
            private string shortName;

            public bool Required
            {
                get { return required; }
            }

            public bool IsBoolean
            {
                get { return (field.FieldType == typeof (bool)); }
            }

            public bool IsDefined
            {
                get { return isDefined; }
                set { isDefined = value; }
            }

            public string ShortName
            {
                get { return shortName; }
            }

            public string LongName
            {
                get { return longName; }
            }

            public bool HasBothNames
            {
                get { return (shortName != null && longName != null); }
            }

            public OptionInfo (CmdLineOptionAttribute attribute, FieldInfo field)
            {
                this.field = field;
                required = attribute.Required;
                shortName = attribute.ShortName;
                longName = attribute.LongName;
            }

            public static OptionMap CreateMap (Type type)
            {
                FieldInfo[] fields = type.GetFields ();
                OptionMap map = new OptionMap (fields.Length);

                foreach (FieldInfo field in fields)
                {
                    if (!field.IsStatic && !field.IsInitOnly && !field.IsLiteral)
                    {
                        CmdLineOptionAttribute attribute = CmdLineOptionAttribute.FromField (field);

                        if (attribute != null)
                        {
                            map[attribute.UniqueName] = new OptionInfo (attribute, field);
                        }
                    }
                }

                return map;
            }

            public bool SetValue (string value, object options)
            {
                try
                {
                    field.SetValue (options, Convert.ChangeType (value, field.FieldType, CultureInfo.InvariantCulture));
                }
                catch (InvalidCastException e)
                {
                    Error.LastError = e.Message;
                    return false;
                }
                catch (FormatException e)
                {
                    Error.LastError = e.Message;
                    return false;
                }

                return true;
            }

            public bool SetValue (bool value, object options)
            {
                field.SetValue (options, value);
                return true;
            }
        }
    }
}
