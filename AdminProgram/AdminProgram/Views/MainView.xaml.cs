using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using AdminProgram.Repositories;

namespace AdminProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
            mainMenu.Visibility = Visibility.Visible;
        }
        int rowsAffected;
        string ConnectionString = ConfigurationManager.ConnectionStrings["ProjectCS"].ConnectionString;
        string[] comboBoxRoomsOptions = { "RoomID", "RoomName", "AccessLevel" };
        string[] comboBoxWorkerOptions = { "CHAT_ID", "LastName", "FirstName",
            "AccessLevel", "FatherName", "Email", "Age", "TelephoneNumber", "BloodType" };
        string[] comboBoxAdminsOptions = { "AdminID", "Login", "Password", "Email", "TelephoneNumber" };
        private void closeAllDependentGrid()
        {
            doorsShowView.Visibility = Visibility.Hidden;
            doorsEditView.Visibility = Visibility.Hidden;
            viewToRemoveDoor.Visibility = Visibility.Hidden;
            viewToEditDoor.Visibility = Visibility.Hidden;
            viewToCreateDoor.Visibility = Visibility.Hidden;
            showWorkersLogsView.Visibility = Visibility.Hidden;
            workersShowView.Visibility = Visibility.Hidden;
            workersEditView.Visibility = Visibility.Hidden;
            viewToVerifyWorker.Visibility = Visibility.Hidden;
            viewToEditWorker.Visibility = Visibility.Hidden;
            viewToRemoveWorker.Visibility = Visibility.Hidden;
            adminsShowView.Visibility = Visibility.Hidden;
            adminsEditView.Visibility = Visibility.Hidden;
            workersDetailsShowView.Visibility = Visibility.Hidden;
            viewToRemoveFromBlackListWorker.Visibility = Visibility.Hidden;
            viewToAddToBlackListWorker.Visibility = Visibility.Hidden;
        }

        //
        //Головне меню
        //
        private void mainMenuRooms_Click(object sender, RoutedEventArgs e)
        {
            mainMenu.Visibility = Visibility.Hidden;
            sidebarMenuDoorsButton.IsEnabled = false;
            sidebarMenuLogsButton.IsEnabled = true;
            sidebarMenuWorkersButton.IsEnabled = true;
            sidebarMenuAdminsButton.IsEnabled = true;
            sideBar.Visibility = Visibility.Visible;
            doorsMenu.Visibility = Visibility.Visible;
            closeAllDependentGrid();
        }
        private void mainMenuLogs_Click(object sender, RoutedEventArgs e)
        {
            mainMenu.Visibility = Visibility.Hidden;
            sidebarMenuDoorsButton.IsEnabled = true;
            sidebarMenuLogsButton.IsEnabled = false;
            sidebarMenuWorkersButton.IsEnabled = true;
            sidebarMenuAdminsButton.IsEnabled = true;
            sideBar.Visibility = Visibility.Visible;
            logsMenu.Visibility = Visibility.Visible;
            closeAllDependentGrid();
        }
        private void mainMenuWorkers_Click(object sender, RoutedEventArgs e)
        {
            mainMenu.Visibility = Visibility.Hidden;
            sidebarMenuDoorsButton.IsEnabled = true;
            sidebarMenuLogsButton.IsEnabled = true;
            sidebarMenuWorkersButton.IsEnabled = false;
            sidebarMenuAdminsButton.IsEnabled = true;
            sideBar.Visibility = Visibility.Visible;
            workersMenu.Visibility = Visibility.Visible;
            closeAllDependentGrid();
        }
        private void mainMenuAdmins_Click(object sender, RoutedEventArgs e)
        {
            mainMenu.Visibility = Visibility.Hidden;
            sidebarMenuDoorsButton.IsEnabled = true;
            sidebarMenuLogsButton.IsEnabled = true;
            sidebarMenuWorkersButton.IsEnabled = true;
            sidebarMenuAdminsButton.IsEnabled = false;
            sideBar.Visibility = Visibility.Visible;
            adminsMenu.Visibility = Visibility.Visible;
            closeAllDependentGrid();
        }
        //
        //Кнопки сайдбару
        //
        private void sidebarMenuDoorsButton_Click(object sender, RoutedEventArgs e)
        {
            sidebarMenuDoorsButton.IsEnabled = false;
            sidebarMenuLogsButton.IsEnabled = true;
            sidebarMenuWorkersButton.IsEnabled = true;
            sidebarMenuAdminsButton.IsEnabled = true;
            doorsMenu.Visibility = Visibility.Visible;
            logsMenu.Visibility = Visibility.Hidden;
            workersMenu.Visibility = Visibility.Hidden;
            adminsMenu.Visibility = Visibility.Hidden;
            closeAllDependentGrid();
        }
        private void sidebarMenuLogsButton_Click(object sender, RoutedEventArgs e)
        {
            sidebarMenuDoorsButton.IsEnabled = true;
            sidebarMenuLogsButton.IsEnabled = false;
            sidebarMenuWorkersButton.IsEnabled = true;
            sidebarMenuAdminsButton.IsEnabled = true;
            doorsMenu.Visibility = Visibility.Hidden;
            logsMenu.Visibility = Visibility.Visible;
            workersMenu.Visibility = Visibility.Hidden;
            adminsMenu.Visibility = Visibility.Hidden;
            closeAllDependentGrid();
        }
        private void sidebarMenuWorkersButton_Click(object sender, RoutedEventArgs e)
        {
            sidebarMenuDoorsButton.IsEnabled = true;
            sidebarMenuLogsButton.IsEnabled = true;
            sidebarMenuWorkersButton.IsEnabled = false;
            sidebarMenuAdminsButton.IsEnabled = true;
            doorsMenu.Visibility = Visibility.Hidden;
            logsMenu.Visibility = Visibility.Hidden;
            workersMenu.Visibility = Visibility.Visible;
            adminsMenu.Visibility = Visibility.Hidden;
            closeAllDependentGrid();
        }
        private void sidebarMenuAdminsButton_Click(object sender, RoutedEventArgs e)
        {
            sidebarMenuDoorsButton.IsEnabled = true;
            sidebarMenuLogsButton.IsEnabled = true;
            sidebarMenuWorkersButton.IsEnabled = true;
            sidebarMenuAdminsButton.IsEnabled = false;
            doorsMenu.Visibility = Visibility.Hidden;
            logsMenu.Visibility = Visibility.Hidden;
            workersMenu.Visibility = Visibility.Hidden;
            adminsMenu.Visibility = Visibility.Visible;
            closeAllDependentGrid();
        }
        private void exitToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            mainMenu.Visibility = Visibility.Visible;
            doorsMenu.Visibility = Visibility.Hidden;
            logsMenu.Visibility = Visibility.Hidden;
            workersMenu.Visibility = Visibility.Hidden;
            adminsMenu.Visibility = Visibility.Hidden;
            sideBar.Visibility = Visibility.Hidden;
            closeAllDependentGrid();
        }
        //
        //Кнопки меню кімнат
        //
        private void showDoorsButton_Click(object sender, RoutedEventArgs e)
        {
            doorsMenu.Visibility = Visibility.Hidden;
            doorsShowView.Visibility = Visibility.Visible;
            DataTable dataTable = new DataTable();
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM Rooms", connection);
                connection.Open();
                dataTable.Load(command.ExecuteReader());
                connection.Close();
            }
            doorsDataShow.CanUserAddRows = false;
            doorsDataShow.AutoGenerateColumns = false;
            doorsDataShow.ItemsSource = dataTable.DefaultView;
        }
        private void editDoorsButton_Click(object sender, RoutedEventArgs e)
        {
            doorsMenu.Visibility = Visibility.Hidden;
            doorsEditView.Visibility = Visibility.Visible;
        }
        private void exitToRoomsMenuButton_Click(object sender, RoutedEventArgs e)
        {
            doorsMenu.Visibility = Visibility.Visible;
            closeAllDependentGrid();
        }
        private void createDoorButton_Click(object sender, RoutedEventArgs e)
        {
            doorsEditView.Visibility = Visibility.Hidden;
            viewToCreateDoor.Visibility = Visibility.Visible;
        }
        private void createDoorActionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rowsAffected = 0;
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                {
                    MySqlCommand command = new MySqlCommand("INSERT INTO Rooms (RoomName, AccessLevel) VALUES (@roomName, @accessLevel)", connection);
                    command.Parameters.AddWithValue("@roomName", roomNameInputCreateTextBox.Text);
                    command.Parameters.AddWithValue("@accessLevel", Convert.ToInt32(roomAccessInputCreateTextBox.Text));
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
                roomInfoCreateTextBox.Text = rowsAffected == 1 ? "Нову кімнату створено." : "Нову кімнату не було створено.";
            }
            catch (Exception) { roomInfoCreateTextBox.Text = "Виникла помилка."; }
        }
        private void editDoorButton_Click(object sender, RoutedEventArgs e)
        {
            doorsEditView.Visibility = Visibility.Hidden;
            viewToEditDoor.Visibility = Visibility.Visible;
        }
        private void editDoorActionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rowsAffected = 0;
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                {
                    MySqlCommand command = new MySqlCommand($"UPDATE Rooms SET {comboBoxRoomsOptions[roomOutputEditComboBox.SelectedIndex]} = " +
                        $"{(roomOutputEditComboBox.SelectedIndex == 1 ? ("\'" + roomOutputEditTextBox.Text + "\'") : roomOutputEditTextBox.Text)} " +
                        $"WHERE {comboBoxRoomsOptions[roomInputEditComboBox.SelectedIndex]} = " +
                        $"{(roomInputEditComboBox.SelectedIndex == 1 ? ("\'" + roomInputEditTextBox.Text + "\'") : roomInputEditTextBox.Text)};", connection);
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
                roomInfoEditTextBox.Text = rowsAffected > 0 ? (rowsAffected + " з кімнат було змінено.") : "Жодної кімнати не було змінено.";
            }
            catch (Exception) { roomInfoEditTextBox.Text = "Виникла помилка."; }
        }
        private void removeDoorButton_Click(object sender, RoutedEventArgs e)
        {
            doorsEditView.Visibility = Visibility.Hidden;
            viewToRemoveDoor.Visibility = Visibility.Visible;
        }
        private void removeDoorActionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rowsAffected = 0;
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                {
                    MySqlCommand command = new MySqlCommand($"DELETE FROM Rooms WHERE {comboBoxRoomsOptions[roomInputRemoveComboBox.SelectedIndex]} = " +
                        $"{(roomInputRemoveComboBox.SelectedIndex == 1 ? ("\'" + roomInputRemoveTextBox.Text + "\'") : roomInputRemoveTextBox.Text)}", connection);
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
                roomInfoRemoveTextBox.Text = rowsAffected > 0 ? (rowsAffected + " з кімнат було видалено.") : "Жодного кімнати не було видалено.";
            }
            catch (Exception) { roomInfoRemoveTextBox.Text = "Виникла помилка."; }
        }
        private void exitToDoorsEditViewButton_Click(object sender, RoutedEventArgs e)
        {
            viewToRemoveDoor.Visibility = Visibility.Hidden;
            viewToEditDoor.Visibility = Visibility.Hidden;
            viewToCreateDoor.Visibility = Visibility.Hidden;
            doorsEditView.Visibility = Visibility.Visible;
        }
        //
        //Кнопки меню логів
        //
        private void showWorkersLogsButton_Click(object sender, RoutedEventArgs e)
        {
            logsMenu.Visibility = Visibility.Hidden;
            showWorkersLogsView.Visibility = Visibility.Visible;
            DataTable dataTable = new DataTable();
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM PeoplesLogins", connection);
                connection.Open();
                dataTable.Load(command.ExecuteReader());
                connection.Close();
            }
            workersLogsDataShow.CanUserAddRows = false;
            workersLogsDataShow.CanUserDeleteRows = false;
            workersLogsDataShow.AutoGenerateColumns = false;
            workersLogsDataShow.ItemsSource = dataTable.DefaultView;
        }
        private void showBlackListButton_Click(object sender, RoutedEventArgs e)
        {
            logsMenu.Visibility = Visibility.Hidden;
            showBlackListView.Visibility = Visibility.Visible;
            DataTable dataTable = new DataTable();
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM black_list", connection);
                connection.Open();
                dataTable.Load(command.ExecuteReader());
                connection.Close();
            }
            blackListShow.CanUserAddRows = false;
            blackListShow.CanUserDeleteRows = false;
            blackListShow.AutoGenerateColumns = false;
            blackListShow.ItemsSource = dataTable.DefaultView;
        }

        private void editBlackListButton_Click(object sender, RoutedEventArgs e)
        {
            logsMenu.Visibility = Visibility.Hidden;
            editBlackListView.Visibility = Visibility.Visible;
        }

        private void addToBlackListButton_Click(object sender, RoutedEventArgs e)
        {
            editBlackListView.Visibility = Visibility.Hidden;
            viewToAddToBlackListWorker.Visibility = Visibility.Visible;
        }
        private void addToBlackListActionButton_Click(object sender, RoutedEventArgs e)
        {
            string input = ((blackListInputAddComboBox.SelectedIndex == 0) || (blackListInputAddComboBox.SelectedIndex == 3))
                ? blackListInputAddTextBox.Text : ("\'" + blackListInputAddTextBox.Text + "\'");
            try
            {
                rowsAffected = 0;
                DataTable table = new DataTable();
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT CHAT_ID FROM " +
                        $"{(blackListInputAddComboBox.SelectedIndex > 3 ? "PeoplesAdditionalInfo" : "People")} " +
                        $"WHERE {comboBoxWorkerOptions[blackListInputAddComboBox.SelectedIndex]} = {input}", connection);
                    adapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        MySqlCommand command = new MySqlCommand($"INSERT INTO black_list (USER_ID) VALUES ({(int)row["CHAT_ID"]});", connection);
                        command.ExecuteNonQuery();
                        rowsAffected++;
                    }
                    connection.Close();
                }
                blackListInfoAddTextBox.Text = rowsAffected > 0 ? (rowsAffected + " з працівників було добавлено у список.") : "Жодного працівника не було добавленого у список.";
            }
            catch (Exception) { blackListInfoAddTextBox.Text = "Виникла помилка."; }
        }
        private void removeFromBlackListButton_Click(object sender, RoutedEventArgs e)
        {
            editBlackListView.Visibility = Visibility.Hidden;
            viewToRemoveFromBlackListWorker.Visibility = Visibility.Visible;
        }
        private void removeFromBlackListActionButton_Click(object sender, RoutedEventArgs e)
        {
            string input = ((blackListInputRemoveComboBox.SelectedIndex == 0) || (blackListInputRemoveComboBox.SelectedIndex == 3))
                ? blackListInputRemoveTextBox.Text : ("\'" + blackListInputRemoveTextBox.Text + "\'");
            try
            {
                rowsAffected = 0;
                DataTable table = new DataTable();
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT CHAT_ID FROM " +
                        $"{(blackListInputRemoveComboBox.SelectedIndex > 3 ? "PeoplesAdditionalInfo" : "People")} " +
                        $"WHERE {comboBoxWorkerOptions[blackListInputRemoveComboBox.SelectedIndex]} = {input}", connection);
                    adapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        MySqlCommand command = new MySqlCommand($"DELETE FROM black_list WHERE USER_ID = {(int)row["CHAT_ID"]};", connection);
                        command.ExecuteNonQuery();
                        rowsAffected++;
                    }
                    connection.Close();
                }
                blackListInfoRemoveTextBox.Text = rowsAffected > 0 ? (rowsAffected + " з працівників було видалено зі списку.") : "Жодного працівника не було видалено зі списку.";
            }
            catch (Exception) { blackListInfoRemoveTextBox.Text = "Виникла помилка."; }
        }
        private void exitToEditBlackListViewButton_Click(object sender, RoutedEventArgs e)
        {
            viewToRemoveFromBlackListWorker.Visibility = Visibility.Hidden;
            viewToAddToBlackListWorker.Visibility = Visibility.Hidden;
            editBlackListView.Visibility = Visibility.Visible;
        }

        private void exitToLogsMenuButton_Click(object sender, RoutedEventArgs e)
        {
            showWorkersLogsView.Visibility = Visibility.Hidden;
            showBlackListView.Visibility = Visibility.Hidden;
            editBlackListView.Visibility = Visibility.Hidden;
            logsMenu.Visibility = Visibility.Visible;
        }
        //
        //Кнопки меню працівників
        //
        private void showWorkersButton_Click(object sender, RoutedEventArgs e)
        {
            workersMenu.Visibility = Visibility.Hidden;
            workersShowView.Visibility = Visibility.Visible;
            DataTable dataTable = new DataTable();
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM People", connection);
                connection.Open();
                dataTable.Load(command.ExecuteReader());
                connection.Close();
            }
            workersDataShow.CanUserAddRows = false;
            workersDataShow.CanUserDeleteRows = false;
            workersDataShow.AutoGenerateColumns = false;
            workersDataShow.ItemsSource = dataTable.DefaultView;
        }

        private void showWorkersDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            workersShowView.Visibility = Visibility.Hidden;
            workersDetailsShowView.Visibility = Visibility.Visible;
            DataTable dataTable = new DataTable();
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM PeoplesAdditionalInfo", connection);
                connection.Open();
                dataTable.Load(command.ExecuteReader());
                connection.Close();
            }
            workersDetailsDataShow.CanUserAddRows = false;
            workersLogsDataShow.CanUserDeleteRows = false;
            workersDetailsDataShow.AutoGenerateColumns = false;
            workersDetailsDataShow.ItemsSource = dataTable.DefaultView;
        }
        private void exitToWorkersMainShowViewButton_Click(object sender, RoutedEventArgs e)
        {
            workersDetailsShowView.Visibility = Visibility.Hidden;
            workersShowView.Visibility = Visibility.Visible;

        }

        private void editWorkersButton_Click(object sender, RoutedEventArgs e)
        {
            workersMenu.Visibility= Visibility.Hidden;
            workersEditView.Visibility= Visibility.Visible;
        }

        private void exitToWorkersMenuButton_Click(object sender, RoutedEventArgs e)
        {
            workersShowView.Visibility = Visibility.Hidden;
            workersEditView.Visibility = Visibility.Hidden;
            workersMenu.Visibility = Visibility.Visible;
        }
        private void verifyWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            workersEditView.Visibility = Visibility.Hidden;
            viewToVerifyWorker.Visibility = Visibility.Visible;
        }

        private void verifyWorkerActionButton_Click(object sender, RoutedEventArgs e)
        {
            string input = ((workerInputVerifyComboBox.SelectedIndex == 0) || (workerInputVerifyComboBox.SelectedIndex == 3)) 
                ? workerInputVerifyTextBox.Text : ("\'" + workerInputVerifyTextBox.Text + "\'");
            try
            {
                rowsAffected = 0;
                DataTable table = new DataTable();
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT CHAT_ID FROM " +
                        $"{(workerInputVerifyComboBox.SelectedIndex > 3 ? "PeoplesAdditionalInfo" : "People")} " +
                        $"WHERE {comboBoxWorkerOptions[workerInputVerifyComboBox.SelectedIndex]} = {input}", connection);
                    adapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        MySqlCommand command = new MySqlCommand("UPDATE People SET Status = " +
                            $"{Convert.ToInt32(workerChooseVerifyOptionComboBox.SelectedIndex == 0 ? 1 : 0)} " +
                            $"WHERE CHAT_ID = {(int)row["CHAT_ID"]};", connection);
                        command.ExecuteNonQuery();
                        rowsAffected++;
                    }
                    connection.Close();
                }
                workerInfoVerifyTextBox.Text = rowsAffected > 0 ? ("Статус " + rowsAffected + " з працівників було затверджено.") : "Статус жодного працівника не було затверджено.";
            }
            catch (Exception) { workerInfoVerifyTextBox.Text = "Виникла помилка."; }
        }

        private void editWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            workersEditView.Visibility = Visibility.Hidden;
            viewToEditWorker.Visibility = Visibility.Visible;
        }
        private void editWorkerActionButton_Click(object sender, RoutedEventArgs e)
        {
            string input = ((workerInputEditComboBox.SelectedIndex == 0) || (workerInputEditComboBox.SelectedIndex == 3))
                ? workerInputEditTextBox.Text : ("\'" + workerInputEditTextBox.Text + "\'"),
                output = ((workerOutputEditComboBox.SelectedIndex == 0) || (workerOutputEditComboBox.SelectedIndex == 3))
                ? workerOutputEditTextBox.Text : ("\'" + workerOutputEditTextBox.Text + "\'");
            try
            {
                rowsAffected = 0;
                DataTable table = new DataTable();
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT CHAT_ID FROM " +
                        $"{(workerInputEditComboBox.SelectedIndex > 3 ? "PeoplesAdditionalInfo" : "People")} " +
                        $"WHERE {comboBoxWorkerOptions[workerInputEditComboBox.SelectedIndex]} = {input}", connection);
                    adapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        MySqlCommand command = new MySqlCommand("UPDATE " +
                            $"{(workerOutputEditComboBox.SelectedIndex > 3 ? "PeoplesAdditionalInfo" : "People")} " +
                            $"SET {comboBoxWorkerOptions[workerOutputEditComboBox.SelectedIndex]} = {output} " +
                            $"WHERE CHAT_ID = {(int)row["CHAT_ID"]};", connection);
                        command.ExecuteNonQuery();
                        rowsAffected++;
                    }
                    connection.Close();
                }
                workerInfoEditTextBox.Text = rowsAffected > 0 ? (rowsAffected + " з працівників було змінено.") : "Жодного працівника не було змінено.";
            }
            catch (Exception) { workerInfoEditTextBox.Text = "Виникла помилка."; }
        }

        private void removeWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            workersEditView.Visibility = Visibility.Hidden;
            viewToRemoveWorker.Visibility = Visibility.Visible;
        }
        private void removeWorkerActionButton_Click(object sender, RoutedEventArgs e)
        {
            string input = ((workerInputRemoveComboBox.SelectedIndex == 0) || (workerInputRemoveComboBox.SelectedIndex == 3)) 
                ? workerInputRemoveTextBox.Text : ("\'" + workerInputRemoveTextBox.Text + "\'"),
                inputTable = workerInputRemoveComboBox.SelectedIndex > 3 ? "PeoplesAdditionalInfo" : "People";
            try
            {
                rowsAffected = 0;
                DataTable table = new DataTable();
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                {
                    connection.Open();
                    MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT CHAT_ID FROM " +
                        $"{(workerInputRemoveComboBox.SelectedIndex > 3 ? "PeoplesAdditionalInfo" : "People")} " +
                        $"WHERE {comboBoxWorkerOptions[workerInputRemoveComboBox.SelectedIndex]} = {input}", connection);
                    adapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        MySqlCommand command = new MySqlCommand($"DELETE FROM PeoplesAdditionalInfo WHERE CHAT_ID = {(int)row["CHAT_ID"]};", connection);
                        command.ExecuteNonQuery();
                        rowsAffected++;
                    }
                    foreach (DataRow row in table.Rows)
                    {
                        MySqlCommand command = new MySqlCommand($"DELETE FROM People WHERE CHAT_ID = {(int)row["CHAT_ID"]};", connection);
                        command.ExecuteNonQuery();
                        rowsAffected++;
                    }
                    connection.Close();
                }
                workerInfoRemoveTextBox.Text = rowsAffected / 2 > 0 ? (rowsAffected / 2 + " з працівників було видалено.") : "Жодного працівника не було видалено.";
            }
            catch (Exception) { workerInfoRemoveTextBox.Text = "Виникла помилка."; }
        }

        private void exitToWorkersEditViewButton_Click(object sender, RoutedEventArgs e)
        {
            viewToVerifyWorker.Visibility = Visibility.Hidden;
            viewToEditWorker.Visibility = Visibility.Hidden;
            viewToRemoveWorker.Visibility = Visibility.Hidden;
            workersEditView.Visibility = Visibility.Visible;
        }
        //
        //Кнопки меню адмінів
        //
        private void showAdminsButton_Click(object sender, RoutedEventArgs e)
        {
            adminsMenu.Visibility = Visibility.Hidden;
            adminsShowView.Visibility = Visibility.Visible;
            workersShowView.Visibility = Visibility.Hidden;
            workersDetailsShowView.Visibility = Visibility.Visible;
            DataTable dataTable = new DataTable();
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM Admins", connection);
                connection.Open();
                dataTable.Load(command.ExecuteReader());
                connection.Close();
            }
            adminsDataShow.CanUserAddRows = false;
            workersLogsDataShow.CanUserDeleteRows = false;
            adminsDataShow.AutoGenerateColumns = false;
            adminsDataShow.ItemsSource = dataTable.DefaultView;
        }

        private void editAdminsButton_Click(object sender, RoutedEventArgs e)
        {
            adminsMenu.Visibility = Visibility.Hidden;
            adminsEditView.Visibility = Visibility.Visible;
        }

        private void exitToAdminsMenuButton_Click(object sender, RoutedEventArgs e)
        {
            adminsShowView.Visibility = Visibility.Hidden;
            adminsEditView.Visibility = Visibility.Hidden;
            adminsMenu.Visibility = Visibility.Visible;
        }

        private void registerAdminButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminRepository.is_SuperGlobal)
            {
                adminsEditView.Visibility = Visibility.Hidden;
                viewToRegisterAdmin.Visibility = Visibility.Visible;
            }
            else
            {
                CanModify.Text = "Ви не маєте доступу";
            }
        }
        private void registerAdminActionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rowsAffected = 0;
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                {
                    MySqlCommand command = new MySqlCommand("INSERT INTO Admins (Login, Password, IS_SUPER, TelephoneNumber, Email) " +
                        "VALUES (@login, @password, 0, @telephonenumber, @email)", connection);
                    command.Parameters.AddWithValue("@login", adminLoginInputRegisterTextBox.Text);
                    command.Parameters.AddWithValue("@password", adminPasswordInputRegisterTextBox.Text);
                    command.Parameters.AddWithValue("@telephonenumber", adminPhoneNumberInputRegisterTextBox.Text);
                    command.Parameters.AddWithValue("@email", adminEmailInputRegisterTextBox.Text);
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
                adminsInfoRegisterTextBox.Text = rowsAffected == 1 ? "Нового адміна зареєстровано." : "Нового адміна не було зареєстровано.";
            }
            catch (Exception) { roomInfoCreateTextBox.Text = "Виникла помилка."; }
        }

        private void editAdminButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminRepository.is_SuperGlobal)
            {
                adminsEditView.Visibility = Visibility.Hidden;
                viewToEditAdmin.Visibility = Visibility.Visible;
            }
            else
            {
                CanModify.Text = "Ви не маєте доступу";
            }
        }
        private void editAdminActionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rowsAffected = 0;
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                {
                    MySqlCommand command = new MySqlCommand($"UPDATE Admins SET {comboBoxAdminsOptions[adminOutputEditComboBox.SelectedIndex]} = " +
                        $"{(adminOutputEditComboBox.SelectedIndex == 0 ? adminOutputEditTextBox.Text : ("\'" + adminOutputEditTextBox.Text + "\'"))} " +
                        $"WHERE {comboBoxAdminsOptions[adminInputEditComboBox.SelectedIndex]} = " +
                        $"{(adminInputEditComboBox.SelectedIndex == 0 ? adminInputEditTextBox.Text : ("\'" + adminInputEditTextBox.Text + "\'"))}", connection);
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
                adminInfoEditTextBox.Text = rowsAffected > 0 ? (rowsAffected + " з адмінів було змінено.") : "Жодної адміна не було змінено.";
            }
            catch (Exception) { adminInfoEditTextBox.Text = "Виникла помилка."; }
        }
        private void removeAdminButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminRepository.is_SuperGlobal)
            {
                viewToEditAdmin.Visibility = Visibility.Hidden;
                viewToRemoveAdmin.Visibility = Visibility.Visible;
            }
            else
            {
                CanModify.Text = "Ви не маєте доступу";
            }
        }
        private void removeAdminActionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                rowsAffected = 0;
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                {
                    MySqlCommand command = new MySqlCommand("DELETE FROM Admins " +
                        $"WHERE {comboBoxAdminsOptions[adminInputRemoveComboBox.SelectedIndex]} = " +
                        $"{(adminInputRemoveComboBox.SelectedIndex == 1 ? ("\'" + adminInputRemoveTextBox.Text + "\'") : adminInputRemoveTextBox.Text)}", connection);
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
                adminInfoRemoveTextBox.Text = rowsAffected > 0 ? (rowsAffected + " з адмінів було видалено.") : "Жодного адміна не було видалено.";
            }
            catch (Exception) { adminInfoRemoveTextBox.Text = "Виникла помилка."; }
        }
        private void exitToAdminsEditViewButton_Click(object sender, RoutedEventArgs e)
        {
            viewToRegisterAdmin.Visibility = Visibility.Hidden;
            viewToEditAdmin.Visibility = Visibility.Hidden;
            viewToRemoveAdmin.Visibility = Visibility.Hidden;
            adminsEditView.Visibility = Visibility.Visible;
        }

    }
}