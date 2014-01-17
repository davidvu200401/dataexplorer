﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataExplorer.Domain.Columns;
using DataExplorer.Domain.Filters.FloatFilters;
using DataExplorer.Infrastructure.Serializers.Properties;

namespace DataExplorer.Infrastructure.Serializers.Filters.FloatFilters
{
    public class FloatFilterSerializer : IFloatFilterSerializer
    {
        private const string FilterTag = "float-filter";
        private const string ColumnIdTag = "column-id";
        private const string LowerValueTag = "lower-value";
        private const string UpperValueTag = "upper-value";
        private const string IncludeNullTag = "include-null";

        private readonly IPropertySerializer _propertySerializer;

        public FloatFilterSerializer(IPropertySerializer propertySerializer)
        {
            _propertySerializer = propertySerializer;
        }

        public XElement Serialize(FloatFilter filter)
        {
            var xFilter = new XElement(FilterTag);

            AddProperty(xFilter, ColumnIdTag, filter.Column.Id);

            AddProperty(xFilter, LowerValueTag, filter.LowerValue);

            AddProperty(xFilter, UpperValueTag, filter.UpperValue);

            AddProperty(xFilter, IncludeNullTag, filter.IncludeNull);

            return xFilter;
        }

        private void AddProperty<T>(XElement xElement, string name, T value)
        {
            var xProperty = _propertySerializer.Serialize(name, value);

            xElement.Add(xProperty);
        }

        public FloatFilter Deserialize(XElement xFilter, IEnumerable<Column> columns)
        {
            var id = DeserializeProperty<int>(xFilter, ColumnIdTag);

            var column = columns.First(p => p.Id == id);

            var lowerValue = DeserializeProperty<Double>(xFilter, LowerValueTag);

            var upperValue = DeserializeProperty<Double>(xFilter, UpperValueTag);

            var includeNull = DeserializeProperty<bool>(xFilter, IncludeNullTag);

            return new FloatFilter(column, lowerValue, upperValue, includeNull);
        }

        private T DeserializeProperty<T>(XElement xColumn, string name)
        {
            var xProperty = xColumn.Elements()
                .First(p => p.Name == name);

            var value = _propertySerializer.Deserialize<T>(xProperty);

            return value;
        }
    }
}