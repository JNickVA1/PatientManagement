using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Configuration;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using PatientManagement.Properties;

namespace PatientManagement
{
	public partial class _Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				// Read the connection string from the web config file.
				GetConnectionString();

				var aes = new AesManaged();
				//aes.KeySize = 128;
				//var ks = aes.KeySize;
				//var k = aes.Key;

				// Get the encryption key and vector.
				GetEncryptionInfo();

				// Create the DbContext.
				CreateDbContext();

				// Populate the ListView with the initial table data.
				if (Session["DataContext"] is PatientManagementDataContext context)
				{
					// Retrieve the Patient rows of data from tha data context.
					List<Patient> patientList = context.Patients.ToList();

					//
					if (patientList.Count > 0)
					{
						// Decrypt the data.

					}
					//
					LVPatients.DataSource = patientList;
					LVPatients.DataBind();
				}
			}
		}

		private void GetEncryptionInfo()
		{
			// Read and save the Key and Vector.
			var keyStr = ConfigurationManager.AppSettings["CryptKey"];
			Session["CryptKey"] = keyStr;
			var keyVec = ConfigurationManager.AppSettings["CryptVector"];
			Session["CryptVector"] = keyVec;
		}

		private void CreateDbContext()
		{
			// Using the previously read connection string, create the DbContext.
			var patientManagementDb = new PatientManagementDataContext(Session["Conn"] as string);

			// Save the Data Context for future use.
			Session["DataContext"] = patientManagementDb;
		}

		private void GetConnectionString()
		{
			// Get the connection string.
			var connectionString = WebConfigurationManager.ConnectionStrings["PatientDb"].ConnectionString;

			// Save the connection string in case we need to re-use it.
			Session["Conn"] = connectionString;
		}

		protected void LVPatients_OnItemInserting(object sender, ListViewInsertEventArgs e)
		{
			// Get the controls that are contained in the insert item.
			TextBox firstName =
				(TextBox)LVPatients.InsertItem.FindControl("FirstNameTextBox");
			TextBox lastName =
				(TextBox)LVPatients.InsertItem.FindControl("LastNameTextBox");
			TextBox phone =
				(TextBox)LVPatients.InsertItem.FindControl("PhoneTextBox");
			TextBox email =
				(TextBox)LVPatients.InsertItem.FindControl("EmailTextBox");
			TextBox gender =
				(TextBox)LVPatients.InsertItem.FindControl("GenderTextBox");
			TextBox notes =
				(TextBox)LVPatients.InsertItem.FindControl("NotesTextBox");

			//Check if the controls are empty.
			if ((firstName.Text.Trim().Length == 0) ||
			    (lastName.Text.Trim().Length == 0) ||
			    (phone.Text.Trim().Length == 0) ||
			    (email.Text.Trim().Length == 0) ||
			    (gender.Text.Trim().Length == 0) ||
			    (notes.Text.Trim().Length == 0))
			{
				MessageLabel.Text =
					"The system could not insert the item. All fields are required.";
				e.Cancel = true;
			}
			// Encrypt the data.
			byte[] keyBytes =
				System.Text.Encoding.Unicode.GetBytes(Session["CryptKey"].ToString());
			byte[] vectorBytes =
				System.Text.Encoding.Unicode.GetBytes(Session["CryptVector"].ToString());
			var newPatient = new Patient();
			newPatient.FirstName = Crypto.Encrypt(firstName.Text.Trim(), keyBytes, vectorBytes);
			newPatient.LastName = Crypto.Encrypt(lastName.Text.Trim(), keyBytes, vectorBytes);
			newPatient.Phone = Crypto.Encrypt(phone.Text.Trim(), keyBytes, vectorBytes);
			newPatient.Email = Crypto.Encrypt(email.Text.Trim(), keyBytes, vectorBytes);
			newPatient.Gender = Crypto.Encrypt(gender.Text.Trim(), keyBytes, vectorBytes);
			newPatient.Notes = Crypto.Encrypt(notes.Text.Trim(), keyBytes, vectorBytes);

			// Add the new Patient object to the Data Context Patient collection.
			var context = Session["DataContext"] as PatientManagementDataContext;
			context?.Patients.InsertOnSubmit(newPatient);

			// Submit changes
			context?.SubmitChanges();
		}

		protected void LVPatients_OnItemDeleting(object sender, ListViewDeleteEventArgs e)
		{
			throw new NotImplementedException();
		}

		protected void LVPatients_OnItemEditing(object sender, ListViewEditEventArgs e)
		{
			throw new NotImplementedException();
		}
	}
}