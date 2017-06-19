namespace MetaDataFileInfo.Comparer
{
  using System.Collections.Generic;
  using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

  /// <summary>
  ///   The Comparer used for two ShellProperties.
  /// </summary>
  internal class DisplayNameComparer : IEqualityComparer<IShellProperty>
  {
    /// <summary>
    ///   Compares the two given shell properties based on its HashCode.
    /// </summary>
    /// <param name="propertyX">The first shell property.</param>
    /// <param name="propertyY">The second shell property.</param>
    /// <returns>True or false, whether the properties are equal or not.</returns>
    public bool Equals(IShellProperty propertyX, IShellProperty propertyY)
    {
      return this.GetHashCode(propertyX) == this.GetHashCode(propertyY);
    }

    /// <summary>
    ///   Gets a HashCode from the ShellProperty.
    /// </summary>
    /// <param name="property">The property to use.</param>
    /// <returns>The HashCode from the property.</returns>
    public int GetHashCode(IShellProperty property)
    {
      return property.Description.DisplayName.GetHashCode();
    }
  }
}