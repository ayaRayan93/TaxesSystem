using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace TaxesSystem
{
    public class DataHelperClassProducts
    {
        private DataSet _DataSet;
        private string _DataMember = "FirstTable";

        public DataHelperClassProducts(DSparametrProducts param)
        {
            switch (param)
            {
                case DSparametrProducts.simpleDS:
                    {
                        MakeFirstTable();
                        break;
                    }
                case DSparametrProducts.doubleDS:
                    {
                        MakeSecondTable();
                        break;
                    }
                case DSparametrProducts.relatedDS:
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
            column.ColumnName = "بيان";
            column.AutoIncrement = false;
            column.Caption = "بيان";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "رقم الفاتورة";
            column.AutoIncrement = false;
            column.Caption = "رقم الفاتورة";
            column.ReadOnly = false;
            column.Unique = false;
            
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(DateTime);
            column.ColumnName = "التاريخ";
            column.AutoIncrement = false;
            column.Caption = "التاريخ";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);
            
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "العميل";
            column.AutoIncrement = false;
            column.Caption = "العميل";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "اضافة";
            column.AutoIncrement = false;
            column.Caption = "اضافة";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "خصم";
            column.AutoIncrement = false;
            column.Caption = "خصم";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "السعر";
            column.AutoIncrement = false;
            column.Caption = "السعر";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "الاجمالى";
            column.AutoIncrement = false;
            column.Caption = "الاجمالى";
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
            column.ColumnName = "بيان";
            column.AutoIncrement = false;
            column.Caption = "بيان";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "رقم الفاتورة";
            column.AutoIncrement = false;
            column.Caption = "رقم الفاتورة";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(DateTime);
            column.ColumnName = "التاريخ";
            column.AutoIncrement = false;
            column.Caption = "التاريخ";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "العميل";
            column.AutoIncrement = false;
            column.Caption = "العميل";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "اضافة";
            column.AutoIncrement = false;
            column.Caption = "اضافة";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "خصم";
            column.AutoIncrement = false;
            column.Caption = "خصم";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "السعر";
            column.AutoIncrement = false;
            column.Caption = "السعر";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "الاجمالى";
            column.AutoIncrement = false;
            column.Caption = "الاجمالى";
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

    public enum DSparametrProducts
    {
        simpleDS = 0, doubleDS = 1, relatedDS = 2
    }
}
