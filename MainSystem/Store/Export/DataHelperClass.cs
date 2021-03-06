﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace TaxesSystem
{
    public class DataHelper
    {
        private DataSet _DataSet;
        private string _DataMember = "FirstTable";

        public DataHelper(DSparametr param)
        {
            switch (param)
            {
                case DSparametr.simpleDS:
                    {
                        MakeFirstTable();
                        break;
                    }
                case DSparametr.simpleBoolDS:
                    {
                        MakeBoolFirstTable();
                        break;
                    }
                case DSparametr.simpleTimeDS:
                    {
                        MakeTimeFirstTable();
                        break;
                    }
                case DSparametr.doubleDS:
                    {
                        MakeSecondTable();
                        break;
                    }
                case DSparametr.relatedDS:
                    {
                        MakeFirstTable();
                        MakeSecondTable();
                        MakeThirdTable();
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
            //DataRow row;

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
            column.ColumnName = "ItemName";
            column.AutoIncrement = false;
            column.Caption = "البند";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "TotalQuantity";
            column.AutoIncrement = false;
            column.Caption = "الكمية";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "DeliveryQuantity";
            column.AutoIncrement = false;
            column.Caption = "الكمية المستلمة";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column); 

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
            column.ColumnName = "NumOfCarton";
            column.AutoIncrement = false;
            column.Caption = "عدد الكراتين المستلمة";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ItemType";
            column.AutoIncrement = false;
            column.Caption = "الفئة";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);



            DataSet = new DataSet();
            DataSet.Tables.Add(table);

        
        }

        private void MakeTimeFirstTable()
        {
            DataTable table = new DataTable("FirstTable");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "value1";
            column.AutoIncrement = false;
            column.Caption = "value1";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);


            column = new DataColumn();
            column.DataType = typeof(DateTime);
            column.ColumnName = "value2";
            column.AutoIncrement = false;
            column.Caption = "value2";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            DataSet = new DataSet();
            DataSet.Tables.Add(table);

            for (int i = 0; i <= 10; i++)
            {
                row = table.NewRow();
                row["value1"] = "Item" + i.ToString();
                row["value2"] = DateTime.Now;
                table.Rows.Add(row);
            }
        }

        private void MakeBoolFirstTable()
        {
            DataTable table = new DataTable("FirstTable");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "value1";
            column.AutoIncrement = false;
            column.Caption = "value1";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);


            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "value2";
            column.AutoIncrement = false;
            column.Caption = "value2";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            DataSet = new DataSet();
            DataSet.Tables.Add(table);

            for (int i = 0; i <= 10; i++)
            {
                row = table.NewRow();
                row["value1"] = "Item " + i.ToString();
                row["value2"] = false;
                table.Rows.Add(row);
            }
        }

        private void MakeSecondTable()
        {
            DataTable table = new DataTable("FirstTable");
            DataColumn column;
            //DataRow row;

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
            column.ColumnName = "ItemName";
            column.AutoIncrement = false;
            column.Caption = "البند";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "TotalQuantity";
            column.AutoIncrement = false;
            column.Caption = "الكمية";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "DeliveryQuantity";
            column.AutoIncrement = false;
            column.Caption = "الكمية المستلمة";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

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
            column.ColumnName = "NumOfCarton";
            column.AutoIncrement = false;
            column.Caption = "عدد الكراتين المستلمة";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(string);
            column.ColumnName = "ReturnItemReason";
            column.AutoIncrement = false;
            column.Caption = "سبب استرجاع البند";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);



            DataSet = new DataSet();
            DataSet.Tables.Add(table);
        }

        private void MakeThirdTable()
        {
            DataTable table = new DataTable("ThirdTable");
            DataColumn column;
            DataRow row;

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "value7";
            column.AutoIncrement = false;
            column.Caption = "value7";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);


            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "value8";
            column.AutoIncrement = false;
            column.Caption = "value7";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = typeof(int);
            column.ColumnName = "value9";
            column.AutoIncrement = false;
            column.Caption = "Value9";
            column.ReadOnly = false;
            column.Unique = false;

            table.Columns.Add(column);

            DataSet.Tables.Add(table);

            for (int i = 0; i <= 10; i++)
            {
                row = table.NewRow();
                row["value7"] = i;
                row["value8"] = 10 * i;
                row["value9"] = 100 * i;
                table.Rows.Add(row);
            }
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

            parentColumn =
                DataSet.Tables["SecondTable"].Columns["value4"];
            childColumn =
                DataSet.Tables["ThirdTable"].Columns["value7"];
            relation = new
                DataRelation("value4_value7", parentColumn, childColumn);
            DataSet.Tables["ThirdTable"].ParentRelations.Add(relation);
        
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

    public enum DSparametr
    {
        simpleDS = 0, doubleDS = 1, relatedDS = 2, simpleBoolDS = 3, simpleTimeDS = 4
    }
}
