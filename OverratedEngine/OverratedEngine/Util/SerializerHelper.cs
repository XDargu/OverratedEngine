using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.IsolatedStorage;
using System.IO;

namespace OverratedEngine.Util
{
    public class SerializerHelper
    {
        /// <summary>
        /// Saves an string array. One element, one line
        /// </summary>
        public void SaveStringArray(string[] stringArray, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < stringArray.Length; i++)
            {
                sw.WriteLine(stringArray[i]);
            }

            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// Saves an string array, appending the content to the existing data
        /// </summary>
        public void AppendStringArray(string[] stringArray, string path)
        {
            FileStream fs = new FileStream(path, FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < stringArray.Length; i++)
            {
                sw.WriteLine(stringArray[i]);
            }

            sw.Close();
            fs.Close();
        }

        /*
        /// <summary>
        /// Loads an string array. One line, one element
        /// </summary>
        public string[] LoadStringArray(string path)
        {
            List<string> rtr = new List<string>();

            IsolatedStorageFile myStore = IsolatedStorageFile.GetUserStoreForApplication();

            try
            {
                using (var isoFileStream = new IsolatedStorageFileStream(path + ".txt", FileMode.Open, myStore))
                {
                    using (var isoFileReader = new StreamReader(isoFileStream))
                    {
                        while (!isoFileReader.EndOfStream)
                        {
                            rtr.Add(isoFileReader.ReadLine());
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {

            }

            return rtr.ToArray();
        }*/
    }
}
