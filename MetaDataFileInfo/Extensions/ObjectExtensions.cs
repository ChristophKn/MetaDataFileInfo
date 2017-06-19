namespace MetaDataFileInfo.Extensions
{
  using System;

  /// <summary>
  ///   The ObjectExtensions class.
  /// </summary>
  public static class ObjectExtensions
  {
    /// <summary>
    ///   Converts the object to a string. If the object is null, it'll return an empty string.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <returns>The object as string.</returns>
    public static string ToStringSafe(this object obj)
    {
      return obj?.ToString() ?? string.Empty;
    }

    /// <summary>
    ///   Converts the given object to the given type.
    /// </summary>
    /// <typeparam name="T">The type to convert too.</typeparam>
    /// <param name="obj">The object to convert.</param>
    /// <param name="throwExceptionOnParseError">Throws an exception when the given value is set to true.</param>
    /// <returns>The converted object.</returns>
    public static T To<T>(this object obj, bool throwExceptionOnParseError = false)
    {
      var sourceIsNum = obj != null && obj.GetType().IsEnum;
      if ((typeof(T).IsClass || typeof(T).IsInterface || sourceIsNum) && typeof(T) != typeof(string))
      {
        try
        {
          if (sourceIsNum)
          {
            var type = Enum.GetUnderlyingType(obj.GetType());
            if (type == typeof(T))
            {
              return (T)Enum.Parse(obj.GetType(), obj.ToString());
            }

            return obj.ToType(type).To<T>(throwExceptionOnParseError);
          }

          return (T)obj;
        }
        catch
        {
          if (throwExceptionOnParseError)
          {
            throw;
          }

          return default(T);
        }
      }

      return obj.ToStringSafe().To<T>(throwExceptionOnParseError);
    }

    /// <summary>
    ///   Converts the object to the given type.
    /// </summary>
    /// <param name="obj">The object to convert.</param>
    /// <param name="type">The type to convert too.</param>
    /// <returns>The converted object.</returns>
    public static object ToType(this object obj, Type type)
    {
      return typeof(ObjectExtensions).CallGenericStaticMethod("To", type, new[] { obj });
    }
  }
}