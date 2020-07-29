using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace TaxesSystem
{
    public class DataHelperClassBillsTransitions
    {
        private DataSet _DataSet;
        private string _DataMember = "FirstTable";

        public DataHelperClassBillsTransitions(DSparametrBillsTransitions param)
        {
            switch (param)
            {
                case DSparametrBillsTransitions.simpleDS:
                    {
                        MakeFirstTable();
                        break;
                    }
                case DSparametrBillsTransitions.doubleDS:
                    {
                        MakeSecondTable();
                        break;
                    }
                case DSparametrBillsTransitions.relatedDS:
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
            column.Caption = "الفاتورة";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColBillType";
            column.AutoIncrement = false;
            column.Caption = "نوع الفاتورة";
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

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColCustomer_ID";
            column.AutoIncrement = false;
            column.Caption = "Customer_ID";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColCustomer";
            column.AutoIncrement = false;
            column.Caption = "المهندس/المقاول/التاجر";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColClient_ID";
            column.AutoIncrement = false;
            column.Caption = "Client_ID";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColClient";
            column.AutoIncrement = false;
            column.Caption = "العميل";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "ColTotal";
            column.AutoIncrement = false;
            column.Caption = "الاجمالى";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "ColDiscount";
            column.AutoIncrement = false;
            column.Caption = "الخصم";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "ColSafy";
            column.AutoIncrement = false;
            column.Caption = "الصافى";
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
            column.Caption = "الفاتورة";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColBillType";
            column.AutoIncrement = false;
            column.Caption = "نوع الفاتورة";
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

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColCustomer_ID";
            column.AutoIncrement = false;
            column.Caption = "Customer_ID";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColCustomer";
            column.AutoIncrement = false;
            column.Caption = "المهندس/المقاول/التاجر";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColClient_ID";
            column.AutoIncrement = false;
            column.Caption = "Client_ID";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ColClient";
            column.AutoIncrement = false;
            column.Caption = "العميل";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "ColTotal";
            column.AutoIncrement = false;
            column.Caption = "الاجمالى";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "ColDiscount";
            column.AutoIncrement = false;
            column.Caption = "الخصم";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "ColSafy";
            column.AutoIncrement = false;
            column.Caption = "الصافى";
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

    public enum DSparametrBillsTransitions
    {
        simpleDS = 0, doubleDS = 1, relatedDS = 2
    }
}
