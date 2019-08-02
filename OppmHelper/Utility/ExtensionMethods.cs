// ***********************************************************************
// Assembly         : DitprService
// Author           : ggreiff
// Created          : 12-31-2017
//
// Last Modified By : ggreiff
// Last Modified On : 12-31-2017
// ***********************************************************************
// <copyright file="ExtensionMethods.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

//using NLog;

namespace OppmHelper.Utility
{
    /// <summary>
    /// Common Used Extention Methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// The nlogger
        /// </summary>
        //public static Logger Nlogger = LogManager.GetCurrentClassLogger();

        private static readonly Dictionary<RuntimeTypeHandle, XmlSerializer> MsSerializers = new Dictionary<RuntimeTypeHandle, XmlSerializer>();

        /// <summary>
        /// Count the number of words in a string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>System.Int32.</returns>
        public static int WordCount(this String input)
        {
            return input.Split(new[] { ' ', '.', '?' },
                               StringSplitOptions.RemoveEmptyEntries).Length;
        }

        /// <summary>
        /// Formats the XML.
        /// </summary>
        /// <param name="xml">The XML.</param>
        /// <returns>String.</returns>
        public static String FormatXml(this String xml)
        {
            try
            {
                var doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                return xml;
            }
        }

        public static bool IsOlderThan(this String filename, int hours)
        {
            var threshold = DateTime.Now.AddHours(-hours);
            return File.GetCreationTime(filename) <= threshold;
        }

        /// <summary>
        /// Extracts the between.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>String.</returns>
        public static String ExtractBetween(this String input, String start, String end)
        {
            if (!input.Contains(start)) return String.Empty;
            if (!input.Contains(end)) return String.Empty;
            var iStart = input.IndexOf(start, StringComparison.Ordinal);
            iStart = (iStart == -1) ? 0 : iStart + start.Length;
            var iEnd = input.LastIndexOf(end, StringComparison.Ordinal);
            if (iEnd == -1) iEnd = input.Length;
            var len = iEnd - iStart;
            return input.Substring(iStart, len);
        }

        /// <summary>
        /// Gets the type of the enumerated.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_">The .</param>
        /// <returns>Type.</returns>
        public static Type GetEnumeratedType<T>(this IEnumerable<T> _)
        {
            return typeof(T);
        }

        /// <summary>
        /// Copies from.
        /// </summary>
        /// <typeparam name="T1">The type of the t1.</typeparam>
        /// <typeparam name="T2">The type of the t2.</typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="otherObject">The other object.</param>
        /// <returns>T1.</returns>
        public static T1 CopyFrom<T1, T2>(this T1 obj, T2 otherObject) where T1 : class where T2 : class
        {
            var srcFields = otherObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
            var destFields = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty);

