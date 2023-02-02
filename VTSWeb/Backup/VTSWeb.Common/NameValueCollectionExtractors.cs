using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace VTSWeb.Common
{
    public sealed class NameValueCollectionExtractor : Base
    {
        private NameValueCollection _nameValCol = null;

        public NameValueCollectionExtractor(NameValueCollection _prmNameValueCollection)
        {
            this._nameValCol = _prmNameValueCollection;
        }

        private string Find(string _prmKey, NameValueCollection _prmNameValueCollection)
        {
            string _result = "";

            String[] arr1 = _prmNameValueCollection.AllKeys;

            for (int loop1 = 0; loop1 < arr1.Length; loop1++)
            {
                String[] arr2 = _prmNameValueCollection.GetValues(arr1[loop1]);

                for (int loop2 = 0; loop2 < arr2.Length; loop2++)
                {
                    if (arr1[loop1].Trim().ToLower() == _prmKey.Trim().ToLower())
                    {
                        _result = arr2[loop2];
                    }
                }
            }

            return _result;
        }

        public string GetValue(string _prmKey)
        {
            string _result = "";

            _result = this.Find(_prmKey, this._nameValCol);

            return _result;
        }

        ~NameValueCollectionExtractor()
        {
        }
    }
}
