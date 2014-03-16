using System;

using SkyNinja.Core.Classes;

namespace SkyNinja.Core.Filters.Parsing
{
    /// <summary>
    /// Wraps query result field.
    /// </summary>
    internal class FieldValueOutputToken: ValueOutputToken
    {
        public string FieldName
        {
            get;
            set;
        }

        public override Filter GetFilter()
        {
            return new FieldValueFilter() {FieldName = FieldName};
        }

        public override string ToString()
        {
            return String.Format(
                "FieldValueOutputToken(ValueOutputToken: {0}, FieldName: {1})",
                base.ToString(),
                FieldName);
        }
    }
}
