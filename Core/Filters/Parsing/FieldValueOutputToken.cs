using System;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters.Parsing
{
    /// <summary>
    /// Wraps query result field.
    /// </summary>
    internal class FieldValueOutputToken: ValueOutputToken
    {
        private readonly string fieldName;

        public string FieldName
        {
            get
            {
                return fieldName;
            }
        }

        public FieldValueOutputToken(string fieldName)
        {
            this.fieldName = fieldName;
        }

        public override Filter GetFilter()
        {
            return new FieldValueFilter(fieldName);
        }

        public override string ToString()
        {
            return String.Format(
                "FieldValueOutputToken(ValueOutputToken: {0}, fieldName: {1})",
                base.ToString(),
                fieldName);
        }
    }
}
