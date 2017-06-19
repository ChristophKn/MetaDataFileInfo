namespace MetaDataFileInfo.Extensions
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;
  using Comparer;
  using Microsoft.WindowsAPICodePack.Shell;
  using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

  /// <summary>
  ///   The ShellFileExtensions class.
  /// </summary>
  internal static class ShellFileExtensions
  {
    /// <summary>
    ///   Gets the properties with its display name of the shell file.
    /// </summary>
    /// <param name="shellFile">The shell file to use.</param>
    /// <returns>The property keys with the display name.</returns>
    public static IDictionary<string, PropertyKey> GetPropertyKeysWithName(this ShellFile shellFile)
    {
      const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
      var propertyType = typeof(SystemProperties.System);

      var propertyKeys = propertyType.GetMembers(bindingFlags)
        .Where(member => member.MemberType == MemberTypes.NestedType)
        .Cast<Type>()
        .SelectMany(type => type.GetProperties(bindingFlags))
        .Union(propertyType.GetProperties(bindingFlags))
        .Where(property => property.PropertyType == typeof(PropertyKey))
        .Select(property => property.GetValue(null))
        .Cast<PropertyKey>();

      var properties = propertyKeys.Select(property => shellFile.Properties.GetProperty(property))
        .Where(property => !property.Description.DisplayName.IsNullOrEmpty())
        .Distinct(new DisplayNameComparer())
        .ToDictionary(property => property.Description.DisplayName, property => property.PropertyKey, StringComparer.InvariantCultureIgnoreCase);

      return properties;
    }

    /// <summary>
    ///   Gets the property by its name.
    /// </summary>
    /// <param name="shellFile">The shell file to use.</param>
    /// <param name="displayName">The name from the property.</param>
    /// <returns>The found shell property or null.</returns>
    public static IShellProperty GetPropertyByDisplayName(this ShellFile shellFile, string displayName)
    {
      var properties = shellFile.GetPropertyKeysWithName();
      if (properties.ContainsKey(displayName))
      {
        return shellFile.Properties.GetProperty(properties[displayName]);
      }

      return null;
    }

    /// <summary>
    ///   Sets the property on the shell file with the given display name and the value.
    /// </summary>
    /// <param name="shellFile">The shell file to use.</param>
    /// <param name="displayName">The name of the property.</param>
    /// <param name="value">The value to set.</param>
    public static void SetProperty(this ShellFile shellFile, string displayName, object value)
    {
      using (var shellWriter = shellFile.Properties.GetPropertyWriter())
      {
        var property = shellFile.GetPropertyByDisplayName(displayName);
        shellWriter.WriteProperty(property, value);
      }
    }

    /// <summary>
    ///   Gets a value indicating whether the properties are read only or not.
    /// </summary>
    /// <param name="shellFile">The shell file to use.</param>
    /// <returns>A value indicating whether the properties are read only or not.</returns>
    public static bool IsReadOnly(this ShellFile shellFile)
    {
      try
      {
        using (var shellWriter = shellFile.Properties.GetPropertyWriter())
        {
          return false;
        }
      }
      catch
      {
        return true;
      }
    }
  }
}