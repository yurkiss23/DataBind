using DataBind.Entities;
using DataBind.Models;
using DataBind.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataBind
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EFContext _context;
        private ObservableCollection<UserModel> users;
        public MainWindow()
        {
            InitializeComponent();
            _context = new EFContext();

            DG_Load();
        }

        public void DG_Load()
        {
            try
            {
                users = new ObservableCollection<UserModel>(
                    _context.UsersDB.Select(u => new UserModel()
                    {
                        Id = u.Id,
                        Name = u.Name
                    }).ToList());
            }
            catch
            {
                MessageBox.Show("uploading errors");
            }
            DG.ItemsSource = users;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddUser addUser = new AddUser();
                addUser.ShowDialog();
                users.Add(new UserModel() { Name = addUser.AddName });
                _context.UsersDB.Add(new User()
                {
                    Name = users.Where(u => u.Id == 0).First().Name
                });
                _context.SaveChanges();
            }
            catch
            {
                MessageBox.Show("adding errors");
            }
            MessageBox.Show($"add user(s)");
            DG_Load();
        }
        
        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            User cng = null;
            if (DG.SelectedItem != null)
            {
                User select = DG.SelectedItem as UserModel;
                AddUser cngUser = new AddUser();
                cngUser.txtAddName.Text = select.Name;
                cngUser.ShowDialog();
                try
                {
                    cng = _context.UsersDB.Where(u => u.Id == select.Id).First();
                    cng.Name = cngUser.AddName;
                    _context.SaveChanges();
                    MessageBox.Show("change user");
                }
                catch
                {
                    MessageBox.Show("changing error");
                }
            }
            DG_Load();
        }
        
        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DG.SelectedItem != null)
                {
                    User select = DG.SelectedItem as UserModel;
                    _context.UsersDB.Remove(_context.UsersDB.Where(u => u.Id == select.Id).First());
                    _context.SaveChanges();
                    MessageBox.Show("delete user(s)");
                }
            }
            catch
            {
                MessageBox.Show("deleting error");
            }
            DG_Load();
            btnChangeUser.IsEnabled = false;
            btnDeleteUser.IsEnabled = false;
        }

        private void DG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnChangeUser.IsEnabled = true;
            btnDeleteUser.IsEnabled = true;
        }
    }
}
