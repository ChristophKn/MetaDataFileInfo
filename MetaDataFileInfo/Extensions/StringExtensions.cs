namespace MetaDataFileInfo.Extensions
{
  using System;
  using System.Globalization;

  /// <summary>
  ///   The StringExtensions class.
  /// </summary>
  public static class StringExtensions
  {
    /// <summary>
    ///   Checks if the given string is null or empty.
    /// </summary>
    /// <param name="s">The string to check.</param>
    /// <returns>True if the value if null or empty, false if not.</returns>
    public static bool IsNullOrEmpty(this string s)
    {
      return string.IsNullOrEmpty(s);
    }

    /// <summary>
    /// Parses the given string to the given type.
    /// </summary>
    /// <typeparam name="T">The type to convert too.</typeparam>
    /// <param name="s">The string to convert.</param>
    /// <param name="throwExceptionIfParseError">Throws an error if the parsing failed.</param>
    /// <param name="numberStyles">The number style.</param>
    /// <returns>The converted string.</returns>
    public static T To<T>(this string s, bool throwExceptionIfParseError = false, NumberStyles numberStyles = NumberStyles.None)
    {
      if (s.IsNullOrEmpty())
      {
        return default(T);
      }

      if (typeof(T) == typeof(string))
      {
        return (T)(object)s;
      }

      if (typeof(T) == typeof(bool))
      {
        return (T)(object)(s.Trim() != "0");
      }

      try
      {
        if (typeof(T).IsEnum)
        {
          var value = (T)Enum.Parse(typeof(T), s.Trim(), true);
          return value;
        }

        var parseMethodParameterTypes = numberStyles == NumberStyles.None ? new[] { typeof(string) } : new[] { typeof(string), typeof(NumberStyles) };
        var parseParameter = numberStyles == NumberStyles.None ? new object[] { s } : new object[] { s, numberStyles };

        var type = typeof(T);
        type = type.IsGenericType ? Nullable.GetUnderlyingType(type) : type;
        var parseMethod = type.GetMethod("Parse", parseMethodParameterTypes);

        return (T)parseMethod.Invoke(null, parseParameter);
      }
      catch when (!throwExceptionIfParseError)
      {
        return default(T);
      }
    }
  }
}