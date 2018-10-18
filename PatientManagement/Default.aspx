<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PatientManagement._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

	<div class="jumbotron">
		<h1>Patient Management</h1>
		<p class="lead">The Patient Management application allows a user to list, edit, insert, and delete patients from the Patient table.</p>
		<p>
			Patient data is accessed through a LINQ to SQL DbContext and a custom class which handles all CRUD and en/decryption operations. All 
			Personally Identifiable Information (PII) in the Patients table is encrypted using using the Advanced Encryption Standard (AES) symmetric algorithm.
		</p>
		<br />
		<p class="small"><b>NOTE: This sample application stores the encryption Key and initialization Vector in the web config file. DO NOT use in a production environment!</b></p>
	</div>

	<div class="row">
		<h5>List, Add, Edit, Delete Patients</h5>
		<div style="overflow-x: auto;">
			<asp:ListView ID="LVPatients" runat="server"
				DataKeyNames="ID"
				InsertItemPosition="LastItem"
				OnItemInserting="LVPatients_OnItemInserting"
						  OnItemInserted="LVPatients_OnItemInserted"
				OnItemDeleting="LVPatients_OnItemDeleting"
				OnItemEditing="LVPatients_OnItemEditing">
				<LayoutTemplate>
					<table>
						<tr>
							<th>First Name</th>
							<th>Last Name</th>
							<th>Phone</th>
							<th>Email</th>
							<th>Gender</th>
							<th>Notes</th>
						</tr>
					</table>
					<asp:PlaceHolder ID="itemPlaceholder" runat="server" />

					<asp:DataPager runat="server" PageSize="5">
						<Fields>
							<asp:NextPreviousPagerField
								ButtonType="Button"
								ShowFirstPageButton="True"
								ShowLastPageButton="True" />
						</Fields>
					</asp:DataPager>
				</LayoutTemplate>
				<ItemTemplate>
					<table>
						<tr>
							<td>
								<%# Eval("FirstName") %>
							</td>
							<td>
								<%# Eval("LastName") %>
							</td>
							<td>
								<%# Eval("Phone") %>
							</td>
							<td>
								<%# Eval("Email") %>
							</td>
							<td>
								<%# Eval("Gender") %>
							</td>
							<td>
								<%# Eval("Notes") %>
							</td>
						</tr>
					</table>
				</ItemTemplate>
				<EmptyDataTemplate>
					<table>
						<tr>
							<td>No data was returned.</td>
						</tr>
					</table>
				</EmptyDataTemplate>
				<SelectedItemTemplate>
				</SelectedItemTemplate>
				<AlternatingItemTemplate>
				</AlternatingItemTemplate>
				<EditItemTemplate>
					<tr class="EditItem">
						<td>
							<asp:LinkButton ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />&nbsp;
							<asp:LinkButton ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
						</td>
						<td>
							<asp:TextBox ID="FirstNameTextBox" runat="server" Text='<%#Bind("FirstName") %>' />
						</td>
						<td>
							<asp:TextBox ID="LastNameTextBox" runat="server" Text='<%#Bind("LastName") %>' />
						</td>
						<td>
							<asp:TextBox ID="PhoneTextBox" runat="server"
								Text='<%#Bind("Phone") %>' TextMode="Phone" />
						</td>
						<td>
							<asp:TextBox ID="EmailTextBox" runat="server"
								Text='<%#Bind("Email") %>' TextMode="Email" />
						</td>
						<td>
							<asp:TextBox ID="GenderTextBox" runat="server" Text='<%#Bind("Gender") %>' />
						</td>
						<td>
							<asp:TextBox ID="NotesTextBox" runat="server"
								Text='<%#Bind("Notes") %>' TextMode="MultiLine" />
						</td>
					</tr>
				</EditItemTemplate>
				<InsertItemTemplate>
					<table class="InsertItem">
						<tr>
							<td>
								<asp:TextBox ID="FirstNameTextBox" runat="server"
									Text='<%#Bind("FirstName") %>' />
							</td>
							<td>
								<asp:TextBox ID="LastNameTextBox" runat="server"
									Text='<%#Bind("LastName") %>' />
							</td>
							<td>
								<asp:TextBox ID="PhoneTextBox" runat="server"
									Text='<%#Bind("Phone") %>' TextMode="Phone" />
							</td>
							<td>
								<asp:TextBox ID="EmailTextBox" runat="server"
									Text='<%#Bind("Email") %>' TextMode="Email" />
							</td>
							<td>
								<asp:TextBox ID="GenderTextBox" runat="server"
									Text='<%#Bind("Gender") %>' TextMode="SingleLine" />
							</td>
							<td>
								<asp:TextBox ID="NotesTextBox" runat="server"
									Text='<%#Bind("Notes") %>' TextMode="MultiLine" />
							</td>
							<td>
								<asp:LinkButton ID="InsertButton" runat="server"
									CommandName="Insert" Text="Insert" />
							</td>
						</tr>
					</table>
				</InsertItemTemplate>
			</asp:ListView>

		</div>

		<br />
		<br />

		<asp:Label ID="MessageLabel"
			ForeColor="Red"
			runat="server" />

	</div>

</asp:Content>
