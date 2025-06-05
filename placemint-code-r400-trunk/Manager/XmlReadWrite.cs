using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace PlaceMint.Manager
{
    using PMException;
    using Properties;

    /// <summary>
    /// Import/Export to/from an xml file
    /// </summary>
    /// <typeparam name="T">Object to be imported/exported</typeparam>
    public static class XmlReadWrite<T> where T : class, IFileNotFound, new()
    {
        /// <summary>
        /// Loads an object from an XML file in Document format.
        /// </summary>
        /// <param name="path">Path of the file to load the object from.</param>
        /// <exception cref="PMFileNotFoundException">Thrown if file cannot be found.</exception>
        /// <returns>Object loaded from an XML file in Document format.</returns>
        public static T Load(string path)
        {
            return Load(path, new Type[] {});
        }

        /// <summary>
        /// Loads an object from an XML file in Document format.
        /// </summary>
        /// <param name="path">Path of the file to load the object from.</param>
        /// <param name="extraTypes">Additional types for the serializer to expect.</param>
        /// <exception cref="PMFileNotFoundException">Thrown if file cannot be found.</exception>
        /// <returns>Object loaded from an XML file in Document format.</returns>
        public static T Load(string path, Type[] extraTypes)
        {
            if (path.Equals(""))
            {
                throw new EmptyPathException();
            }
            T serializableObject = null;
            try
            {
                using (XmlReader xmlReader = new XmlTextReader(new StreamReader(path)))
                {
                    Type ObjectType = typeof(T);
                    Logger.Debug("Load a {0}", ObjectType.ToString());
                    XmlSerializer xmlSerializer = new XmlSerializer(ObjectType, extraTypes);
                    try
                    {
                        if (!xmlSerializer.CanDeserialize(xmlReader))
                        {
                            //xml formated, but doesn't match object
                            throw new WrongXmlFormatException(Resources.wrongXmlFormat);
                        }
                    }
                    catch (XmlException e)
                    {
                        //not xml formated
                        throw new WrongXmlFormatException(Resources.wrongXmlFormat, e);
                    }

                    serializableObject = xmlSerializer.Deserialize(xmlReader) as T;
                    if (Logger.Level == LoggingLevel.Trace)
                    {
                        //toString of an object can be lots of recursions, so check level before hand
                        Logger.Trace("resultant object {0}", serializableObject.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                //handle file reading exceptions
                if (e is FileNotFoundException || e is DirectoryNotFoundException || e is DriveNotFoundException)
                {
                    if (serializableObject == null)
                    {
                        serializableObject = new T();
                    }
                    throw new PMFileNotFoundException(serializableObject.FileNotFoundMsg(), e);
                }
                else if (e is PathTooLongException)
                {
                    throw new PMPathTooLongException(Resources.pathTooLong, e);
                }
                else if (e is InvalidOperationException)
                {
                    string line = "";
                    Match lineMatch = Regex.Match(e.Message, @"\((\d+),\s\d+\)");
                    if (lineMatch.Success)
                    {
                        line = lineMatch.Groups[1].ToString();
                    }
                    throw new InvalidXmlValueException(string.Format(Resources.InvalidXmlValueFormat, line), e);
                }
                else
                {
                    throw;
                }
            }

            return serializableObject;
        }

        /// <summary>
        /// Saves an object to an XML file in Document format.
        /// </summary>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="path">Path of the file to save the object to.</param>
        public static void Save(T serializableObject, string path)
        {
            Save(serializableObject, path, new Type[]{});
        }

        /// <summary>
        /// Saves an object to an XML file in Document format.
        /// </summary>
        /// <param name="serializableObject">Serializable object to be saved to file.</param>
        /// <param name="path">Path of the file to save the object to.</param>
        /// <param name="extraTypes">Additional types for the serializer to expect.</param>
        public static void Save(T serializableObject, string path, Type[] extraTypes)
        {
            using (TextWriter textWriter = new StreamWriter(path))
            {
                if (Logger.Level == LoggingLevel.Trace)
                {
                    //toString of an object can be lots of recursions, so check level before hand
                    Logger.Trace("object to save {0}", serializableObject.ToString());
                }
                Type ObjectType = typeof(T);
                XmlSerializer xmlSerializer = new XmlSerializer(ObjectType, extraTypes);
                xmlSerializer.Serialize(textWriter, serializableObject);
            }
        }
    }
}