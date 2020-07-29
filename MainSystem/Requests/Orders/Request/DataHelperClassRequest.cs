using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace TaxesSystem
{
    public class DataHelperRequest
    {
        private DataSet _DataSet;
        private string _DataMember = "FirstTable";

        public DataHelperRequest(DSparametrRequest param)
        {
            switch (param)
            {
                case DSparametrRequest.simpleDS:
                    {
                        MakeFirstTable();
                        break;
                    }
                case DSparametrRequest.doubleDS:
                    {
                        MakeSecondTable();
                        break;
                    }
                case DSparametrRequest.relatedDS:
                    {
                        MakeFirstTable();
                        MakeSecondTable();
                        MakeDataRelation();
                        break;
                    }
            }
            DataSet.AcceptChanges();
        }

        private void MakeFirstTable()
        {
            DataTable table = new DataTable("FirstTable");
            DataColumn column;
            
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ItemName";
            column.AutoIncrement = false;
            column.Caption = "الاسم";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "TotalQuantity";
            column.AutoIncrement = false;
            column.Caption = "عدد المتر/القطعة";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);
            
            DataSet = new DataSet();
            DataSet.Tables.Add(table);
        }

        private void MakeSecondTable()
        {
            DataTable table = new DataTable("FirstTable");
            DataColumn column;
            
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ItemName";
            column.AutoIncrement = false;
            column.Caption = "الاسم";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "TotalQuantity";
            column.AutoIncrement = false;
            column.Caption = "عدد المتر/القطعة";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);
            
            DataSet = new DataSet();
            DataSet.Tables.Add(table);
        }

        private void MakeDataRelation()
        {
            DataColumn parentColumn =
                DataSet.Tables["FirstTable"].Columns["value1"];
            DataColumn childColumn =
                DataSet.Tables["SecondTable"].Columns["value4"];
            DataRelation relation = new
                DataRelation("value1_value4", parentColumn, childColumn);
            DataSet.Tables["SecondTable"].ParentRelations.Add(relation);

            parentColumn = DataSet.Tables["SecondTable"].Columns["value4"];
        }
        
        public DataSet DataSet
        {
            get { return _DataSet; }
            set { _DataSet = value; }
        }

        public string DataMember
        {
            get { return _DataMember; }
            set
            {
                _DataMember = value;
            }
        }
        
        public static void CommitTransactionStub()
        {
            throw new InvalidOperationException("Fake exception");
        }
    }

    public enum DSparametrRequest
    {
        simpleDS = 0, doubleDS = 1, relatedDS = 2
    }
}
