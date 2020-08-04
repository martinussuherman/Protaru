using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Itm.Misc
{
    public static class TypeExtensions
    {
        public static List<Type> GetOrderedNestedTypes(this Type type)
        {
            return type
                .GetNestedTypes(BindingFlags.Public | BindingFlags.Static)
                .OrderBy(e => e.Name)
                .ToList();
        }

        public static List<FieldInfo> GetOrderedConstants(this Type type)
        {
            // IsLiteral determines if its value is written at 
            //   compile time and not changeable
            // IsInitOnly determine if the field can be set 
            //   in the body of the constructor
            // for C# a field which is readonly keyword would have both true 
            //   but a const field would have only IsLiteral equal to true
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(e => e.IsLiteral && !e.IsInitOnly)
                .OrderBy(e => e.Name)
                .ToList();
        }
    }
}