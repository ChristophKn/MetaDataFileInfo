namespace MetaDataFileInfo.Exceptions
{
  using System;

  /// <summary>
  ///   The SetByImplicitOperatorException class.
  /// </summary>
  internal class SetByImplicitOperatorException : Exception
  {
    /// <summary>
    ///   Initializes a new instance of the SetByImplicitOperatorException class.
    /// </summary>
    /// <param name="key">The key to use.</param>
    public SetByImplicitOperatorException(string key)
      : base($"Cannot set the value of an instance which value was set by an implicit operator. {key}")
    {
    }
  }
}