            foreach (var property in srcFields)
            {
                var dest = destFields.FirstOrDefault(x => x.Name == property.Name);
                if (dest != null && dest.CanWrite)
                    dest.SetValue(obj, property.GetValue(otherObject, null), null);
            }
            return obj;
        }

        public static string CleanFileName(this String input)
        {
            var fileName = String.Join("", input.Split(Path.GetInvalidFileNameChars()));
            return new Regex(@"\.(?!(\w{3,4}$))").Replace(fileName, "");
        }

        /// <summary>
        /// Copies the properties.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <exception cref="Exception">Source or/and Destination Objects are null</exception>
        public static void CopyProperties(this object source, object destination)
        {
            // If any this null throw an exception
            if (source == null || destination == null) throw new Exception("Source or/and Destination Objects are null");
            // Getting the Types of the objects
            var typeDest = destination.GetType();
            var typeSrc = source.GetType();

            // Iterate the Properties of the source instance and  
            // populate them from their desination counterparts  
            var srcProps = typeSrc.GetProperties();
            foreach (PropertyInfo srcProp in srcProps)
            {
                if (!srcProp.CanRead) continue;

                var targetProperty = typeDest.GetProperty(srcProp.Name);
                if (targetProperty == null) continue;
                if (!targetProperty.CanWrite) continue;
                if (targetProperty.GetSetMethod(true) != null && targetProperty.GetSetMethod(true).IsPrivate) continue;
                if ((targetProperty.GetSetMethod().Attributes & MethodAttributes.Static) != 0) continue;
                if (!targetProperty.PropertyType.IsAssignableFrom(srcProp.PropertyType)) continue;

                // Passed all tests, lets set the value
                targetProperty.SetValue(destination, srcProp.GetValue(source, null), null);
            }
        }



        /// <summary>
        /// Tries the cast.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The object.</param>
        /// <param name="result">The result.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool TryCast<T>(object obj, out T result)
        {
            result = default(T);
            if (obj is T)
            {
                result = (T)obj;
                return true;
            }

            // If it's null, we can't get the type.
            if (obj != null)
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter.CanConvertFrom(obj.GetType()))
                    result = (T)converter.ConvertFrom(obj);
                else
                    return false;

                return true;
            }

            //Be permissive if the object was null and the target is a ref-type
            return !typeof(T).IsValueType;
        }

        /// <summary>
        /// To the rounded double.
        /// </summary>
        /// <param name="toRoundDecimal">To round decimal.</param>
        /// <param name="places">The places.</param>
        /// <returns>Double.</returns>
        public static Double ToRoundedDouble(this Decimal toRoundDecimal, Int32 places)
        {
            return Math.Round(Decimal.ToDouble(toRoundDecimal), places, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// To the rounded double.
        /// </summary>
        /// <param name="toRoundDouble">To round double.</param>
        /// <param name="places">The places.</param>
        /// <returns>System.Nullable&lt;Double&gt;.</returns>
        public static Double? ToRoundedDouble(this Double? toRoundDouble, Int32 places)
        {
            if (toRoundDouble == null) return null;
            return Math.Round(toRoundDouble.Value, places, MidpointRounding.AwayFromZero);
        }


        /// <summary>
        /// Hexadecimals the string to string.
        /// </summary>
        /// <param name="hexString">The hexadecimal string.</param>
        /// <returns>String.</returns>
        public static String HexStringToString(this String hexString)
        {
            if (hexString == null || (hexString.Length & 1) == 1) return String.Empty;

            var sb = new StringBuilder();
            for (var i = 0; i < hexString.Length; i += 2)
            {
                var hexChar = hexString.Substring(i, 2);
                sb.Append((char)Convert.ToByte(hexChar, 16));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Lineses the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>String[].</returns>
        public static String[] Lines(this String source)
        {
            return source.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        /// <summary>
        /// Truncates the specified maximum length.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <returns>String.</returns>
        public static String Truncate(this String value, Int32 maxLength)
        {
            if (String.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns>String.</returns>
        public static String Right(this String value, int length)
        {
            if (value.IsNullOrEmpty() || value.Length < length) return value;
            return value.Substring(value.Length - length);
        }

        /// <summary>
        /// Lefts the specified length.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns>String.</returns>
        public static String Left(this String value, int length)
        {
            if (value.IsNullOrEmpty() || value.Length < length) return value;
            return value.Substring(0, length);
        }

        /// <summary>
        /// Reverses the specified s.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns>String.</returns>
        public static String Reverse(this String s)
        {
            var charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// Determines whether [contains] [the specified source].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="toCheck">To check.</param>
        /// <param name="comp">The comp.</param>
        /// <returns><c>true</c> if [contains] [the specified source]; otherwise, <c>false</c>.</returns>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            if (string.IsNullOrEmpty(toCheck) || string.IsNullOrEmpty(source))
                return false;

            return source.IndexOf(toCheck, comp) >= 0;
        }

        /// <summary>
        /// Converts a String to an Int32?
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>System.Nullable&lt;Int32&gt;.</returns>
        public static Int32? ToInt(this string input)
        {
            int val;
            if (int.TryParse(input, out val))
                return val;
            return null;
        }

        /// <summary>
        /// Converts a string to a DateTime?
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>System.Nullable&lt;DateTime&gt;.</returns>
        public static DateTime? ToDate(this string input)
        {
            DateTime val;
            if (DateTime.TryParse(input, out val)) return val;
            return null;
        }

        /// <summary>
        /// Converts a string to a Decimal?.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>System.Nullable&lt;Decimal&gt;.</returns>
        public static Decimal? ToDecimal(this string input)
        {
            decimal val;
            if (decimal.TryParse(input, out val))
                return val;
            return null;
        }

        /// <summary>
        /// Converts a string to a Single?.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>System.Nullable&lt;Single&gt;.</returns>
        public static Single? ToSingle(this string input)
        {
            Single val;
            if (Single.TryParse(input, out val))
                return val;
            return null;
        }

        /// <summary>
        /// Converts a string to a Double?.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>System.Nullable&lt;Double&gt;.</returns>
        public static Double? ToDouble(this string input)
        {
            Double val;
            if (Double.TryParse(input, out val))
                return val;
            return null;
        }

        /// <summary>
        /// Almosts the equals.
        /// </summary>
        /// <param name="double1">The double1.</param>
        /// <param name="double2">The double2.</param>
        /// <param name="precision">The precision.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AlmostEquals(this Double double1, Double double2, Double precision)
        {
            return (Math.Abs(double1 - double2) <= precision);
        }


        /// <summary>
        /// Almosts the equals.
        /// </summary>
        /// <param name="single1">The single1.</param>
        /// <param name="single2">The single2.</param>
        /// <param name="precision">The precision.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AlmostEquals(this Single single1, Single single2, Single precision)
        {
            return (Math.Abs(single1 - single2) <= precision);
        }

        /// <summary>
        /// Almosts the zero.
        /// </summary>
        /// <param name="double1">The double1.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AlmostZero(this Double double1)
        {
            return (Math.Abs(double1 - 0.0) <= .00000001);
        }

        /// <summary>
        /// Almosts the zero.
        /// </summary>
        /// <param name="single1">The single1.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AlmostZero(this Single single1)
        {
            return Convert.ToDouble(single1).AlmostZero();
        }

        /// <summary>
        /// Almosts the zero.
        /// </summary>
        /// <param name="decimal1">The decimal1.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AlmostZero(this Decimal? decimal1)
        {
            return decimal1 == null || Convert.ToDouble(decimal1).AlmostZero();
        }

        /// <summary>
        /// Almosts the zero.
        /// </summary>
        /// <param name="decimal1">The decimal1.</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public static bool AlmostZero(this Decimal decimal1)
        {
            return Convert.ToDouble(decimal1).AlmostZero();
        }

        /// <summary>
        /// Determines whether the compareTo string [is equal to] [the specified input string].
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="compareTo">The string to compare to.</param>
        /// <param name="ignoreCase">if set to <c>true</c> [ignore case].</param>
        /// <returns><c>true</c> if [is equal to] [the specified string]; otherwise, <c>false</c>.</returns>
        public static Boolean IsEqualTo(this String input, String compareTo, Boolean ignoreCase)
        {
            return String.Compare(input, compareTo, ignoreCase) == 0;
        }


        /// <summary>
        /// Determines whether [is not equal to] [the specified compare to].
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="compareTo">The compare to.</param>
        /// <param name="ignoreCase">The ignore case.</param>
        /// <returns>Boolean.</returns>
        public static Boolean IsNotEqualTo(this String input, String compareTo, Boolean ignoreCase)
        {
            return !input.IsEqualTo(compareTo, ignoreCase);
        }

        /// <summary>
        /// Determines whether the specified input string is empty.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if the specified input is empty; otherwise, <c>false</c>.</returns>
        public static Boolean IsEmpty(this String input)
        {
            return (input != null && input.Length == 0);
        }

        /// <summary>
        /// Determines whether the specified input is null.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns><c>true</c> if the specified input string is null; otherwise, <c>false</c>.</returns>
        public static Boolean IsNull(this String input)
        {
            return (input == null);
        }

        /// <summary>
        /// Determines whether the input string [is null or empty].
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns><c>true</c> if [is null or empty] [the specified input]; otherwise, <c>false</c>.</returns>
        public static Boolean IsNullOrEmpty(this String input)
        {
            return String.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Determines whether [is not null or white space] [the specified input].
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Boolean.</returns>
        public static Boolean IsNotNullOrWhiteSpace(this String input)
        {
            return !input.IsNullOrWhiteSpace();
        }

        /// <summary>
        /// Determines whether [is null or white space] [the specified input].
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Boolean.</returns>
        public static Boolean IsNullOrWhiteSpace(this String input)
        {
            return input.IsNullOrEmpty() || input.Trim().Length == 0;
        }

        /// <summary>
        /// Determines whether the input string [is not null or empty].
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns><c>true</c> if [is not null or empty] [the specified input string]; otherwise, <c>false</c>.</returns>
        public static Boolean IsNotNullOrEmpty(this String input)
        {
            return !String.IsNullOrEmpty(input);
        }

        /// <summary>
        /// Gets a CLS complaint string based on the input string.
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <returns>String.</returns>
        public static String GetClsString(this String input)
        {
            return Regex.Replace(input.Trim(), @"[\W]", @"_");
        }

        /// <summary>
        /// Gets the name of the month.
        /// </summary>
        /// <param name="monthNumber">The month number.</param>
        /// <returns>String.</returns>
        public static String GetMonthName(Int32 monthNumber)
        {
            return GetMonthName(monthNumber, false);
        }

        /// <summary>
        /// Gets the name of the month.
        /// </summary>
        /// <param name="monthNumber">The month number.</param>
        /// <param name="abbreviateMonth">if set to <c>true</c> [abbreviate month name].</param>
        /// <returns>String.</returns>
        public static String GetMonthName(Int32 monthNumber, Boolean abbreviateMonth)
        {
            if (monthNumber < 1 || monthNumber > 12) return String.Empty;
            var date = new DateTime(1, monthNumber, 1);
            return abbreviateMonth ? date.ToString("MMM") : date.ToString("MMMM");
        }

        /// <summary>
        /// Datas the table to list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table">The table.</param>
        /// <returns>List&lt;T&gt;.</returns>
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                var list = new List<T>();
                foreach (var row in table.AsEnumerable())
                {
                    var obj = new T();
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            var propertyInfo = obj.GetType().GetProperty(prop.Name);
                            if (row[prop.Name] == null) continue;
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            // ignored
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Get the Rows in a list for a the table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <returns>List&lt;DataRow&gt;.</returns>
        public static List<DataRow> RowList(this DataTable table)
        {
            return table.Rows.Cast<DataRow>().ToList();
        }

        /// <summary>
        /// Gets the field item frp a row.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="field">The field.</param>
        /// <returns>System.Object.</returns>
        public static object GetItem(this DataRow row, String field)
        {
            return !row.Table.Columns.Contains(field) ? null : row[field];
        }

        /// <summary>
        /// Filters the rows to a list.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>List&lt;DataRow&gt;.</returns>
        public static List<DataRow> FilterRowsToList(this DataTable table, List<Int32> ids, String fieldName)
        {
            Func<DataRow, bool> filter = row => ids.Contains((Int32)row.GetItem(fieldName));
            return table.RowList().Where(filter).ToList();
        }

        /// <summary>
        /// Filters the rows to a list.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>List&lt;DataRow&gt;.</returns>
        public static List<DataRow> FilterRowsToList(this DataTable table, List<String> ids, String fieldName)
        {
            Func<DataRow, bool> filter = row => ids.Contains((String)row.GetItem(fieldName));
            return table.RowList().Where(filter).ToList();
        }

        /// <summary>
        /// Filters the rows to a data table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>DataTable.</returns>
        public static DataTable FilterRowsToDataTable(this DataTable table, List<Int32> ids, String fieldName)
        {
            DataTable filteredTable = table.Clone();
            List<DataRow> matchingRows = FilterRowsToList(table, ids, fieldName);

            foreach (DataRow filteredRow in matchingRows)
            {
                filteredTable.ImportRow(filteredRow);
            }
            return filteredTable;
        }

        /// <summary>
        /// Filters the rows to a data table.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="ids">The ids.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns>DataTable.</returns>
        public static DataTable FilterRowsToDataTable(this DataTable table, List<String> ids, String fieldName)
        {
            DataTable filteredTable = table.Clone();
            List<DataRow> matchingRows = FilterRowsToList(table, ids, fieldName);

            foreach (DataRow filteredRow in matchingRows)
            {
                filteredTable.ImportRow(filteredRow);
            }
            return filteredTable;
        }

        /// <summary>
        /// Selects the into table the rows base on the filter.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>DataTable.</returns>
        public static DataTable SelectIntoTable(this DataTable table, String filter)
        {
            DataTable selectResults = table.Clone();
            DataRow[] rows = table.Select(filter);
            foreach (DataRow row in rows)
            {
                selectResults.ImportRow(row);
            }
            return selectResults;
        }

        /// <summary>
        /// Deletes rows from the table base on the filter.
        /// </summary>
        /// <param name="table">The table.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>DataTable.</returns>
        public static DataTable Delete(this DataTable table, string filter)
        {
            table.Select(filter).Delete();
            return table;
        }

        /// <summary>
        /// Deletes the specified rows.
        /// </summary>
        /// <param name="rows">The rows.</param>
        public static void Delete(this IEnumerable<DataRow> rows)
        {
            foreach (DataRow row in rows)
                row.Delete();
        }

    /// <summary>
        /// Gets the or add value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dict">The dict.</param>
        /// <param name="key">The key.</param>
        /// <returns>TValue.</returns>
        public static TValue GetOrAddValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, TKey key)
                where TValue : new()
        {
            TValue value;
            if (dict.TryGetValue(key, out value))
                return value;
            value = new TValue();
            dict.Add(key, value);
            return value;
        }

        /// <summary>
        /// Gets the or add value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="dict">The dict.</param>
        /// <param name="key">The key.</param>
        /// <param name="generator">The generator.</param>
        /// <returns>TValue.</returns>
        public static TValue GetOrAddValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, TKey key, Func<TValue> generator)
        {
            TValue value;
            if (dict.TryGetValue(key, out value))
                return value;
            value = generator();
            dict.Add(key, value);
            return value;
        }

        /// <summary>
        /// Determines whether the specified enumerable has items.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns><c>true</c> if the specified enumerable has items; otherwise, <c>false</c>.</returns>
        public static Boolean HasItems(this IEnumerable enumerable)
        {
            if (enumerable == null) return false;
            try
            {
                var enumerator = enumerable.GetEnumerator();
                if (enumerator.MoveNext()) return true;
            }
            catch
            {
                return false;
            }
            return false;
        }

        public static Boolean NoItems(this IEnumerable enumerable)
        {
            return !HasItems(enumerable);
        }

        /// <summary>
        /// Determines whether the specified enumerable is empty.
        /// </summary>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns><c>true</c> if the specified enumerable is empty; otherwise, <c>false</c>.</returns>
        public static Boolean IsEmpty(this IEnumerable enumerable)
        {
            return !enumerable.HasItems();
        }

        /// <summary>
        /// Removes the invalid chars.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>String.</returns>
        public static String RemoveInvalidChars(this String fileName)
        {
            var regex = $"[{Regex.Escape(new String(Path.GetInvalidFileNameChars()))}]";
            var removeInvalidChars = new Regex(regex,
                                               RegexOptions.Singleline | RegexOptions.Compiled |
                                               RegexOptions.CultureInvariant);
            return removeInvalidChars.Replace(fileName, "");
        }

        /// <summary>
        /// Properties the set on a given object.
        /// </summary>
        /// <param name="p">The object to set the property on.</param>
        /// <param name="propName">Name of the prop.</param>
        /// <param name="value">The value.</param>
        public static void PropertySet(this Object p, String propName, Object value)
        {
            var t = p.GetType();
            var info = t.GetProperty(propName);
            if (info == null) return;
            if (!info.CanWrite) return;
            info.SetValue(p, value, null);
        }

        /*
        public static Boolean IsAnyNullOrEmpty(this Object obj)
        {
            if (obj is null) return false;
            return obj.GetType().GetProperties().Any(x => IsNullOrEmpty(x.GetValue(obj)));
        }

        public static Boolean IsNullOrEmpty(this Object value)
        {
            if (value is null) return false;
            var type = value.GetType();
            return type.IsValueType && Object.Equals(value, Activator.CreateInstance(type));
        }
        */

        /// <summary>
        /// Determines whether the specified item is between.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns><c>true</c> if the specified item is between; otherwise, <c>false</c>.</returns>
        public static bool IsBetween<T>(this T item, T start, T end)
        {
            return Comparer<T>.Default.Compare(item, start) >= 0
                && Comparer<T>.Default.Compare(item, end) <= 0;
        }

        /// <summary>
        /// Objects to XML. UTF-8 -- use this one!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <returns>System.String.</returns>
        public static String ObjectToXml<T>(this T item)
        {
            var serializer = new XmlSerializer(typeof(T));
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(true),
                Indent = true,
                OmitXmlDeclaration = false,
                NewLineHandling = NewLineHandling.None
            };
            using (var stream = new MemoryStream())
            {
                using (var xmlWriter = XmlWriter.Create(stream, settings))
                {
                    serializer.Serialize(xmlWriter, item, ns);
                }
                return Encoding.UTF8.GetString(stream.ToArray()).RemoveBom();
            }
        }

        /// <summary>
        /// Removes the bom.
        /// </summary>
        /// <param name="p">The p.</param>
        /// <returns>String.</returns>
        public static String RemoveBom(this String p)
        {
            var bomMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (p.StartsWith(bomMarkUtf8)) p = p.Remove(0, bomMarkUtf8.Length);
            return p.Replace("\0", "");
        }

        public static string ToXml<T>(this T value)
    where T : new()
        {
            var serializer = GetValue(typeof(T));
            using (var stream = new MemoryStream())
            {
                using (var writer = new XmlTextWriter(stream, new UTF8Encoding()))
                {
                    serializer.Serialize(writer, value);
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }
        }
        /// <summary>
        ///   Serialize object to stream by <see cref = "XmlSerializer" />
        /// </summary>
        /// <typeparam name = "T"></typeparam>
        /// <param name = "value"></param>
        /// <param name = "stream"></param>
        public static void ToXml<T>(this T value, Stream stream)
            where T : new()
        {
            var serializer = GetValue(typeof(T));
            serializer.Serialize(stream, value);
        }

        /// <summary>
        ///   Deserialize object from string
        /// </summary>
        /// <typeparam name = "T">Type of deserialized object</typeparam>
        /// <param name = "srcString">Xml source</param>
        /// <returns></returns>
        public static T FromXml<T>(this string srcString)
            where T : new()
        {
            var serializer = GetValue(typeof(T));
            using (var stringReader = new StringReader(srcString))
            {
                using (XmlReader reader = new XmlTextReader(stringReader))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
        }
        /// <summary>
        ///   Deserialize object from stream
        /// </summary>
        /// <typeparam name = "T">Type of deserialized object</typeparam>
        /// <param name = "source">Xml source</param>
        /// <returns></returns>
        public static T FromXml<T>(this Stream source)
            where T : new()
        {
            var serializer = GetValue(typeof(T));
            return (T)serializer.Deserialize(source);
        }

        private static XmlSerializer GetValue(Type type)
        {
            var retVal = new XmlSerializer(type);
            lock (MsSerializers)
            {
                if (MsSerializers.TryGetValue(type.TypeHandle, out retVal)) return retVal;
                lock (MsSerializers)
                {
                    if (!MsSerializers.TryGetValue(type.TypeHandle, out retVal))
                    {
                        retVal = new XmlSerializer(type);
                        MsSerializers.Add(type.TypeHandle, retVal);
                        return retVal;
                    }
                }
            }
            return retVal;
        }
    }
}