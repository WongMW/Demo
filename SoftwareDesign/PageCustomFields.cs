using System;
using System.ComponentModel;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;

namespace SoftwareDesign
{
    public class PageCustomFields
    {
        public static void CreateCustomFieldFluentAPI(string fieldName, Type nodeType, Type fieldType, UserFriendlyDataType dataType)
        { 
            // checking if custom field exists
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(nodeType);
            PropertyDescriptor property = properties[fieldName];

            if (property == null)
            {
                String dTypeString = dataType.ToString();
                App.WorkWith()
                    .DynamicData()
                    .Type(nodeType)
                    .Field()
                    .TryCreateNew(fieldName, fieldType)
                    .Do((metaField) =>
                    {
                        metaField.MetaAttributes.Add(new MetaFieldAttribute()
                        {
                            Name = "UserFriendlyDataType",
                            Value = dTypeString
                        });
                    })
                    .SaveChanges();
            }
        }
    }
}