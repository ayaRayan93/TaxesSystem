using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxesSystem
{
    public partial class CustomerReturnItems_Report : Form
    {
        public GridControl gridControl;
        dataX d;
        int ReturnPermissionNum;
        public CustomerReturnItems_Report(GridControl gridControl, dataX d, int ReturnPermissionNum)
        {
            try
            {
                InitializeComponent();
                this.gridControl = gridControl;
                this.d = d;
                this.ReturnPermissionNum = ReturnPermissionNum;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Product_Report_Load(object sender, EventArgs e)
        {
            try
            {
                PrintableComponentLink printableComponentLink = new PrintableComponentLink();
                
                printableComponentLink.Component = gridControl;
                printableComponentLink.RightToLeftLayout = false;
                printableComponentLink.Landscape = true;
                printableComponentLink.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
                printingSystem1.Links.Add(printableComponentLink);
                printableComponentLink.CreateReportHeaderArea += PrintableComponentLink_CreateReportHeaderArea;
                printableComponentLink.CreateMarginalFooterArea += PrintableComponentLink_CreateMarginalFooterArea;
                printableComponentLink.CreateDocument();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrintableComponentLink_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            // Declare bricks.
            PageInfoBrick pageInfoBrick;

            // Specify required settings for the brick graphics.
            BrickGraphics brickGraphics = e.Graph;
            brickGraphics.BackColor = Color.White;
            brickGraphics.Font = new Font("Neo Sans Arabic", 10);

            // Set the rectangle for a page info brick. 
            RectangleF r = RectangleF.Empty;
            r.Height = 20;

            // Display the PageInfoBrick containing the page number among total pages. The page number
            // is displayed in the right part of the MarginalHeader section.
            pageInfoBrick = brickGraphics.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", Color.Black, r, BorderSide.None);
            pageInfoBrick.Alignment = BrickAlignment.Far;
        }

        private void PrintableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            // Specify required settings for the brick graphics.
            BrickGraphics brickGraphics = e.Graph;
            brickGraphics.BackColor = Color.White;
            brickGraphics.Font = new Font("Neo Sans Arabic", 10);

            // Declare bricks.
            PageInfoBrick pageInfoBrickFrom;
            PageInfoBrick pageInfoBrickTo;
            PageInfoBrick pageInfoBrick;
            PageInfoBrick pageInfo;
            PageInfoBrick pageInfo1;
            PageImageBrick pageImageBrick;

            // Define the image to display.
            Image pageImage = TaxesSystem.Properties.Resources.logo_option2;

            // Display the PageImageBrick containing the DevExpress logo.
            pageImageBrick = brickGraphics.DrawPageImage(pageImage, new Rectangle(10, 0, 150, 150), BorderSide.None, Color.Transparent);
            pageImageBrick.Alignment = BrickAlignment.Far;

            // Display the PageInfoBrick containing date-time information. Date-time information is displayed
            // in the left part of the MarginalHeader section using the FullDateTimePattern.
            //{0:F}
            pageInfoBrickFrom = brickGraphics.DrawPageInfo(PageInfo.None, ":العميل" , Color.Black, new Rectangle(180, 160, 60, 80), BorderSide.None);
            pageInfoBrickFrom.Font = new Font("Tahoma", 10, FontStyle.Bold);
            pageInfoBrickFrom = brickGraphics.DrawPageInfo(PageInfo.None, d.dateFrom, Color.Black, new Rectangle(40, 160, 120, 80), BorderSide.None);

            pageInfoBrickFrom.Alignment = BrickAlignment.Near;
            pageInfoBrickFrom.Font = new Font("Tahoma", 10, FontStyle.Bold);
            pageInfoBrickTo = brickGraphics.DrawPageInfo(PageInfo.None, ":تلفون العميل", Color.Black, new Rectangle(180, 180, 160, 80), BorderSide.None);
            pageInfoBrickTo.Font = new Font("Tahoma", 10, FontStyle.Bold);
            pageInfoBrickTo = brickGraphics.DrawPageInfo(PageInfo.None,  d.dateTo, Color.Black, new Rectangle(40, 180, 120, 80), BorderSide.None);

            pageInfoBrickTo.Alignment = BrickAlignment.Far;
            pageInfoBrickTo.Font = new Font("Tahoma", 10, FontStyle.Bold);
            BrickGraphics brickGraphics1 = e.Graph;
            pageInfo = brickGraphics1.DrawPageInfo(PageInfo.None, d.dateFrom, Color.Black, new Rectangle(825, 160, 120, 70), BorderSide.None);
            pageInfo.Alignment = BrickAlignment.Near;
            pageInfo.Font = new Font("Tahoma", 10, FontStyle.Bold);
            BrickGraphics brickGraphics3 = e.Graph;
            pageInfo1 = brickGraphics3.DrawPageInfo(PageInfo.None, d.dateTo, Color.Black, new Rectangle(828, 180, 120, 70), BorderSide.None);
            pageInfo1.Alignment = BrickAlignment.Near;
            pageInfo1.Font = new Font("Tahoma", 10, FontStyle.Bold);

            pageInfoBrickTo = brickGraphics.DrawPageInfo(PageInfo.None, ":تاريخ الاسترجاع", Color.Black, new Rectangle(825, 240, 100, 80), BorderSide.None);
            pageInfoBrickTo.Font = new Font("Tahoma", 10, FontStyle.Bold);
            pageInfoBrickTo = brickGraphics.DrawPageInfo(PageInfo.None,":سبب الاسترجاع", Color.Black, new Rectangle(628, 240, 150, 80), BorderSide.None);

            pageInfoBrickTo.Alignment = BrickAlignment.Far;
            pageInfoBrickTo.Font = new Font("Tahoma", 10, FontStyle.Bold);
            int y = 240;
            PageInfoBrick pageInfoBrick3;
               
            pageInfoBrick3 = brickGraphics.DrawPageInfo(PageInfo.None, d.delegateName, Color.Black, new Rectangle(825, y, 120, 80), BorderSide.None);
            pageInfoBrick3.Font = new Font("Tahoma", 10, FontStyle.Regular);
            pageInfoBrick3 = brickGraphics.DrawPageInfo(PageInfo.None, d.delegateProfit, Color.Black, new Rectangle(628, y, 150, 80), BorderSide.None);

            pageInfoBrick3.Alignment = BrickAlignment.Far;
            pageInfoBrick3.Font = new Font("Tahoma", 10, FontStyle.Regular);
                
           
            

            // Declare text strings.
            string devexpress = "أذن مرتجع";
            // Specify required settings for the brick graphics.
            BrickGraphics brickGraphics2 = e.Graph;
            brickGraphics2.BackColor = Color.White;
            brickGraphics2.Font = new Font("Tahoma", 14, FontStyle.Bold);

            // Display the DevExpress text string.
            SizeF size = brickGraphics2.MeasureString(devexpress);
            pageInfoBrick = brickGraphics2.DrawPageInfo(PageInfo.None, devexpress, Color.Black, new RectangleF(new PointF(420, 50), size), BorderSide.None);
            pageInfoBrick.Alignment = BrickAlignment.Center;

            size = brickGraphics2.MeasureString(ReturnPermissionNum.ToString());
            pageInfoBrick = brickGraphics2.DrawPageInfo(PageInfo.None, ReturnPermissionNum.ToString(), Color.Black, new RectangleF(new PointF(480, 80), size), BorderSide.None);
            pageInfoBrick.Alignment = BrickAlignment.Center;
        }

    }
}
