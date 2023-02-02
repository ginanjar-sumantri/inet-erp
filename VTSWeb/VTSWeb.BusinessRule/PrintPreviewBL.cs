using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using VTSWeb.Common;
using VTSWeb.SystemConfig;
using VTSWeb.Database;

namespace VTSWeb.UI
{
    public sealed class PrintPreviewBL : VTSWeb.SystemConfig.Base
    {
        public PrintPreviewBL()
        {
        }

        #region PrintPreview

        public ReportDataSource PrintPreviewGoodsIn(String _prmTransNumb)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();

            try
            {
                SqlConnection _conn = new SqlConnection(ApplicationConfig.ConnString);

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                _cmd.CommandText = "Sp_PrintPreviewIn";
                _cmd.Parameters.AddWithValue("@TransNumb", _prmTransNumb);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        #endregion

        ~PrintPreviewBL()
        {

        }

    }
}