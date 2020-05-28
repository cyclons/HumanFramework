using System;

namespace HumanFramework
{
    public abstract class InjectAttribute : Attribute
    {
    }

    [System.AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class InjectInstanceAttribute : InjectAttribute
    {
        public string NameID;

        public InjectInstanceAttribute()
        {
            NameID = null;
        }

        public InjectInstanceAttribute(string nameID)
        {
            NameID = nameID;
        }

    }

    [System.AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class InjectConstructionAttribute : InjectAttribute
    {
        public string NameID;
        public InjectConstructionAttribute(string nameID = null)
        {
            NameID = nameID;
        }
    }

}