namespace MetaDataFileInfo.Classes
{
  using System;
  using System.IO;
  using Exceptions;
  using Extensions;
  using Interfaces;

  /// <summary>
  ///   The Property class.
  /// </summary>
  public class Property : IProperty
  {
    /// <summary>
    ///   The used display name.
    /// </summary>
    private readonly string displayName;

    /// <summary>
    ///   The used file info.
    /// </summary>
    private readonly FileInfo fileInfo;

    /// <summary>
    ///   Flags the given instance, if the instance is created by an implicit operator.
    /// </summary>
    private bool createdByImplicitOperator;

    /// <summary>
    ///   The value set by the implicit operator.
    /// </summary>
    private object implicitOperatorValue;

    /// <summary>
    ///   Initializes a new instance of the Property class.
    /// </summary>
    /// <param name="fileInfo">The file info to use.</param>
    /// <param name="displayName">The display name for the property.</param>
    public Property(FileInfo fileInfo, string displayName)
    {
      this.fileInfo = fileInfo;
      this.displayName = displayName;
    }

    /// <summary>
    ///   Initializes a new instance of the Property class.
    /// </summary>
    /// <param name="value">The value to set.</param>
    private Property(object value)
    {
      this.implicitOperatorValue = value;
      this.createdByImplicitOperator = true;
    }

    /// <summary>
    ///   Gets the
    /// </summary>
    private XmlHelper Xml => new XmlHelper(this.Value.ToString());

    /// <summary>
    ///   Gets or sets the value of the Property.
    /// </summary>
    public object Value
    {
      get
      {
        if (this.createdByImplicitOperator)
        {
          return this.implicitOperatorValue;
        }

        using (var shellFile = this.fileInfo.GetShellFile())
        {
          return shellFile.GetPropertyByDisplayName(this.displayName)?.ValueAsObject;
        }
      }

      set
      {
        if (this.createdByImplicitOperator)
        {
          throw new SetByImplicitOperatorException(this.displayName);
        }

        using (var shellFile = this.fileInfo.GetShellFile())
        {
          shellFile.SetProperty(this.displayName, value);
        }
      }
    }

    /// <summary>
    ///   Gets the type of the Value.
    /// </summary>
    public Type Type
    {
      get
      {
        if (this.createdByImplicitOperator)
        {
          return this.implicitOperatorValue.GetType();
        }

        using (var shellFile = this.fileInfo.GetShellFile())
        {
          return shellFile.GetPropertyByDisplayName(this.displayName).ValueType;
        }
      }
    }

    /// <summary>
    ///   Gets or sets the element value of the property if its an xml content.
    /// </summary>
    /// <param name="xmlElementName">The xml element name to use.</param>
    /// <returns>The xml element value.</returns>
    public string this[string xmlElementName]
    {
      get => this.Xml[xmlElementName];

      set
      {
        var xml = this.Xml;
        xml[xmlElementName] = value;
        this.Value = xml.ToString();
      }
    }

    /// <summary>
    ///   Converts string into property.
    /// </summary>
    /// <param name="val">String to convert.</param>
    public static implicit operator Property(string val)
    {
      return new Property(val);
    }

    /// <summary>
    ///   Converts property into string.
    /// </summary>
    /// <param name="property">Property to convert.</param>
    public static implicit operator string(Property property)
    {
      return property.Value.To<string>();
    }

    /// <summary>
    ///   Converts integer into Property.
    /// </summary>
    /// <param name="val">value as integer.</param>
    public static implicit operator Property(int val)
    {
      return new Property(val);
    }

    /// <summary>
    ///   Converts Property into integer.
    /// </summary>
    /// <param name="property">property to convert.</param>
    public static implicit operator int(Property property)
    {
      return property.Value.To<int>();
    }

    /// <summary>
    ///   Converts boolean into Property.
    /// </summary>
    /// <param name="val">value as boolean.</param>
    public static implicit operator Property(bool val)
    {
      return new Property(val);
    }

    /// <summary>
    ///   Converts Property into boolean.
    /// </summary>
    /// <param name="property">property to convert.</param>
    public static implicit operator bool(Property property)
    {
      return property.Value.To<bool>();
    }

    /// <summary>
    ///   Gets the typed value.
    /// </summary>
    /// <typeparam name="T">type of value.</typeparam>
    /// <returns>typed value.</returns>
    public T To<T>()
    {
      return this.Value.To<T>();
    }

    /// <summary>
    ///   Gets if the value is empty.
    /// </summary>
    /// <returns>true if null or empty.</returns>
    public bool IsNullOrEmpty()
    {
      return this.Value.To<string>().IsNullOrEmpty();
    }
  }
}