using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;

namespace MainSystem
{
    public partial class Permissions_Edit : Form
    {
        public Permissions_Edit()
        {
            InitializeComponent();
        }

        private void btnAddNum_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPermisionNum.Text != "")
                {
                    for (int i = 0; i < checkedListBoxControlNum.ItemCount; i++)
                    {
                        if (txtPermisionNum.Text == checkedListBoxControlNum.Items[i].Value.ToString())
                        {
                            MessageBox.Show("هذا الاذن تم اضافتة");
                            return;
                        }
                    }

                    checkedListBoxControlNum.Items.Add(txtPermisionNum.Text);
                    txtPermisionNum.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteNum_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkedListBoxControlNum.CheckedItemsCount > 0)
                {
                    if (MessageBox.Show("هل انت متاكد انك تريد الحذف؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;

                    ArrayList temp = new ArrayList();
                    foreach (int index in checkedListBoxControlNum.CheckedIndices)
                        temp.Add(checkedListBoxControlNum.Items[index]);
                    foreach (object item in temp)
                        checkedListBoxControlNum.Items.Remove(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}