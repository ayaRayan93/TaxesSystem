using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace TaxesSystem
{
    public class DataHelperClassPermissionsTransitions
    {
        private DataSet _DataSet;
        private string _DataMember = "FirstTable";

        public DataHelperClassPermissionsTransitions(DSparametrPermissionsTransitions param)
        {
            switch (param)
            {
                case DSparametrPermissionsTransitions.simpleDS:
                    {
                        MakeFirstTable();
                        break;
                    }
                case DSparametrPermissionsTransitions.doubleDS:
                    {
                        MakeSecondTable();
                        break;
                    }
                case DSparametrPermissionsTransitions.relatedDS:
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
            column.ColumnName = "ColID";
            column.AutoIncrement = false;
            column.Caption = "ID";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColType";
            column.AutoIncrement = false;
            column.Caption = "النوع";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColBill";
            column.AutoIncrement = false;
            column.Caption = "رقم الاذن";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColStoreName";
            column.AutoIncrement = false;
            column.Caption = "المخزن";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColSupplierName";
            column.AutoIncrement = false;
            column.Caption = "المورد";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(DateTime);
            column.ColumnName = "ColDate";
            column.AutoIncrement = false;
            column.Caption = "التاريخ";
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
            column.ColumnName = "ColID";
            column.AutoIncrement = false;
            column.Caption = "ID";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColType";
            column.AutoIncrement = false;
            column.Caption = "النوع";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColBill";
            column.AutoIncrement = false;
            column.Caption = "رقم الاذن";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColStoreName";
            column.AutoIncrement = false;
            column.Caption = "المخزن";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColSupplierName";
            column.AutoIncrement = false;
            column.Caption = "المورد";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(DateTime);
            column.ColumnName = "ColDate";
            column.AutoIncrement = false;
            column.Caption = "التاريخ";
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

    public enum DSparametrPermissionsTransitions
    {
        simpleDS = 0, doubleDS = 1, relatedDS = 2
    }
}
