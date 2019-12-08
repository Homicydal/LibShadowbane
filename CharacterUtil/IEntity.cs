using System;

namespace LibShadowbane.CharacterUtil
{
    ///<Summary>
    /// Interface for EntityReader to be able to read the names of json
    /// entities
    ///</Summary>
    public interface IEntity
    {
        string Name { get; set; }
    }
}