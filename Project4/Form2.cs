using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace Project4
{
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;
        public Form2()
        {
            InitializeComponent();
            string strConnection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            con = new SqlConnection(strConnection);
        }
        private DataSet GetCourseData()
        {
            da = new SqlDataAdapter("select * from Course_Details", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "course_details");
            return ds;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetCourseData();
                DataRow row = ds.Tables["course_details"].NewRow();
                row["CourseName"] = txtCourseName.Text;
                row["Fees"] = txtFees.Text;
                ds.Tables["course_details"].Rows.Add(row);
                int result = da.Update(ds.Tables["course_details"]);
                if(result==1)
                {
                    MessageBox.Show("Course Details Inserted");

                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            try
            {
                ds = GetCourseData();
                DataRow row = ds.Tables["course_details"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    row["CourseName"] =txtCourseName.Text;
                    row["Fees"] = txtFees.Text;
                    int result = da.Update(ds.Tables["course_details"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Course Record Updated");
                    }

                }
                else
                {
                    MessageBox.Show("Id does not exists to update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetCourseData();
                DataRow row = ds.Tables["course_details"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    row.Delete();//delete the row from dataset
                    int result = da.Update(ds.Tables["course_details"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Course Record Deleted");
                    }

                }
                else
                {
                    MessageBox.Show("Id does not exists to Delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetCourseData();
                DataRow row = ds.Tables["course_details"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    txtCourseName.Text = row["CourseName"].ToString();
                    txtFees.Text = row["Fees"].ToString();

                }
                else
                {
                    MessageBox.Show("Course Record does not exists ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowAllDetails_Click(object sender, EventArgs e)
        {
            ds = GetCourseData();
            dataGridView1.DataSource = ds.Tables["course_details"];
        }
    }
}
