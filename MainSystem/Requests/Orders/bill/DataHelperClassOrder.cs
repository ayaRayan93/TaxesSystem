using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace TaxesSystem
{
    public class DataHelperOrder
    {
        private DataSet _DataSet;
        private string _DataMember = "FirstTable";

        public DataHelperOrder(DSparametrOrder param)
        {
            switch (param)
            {
                case DSparametrOrder.simpleDS:
                    {
                        MakeFirstTable();
                        break;
                    }
                case DSparametrOrder.doubleDS:
                    {
                        MakeSecondTable();
                        break;
                    }
                case DSparametrOrder.relatedDS:
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
            column.DataType = typeof(int);
            column.ColumnName = "Data_ID";
            column.AutoIncrement = false;
            column.Caption = "Data_ID";
            column.ReadOnly = false;
            column.Unique = false;
            
            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Code";
            column.AutoIncrement = false;
            column.Caption = "الكود";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);
            
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ItemType";
            column.AutoIncrement = false;
            column.Caption = "النوع";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

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

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "Balatat";
            column.AutoIncrement = false;
            column.Caption = "عدد البلتات";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "Cartons_Balate";
            column.AutoIncrement = false;
            column.Caption = "عدد الكراتين";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            //column = new DataColumn();
            //column.DataType = typeof(double);
            //column.ColumnName = "Price";
            //column.AutoIncrement = false;
            //column.Caption = "السعر";
            //column.ReadOnly = false;
            //column.Unique = false;

            //table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Carton";
            column.AutoIncrement = false;
            column.Caption = "الكرتنة";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Type";
            column.AutoIncrement = false;
            column.Caption = "Type";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "SpecialOrderID";
            column.AutoIncrement = false;
            column.Caption = "SpecialOrderID";
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
            column.DataType = typeof(int);
            column.ColumnName = "Data_ID";
            column.AutoIncrement = false;
            column.Caption = "Data_ID";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Code";
            column.AutoIncrement = false;
            column.Caption = "الكود";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);
            
            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ItemType";
            column.AutoIncrement = false;
            column.Caption = "النوع";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);
            
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

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "Balatat";
            column.AutoIncrement = false;
            column.Caption = "عدد البلتات";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(double);
            column.ColumnName = "Cartons_Balate";
            column.AutoIncrement = false;
            column.Caption = "عدد الكراتين";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            //column = new DataColumn();
            //column.DataType = typeof(double);
            //column.ColumnName = "Price";
            //column.AutoIncrement = false;
            //column.Caption = "السعر";
            //column.ReadOnly = false;
            //column.Unique = false;

            //table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Carton";
            column.AutoIncrement = false;
            column.Caption = "الكرتنة";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "Type";
            column.AutoIncrement = false;
            column.Caption = "Type";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "SpecialOrderID";
            column.AutoIncrement = false;
            column.Caption = "SpecialOrderID";
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

    public enum DSparametrOrder
    {
        simpleDS = 0, doubleDS = 1, relatedDS = 2
    }
}
