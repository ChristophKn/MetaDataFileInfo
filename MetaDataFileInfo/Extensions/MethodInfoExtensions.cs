namespace MetaDataFileInfo.Extensions
{
  using System;
  using System.Linq;
  using System.Reflection;

  /// <summary>
  ///   The MethodInfoExtensions class.
  /// </summary>
  public static class MethodInfoExtensions
  {
    /// <summary>
    ///   Makes a parameter array for calling the method.
    /// </summary>
    /// <param name="method">The method to use.</param>
    /// <param name="parameters">The parameter to use.</param>
    /// <returns>The parameter to call the method.</returns>
    public static object[] MakeParameterArray(this MethodInfo method, object[] parameters)
    {
      var i = 0;
      var callParameters = method.GetParameters().Select(parameter => ++i > parameters.Length ? Type.Missing : parameters[i - 1]).ToArray();

      return callParameters;
    }
  }
}