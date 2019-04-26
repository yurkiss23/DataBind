﻿using DataBind.Entities;
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
        private ObservableCollection<UserModel> users;// = new ObservableCollection<User>();
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

                //using (TransactionScope scope = new TransactionScope())
                //{
                //    List<Users> usersList = new List<Users>();
                //    _connect.Open();
                //    SqlCommand cmd = new SqlCommand("SELECT [Id],[Name]FROM[yurkissdb].[dbo].[testUsers]", _connect);
                //    SqlDataReader rdr = cmd.ExecuteReader();
                //    while (rdr.Read())
                //    {
                //        usersList.Add(new Users
                //        {
                //            Id = (int)rdr["Id"],
                //            Name = rdr["Name"].ToString()
                //        });
                //    }
                //    users = new ObservableCollection<Users>(usersList);
                //    _connect.Close();
                //    scope.Complete();
                //}
            }
            catch
            {
                MessageBox.Show("uploading errors");
            }
            DG.ItemsSource = users;
        }
        //public static explicit operator User(UserModel model)
        //{
        //    //User res = new User();
        //    //res.Id = model.Id;
        //    //res.Name = model.Name;
        //    return new User
        //    {
        //        Id = model.Id,
        //        Name = model.Name
        //    };
        //}

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

                //using (TransactionScope scope = new TransactionScope())
                //{
                //    _connect.Open();
                //    for (int i = 0; i < users.Count; i++)
                //    {
                //        if (users[i].Id != 0)
                //        {
                //            SqlCommand cmd = new SqlCommand($"UPDATE [dbo].[testUsers]SET[Name] = '{users[i].Name}'WHERE [Id] = {users[i].Id}", _connect);
                //            cmd.ExecuteNonQuery();
                //        }
                //        else
                //        {
                //            SqlCommand cmd = new SqlCommand($"INSERT INTO [dbo].[testUsers]([Name])VALUES('{users[i].Name}')", _connect);
                //            cmd.ExecuteNonQuery();
                //        }
                //    }
                //    _connect.Close();
                //    scope.Complete();
                //}
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
            //if (DG.SelectedItem != null)
            //{
            //    int cngId = (DG.SelectedItem as Users).Id;
            //    AddUser cngUser = new AddUser();
            //    cngUser.txtAddName.Text = (DG.SelectedItem as Users).Name;
            //    cngUser.ShowDialog();
            //    try
            //    {
            //        using (TransactionScope sc = new TransactionScope())
            //        {
            //            _connect.Open();
            //            SqlCommand cmd = new SqlCommand($"UPDATE [dbo].[testUsers]SET[Name] = '{cngUser.AddName}' WHERE [Id] = {cngId}", _connect);
            //            cmd.ExecuteNonQuery();
            //            _connect.Close();
            //            sc.Complete();
            //        }
            //    }
            //    catch
            //    {
            //        MessageBox.Show("error");
            //    }
            //}
            MessageBox.Show("update user");
            DG_Load();
        }
        
        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            //int delId = (DG.SelectedItem as UserModel).Id;
            //int deleted = 0;
            try
            {
                if (DG.SelectedItem != null)
                {
                    //MessageBox.Show("if");

                    User m = DG.SelectedItem as UserModel;
                    ////User u = (User)m;
                    MessageBox.Show(m.Id.ToString()+" "+ m.Name);


                    

                    //_context.UsersDB.Remove
                    _context.SaveChanges();
                    MessageBox.Show("delete user(s)");
                }

                //using (TransactionScope scope = new TransactionScope())
                //{
                //    _connect.Open();
                //    SqlCommand cmd = new SqlCommand($"DELETE FROM [dbo].[testUsers]WHERE[Id] = {delId}", _connect);
                //    deleted = cmd.ExecuteNonQuery();
                //    _connect.Close();
                //    scope.Complete();
                //}
            }
            catch
            {
                MessageBox.Show("deleting error");
            }
            //if (deleted > 0)
            //{
            //    MessageBox.Show("delete user(s)");
            //}
            //else
            //{
            //    MessageBox.Show("delete nothing");
            //}
            
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
