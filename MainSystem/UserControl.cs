using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;


namespace TaxesSystem
{
    class UserControl
    {
        public static int userID;
        public static string userName;
        public static int userType;
        public static int EmpID;
        public static string EmpName;
        public static string EmpType;
        public static int EmpBranchID;
        public static string EmpBranchName;

        public static void ItemRecord(string tableName, string status, int recordID, DateTime date, string reason, MySqlConnection conn)
        {
            string query = "insert into usercontrol (UserControl_UserID,UserControl_TableName,UserControl_Status,UserControl_RecordID,UserControl_Date,UserControl_Reason)values (@UserControl_UserID,@UserControl_TableName,@UserControl_Status,@UserControl_RecordID,@UserControl_Date,@UserControl_Reason)";
            MySqlCommand com = new MySqlCommand(query, conn);
            com.Parameters.Add("@UserControl_UserID", MySqlDbType.Int16);
            com.Parameters["@UserControl_UserID"].Value = userID;
            com.Parameters.Add("@UserControl_TableName", MySqlDbType.VarChar, 255);
            com.Parameters["@UserControl_TableName"].Value = tableName;
            com.Parameters.Add("@UserControl_Status", MySqlDbType.VarChar, 255);
            com.Parameters["@UserControl_Status"].Value = status;
            com.Parameters.Add("@UserControl_RecordID", MySqlDbType.Int16);
            com.Parameters["@UserControl_RecordID"].Value = recordID;
            com.Parameters.Add("@UserControl_Date", MySqlDbType.DateTime);
            com.Parameters["@UserControl_Date"].Value = date;
            com.Parameters.Add("@UserControl_Reason", MySqlDbType.VarChar, 255);
            com.Parameters["@UserControl_Reason"].Value = reason;
            com.ExecuteNonQuery();
        }
    }
}

