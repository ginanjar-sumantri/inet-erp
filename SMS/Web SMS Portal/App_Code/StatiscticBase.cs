using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMSLibrary;

namespace SMS.SMSWeb
{
    public abstract class StatisticBase : SMSWebBase
    {
        protected string _homePage = "Statistic.aspx";
        protected string _viewDetailTransactionPage = "StatisticDeatilTransaction.aspx";

        protected NameValueCollectionExtractor _nvcExtractor;
        protected string _codeKey = "code";

        public StatisticBase() { }
        ~StatisticBase() { }
    }
}