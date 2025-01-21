using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Railwaysyestem
{
    public partial class Form1 : Form
    {
       // private int TicketPrice;
        //private int NoSeats;
        //private int Total;
        private SqlConnection xConn;
        public Form1()
        {
            InitializeComponent();
            xConn = new SqlConnection("Server=SYED-ALI\\MSSQLSERVER03;DataBase=Railwaysystem;UID=sa;PWD=123;");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            xConn.Open();
            int TicketPrice = int.Parse(txtprice.Text);
            int NoSeats = int.Parse(txtseats.Text);
            txttotal.Text = (TicketPrice * NoSeats).ToString();
            new SqlCommand("INSERT INTO tblrailway values('" + txtname.Text + "','" + txtphone.Text + "','" + comboBox.Text + "','" + txtfrom.Text + "','" + txtto.Text + "','" + txtdate.Text + "','" + txtprice.Text + "','" + txtseats.Text + "','"+txttotal.Text+"')", xConn).ExecuteNonQuery();
           //int TicketPrice = int.Parse(txtprice.Text);
           //int NoSeats = int.Parse(txtseats.Text);
           //txttotal.Text = (TicketPrice * NoSeats).ToString();
            xConn.Close();
           id.Text= txtname.Text = txtphone.Text = comboBox.Text = txtfrom.Text = txtto.Text = txtdate.Text = txtprice.Text = txtseats.Text = txttotal.Text = null;
            MessageBox.Show("Data Saved In SQL Table....");
        }

        private void btnview_Click(object sender, EventArgs e)
        {
            xConn.Open();
            DataTable xTable = new DataTable();
            new SqlDataAdapter("select * from tblrailway", xConn).Fill(xTable);
            xGrid.DataSource = xTable;
            xConn.Close();
        }

        private void btnse_Click(object sender, EventArgs e)
        {
            xConn.Open();
            SqlCommand cmd = new SqlCommand("select * from tblrailway where @PassengerName=PassengerName AND @PhoneNo=PhoneNo", xConn);
            cmd.Parameters.AddWithValue("@PassengerName",txtname.Text);
            cmd.Parameters.AddWithValue("@PhoneNo",Int64.Parse (txtphone.Text));
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable xTable = new DataTable();
            da.Fill(xTable);
            xGrid.DataSource = xTable;
            xConn.Close();
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            xConn.Open();
            SqlCommand command = new SqlCommand("DELETE tblrailway where RID='"+ int.Parse(id.Text) + "'", xConn);
            command.ExecuteNonQuery();
            xConn.Close();
            MessageBox.Show("Data Delete");
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            xConn.Open();
            int TicketPrice = int.Parse(txtprice.Text);
            int NoSeats = int.Parse(txtseats.Text);
            txttotal.Text = (TicketPrice * NoSeats).ToString();
            SqlCommand cmd = new SqlCommand("update tblrailway set PassengerName='"+txtname.Text+"',PhoneNo='"+txtphone.Text+"',Railway='"+comboBox.Text+"',FromCity='"+txtfrom.Text+"',ToCity='"+txtto.Text+"',TravelDate='"+txtdate.Text+"',TicketPrice='"+txtprice.Text+"',NoSeats='"+txtseats.Text+"',Total='"+txttotal.Text+"' where RID='"+int.Parse(id.Text)+"'", xConn);
            cmd.ExecuteNonQuery();
            xConn.Close();
            MessageBox.Show("Data Update!");
        }


    }
}
