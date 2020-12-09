using System;
using System.Linq;

namespace OMSTSystem.ViewModels
{
    /// <summary>
    /// This View Model class is ideal for representing data that will ultimately be displayed in a DropDownList, RadioButtonList or CheckBoxList.
    /// </summary>
    /// <typeparam name="T">Native data type of the Key</typeparam>
    public sealed class KeyValueOption<T>
    {
        ///<summary>Key value</summary>
        public T Key { private get; set; }
        ///<summary>String representation of the key value</summary>
        public string DisplayValue => Key.ToString();
        ///<summary>Text representation of the value associated with the Key</summary>
        public string DisplayText { get; set; }
    }
}
