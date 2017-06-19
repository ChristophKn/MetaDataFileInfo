namespace MetaDataFileInfo.Extensions
{
  using System;
  using System.Reflection;

  /// <summary>
  ///   The TypeExtensions class.
  /// </summary>
  public static class TypeExtensions
  {
    /// <summary>
    ///   Calls a generic static method on the given type.
    /// </summary>
    /// <param name="type">The type to use.</param>
    /// <param name="methodName">The method name for the method to call.</param>
    /// <param name="genericType">The generic type.</param>
    /// <param name="parameters">The parameters for the method.</param>
    /// <returns></returns>
    public static object CallGenericStaticMethod(this Type type, string methodName, Type genericType, object[] parameters)
    {
      var method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.OptionalParamBinding)
        .MakeGenericMethod(genericType);

      return method.Invoke(null, method.MakeParameterArray(parameters));
    }
  }
}