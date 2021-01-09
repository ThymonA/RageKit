namespace RageKit.Extensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    using SharpDX;

    public static class XmlExtensions
    {
        public static void AddChildEmptyItem(this XmlDocument document, XmlNode node, string item = "Item")
        {
            var child = document.CreateElement(item);

            child.SetAttribute("type", "NULL");

            node.AppendChild(child);
        }

        public static void AddChildInnerText(this XmlDocument document, XmlNode node, string name, string innerText)
        {
            var child = document.CreateElement(name);

            child.InnerText = innerText;

            node.AppendChild(child);
        }

        public static void AddChildInnerArray(this XmlDocument document, XmlNode node, string name, string[] array, string delimiter = "\n")
        {
            var child = document.CreateElement(name);

            child.InnerText = string.Join(delimiter, array ?? new string[] { });

            node.AppendChild(child);
        }

        public static void AddChildInnerItemArray(this XmlDocument document, XmlNode node, string name, string[] array, string item = "Item")
        {
            var root = document.CreateElement(name);

            foreach (var str in array ?? new string[] { })
            {
                var child = document.CreateElement(item);

                if (!string.IsNullOrWhiteSpace(str))
                {
                    child.InnerText = str;
                }

                root.AppendChild(child);
            }

            node.AppendChild(root);
        }

        public static void AddChildContentArray(this XmlDocument document, XmlNode node, string name, int[] array, string delimiter = "\n", string content = "int_array")
        {
            var strList = (array ?? new int[] { }).Select(e => e.ToString());
            var root = document.CreateElement(name);

            root.SetAttribute("content", content);
            root.InnerText = string.Join(delimiter, strList);

            node.AppendChild(root);
        }

        public static void AddChildContentArray(this XmlDocument document, XmlNode node, string name, float[] array, string delimiter = "\n", string content = "float_array")
        {
            var strList = (array ?? new float[] { }).Select(e => e.ToString("0.000000"));
            var root = document.CreateElement(name);

            root.SetAttribute("content", content);
            root.InnerText = string.Join(delimiter, strList);

            node.AppendChild(root);
        }

        public static void AddChildWithAttribute(this XmlDocument document, XmlNode node, string name, string value, string attribute = "value")
        {
            var child = document.CreateElement(name);

            child.SetAttribute(attribute, value);

            node.AppendChild(child);
        }

        public static void AddChildWithAttribute(this XmlDocument document, XmlNode node, string name, float value, string attribute = "value")
        {
            var child = document.CreateElement(name);

            child.SetAttribute(attribute, value.ToString("0.000000"));

            node.AppendChild(child);
        }

        public static void AddChildWithAttribute(this XmlDocument document, XmlNode node, string name, int value, string attribute = "value")
        {
            var child = document.CreateElement(name);

            child.SetAttribute(attribute, value.ToString());

            node.AppendChild(child);
        }

        public static void AddChildWithAttribute(this XmlDocument document, XmlNode node, string name, uint value, string attribute = "value")
        {
            var child = document.CreateElement(name);

            child.SetAttribute(attribute, value.ToString());

            node.AppendChild(child);
        }

        public static void AddChildWithAttribute(this XmlDocument document, XmlNode node, string name, ulong value, string attribute = "value")
        {
            var child = document.CreateElement(name);

            child.SetAttribute(attribute, value.ToString("0.000000"));

            node.AppendChild(child);
        }

        public static void AddChildWithAttribute(this XmlDocument document, XmlNode node, string name, long value, string attribute = "value")
        {
            var child = document.CreateElement(name);

            child.SetAttribute(attribute, value.ToString("0.000000"));

            node.AppendChild(child);
        }

        public static void AddChildWithAttribute(this XmlDocument document, XmlNode node, string name, short value, string attribute = "value")
        {
            var child = document.CreateElement(name);

            child.SetAttribute(attribute, value.ToString());

            node.AppendChild(child);
        }

        public static void AddChildWithAttribute(this XmlDocument document, XmlNode node, string name, Vector2 value, string x = "x", string y = "y")
        {
            var child = document.CreateElement(name);

            child.SetAttribute(x, value.X.ToString("0.000000"));
            child.SetAttribute(y, value.Y.ToString("0.000000"));

            node.AppendChild(child);
        }

        public static void AddChildWithAttribute(this XmlDocument document, XmlNode node, string name, Vector3 value, string x = "x", string y = "y", string z = "z")
        {
            var child = document.CreateElement(name);

            child.SetAttribute(x, value.X.ToString("0.000000"));
            child.SetAttribute(y, value.Y.ToString("0.000000"));
            child.SetAttribute(z, value.Z.ToString("0.000000"));

            node.AppendChild(child);
        }

        public static void AddChildWithAttribute(this XmlDocument document, XmlNode node, string name, Vector4 value, string x = "x", string y = "y", string z = "z", string w = "w")
        {
            var child = document.CreateElement(name);

            child.SetAttribute(x, value.X.ToString("0.000000"));
            child.SetAttribute(y, value.Y.ToString("0.000000"));
            child.SetAttribute(z, value.Z.ToString("0.000000"));
            child.SetAttribute(w, value.W.ToString("0.000000"));

            node.AppendChild(child);
        }
    }
}
