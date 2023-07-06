using Google.Protobuf.WellKnownTypes;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using QRCoder;
using QRCoder.Xaml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.Xml.Linq;

namespace PassProgram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["ProjectCS"].ConnectionString;
        private static Random random = new Random();
        private static string QRCode = string.Empty;
        int rowsAffected = 0; bool isGenerated = false;
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public MainWindow()
        {
            InitializeComponent();
            Closing += Window_Closing;
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
            SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isGenerated)
            {
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("DELETE FROM Code WHERE Code = @code", connection);
                    command.Parameters.AddWithValue("@code", QRCode);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    verifyInfoOutputTextBlock.Text = "QR код вже не дійсний.";
                    break;

                case PowerModes.Suspend:
                    if (isGenerated)
                    {
                        MySqlConnection connection = new MySqlConnection(ConnectionString);
                        {
                            connection.Open();
                            MySqlCommand command = new MySqlCommand("DELETE FROM Code WHERE Code = @code", connection);
                            command.Parameters.AddWithValue("@code", QRCode);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                        isGenerated = false;
                    }
                    break;
            }
        }
        private void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    verifyInfoOutputTextBlock.Text = "QR код вже не дійсний.";
                    break;

                case SessionSwitchReason.SessionUnlock:
                    if (isGenerated)
                    {
                        MySqlConnection connection = new MySqlConnection(ConnectionString);
                        {
                            connection.Open();
                            MySqlCommand command = new MySqlCommand("DELETE FROM Code WHERE Code = @code", connection);
                            command.Parameters.AddWithValue("@code", QRCode);
                            command.ExecuteNonQuery();
                            connection.Close();
                        }
                        isGenerated = false;
                    }
                    break;
            }
        }
        private void GenerateQR_Click(object sender, RoutedEventArgs e)
        {
            verifyInfoOutputTextBlock.Foreground = Brushes.Black; string RoomID = "";
            using (var stream = new FileStream("room_id.bin", FileMode.Open))
            using (var reader = new BinaryReader(stream)) { RoomID = reader.ReadString(); }
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            {
                MySqlCommand checkCommand = new MySqlCommand($"SELECT * FROM Rooms " +
                    $"WHERE RoomID = {RoomID}", connection);
                connection.Open();
                if (checkCommand.ExecuteScalar() == null) { verifyInfoOutputTextBlock.Text = "Двері не зареєстровані."; }
                else if (isGenerated) { verifyInfoOutputTextBlock.Text = "QR код вже згенеровано."; }
                else
                {
                    try
                    {
                        QRCode = RandomString(20);
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(("https://t.me/QRBadge_bot?start=_" + QRCode + "_" + RoomID), QRCodeGenerator.ECCLevel.H);
                        XamlQRCode qrCode = new XamlQRCode(qrCodeData);
                        DrawingImage qrCodeAsXaml = qrCode.GetGraphic(20, "#000000", "#FFFFF3E8", true);
                        qrImage.Source = qrCodeAsXaml;
                        MySqlCommand command = new MySqlCommand("INSERT INTO Code (Code, RoomID, Access, Last_change) VALUES (@code, @roomID, 0, @last_change)", connection);
                        command.Parameters.AddWithValue("@code", QRCode);
                        command.Parameters.AddWithValue("@roomID", RoomID);
                        command.Parameters.AddWithValue("@last_change", Convert.ToString(DateTime.Now));
                        rowsAffected = command.ExecuteNonQuery();
                        isGenerated = rowsAffected == 1 ? true : false;
                        verifyInfoOutputTextBlock.Text = rowsAffected == 1 ? "QR код згенеровано" : "QR код не було згенеровано";
                    }
                    catch (Exception) { verifyInfoOutputTextBlock.Text = "Виникла помилка."; }
                }
                connection.Close();
            }
        }

        private void VerifyQR_Click(object sender, RoutedEventArgs e)
        {
            verifyInfoOutputTextBlock.Foreground = Brushes.Black;
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT RoomID FROM Code WHERE Code = @code and Access = 1", connection);
                    command.Parameters.AddWithValue("@code", QRCode);
                    if (command.ExecuteScalar() == null)
                    {
                        verifyInfoOutputTextBlock.Foreground = Brushes.Red;
                        verifyInfoOutputTextBlock.Text = "Прохід не підтверджено";
                    }
                    else
                    {
                        MySqlCommand secondCommand = new MySqlCommand("SELECT RoomName FROM Rooms WHERE RoomID = @roomID", connection);
                        secondCommand.Parameters.AddWithValue("@roomID", command.ExecuteScalar());
                        verifyInfoOutputTextBlock.Foreground = Brushes.Green;
                        verifyInfoOutputTextBlock.Text = $"{secondCommand.ExecuteScalar()} відчинено";
                        secondCommand = new MySqlCommand("DELETE FROM Code WHERE Code = @code", connection);
                        secondCommand.Parameters.AddWithValue("@code", QRCode);
                        secondCommand.ExecuteNonQuery();
                        isGenerated = false;
                    }
                    connection.Close();
                }
                catch (Exception) { changeInfoOutputTextBlock.Text = "Виникла помилка"; }
            }
        }
        private void GoToChangeIDView_Click(object sender, RoutedEventArgs e)
        {
            PassView.Visibility = Visibility.Hidden;
            ChangeIDView.Visibility = Visibility.Visible;
        }
        private void ChangeID_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM Admins WHERE Login = @login and Password = @password", connection);
                    command.Parameters.AddWithValue("@login", Convert.ToString(adminLoginInputTextBox.Text));
                    command.Parameters.AddWithValue("@password", adminPasswordInputTextBox.Password);
                    if (command.ExecuteScalar() == null)
                    {
                        changeInfoOutputTextBlock.Text = "Неправильний логін або пароль";
                    }
                    else
                    {
                        command = new MySqlCommand("SELECT * FROM Rooms WHERE RoomID=@roomID", connection);
                        command.Parameters.AddWithValue("@roomID", Convert.ToString(roomNewIDInputTextBox.Text));
                        if (command.ExecuteScalar() == null)
                        {
                            changeInfoOutputTextBlock.Text = "Таких дверей не існує";
                        }
                        else 
                        {
                            using (var stream = new FileStream("room_id.bin", FileMode.Create))
                            using (var writer = new BinaryWriter(stream)) { writer.Write(roomNewIDInputTextBox.Text); }
                            changeInfoOutputTextBlock.Text = "Прив'язку дверей виконано";
                        }
                    }
                }
                catch (Exception) { changeInfoOutputTextBlock.Text = "Виникла помилка"; }
            }
        }
        private void GoToPassView_Click(object sender, RoutedEventArgs e)
        {
            ChangeIDView.Visibility = Visibility.Hidden;
            PassView.Visibility = Visibility.Visible;
        }
    }
}